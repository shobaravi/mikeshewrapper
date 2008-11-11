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
