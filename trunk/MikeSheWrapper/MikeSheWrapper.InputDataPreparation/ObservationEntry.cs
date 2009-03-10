using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MikeSheWrapper.Tools;

namespace MikeSheWrapper.InputDataPreparation
{
  /// <summary>
  /// A small class that can hold an observation entry and calculate basic statistics. 
  /// Note. Entries are equal if the time is equal.
  /// </summary>
  public class ObservationEntry : TimeSeriesEntry, IComparable<ObservationEntry>, IEquatable<ObservationEntry>
  {
    
    /// <summary>
    /// Constructs a time series entry
    /// </summary>
    /// <param name="Time"></param>
    /// <param name="Value"></param>
    public ObservationEntry(DateTime Time, double Value):base(Time, Value)
    {
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

    #region IComparable<TimeSeriesEntry> Members

    public int CompareTo(ObservationEntry other)
    {
      return base.CompareTo(other);
    }

    #endregion

    #region IEquatable<TimeSeriesEntry> Members

    public bool Equals(ObservationEntry other)
    {
      return base.Equals(other);
    }

    #endregion

  }
}
