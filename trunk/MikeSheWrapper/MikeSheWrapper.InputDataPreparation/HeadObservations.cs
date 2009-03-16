using System;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;

using MathNet.Numerics.LinearAlgebra;

using MikeSheWrapper.Tools;
using MikeSheWrapper.DFS;
using MikeSheWrapper.JupiterTools;

using DHI.TimeSeries;
using DHI.Generic.MikeZero.DFS;

namespace MikeSheWrapper.InputDataPreparation
{
  public class HeadObservations
  {
    
    //Object used for thread safety. What happens if two instances are running in the same process?
    private static object _lock = new object();

    /// <summary>
    /// Function that returns true if a time series entry is between the two dates
    /// </summary>
    public static Func<ObservationEntry, DateTime, DateTime, bool> InBetween = (TSE, Start, End) => TSE.Time >= Start & TSE.Time < End;


    /// <summary>
    /// Function that returns true if an Intake has more than Count observations in the period between Start and End
    /// </summary>
    public static Func<IIntake, DateTime, DateTime, int, bool> NosInBetween = (OW, Start, End, Count) => Count <= OW.Observations.Distinct().Count(num => InBetween(num, Start, End));


    public HeadObservations()
    { }


    /// <summary>
    /// Obsolete!
    /// </summary>
    /// <param name="FileName"></param>
    public HeadObservations(string FileName)
    {
      switch (Path.GetExtension(FileName))
      {
        case ".she":
          ReadInDetailedTimeSeries(new Model(FileName));
          break;
        case ".mdb":
//          ReadWellsFromJupiter(FileName);
          break;
        case ".shp":
          ReadFromShape(FileName,"");
          break;
        default:
          break;
      }
    }
 
    /// <summary>
    /// Select the wells that are inside the model area. Does not look at the 
    /// z - coordinate
    /// </summary>
    /// <param name="MikeShe"></param>
    public static IEnumerable<MikeSheWell> SelectByMikeSheModelArea(MikeSheGridInfo Grid, IEnumerable<MikeSheWell> Wells)
    {
      int Column;
      int Row;
      foreach (MikeSheWell W in Wells)
      {

        //Gets the index and sets the column and row
        if (Grid.GetIndex(W.X, W.Y, out Column, out Row))
        {
          W.Column = Column;
          W.Row = Row;
          yield return W;
        }
      }
    }

    /// <summary>
    /// Writes a textfile that can be used for importing detailed timeseries output
    /// Depth is calculated as the midpoint of the lowest screen
    /// </summary>
    /// <param name="TxtFileName"></param>
    public static void WriteToMikeSheModel(string TxtFileName, IEnumerable<IIntake> SelectedIntakes)
    {
      using (StreamWriter SW = new StreamWriter(TxtFileName, false, Encoding.Default))
      {
        foreach (Intake I in SelectedIntakes)
        {
          double depth = (I.ScreenTop.Max() + I.ScreenBottom.Max()) / 2;
          //          if (W.Dfs0Written)
          SW.WriteLine(I.ToString() + "\t101\t1\t" + I.well.X + "\t" + I.well.Y + "\t" + depth + "\t1\t" + I.ToString() + "\t1 ");
          //When is this necessary
          //        else  
          //        SW.WriteLine(W.ID + "\t101\t1\t" + W.X + "\t" + W.Y + "\t" + W.Depth + "\t0\t \t ");
        }
      }
    }

  
    /// <summary>
    /// Finds a well based on the ID in the detailed SZ output dfs0. When a well is found it is added to the workinglist
    /// The working list should be cleared before entering this method
    /// </summary>
    /// <param name="DFS0FileName"></param>
    public static void GetSimulatedValuesFromDetailedTSOutput(string DFS0FileName, List<IIntake> Intakes)
    {
      DFS0 _data = new DFS0(DFS0FileName);

      IIntake OW;
      int item=1;

      //Loop all Items
      foreach (string DI in _data.ItemNames)
      {
        OW = Intakes.Find(var => var.ToString() == DI);
        if (OW!=null)
        {
          //Loop the observations
          foreach (ObservationEntry TSE in OW.Observations)
            TSE.SimulatedValue = _data.GetData(TSE.Time, item);
        }
        item++;
      }
    }


