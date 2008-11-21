using System;
      using System.Data.OleDb;
using System.Data;

using System.IO;
using System.Data;
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
    private List<ObservationWell> _workingList = new List<ObservationWell>();

    /// <summary>
    /// Function that returns true if a time series entry is between the two dates
    /// </summary>
    public static Func<TimeSeriesEntry, DateTime, DateTime, bool> InBetween = (TSE, Start, End) => TSE.Time >= Start & TSE.Time < End;


    /// <summary>
    /// Function that returns true if a well has more than Count observations in the period between Start and End
    /// </summary>
    public Func<ObservationWell, DateTime, DateTime, int, bool> NosInBetween = (OW, Start, End, Count) => Count <= OW.UniqueObservations.Count(num => InBetween(num, Start, End));



    private static object _lock = new object();


    /// <summary>
    /// Gets the number of wells that have more observations than Count with in the 
    /// given period of time
    /// </summary>
    /// <param name="Start"></param>
    /// <param name="End"></param>
    /// <param name="Count"></param>
    /// <returns></returns>
    public int GetNumberOfWells(DateTime Start, DateTime End, int Count)
    {
      int Number = 0;
      foreach(ObservationWell W in _workingList)
        if (W.GetNumberOfObservations(Start, End)>= Count)
          Number++;

      return Number;
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
        if (Grid.GetIndex(W.X, W.Y, out W._column, out W._row))
          lock (_lock)
          {
            _workingList.Add(W);
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
        foreach (ObservationWell W in _workingList)
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
    /// Calculate Mean error and RMSE for all observations based on the detailed ts-output.
    /// Finds a well based on the ID
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
          _workingList.Add(OW);
          //Loop the observations
          foreach (TimeSeriesEntry TSE in OW.Observations)
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
    public void GetSimulatedValuesFromGridOutput(Results MSheResults, MikeSheGridInfo GridInfo)
    {
      foreach(ObservationWell W in _workingList)
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
    /// Reads in all wells from a Jupiter database. Only reads X and Y
    /// </summary>
    /// <param name="DataBaseFile"></param>
    public void ReadWellsFromJupiter(string DataBaseFile)
    {
      OleDbConnection dbconnection;
      String databaseConnection = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DataBaseFile + ";Persist Security Info=False";
      dbconnection = new OleDbConnection(databaseConnection);
      dbconnection.Open();
      string select = "SELECT borehole.boreholeno, intakeno, xutm, yutm FROM borehole, intake WHERE borehole.boreholeno= intake.boreholeno and xutm IS NOT NULL AND yutm IS NOT NULL";

      OleDbDataAdapter da = new OleDbDataAdapter(select, databaseConnection);
      DataSet ds = new DataSet();
      da.Fill(ds, "watlevel");

      foreach (DataRow Dr in ds.Tables["watlevel"].Rows)
      {
        string wellname = ((string)Dr["boreholeno"]).Replace(" ", "") + "_" + (int)Dr["intakeno"];

        ObservationWell CurrentWell = new ObservationWell(wellname);
        CurrentWell.X = (double) Dr["xutm"];
        CurrentWell.Y = (double) Dr["yutm"];
        _wells.Add(wellname, CurrentWell);
      }
    }

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
        if (dt.InclObserved == 1)
        {
          CurrentWell.ReadDfs0(dt.TIME_SERIES_FILE.FILE_NAME, dt.TIME_SERIES_FILE.ITEM_NUMBERS);
        }
      }
    }

    /// <summary>
    /// Read in water levels from a Jupiter access database. 
    /// If CreateWells is true a new well is created if it does not exist in the list.
    /// Entries with blank dates of waterlevels are skipped.
    /// </summary>
    /// <param name="DataBaseFile"></param>
    /// <param name="CreateWells"></param>
    public void ReadWaterlevelsFromJupiterAccess(string DataBaseFile, bool CreateWells)
    {

      OleDbConnection dbconnection;
      String databaseConnection = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DataBaseFile + ";Persist Security Info=False";
      dbconnection = new OleDbConnection(databaseConnection);
      dbconnection.Open();
      OleDbDataAdapter da = new OleDbDataAdapter("SELECT boreholeno, intakeno, timeofmeas, waterlevel FROM watlevel WHERE timeofmeas IS NOT NULL AND waterlevel IS NOT NULL", databaseConnection);
      DataSet ds = new DataSet();
      da.Fill(ds, "watlevel");

      int nrow = ds.Tables[0].Rows.Count;
      foreach (DataRow Dr in ds.Tables["watlevel"].Rows)
      {
        ObservationWell CurrentWell;

        string well = (((string)Dr["boreholeno"]).Replace(" ", "") + "_" + (int)Dr["intakeno"]).Trim();
        //Find the well in the dictionary
        if (!_wells.TryGetValue(well, out CurrentWell))
        {
          if (CreateWells)
          {
            CurrentWell = new ObservationWell(well);
            _wells.Add(well, CurrentWell);
          }
        }
        if (CurrentWell!=null)
        CurrentWell.Observations.Add(new TimeSeriesEntry((DateTime)Dr["timeofmeas"], (double)Dr["waterlevel"]));

      }
    }



    


    /// <summary>
    /// Reads in observations from a shape file
    /// </summary>
    /// <param name="ShapeFileName"></param>
    public void ReadFromShape(string ShapeFileName)
    {
      ShapeReader SR = new ShapeReader(ShapeFileName);

      DataTable DT = new DataTable();
      DT.Columns.Add("NOVANAID", typeof(string));
      DT.Columns.Add("XUTM", typeof (double));
      DT.Columns.Add("YUTM", typeof (double));
      DT.Columns.Add("INTAKETOP", typeof(double));
      DT.Columns.Add("INTAKEBOT", typeof(double));
      DT.Columns.Add("DTMKOTE", typeof(double));
      DT.Columns.Add("MIDTJUST", typeof(double));
      DT.Columns.Add("DRILLDEPTH", typeof(double));

      DT.Columns.Add("tiemofmeas", typeof(DateTime));
      DT.Columns.Add("WATERLEVEL", typeof(double));

      SR.Columns["tiemofmeas"]._dotNetType = typeof(DateTime);
      SR.Columns["tiemofmeas"]._dbfType = ShapeLib.DBFFieldType.FTDate;

      ObservationWell CurrentWell = new ObservationWell("");

      //Loop the data
      while(!SR.EndOfData)
      {
        DataRow DR = DT.NewRow(); 

        SR.ReadNext(DR);

        //Find the well in the dictionary
        if (!_wells.TryGetValue((string) DR["NOVANAID"], out CurrentWell))
        {
          //Add a new well if it was not found
          CurrentWell = new ObservationWell((string)DR["NOVANAID"], (double)DR["XUTM"], (double)DR["YUTM"]);
          if ((int)(double)DR["MIDTJUST"] == -999)
            if ((int)(double)DR["INTAKETOP"] == -999)
              CurrentWell.Depth = (double)DR["DRILLDEPTH"];
            else
              CurrentWell.Depth = 0.5 * ((double)DR["INTAKETOP"] + (double)DR["INTAKEBOT"]);
          else
              CurrentWell.Depth = (double)DR["DTMKOTE"] - (double)DR["MIDTJUST"];

          _wells.Add((string) DR["NOVANAID"],CurrentWell);
        }
        CurrentWell.Observations.Add(new TimeSeriesEntry ((DateTime) DR["tiemofmeas"], (double)DR["WATERLEVEL"]));
      }
    }

#endregion

    /// <summary>
    /// Writes dfs0 files for all wells with more than one observation
    /// </summary>
    /// <param name="OutputPath"></param>
    public void WriteToDfs0(string OutputPath)
    {
      //Prepare the time series if there is more than one observation
      Parallel.ForEach<ObservationWell>(_workingList, delegate(ObservationWell W)
      {
        if (W.Observations.Count > 1)
          W.InitializeToWriteDFS0();
      });

      //Write the dfs0s
      Parallel.ForEach<ObservationWell>(_workingList, delegate(ObservationWell W)
      {
        if (W.DHITimeSeriesDataCount > 1)
          W.WriteToDfs0(OutputPath);
      });
    }

    public Dictionary<string, ObservationWell> Wells
    {
      get { return _wells; }
    }

    public List<ObservationWell> WorkingList
    {
      get { return _workingList; }
    }



  }
}
