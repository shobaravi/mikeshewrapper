using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MikeSheWrapper.Tools;

namespace MikeSheWrapper.JupiterTools
{
  public class JupiterIntake:Intake,IEquatable<JupiterIntake>, IEqualityComparer<JupiterIntake>
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


    #region IEquatable<JupiterIntake> Members

    public bool Equals(JupiterIntake other)
    {
      return base.Equals(other);
    }

    #endregion

    #region IEqualityComparer<JupiterIntake> Members

    public bool Equals(JupiterIntake x, JupiterIntake y)
    {
      return base.Equals(x,y);
    }

    public int GetHashCode(JupiterIntake obj)
    {
      return base.GetHashCode(obj);
    }

    #endregion
  }
}
