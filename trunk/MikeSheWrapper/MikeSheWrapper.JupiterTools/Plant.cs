using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MikeSheWrapper.Tools;

namespace MikeSheWrapper.JupiterTools
{
  /// <summary>
  /// A class representing a water production plant. Holds the historical pumping time series and the associated wells
  /// </summary>
  public class Plant
  {

    #region Properties

    /// <summary>
    /// Unique ID number
    /// </summary>
    public int IDNumber { get; private set; }

    /// <summary>
    /// Timeseries with extraction rates. 
    /// </summary>
    public List<TimeSeriesEntry> Extractions { get; private set; }
    
    /// <summary>
    /// Time series with extraction from surface water.
    /// </summary>
    public List<TimeSeriesEntry> SurfaceWaterExtrations { get; private set; }
    
    /// <summary>
    /// The wells associated to this plant
    /// </summary>
    public List<Well> PumpingWells { get; private set; }

    /// <summary>
    /// The name of the plant
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// The street where the plant is located
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// The postal code
    /// </summary>
    public int PostalCode { get; set; }

    /// <summary>
    /// The permit was given at this date
    /// </summary>
    public DateTime PermitDate { get; set; }

    /// <summary>
    /// The date at which this permit expires
    /// </summary>
    public DateTime PermitExpiryDate { get; set; }

    /// <summary>
    /// The yearly permit in m3
    /// </summary>
    public double Permit { get; set; }

    #endregion

    public Plant(int IDNumber)
    {
      Extractions = new List<TimeSeriesEntry>();
      PumpingWells = new List<Well>();
      SurfaceWaterExtrations = new List<TimeSeriesEntry>();
      this.IDNumber = IDNumber;
    }
  }
}
