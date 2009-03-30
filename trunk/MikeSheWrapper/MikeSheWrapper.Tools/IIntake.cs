using System;
using System.Collections.Generic;

namespace MikeSheWrapper.Tools
{
  public interface IIntake
  {
    int CompareTo(Intake other);
    int IDNumber { get; set; }
    double? MAE { get; }
    double? ME { get; }
    List<ObservationEntry> Observations { get; }
    double? RMS { get; }
    double? RMST { get; }
    List<double> ScreenBottom { get; set; }
    List<double> ScreenTop { get; set; }
    string ToString();
    IWell well { get; }
    DateTime PumpingStart{get;set;}
    DateTime PumpingStop { get; set;}
  }
}
