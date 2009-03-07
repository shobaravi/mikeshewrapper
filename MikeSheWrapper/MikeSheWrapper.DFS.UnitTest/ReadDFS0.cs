using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DHI.TimeSeries;
using NUnit.Framework;


namespace MikeSheWrapper.DFS.UnitTest
{
  [TestFixture]
  public class ReadDFS0
  {
    [Test]
    public void ReadEUM()
    {
      TSObject tso = new TSObjectClass();
      tso.Connection.FilePath = @"..\..\..\TestData\1.102_1.dfs0";
      tso.Connection.Open();

      int eumItem = tso.Item(1).EumType;
      int eumUnit = tso.Item(1).EumUnit;

    }

    [Test]
    public void ReadItems()
    {
      DFS0 _data = new DFS0(@"..\..\..\\TestData\Detailed timeseries output.dfs0");

      Assert.AreEqual(3, _data.GetTimeStep(new DateTime(2000, 1, 4, 11, 0, 0)));
      Assert.AreEqual(3, _data.GetTimeStep(new DateTime(2000, 1, 4, 12, 0, 0)));

      Assert.AreEqual(4, _data.GetTimeStep(new DateTime(2000, 1, 4, 13, 0, 0)));
      Assert.AreEqual(0, _data.GetTimeStep(new DateTime(1200, 1, 4, 13, 0, 0)));
      Assert.AreEqual(31, _data.GetTimeStep(new DateTime(2200, 1, 4, 13, 0, 0)));

      

      _data.Dispose();

    }
  }
}
