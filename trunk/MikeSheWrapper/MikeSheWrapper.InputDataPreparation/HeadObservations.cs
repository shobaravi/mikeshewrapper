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
    public static Func<TimeSeriesEntry, DateTime, DateTime, bool> InBetween = (TSE, Start, End) => TSE.Time >= Start & TSE.Time < End;


    /// <summary>
    /// Function that returns true if a well has more than Count observations in the period between Start and End
    /// </summary>
    public Func<ObservationWell, DateTime, DateTime, int, bool> NosInBetween = (OW, Start, End, Count) => Count <= OW.UniqueObservations.Count(num => InBetween(num, Start, End));


    public HeadObservations()
    { }


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
    public void SelectByMikeSheModelArea(MikeSheGridInfo Grid)
    {
      //     Parallel.ForEach<ObservationWell>(_wells.Values, delegate(ObservationWell W)
      foreach (ObservationWell W in _wells.Values)
      {
        //Gets the index and sets the column and row
        if (!Grid.GetIndex(W.X, W.Y, out W._column, out W._row))
          lock (_lock)
          {
            WorkingList.Remove(W);
          }
      }
      //      );
    }

    /// <summary>
    /// Writes a textfile that can be used for importing detailed timeseries output
    /// </summary>
    /// <param name="TxtFileName"></param>
    public void WriteToMikeSheModel(string TxtFileName)
    {
      using (StreamWriter SW = new StreamWriter(TxtFileName, false, Encoding.Default))
      {
        foreach (ObservationWell W in WorkingList)
        {
          SW.WriteLine(W.ID + "\t101\t1\t" + W.X + "\t" + W.Y + "\t" + W.Depth + "\t0\t \t ");
        }
      }
    }


    public void WriteToMikeSheModel(Model Mshe)
    {
      Mshe.Input.MIKESHE_FLOWMODEL.StoringOfResults.DetailedTimeseriesOutput.Item_1s.Clear();

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
      foreach (DfsFileItemInfo DI in _data.DynamicItemInfos)
      {
        if (_wells.TryGetValue(DI.Name, out OW))
        {
          WorkingList.Add(OW);
          //Loop the observations
          foreach (TimeSeriesEntry TSE in OW.Observations)
            TSE.SimulatedValue = _data.GetData(TSE.Time, item);
        }
        item++;
      }
    }


    /// <summary>
    /// 4-point bilinear interpolation is used to get the value in a point.
    /// Uses the working list.
    /// </summary>
    /// <param name="MSheResults"></param>
    /// <param name="GridInfo"></param>
    public void GetSimulatedValuesFromGridOutput(Results MSheResults, MikeSheGridInfo GridInfo)
    {
      foreach(ObservationWell W in WorkingList)
      {
        foreach (TimeSeriesEntry TSE in W.Observations)
        {
          Matrix M = MSheResults.PhreaticHead.TimeData(TSE.Time)[W.Layer];
          TSE.SimulatedValueCell = M[W.Row, W.Column];
          //Interpolates in the matrix
          TSE.SimulatedValue  = GridInfo.Interpolate(W.X, W.Y, W.Layer, M, out TSE.DryCells, out TSE.BoundaryCells);
        }
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
          CurrentWell.ReadDfs0(dt.TIME_SERIES_FILE.FILE_NAME, dt.TIME_SERIES_FILE.ITEM_NUMBERS);
        }
      }
    }


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

      FillInFromNovanaShape(DT.Select(SelectString));
    }

#endregion


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
          List<TimeSeriesEntry> SelectedObs = OW.Observations.Where(TSE => InBetween(TSE, Start, End)).ToList<TimeSeriesEntry>();

          StringBuilder S = new StringBuilder();
          S.Append(OW.ID + "\t" + OW.X + "\t" + OW.Y + "\t" + OW.Depth + "\t");

          if (AllObs)
            foreach (TimeSeriesEntry TSE in SelectedObs)
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
      //Prepare the time series if there is more than one observation
      Parallel.ForEach<ObservationWell>(SelectedWells, delegate(ObservationWell W)
      {
          W.InitializeToWriteDFS0(Start, End);
      });

      //Write the dfs0s
      Parallel.ForEach<ObservationWell>(SelectedWells, delegate(ObservationWell W)
      {
          W.WriteToDfs0(OutputPath);
      });
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
        List<TimeSeriesEntry> SelectedObs = W.Observations.Where(TSE => InBetween(TSE, Start, End)).ToList<TimeSeriesEntry>();

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
