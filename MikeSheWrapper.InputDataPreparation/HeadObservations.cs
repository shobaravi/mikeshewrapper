using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;

using DHI.TimeSeries;

namespace MikeSheWrapper.InputDataPreparation
{
  public class HeadObservations
  {
    private Dictionary<string, ObservationWell> _wells;
    List<ObservationWell> _insideDomain = new List<ObservationWell>();

    public void WriteStatistics()
    {


    }

    public void SelectByMikeSheModelArea(Model MikeShe)
    {
      foreach (ObservationWell W in _wells)
      {
        int Column;
        int Row;
        if (MikeShe.Processed.GetIndex(W.X, W.Y, out Column, out Row))
          _insideDomain.Add(W);
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

    /// <summary>
    /// Writes dfs0 files for all wells with more than one observation
    /// </summary>
    /// <param name="OutputPath"></param>
    public void WriteToDfs0(string OutputPath)
    {
      //Prepare the time series if there is more than one observation
      Parallel.ForEach<ObservationWell>(_wells.Values, delegate(ObservationWell W)
      {
        if (W.Observations.Count > 1)
          W.InitializeToWriteDFS0();
      });

      //Write the dfs0s
      Parallel.ForEach<ObservationWell>(_wells.Values, delegate(ObservationWell W)
      {
        if (W.DHITimeSeriesDataCount > 1)
          W.WriteToDfs0(OutputPath);
      });
    }


  }
}
