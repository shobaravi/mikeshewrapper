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
    public int IDNumber { get; private set; }

    public List<TimeSeriesEntry> Extractions { get; private set; }
    public List<TimeSeriesEntry> SurfaceWaterExtrations { get; private set; }
    public List<Well> PumpingWells { get; private set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public int ZipCode { get; set; }
    public DateTime PermitDate { get; set; }
    public DateTime PermitExpiryDate { get; set; }
    public double Permit { get; set; }

    public Plant(int IDNumber)
    {
      Extractions = new List<TimeSeriesEntry>();
      PumpingWells = new List<Well>();
      SurfaceWaterExtrations = new List<TimeSeriesEntry>();
      this.IDNumber = IDNumber;
    }
  }
}
