using System;
namespace MikeSheWrapper.Tools
{
  public interface IWell
  {
    string Description { get; set; }
    string ID { get; set; }
    double Terrain { get; set; }
    string ToString();
    double X { get; set; }
    double Y { get; set; }
  }
}
