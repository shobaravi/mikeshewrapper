using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MikeSheWrapper.Tools
{
  public class TimeSeriesEntry:IComparable<TimeSeriesEntry>
  {
    private DateTime _time;
    private double _value;

    public int BoundaryCells;
    public int DryCells;

    private double _simulatedValueCell;

    public double SimulatedValueCell
    {
      get { return _simulatedValueCell; }
      set { _simulatedValueCell = value; }
    }
    private double _simulatedValueInterpolated;

    public double SimulatedValueInterpolated
    {
      get { return _simulatedValueInterpolated; }
      set { _simulatedValueInterpolated = value; }
    }
    private double _mE;

    public double ME
    {
      get { return _mE; }
      set { _mE = value; }
    }
    private double _rMSE;

    public double RMSE
    {
      get { return _rMSE; }
      set { _rMSE = value; }
    }

    public TimeSeriesEntry(DateTime Time, double Value)
    {
      _time = Time;
      _value = Value;
    }

    public DateTime Time
    {
      get { return _time; }
      set { _time = value; }
    }

    public double Value
    {
      get { return _value; }
      set { _value = value; }
    }


    #region IComparable<TimeSeriesEntry> Members

    public int CompareTo(TimeSeriesEntry other)
    {
      return _time.CompareTo(other._time);
    }

    #endregion
  }
}
