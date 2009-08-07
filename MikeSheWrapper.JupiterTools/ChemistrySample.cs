using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MikeSheWrapper.JupiterTools
{
  public class ChemistrySample
  {
    public int SampleID { get; set; }
    public DateTime SampleDate {get;set;}
    public int CompoundNo { get; set; }
    public string CompoundName { get; set; }
    public int Unit { get; set; }
    public double Amount { get; set; }

    public override string ToString()
    {
      return SampleDate.ToShortDateString() + ": "+ Amount +" "+ CompoundName;
    }
  }
}
