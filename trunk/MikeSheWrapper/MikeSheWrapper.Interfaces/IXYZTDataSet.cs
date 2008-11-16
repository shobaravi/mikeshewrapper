using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MikeSheWrapper.Tools;


namespace MikeSheWrapper.Interfaces
{
  public interface IXYZTDataSet
  {
    IMatrix3d TimeData(int TimeStep);
    IMatrix3d TimeData(DateTime TimeStep);
    int GetTimeStep(DateTime TimeStep);
  }
}
