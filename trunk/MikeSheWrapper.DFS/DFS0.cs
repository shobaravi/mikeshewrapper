using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MikeSheWrapper.DFS
{
  public class DFS0 : DFS
  {
    public DFS0(string DFSFileName)
      : base(DFSFileName)
    {

    }

    /// <summary>
    /// Gets the value for the Time step and Item
    /// </summary>
    /// <param name="TimeStep"></param>
    /// <param name="Item"></param>
    /// <returns></returns>
    public double GetData(int TimeStep, int Item)
    {
      readNextItemTimeStep(TimeStep, Item);
      return dfsdata[0];
    }
  }
}
