using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MikeSheWrapper.Tools;

namespace MikeSheWrapper.Irrigation
{
  public class IrrigationWell:Well
  {
    private double _maxRate;
    private double _maxDepth;
    private int _gridCode;

    public int GridCode
    {
      get { return _gridCode; }
      set { _gridCode = value; }
    }

    public double MaxRate
    {
      get { return _maxRate; }
      set { _maxRate = value; }
    }

    public double MaxDepth
    {
      get { return _maxDepth; }
      set { _maxDepth = value; }
    }
  }
}
