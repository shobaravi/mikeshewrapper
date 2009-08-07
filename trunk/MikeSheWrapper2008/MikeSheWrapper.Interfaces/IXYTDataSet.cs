using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.LinearAlgebra;

namespace MikeSheWrapper.Interfaces
{
  public interface IXYTDataSet
  {
    Matrix TimeData(int TimeStep);
    Matrix TimeData(DateTime TimeStep);
  }
}
