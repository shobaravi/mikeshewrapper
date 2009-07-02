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
    int? Layer { get; set;}
    List<ObservationEntry> Observations { get; }
    double? RMS { get; }
    double? RMST { get; }
    List<Screen> Screens { get; }
    //List<double> ScreenBottom { get; set; }
    //List<double> ScreenTop { get; set; }
    //List<double> ScreenBottomAsKote { get; set; }
    //List<double> ScreenTopAsKote { get; }
    string ToString();
    IWell well { get; }
  }
}
