using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MikeSheWrapper.JupiterTools
{
  /// <summary>
  /// A small class holding the data from a Jupiter lithology sample. Sorts by depth.
  /// </summary>
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

    public override string ToString()
    {
      return Top + " - " + Bottom + ": "+ RockSymbol;
    }


    #region IComparable<Lithology> Members

    public int CompareTo(Lithology other)
    {
      return Top.CompareTo(other.Top);
    }

    #endregion
  }
}
