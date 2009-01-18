using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MikeSheWrapper.InputDataPreparation
{
  /// <summary>
  /// A small class that can hold an entry in a time series and calculate basic statistics. 
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
    /// The number of boundary cells close to the simulated value. Due to LayerStatistics
    /// </summary>
    public int BoundaryCells;

    /// <summary>
    /// The number of dry cells close to the simulated value. Due to LayerStatistics
    /// </summary>
    public int DryCells;

    /// <summary>
    /// Am additional simulated value for this entry. Not used in the statistics calculation. Due to LayerStatistics
    /// </summary>
    public double SimulatedValueCell {get;set;}
    
    /// <summary>
    /// Gets and sets the time for this entry
    /// </summary>
    public DateTime Time { get; set; }

    /// <summary>
    /// Gets and sets the value for this entry
    /// </summary>
    public double Value { get; set; }

    /// <summary>
    /// Gets and sets the simulated value. This value is used for the statistics calculation
    /// </summary>
    public double SimulatedValue {get;set;}

    /// <summary>
    /// Gets the mean error. Value - simulated value
    /// </summary>
    public double ME
    {
      get
      {
        return Value - SimulatedValue;
      }
    }

    /// <summary>
    /// Gets the root mean square error. ME^2
    /// </summary>
    public double RMSE
    {
      get { return Math.Pow(ME, 2.0);  }
    }
   
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
