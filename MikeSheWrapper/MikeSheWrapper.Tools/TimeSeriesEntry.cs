using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MikeSheWrapper.Tools
{
  public class TimeSeriesEntry:IComparable<TimeSeriesEntry>,IEquatable<TimeSeriesEntry>
  {
    private DateTime _time;
    private double _value;

    public int BoundaryCells;
    public int DryCells;
    private double _simulatedValueCell;
    private double _simulatedValue;



    public double SimulatedValueCell
    {
      get { return _simulatedValueCell; }
      set { _simulatedValueCell = value; }
    }

    public double SimulatedValue
    {
      get { return _simulatedValue; }
      set { _simulatedValue = value; }
    }

    public double ME
    {
      get
      {
        return _value - _simulatedValue;
      }
    }

    public double RMSE
    {
      get { return Math.Pow(ME, 2.0);  }
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

    public override string ToString()
    {
      return "T= " + _time.ToShortDateString() + ", V = " + _value;
    }


    public override int GetHashCode()
    {
      return _time.GetHashCode();
    }

    #region IComparable<TimeSeriesEntry> Members

    public int CompareTo(TimeSeriesEntry other)
    {
      return _time.CompareTo(other._time);
    }



    #endregion

    #region IEquatable<TimeSeriesEntry> Members

    public bool Equals(TimeSeriesEntry other)
    {
      return other.Time.Equals(_time);
    }

    #endregion
  }
}
