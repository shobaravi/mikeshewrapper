using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MikeSheWrapper.Tools
{
  /// <summary>
  /// A small class that can hold an entry in a time series. 
  /// Note. Entries are equal if the time is equal.
  /// </summary>
  public class TimeSeriesEntry:IComparable<TimeSeriesEntry>,IEquatable<TimeSeriesEntry>
  {
    
    /// <summary>
    /// Constructs a time series entry
    /// </summary>
    /// <param name="Time"></param>
    /// <param name="Value"></param>
    public TimeSeriesEntry(DateTime Time, double Value)
    {
      this.Time = Time;
      this.Value = Value;
    }


    /// <summary>
    /// Gets and sets the time for this entry
    /// </summary>
    public DateTime Time { get; set; }

    /// <summary>
    /// Gets and sets the value for this entry
    /// </summary>
    public double Value { get; set; }


   
#region System.Object overrides

    public override string ToString()
    {
      return "T= " + Time.ToShortDateString() + ", V = " + Value;
    }

    public override int GetHashCode()
    {
      return Time.GetHashCode();
    }

#endregion

    #region IComparable<TimeSeriesEntry> Members

    public int CompareTo(TimeSeriesEntry other)
    {
      return Time.CompareTo(other.Time);
    }

    #endregion

    #region IEquatable<TimeSeriesEntry> Members

    public bool Equals(TimeSeriesEntry other)
    {
      return other.Time.Equals(Time);
    }

    #endregion
  }
}
