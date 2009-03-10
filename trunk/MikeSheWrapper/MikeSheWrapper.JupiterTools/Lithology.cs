using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MikeSheWrapper.JupiterTools
{
  public class Lithology:IComparable<Lithology>
  {
    public double Top { get; set; }
    public double Bottom { get; set; }
    public string RockType { get; set; }
    public string RockSymbol { get; set; }
    public string TotalDescription { get; set; }

    public Lithology()
    { 
    }


    #region IComparable<Lithology> Members

    public int CompareTo(Lithology other)
    {
      return Bottom.CompareTo(other.Bottom);
    }

    #endregion
  }
}
