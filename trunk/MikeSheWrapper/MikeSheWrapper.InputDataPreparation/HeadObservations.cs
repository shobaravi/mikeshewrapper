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

using DHI.TimeSeries;
using DHI.Generic.MikeZero.DFS;

namespace MikeSheWrapper.InputDataPreparation
{
  public class HeadObservations
  {
    private Dictionary<string, ObservationWell> _wells = new Dictionary<string,ObservationWell>();
    
    private List<ObservationWell> _workingList;

    //Object used for thread safety. What happens if two instances are running in the same process?
    private static object _lock = new object();

    /// <summary>
    /// Function that returns true if a time series entry is between the two dates
    /// </summary>
    public static Func<ObservationEntry, DateTime, DateTime, bool> InBetween = (TSE, Start, End) => TSE.Time >= Start & TSE.Time < End;


    /// <summary>
    /// Function that returns true if a well has more than Count observations in the period between Start and End
    /// </summary>
    public Func<ObservationWell, DateTime, DateTime, int, bool> NosInBetween = (OW, Start, End, Count) => Count <= OW.Observations.Distinct().Count(num => InBetween(num, Start, End));


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
    public static IEnumerable<ObservationWell> SelectByMikeSheModelArea(MikeSheGridInfo Grid, Dictionary<string, ObservationWell> Wells )
    {
      //     Parallel.ForEach<ObservationWell>(_wells.Values, delegate(ObservationWell W)
      foreach (ObservationWell W in Wells.Values)
      {
        //Gets the index and sets the column and row
        if (Grid.GetIndex(W.X, W.Y, out W._column, out W._row))
          yield return W;
      }
      //      );
    }

