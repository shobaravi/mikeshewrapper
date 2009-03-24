using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MikeSheWrapper.Tools;

namespace MikeSheWrapper.JupiterTools
{
  public class JupiterIntake:Intake
  {
    public DataRow Data { get; set; }

    internal JupiterIntake(IWell Well, int IDNumber)
    {
      this.well = Well;
      this.IDNumber = IDNumber;
    }

    internal JupiterIntake(JupiterWell Well, IIntake Intake):this(Well, Intake.IDNumber)
    {
      foreach (ObservationEntry OE in Intake.Observations)
        this.Observations.Add(OE);
      foreach (double SB in Intake.ScreenBottom)
        ScreenBottom.Add(SB);
      foreach (double ST in Intake.ScreenTop)
        ScreenTop.Add(ST);

      if (Intake is JupiterIntake)
        Data = ((JupiterIntake)Intake).Data;
    }

  }
}