    /// <summary>
    /// 4-point bilinear interpolation is used to get the value in a point.
    /// </summary>
    /// <param name="MSheResults"></param>
    /// <param name="GridInfo"></param>
    public static void GetSimulatedValuesFromGridOutput(Results MSheResults, MikeSheGridInfo GridInfo, MikeSheWell Well)
    {
      foreach(Intake I in Well.Intakes)
        foreach (ObservationEntry TSE in I.Observations)
        {
          Matrix M = MSheResults.PhreaticHead.TimeData(TSE.Time)[Well.Layer];
          TSE.SimulatedValueCell = M[Well.Row, Well.Column];
          //Interpolates in the matrix
          TSE.SimulatedValue = GridInfo.Interpolate(Well.X, Well.Y, Well.Layer, M, out TSE.DryCells, out TSE.BoundaryCells);
        }
    }


#region Population Methods

    /// <summary>
    /// Reads in the wells defined in detailed timeseries input section
    /// </summary>
    /// <param name="Mshe"></param>
    public static IEnumerable<IWell> ReadInDetailedTimeSeries(Model Mshe)
    {
      MikeSheWell CurrentWell;
      IIntake CurrentIntake;
      TSObject _tso = null;

      foreach (MikeSheWrapper.InputFiles.Item_11 dt in Mshe.Input.MIKESHE_FLOWMODEL.StoringOfResults.DetailedTimeseriesOutput.Item_1s)
      {
        CurrentWell = new MikeSheWell(dt.Name);
        CurrentWell.X = dt.X;
        CurrentWell.Y = dt.Y;
        CurrentWell.Z = dt.Z;

        //Read in observations if they are included
        if (dt.InclObserved == 1)
        {
          CurrentIntake = new Intake(CurrentWell, 1);

          if (_tso == null || _tso.Connection.FilePath != dt.TIME_SERIES_FILE.FILE_NAME)
          {
            _tso = new TSObjectClass();
            _tso.Connection.FilePath = dt.TIME_SERIES_FILE.FILE_NAME;
            _tso.Connection.Open();
          }

          //Loop the observations and add
          for (int i = 1; i <= _tso.Time.NrTimeSteps; i++)
          {
            CurrentIntake.Observations.Add(new ObservationEntry((DateTime)_tso.Time.GetTimeForTimeStepNr(i), (float)_tso.Item(dt.TIME_SERIES_FILE.ITEM_NUMBERS).GetDataForTimeStepNr(i)));
          }
        }
        yield return CurrentWell;
      }
    }


    /// <summary>
    /// Creates wells from DataRows based on ShapeReaderConfiguration
    /// </summary>
    /// <param name="DS"></param>
    /// <param name="SRC"></param>
    public static Dictionary<string, IWell> FillInFromNovanaShape(DataRow[] DS, ShapeReaderConfiguration SRC)
    {
      Dictionary<string, IWell> Wells = new Dictionary<string, IWell>();
      IWell CurrentWell;
      foreach (DataRow DR in DS)
      {
        //Find the well in the dictionary
        if (!Wells.TryGetValue((string)DR[SRC.WellIDHeader], out CurrentWell))
        {
          //Add a new well if it was not found
            CurrentWell = new MikeSheWell((string)DR[SRC.WellIDHeader]);
          CurrentWell.X =(double)DR[SRC.XHeader];
          CurrentWell.Y =(double)DR[SRC.YHeader];
           ((MikeSheWell) CurrentWell).Z = (double)DR[SRC.ZHeader];

            Wells.Add(CurrentWell.ID, CurrentWell);
        }
      }
      return Wells;
    }

