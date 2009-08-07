using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MikeSheWrapper.Tools;

namespace MikeSheWrapper.Interfaces
{
  public interface IXYZDataSet
  {
    IMatrix3d Data { get; }
  }
}
