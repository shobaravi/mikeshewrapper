using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MikeSheWrapper.Tools;


namespace MikeSheWrapper.Interfaces
{
  public interface IXYZTDataSet
  {
    Matrix3d TimeData(int TimeStep);
    Matrix3d TimeData(DateTime TimeStep);
  }
}
