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
  }
}