    /// <summary>
    /// Reads in observations from a shape file
    /// </summary>
    /// <param name="ShapeFileName"></param>
    public void ReadFromShape(string ShapeFileName, string SelectString)
    {
      PointShapeReader SR = new PointShapeReader(ShapeFileName);

      DataTable DT = SR.Data.Read();

     // FillInFromNovanaShape(DT.Select(SelectString));
    }

#endregion

    /// <summary>
    /// Write a text-file that can be used by LayerStatistics.
    /// </summary>
    /// <param name="FileName"></param>
    /// <param name="SelectedWells"></param>
    /// <param name="Start"></param>
    /// <param name="End"></param>
    /// <param name="AllObs"></param>
    public static void WriteToLSInput(string FileName, IEnumerable<IIntake> SelectedIntakes, DateTime Start, DateTime End, bool AllObs)
    {
      using (StreamWriter SW = new StreamWriter(FileName, false, Encoding.Default))
      {
        if (AllObs)
          SW.WriteLine("NOVANAID\tXUTM\tYUTM\tMIDTK_FNL\tPEJL\tDATO\tBERELAG");
        else
          SW.WriteLine("NOVANAID\tXUTM\tYUTM\tMIDTK_FNL\tMEANPEJ\tMAXDATO\tBERELAG");

          foreach (Intake I in SelectedIntakes)
          {
            List<ObservationEntry> SelectedObs = I.Observations.Where(TSE => InBetween(TSE, Start, End)).ToList<ObservationEntry>();

            SelectedObs.Sort();

            StringBuilder S = new StringBuilder();
            double depth = (I.ScreenTop.Max() + I.ScreenBottom.Max()) / 2;
            S.Append(I.ToString() + "\t" + I.well.X + "\t" + I.well.Y + "\t" + depth + "\t");

            if (AllObs)
              foreach (ObservationEntry TSE in SelectedObs)
              {
                StringBuilder ObsString = new StringBuilder(S.ToString());
                ObsString.Append(TSE.Value + "\t" + TSE.Time.ToShortDateString());
                SW.WriteLine(ObsString.ToString());
              }
            else
            {
              S.Append(SelectedObs.Average(num => num.Value).ToString() + "\t");
              S.Append(SelectedObs.Max(num => num.Time).ToShortDateString());
              SW.WriteLine(S.ToString());
            }
          }
      }
    }
    


    /// <summary>
    /// Writes dfs0 files for the SelectedWells wells
    /// Only includes data within the period bounded by Start and End
    /// </summary>
    /// <param name="OutputPath"></param>
    public static void WriteToDfs0(string OutputPath, IEnumerable<IIntake> SelectedIntakes, DateTime Start, DateTime End)
    {
      foreach (Intake I in SelectedIntakes)
      {
        //Create the TSObject
        TSObject _tso = new TSObjectClass();
        TSItem _item = new TSItemClass();
        _item.DataType = ItemDataType.Type_Float;
        _item.ValueType = ItemValueType.Instantaneous;
        _item.EumType = 171;
        _item.EumUnit = 1;
        _item.Name = "Head";
        _tso.Add(_item);

        DateTime _previousTimeStep = DateTime.MinValue;

        //Select the observations
        List<ObservationEntry> SelectedObs = I.Observations.Where(TSE => InBetween(TSE, Start, End)).ToList<ObservationEntry>();

        SelectedObs.Sort();

        for (int i = 0; i < SelectedObs.Count; i++)
        {
          //Only add the first measurement of the day
          if (SelectedObs[i].Time != _previousTimeStep)
          {
            _tso.Time.AddTimeSteps(1);
            _tso.Time.SetTimeForTimeStepNr(i + 1, SelectedObs[i].Time);
            _item.SetDataForTimeStepNr(i + 1, (float)SelectedObs[i].Value);
          }
        }

        //Now write the DFS0.
        if (_tso.Time.NrTimeSteps != 0)
        {
          _tso.Connection.FilePath = Path.Combine(OutputPath, I.ToString() + ".dfs0");
          _tso.Connection.Save();
        }
      }
    }
  

