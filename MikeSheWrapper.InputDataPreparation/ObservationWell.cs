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

    private List<TimeSeriesEntry> _observations = new List<TimeSeriesEntry>();

    private string _toLog;

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
    /// Create the timeseries and move data
    /// </summary>
    public void InitializeToWriteDFS0()
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

      _observations.Sort();

      for (int i = 0; i < _observations.Count; i++)
      {
        //Only add the first measurement of the day
        if (_observations[i].Time != _previousTimeStep)
        {
          _tso.Time.AddTimeSteps(1);
          _tso.Time.SetTimeForTimeStepNr(i + 1, _observations[i].Time);
          _item.SetDataForTimeStepNr(i + 1, (float)_observations[i].Value);
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


    public override string ToString()
    {
      return base.ToString();
    }

    public string LogString()
    {
      return _toLog;
    }

    #region Properties
    /// <summary>
    /// Gets the observation data. 
    /// Contains methods to retrieve max, min, average etc.
    /// </summary>
    public TSItemData DHITimeSeriesData
    {
      get
      {
        if (_item == null)
          InitializeToWriteDFS0();

        return _item.Data;
      }
    }

    /// <summary>
    /// Gets the number of time steps in the DHI time series.
    /// Can be different from the actual number of time steps depending on how multiple time steps with the same
    /// date are handled.
    /// </summary>
    public int DHITimeSeriesDataCount
    {
      get
      {
        if (_tso == null)
          InitializeToWriteDFS0();
        return _tso.Time.NrTimeSteps;
      }
    }


    /// <summary>
    /// Gets the observations. Also used to add data
    /// </summary>
    public List<TimeSeriesEntry> Observations
    {
      get { return _observations; }
    }

    #endregion
  }
}
