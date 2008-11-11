using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using MikeSheWrapper.Tools;

namespace MikeSheWrapper.Tools.UnitTest
{
  [TestFixture]
  public class ShapeReaderTest
  {
    [Test]
    public void TestSingleRead()
    {
      ShapeReader SP = new ShapeReader(@"F:\Jacob\Pejlinger\novomr456_pejle_ks.shp");

      double d = SP.ReadDouble(600000, "YUTM");

      DateTime dd = SP.ReadDate(1, "tiemofmeas");
      int di = SP.ReadInt(1, "tiemofmeas");

    }
  }
}
