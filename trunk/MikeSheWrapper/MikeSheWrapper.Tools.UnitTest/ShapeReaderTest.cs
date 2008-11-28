using System;
using System.Data;
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
      PointShapeReader SP = new PointShapeReader(@"F:\Jacob\Pejlinger\novomr456_pejle_ks.shp");


      double d = SP.Data.ReadDouble(600000, "YUTM");

      DateTime dd = SP.Data.ReadDate(1, "tiemofmeas");
      int di = SP.Data.ReadInt(1, "tiemofmeas");
    }


  }
}