    /// <summary>
    /// Writes a textfile that can be used for importing detailed timeseries output
    /// </summary>
    /// <param name="TxtFileName"></param>
    public void WriteToMikeSheModel(string TxtFileName, IEnumerable<ObservationWell> SelectedWells)
    {
      using (StreamWriter SW = new StreamWriter(TxtFileName, false, Encoding.Default))
      {
        foreach (ObservationWell W in SelectedWells)
        {
//          if (W.Dfs0Written)
            SW.WriteLine(W.ID + "\t101\t1\t" + W.X + "\t" + W.Y + "\t" + W.Depth + "\t1\t"+W.ID +"\t1 ");
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
    public void GetSimulatedValuesFromDetailedTSOutput(string DFS0FileName)
    {
      DFS0 _data = new DFS0(DFS0FileName);

      ObservationWell OW;
      int item=1;

      //Loop all Items
      foreach (string DI in _data.ItemNames)
      {
        if (_wells.TryGetValue(DI, out OW))
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
    public static void GetSimulatedValuesFromGridOutput(Results MSheResults, MikeSheGridInfo GridInfo, ObservationWell Well)
    {
        foreach (ObservationEntry TSE in Well.Observations)
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
    public void ReadInDetailedTimeSeries(Model Mshe)
    {
      ObservationWell CurrentWell;
      TSObject _tso = null;

      foreach (MikeSheWrapper.InputFiles.Item_11 dt in Mshe.Input.MIKESHE_FLOWMODEL.StoringOfResults.DetailedTimeseriesOutput.Item_1s)
      {
        if (!_wells.TryGetValue(dt.Name, out CurrentWell))
        {
          CurrentWell = new ObservationWell(dt.Name);
          CurrentWell.X = dt.X;
          CurrentWell.Y = dt.Y;
          CurrentWell.Depth = dt.Y;
          _wells.Add(dt.Name, CurrentWell);
        }
        //Read in observations if they are included
        if (dt.InclObserved == 1)
        {
          if (_tso == null || _tso.Connection.FilePath != dt.TIME_SERIES_FILE.FILE_NAME)
          {
            _tso = new TSObjectClass();
            _tso.Connection.FilePath = dt.TIME_SERIES_FILE.FILE_NAME;
            _tso.Connection.Open();
          }

          for (int i = 1; i <= _tso.Time.NrTimeSteps; i++)
          {
            CurrentWell.Observations.Add(new ObservationEntry((DateTime)_tso.Time.GetTimeForTimeStepNr(i), (float)_tso.Item(dt.TIME_SERIES_FILE.ITEM_NUMBERS).GetDataForTimeStepNr(i)));
          }
        }
      }
    }


    /// <summary>
    /// Creates wells from DataRows based on ShapeReaderConfiguration
    /// </summary>
    /// <param name="DS"></param>
    /// <param name="SRC"></param>
    public void FillInFromNovanaShape(DataRow[] DS, ShapeReaderConfiguration SRC)
    {
      ObservationWell CurrentWell;
      foreach (DataRow DR in DS)
      {
        //Find the well in the dictionary
        if (!_wells.TryGetValue((string)DR[SRC.WellIDHeader], out CurrentWell))
        {
          //Add a new well if it was not found
            CurrentWell = new ObservationWell((string)DR[SRC.WellIDHeader], (double)DR[SRC.XHeader], (double)DR[SRC.YHeader]);
            CurrentWell.Depth = (double)DR[SRC.ZHeader];

          _wells.Add(CurrentWell.ID, CurrentWell);
        }
      }
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
    public void WriteToLSInput(string FileName, IEnumerable<ObservationWell> SelectedWells, DateTime Start, DateTime End, bool AllObs)
    {
      using (StreamWriter SW = new StreamWriter(FileName, false, Encoding.Default))
      {
        if (AllObs)
          SW.WriteLine("NOVANAID\tXUTM\tYUTM\tMIDTK_FNL\tPEJL\tDATO\tBERELAG");
        else
          SW.WriteLine("NOVANAID\tXUTM\tYUTM\tMIDTK_FNL\tMEANPEJ\tMAXDATO\tBERELAG");

        foreach (ObservationWell OW in SelectedWells)
        {
          List<ObservationEntry> SelectedObs = OW.Observations.Where(TSE => InBetween(TSE, Start, End)).ToList<ObservationEntry>();
        
          SelectedObs.Sort();

          StringBuilder S = new StringBuilder();
          S.Append(OW.ID + "\t" + OW.X + "\t" + OW.Y + "\t" + OW.Depth + "\t");

          if (AllObs)
            foreach (ObservationEntry TSE in SelectedObs)
            {
              StringBuilder ObsString = new StringBuilder(S.ToString());
              ObsString.Append(TSE.Value + "\t" + TSE.Time.ToShortDateString());
              if (OW.Layer > 0)
                ObsString.Append("\t" + OW.Layer);
              SW.WriteLine(ObsString.ToString());
            }
          else
          {
            S.Append(SelectedObs.Average(num => num.Value).ToString() + "\t");
            S.Append(SelectedObs.Max(num => num.Time).ToShortDateString());
            if (OW.Layer > 0)
              S.Append("\t" + OW.Layer);
            SW.WriteLine(S.ToString());
          }
        }
      }
    }

    /// <summary>
    /// Writes dfs0-files for all wells in the workinglist included all data
    /// </summary>
    /// <param name="OutputPath"></param>
    public void WriteToDfs0(string OutputPath)
    {
      WriteToDfs0(OutputPath, WorkingList, DateTime.MinValue, DateTime.MaxValue);
    }

    /// <summary>
    /// Writes dfs0 files for the SelectedWells wells
    /// Only includes data within the period bounded by Start and End
    /// </summary>
    /// <param name="OutputPath"></param>
    public void WriteToDfs0(string OutputPath, IEnumerable<ObservationWell> SelectedWells, DateTime Start, DateTime End)
    {
      //Write the time series if there is more than one observation
     // Parallel.ForEach<ObservationWell>(SelectedWells, delegate(ObservationWell W)
      foreach(ObservationWell W in SelectedWells)
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
        List<ObservationEntry> SelectedObs = W.Observations.Where(TSE => InBetween(TSE, Start, End)).ToList<ObservationEntry>();

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
          _tso.Connection.FilePath = Path.Combine(OutputPath, W.ID + ".dfs0");
          _tso.Connection.Save();
        }

      }
//      );

    }

    /// <summary>
    /// Write a specialized output with all observation in one long .dat file
    /// </summary>
    /// <param name="FileName"></param>
    /// <param name="SelectedWells"></param>
    /// <param name="Start"></param>
    /// <param name="End"></param>
    public void WriteToDatFile(string FileName, IEnumerable<ObservationWell> SelectedWells, DateTime Start, DateTime End)
    {
      StringBuilder S = new StringBuilder();

      using (StreamWriter SW = new StreamWriter(FileName, false, Encoding.Default))
      {
        foreach (ObservationWell OW in SelectedWells)
        {
          List<ObservationEntry> SelectedObs = OW.Observations.Where(TSE => InBetween(TSE, Start, End)).ToList<ObservationEntry>();
          SelectedObs.Sort();
          foreach (ObservationEntry TSE in SelectedObs)
          {
            S.Append(OW.ID + "    " + TSE.Time.ToString("dd/MM/yyyy hh:mm:ss") + " " + TSE.Value.ToString() + "\n");
          }
        }
        SW.Write(S.ToString());
      }
    }

    public void WriteShapeFromDataRow(string FileName, IEnumerable<ObservationWell> Wells, DateTime Start, DateTime End)
    {
      PointShapeWriter PSW = new PointShapeWriter(FileName);
      foreach(ObservationWell OW in Wells)
      {
        PSW.WritePointShape(OW.X, OW.Y);
        PSW.Data.WriteData(OW.Data);
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
    public void WriteSimpleShape(string FileName, IEnumerable<ObservationWell> Wells, DateTime Start, DateTime End)
    {
      PointShapeWriter PSW = new PointShapeWriter(FileName);


      OutputTables NT = new OutputTables();
      OutputTables.PejlingerOutputDataTable PDT = new OutputTables.PejlingerOutputDataTable();

      foreach (ObservationWell W in Wells)
      {
        List<ObservationEntry> SelectedObs = W.Observations.Where(TSE => InBetween(TSE, Start, End)).ToList<ObservationEntry>();

        PSW.WritePointShape(W.X, W.Y);

        OutputTables.PejlingerOutputRow PR = PDT.NewPejlingerOutputRow();

        PR.NOVANAID = W.ID;
        PR.LOCATION = W.Description;
        PR.XUTM = W.X;
        PR.YUTM = W.Y;
        PR.JUPKOTE = W.Terrain;

        if (W.ScreenTop.Count > 0)
          PR.INTAKETOP = W.ScreenTop.Min();
        if (W.ScreenBottom.Count > 0)
          PR.INTAKEBOT = W.ScreenBottom.Min();

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


    public Dictionary<string, ObservationWell> Wells
    {
      get { return _wells; }
      set { _wells = value; }
    }

    public List<ObservationWell> WorkingList
    {
      get {
        if (_workingList ==null)
          _workingList = _wells.Values.ToList();
        return _workingList; }
    }



  }
}
