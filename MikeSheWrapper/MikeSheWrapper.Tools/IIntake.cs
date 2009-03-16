using System;
namespace MikeSheWrapper.Tools
{
  public interface IIntake
  {
    int CompareTo(Intake other);
    int IDNumber { get; set; }
    double? MAE { get; }
    double? ME { get; }
    System.Collections.Generic.List<ObservationEntry> Observations { get; }
    double? RMS { get; }
    double? RMST { get; }
    System.Collections.Generic.List<double> ScreenBottom { get; set; }
    System.Collections.Generic.List<double> ScreenTop { get; set; }
    string ToString();
    IWell well { get; }
  }
}
