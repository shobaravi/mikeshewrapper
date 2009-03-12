using System;
namespace MikeSheWrapper.Tools
{
  public interface IWell
  {
    string Description { get; set; }
    string ID { get; set; }
    System.Collections.Generic.List<double> ScreenBottom { get; set; }
    System.Collections.Generic.List<double> ScreenTop { get; set; }
    double Terrain { get; set; }
    string ToString();
    double X { get; set; }
    double Y { get; set; }
  }
}
