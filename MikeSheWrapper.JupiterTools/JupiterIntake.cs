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
    public string RefPoint { get; set; }

    internal JupiterIntake(IWell Well, int IDNumber)
    {
      this.well = Well;
      this.IDNumber = IDNumber;
    }

    internal JupiterIntake(JupiterWell Well, IIntake Intake):this(Well, Intake.IDNumber)
    {
      foreach (ObservationEntry OE in Intake.Observations)
        this.Observations.Add(OE);
      foreach (Screen SB in Intake.Screens)
      {
        Screen SBClone = new Screen(this);
        SBClone.DepthToBottom = SB.DepthToBottom;
        SBClone.DepthToTop = SB.DepthToTop;
        SBClone.Number = SB.Number;
      }

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
