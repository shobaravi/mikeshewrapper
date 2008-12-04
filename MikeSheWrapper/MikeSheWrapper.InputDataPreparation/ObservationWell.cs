using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DHI.TimeSeries;

using MikeSheWrapper.Tools;


namespace MikeSheWrapper.InputDataPreparation
{
  /// <summary>
  /// The observation well is a small class that holds the timeseries measurements for an observation. 
  /// An observation well can only have a single timeseries.  
  /// </summary>
  public class ObservationWell:Well 
  {

    public int _column;
    public int _row;
    private int _layer =-3;
    private double _depth;
    private double _z;

    public double Z
    {
      get { return _z; }
      set { _z = value; }
    }

    private List<TimeSeriesEntry> _observations = new List<TimeSeriesEntry>();


    //TSObject members
    private TSObject _tso;
    private TSItem _item;
    private TimeSpan _minTimeStep = new TimeSpan(0, 0, 10);


    #region Constructors
    public ObservationWell(string ID)
      : base(ID)
    {
    }

    public ObservationWell(string ID, double UTMX, double UTMY):base(ID, UTMX, UTMY)
    {
    }

    #endregion


    /// <summary>
    /// Create the timeseries including all entries
    /// </summary>
    public void InitializeToWriteDFS0()
    {
      InitializeToWriteDFS0(DateTime.MinValue, DateTime.MaxValue);
    }


    /// <summary>
    /// Create the timeseries including entries in the period between start and end
    /// </summary>
    public void InitializeToWriteDFS0(DateTime Start, DateTime End)
    {
      _tso = new TSObjectClass();
      _item = new TSItemClass();
      _item.DataType = ItemDataType.Type_Float;
      _item.ValueType = ItemValueType.Instantaneous;
      _item.EumType = 171;
      _item.EumUnit = 1;
      _item.Name = "Head";
      _tso.Add(_item);

      DateTime _previousTimeStep = DateTime.MinValue;

      List<TimeSeriesEntry> SelectedObs = _observations.Where(TSE => HeadObservations.InBetween(TSE, Start, End)).ToList<TimeSeriesEntry>();

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


        ////TimeSteps have to be increasing
        //if (_previousTimeStep >= _observations[i].Time)
        //{
        //  _previousTimeStep = _previousTimeStep.Add(_minTimeStep);
        //}
        //else
        //{
        //  _previousTimeStep = _observations[i].Time;
        //}
        //_tso.Time.SetTimeForTimeStepNr(i+1, _previousTimeStep);


      }
    }


    /// <summary>
    /// Writes the data in dfs0-file in the folder OutputPath.
    /// FileName is ID + .dfs0
    /// </summary>
    /// <param name="OutputPath"></param>
    public void WriteToDfs0(string OutputPath)
    {
      if (_tso == null)
        InitializeToWriteDFS0();

      _tso.Connection.FilePath = Path.Combine(OutputPath, _id + ".dfs0");
      _tso.Connection.Save();
    }

    /// <summary>
    /// Reads in observations from a dfs0-file
    /// </summary>
    /// <param name="filename"></param>
    /// <param name="item"></param>
    public void ReadDfs0(string filename, int item)
    {
      _tso = new TSObjectClass();
      _tso.Connection.FilePath = filename;
      _tso.Connection.Open();

      for (int i = 1; i <= _tso.Time.NrTimeSteps; i++)
      {
        _observations.Add(new TimeSeriesEntry((DateTime)_tso.Time.GetTimeForTimeStepNr(i), (float)_tso.Item(item).GetDataForTimeStepNr(i)));
      }
    }


    public override string ToString()
    {
      return base.ToString();
    }


    #region Properties
    ///// <summary>
    ///// Gets the observation data. 
    ///// Contains methods to retrieve max, min, average etc.
    ///// </summary>
    //public TSItemData DHITimeSeriesData
    //{
    //  get
    //  {
    //    if (_item == null)
    //      InitializeToWriteDFS0();

    //    return _item.Data;
    //  }
    //}

    ///// <summary>
    ///// Gets the number of time steps in the DHI time series.
    ///// Can be different from the actual number of time steps depending on how multiple time steps with the same
    ///// date are handled.
    ///// </summary>
    //public int DHITimeSeriesDataCount
    //{
    //  get
    //  {
    //    if (_tso == null)
    //      InitializeToWriteDFS0();
    //    return _tso.Time.NrTimeSteps;
    //  }
    //}



#region Statistics

    public double? RMS
    {
      get
      {
        if (_observations.Count == 0)
          return null;

        return Math.Pow(_observations.Average(new Func<TimeSeriesEntry, double>(num => num.RMSE)), 0.5);
      }
    }

    public double? ME
    {
      get
      {
        if (_observations.Count == 0)
          return null;
        return _observations.Average(new Func<TimeSeriesEntry, double>(num => num.ME));
      }
    }

    public double? MAE
    {
      get
      {
        if (_observations.Count == 0)
          return null;
        return _observations.Average(new Func<TimeSeriesEntry, double>(num => Math.Abs(num.ME)));
      }
    }


    public double? RMST
    {
      get 
      {
        if (_observations.Count == 0)
          return null;
        double simmean = _observations.Average(new Func<TimeSeriesEntry, double>(num => num.SimulatedValue));
        double obsmean = _observations.Average(new Func<TimeSeriesEntry, double>(num => num.Value)); 

        double val = _observations.Sum(new Func<TimeSeriesEntry,double>(num => Math.Pow(num.Value - obsmean-(num.SimulatedValue - simmean),2)));
        return  Math.Pow(val/_observations.Count, 0.5);
      }
    }


#endregion

    /// <summary>
    /// Gets the observations. Also used to add data
    /// </summary>
    public List<TimeSeriesEntry> Observations
    {
      get { return _observations; }
    }

    public List<TimeSeriesEntry> UniqueObservations
    {
      get
      {
        return _observations.Distinct().ToList();
      }
    }

    public int Column
    {
      get { return _column; }
      set { _column = value; }
    }

    public int Row
    {
      get { return _row; }
      set { _row = value; }
    }

    public int Layer
    {
      get { return _layer; }
      set { _layer = value; }
    }

    public double Depth
    {
      get { return _depth; }
      set { _depth = value; }
    }


    #endregion
  }
}
