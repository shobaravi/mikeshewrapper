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
    private Dictionary<string, ObservationWell> _wells;
    List<ObservationWell> _workingList = new List<ObservationWell>();

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


    /// <summary>
    /// Select the wells that are inside the model area. Does not look at the 
    /// z - coordinate
    /// </summary>
    /// <param name="MikeShe"></param>
    public void SelectByMikeSheModelArea(Model MikeShe)
    {
     Parallel.ForEach<ObservationWell>(_wells.Values, delegate(ObservationWell W)
      {
        //Gets the index and sets the column and row
        if (MikeShe.GridInfo.GetIndex(W.X, W.Y, out W._column, out W._row))
          _workingList.Add(W);
      });
    }

    public void StatisticsFromGridOutput(Results MSheResults, MikeSheGridInfo GridInfo)
    {
      Parallel.ForEach<ObservationWell>(_workingList, delegate(ObservationWell W)
      {
        foreach (TimeSeriesEntry TSE in W.Observations)
        {
          int _dryCells;
          int _boundaryCells;
          Matrix M = MSheResults.PhreaticHead.TimeData(TSE.Time)[W.Layer];

          double _simulatedValueCell = M[W.Row, W.Column];
          double _simulatedValueInterpolated = GridInfo.Interpolate(W.X, W.Y, M, out _dryCells, out _boundaryCells);
          if (_simulatedValueInterpolated != MSheResults.DeleteValue)
          {
            double _mE = TSE.Value - _simulatedValueInterpolated;
            double _rMSE = Math.Pow(_mE, 2.0);
          }
        }
      }
      );

    }






#region Population Methods

    /// <summary>
    /// Reads in head observations from txt file with this format.
    /// "WellID X Y Z Head  Date  Layer". Separated with tabs. Layer is optional
    /// </summary>
    /// <param name="LSFileName"></param>
    public void ReadFromLSText(string LSFileName)
    {
      using (StreamReader SR = new StreamReader(LSFileName))
      {
        //Reads the HeadLine
        string line = SR.ReadLine();
        string[] s;
        ObservationWell OW;

        while ((line = SR.ReadLine()) != null)
        {
          s = line.Split('\t');
          if (s.Length > 5)
          {
            try
            {
              //If the well has not already been read in create a new one
              if (!_wells.TryGetValue(s[0], out OW))
              {
                OW = new ObservationWell(s[0]);
                _wells.Add(OW.ID, OW);
                OW.X = double.Parse(s[1]);
                OW.Y = double.Parse(s[2]);

                //Layer is provided directly. Calculate Z
                if (s.Length > 7 && s[6] != "")
                {
                  OW.Layer = int.Parse(s[6]);
                }
                //Use the Z-coordinate
                else
                {
                  OW.Z = double.Parse(s[3]);
                }
              }
              //Now add the observation
              OW.Observations.Add(new TimeSeriesEntry(DateTime.Parse(s[5]), double.Parse(s[4])));
            }
            catch (FormatException e)
            {
              throw new Exception("Error reading input-file: " + LSFileName, e);
            }
          }
        }
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
      DT.Columns.Add("tiemofmeas", typeof(DateTime));
      DT.Columns.Add("WATERLEVEL", typeof(double));

      SR.Columns["tiemofmeas"]._dotNetType = typeof(DateTime);
      SR.Columns["tiemofmeas"]._dbfType = ShapeLib.DBFFieldType.FTDate;

      _wells = new Dictionary<string, ObservationWell>();

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
