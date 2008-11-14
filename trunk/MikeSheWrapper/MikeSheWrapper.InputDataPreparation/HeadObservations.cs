using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;

using MathNet.Numerics.LinearAlgebra;

using MikeSheWrapper.Tools;
using DHI.TimeSeries;

namespace MikeSheWrapper.InputDataPreparation
{
  public class HeadObservations
  {
    private Dictionary<string, ObservationWell> _wells = new Dictionary<string,ObservationWell>();
    private List<ObservationWell> _workingList = new List<ObservationWell>();

    public Dictionary<string, ObservationWell> Wells
    {
      get { return _wells; }
    }

    public List<ObservationWell> WorkingList
    {
      get { return _workingList; }
    }

    public void WriteStatistics()
    {


    }

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
    private static object _lock = new object();


    /// <summary>
    /// Select the wells that are inside the model area. Does not look at the 
    /// z - coordinate
    /// </summary>
    /// <param name="MikeShe"></param>
    public void SelectByMikeSheModelArea(MikeSheGridInfo Grid)
    {

     Parallel.ForEach<ObservationWell>(_wells.Values, delegate(ObservationWell W)
      {
        //Gets the index and sets the column and row
        if (Grid.GetIndex(W.X, W.Y, out W._column, out W._row))
          lock (_lock)
          {
            _workingList.Add(W);
          }
      });
    }

    public void StatisticsFromGridOutput(Results MSheResults, MikeSheGridInfo GridInfo)
    {

      Parallel.ForEach<ObservationWell>(_workingList, delegate(ObservationWell W)
      {
        foreach (TimeSeriesEntry TSE in W.Observations)
        {
          Matrix M = MSheResults.PhreaticHead.TimeData(TSE.Time)[W.Layer];

          TSE.SimulatedValueCell = M[W.Row, W.Column];
          //Interpolates in the matrix
          TSE.SimulatedValueInterpolated  = GridInfo.Interpolate(W.X, W.Y, M, out TSE.DryCells, out TSE.BoundaryCells);
          if (TSE.SimulatedValueInterpolated != MSheResults.DeleteValue)
          {
            TSE.ME = TSE.Value - TSE.SimulatedValueInterpolated;
            TSE.RMSE = Math.Pow(TSE.ME, 2.0);
          }
        }
      }
      );
    }


#region Population Methods

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


  }
}
