using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MikeSheWrapper.Tools;

namespace MikeSheWrapper.JupiterTools
{
  /// <summary>
  /// Small class. Only holds information about when the intake is active
  /// The class is necessary because the same intake can have different active periods on different plants
  /// </summary>
  public class PumpingIntake
  {
    public IIntake Intake { get; private set; }

    public DateTime Start {get;set;}

    public DateTime End { get; set; }

    public PumpingIntake(IIntake intake)
    {
      Intake = intake;
    }

  }
}