    /// <summary>
    /// Write a specialized output with all observation in one long .dat file
    /// </summary>
    /// <param name="FileName"></param>
    /// <param name="SelectedWells"></param>
    /// <param name="Start"></param>
    /// <param name="End"></param>
    public static void WriteToDatFile(string FileName, IEnumerable<IIntake> SelectedIntakes, DateTime Start, DateTime End)
    {
      StringBuilder S = new StringBuilder();

      using (StreamWriter SW = new StreamWriter(FileName, false, Encoding.Default))
      {
        foreach (Intake I in SelectedIntakes)
        {
          List<ObservationEntry> SelectedObs = I.Observations.Where(TSE => InBetween(TSE, Start, End)).ToList<ObservationEntry>();
          SelectedObs.Sort();
          foreach (ObservationEntry TSE in SelectedObs)
          {
            S.Append(I.ToString() + "    " + TSE.Time.ToString("dd/MM/yyyy hh:mm:ss") + " " + TSE.Value.ToString() + "\n");
          }
        }

        SW.Write(S.ToString());
      }
    }

    public static void WriteShapeFromDataRow(string FileName, IEnumerable<JupiterIntake> Intakes, DateTime Start, DateTime End)
    {
      PointShapeWriter PSW = new PointShapeWriter(FileName);
        foreach (JupiterIntake JI in Intakes)
        {
          PSW.WritePointShape(JI.well.X, JI.well.Y);
          PSW.Data.WriteData(JI.Data);
        }
      PSW.Dispose();

    }

    /// <summary>
    /// Writes the wells to a point shape
    /// Calculates statistics on the observations within the period from start to end
    /// </summary>
    /// <param name="FileName"></param>
    /// <param name="Wells"></param>
    /// <param name="Start"></param>
    /// <param name="End"></param>
    public static void WriteSimpleShape(string FileName, IEnumerable<IIntake> Intakes, DateTime Start, DateTime End)
    {
      PointShapeWriter PSW = new PointShapeWriter(FileName);


      OutputTables NT = new OutputTables();
      OutputTables.PejlingerOutputDataTable PDT = new OutputTables.PejlingerOutputDataTable();

        foreach (Intake I in Intakes)
        {
          List<ObservationEntry> SelectedObs = I.Observations.Where(TSE => InBetween(TSE, Start, End)).ToList<ObservationEntry>();

          PSW.WritePointShape(I.well.X, I.well.Y);

          OutputTables.PejlingerOutputRow PR = PDT.NewPejlingerOutputRow();

          PR.NOVANAID = I.ToString();
          PR.LOCATION = I.well.Description;
          PR.XUTM = I.well.X;
          PR.YUTM = I.well.Y;
          PR.JUPKOTE = I.well.Terrain;

          if (I.ScreenTop.Count > 0)
            PR.INTAKETOP = I.ScreenTop.Max();
          if (I.ScreenBottom.Count > 0)
            PR.INTAKEBOT = I.ScreenBottom.Max();

          PR.NUMBEROFOB = SelectedObs.Count;
          if (SelectedObs.Count > 0)
          {
            PR.STARTDATO = SelectedObs.Min(x => x.Time);
            PR.ENDDATO = SelectedObs.Max(x => x.Time);
            PR.MAXOBS = SelectedObs.Max(num => num.Value);
            PR.MINOBS = SelectedObs.Min(num => num.Value);
            PR.MEANOBS = SelectedObs.Average(num => num.Value);
          }
          PDT.Rows.Add(PR);
        }
      

      PSW.Data.WriteDate(PDT);
      PSW.Dispose();
    }
  }
}
