using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MikeSheWrapper.Tools;

using NUnit.Framework;

namespace MikeSheWrapper.Tools.UnitTest
{
  [TestFixture]
  public class ShapeWriterTest
  {
    [Test]
    public void WriteTest()
    {
      string File = @"..\..\..\TestData\WriteTest.Shp";

      PointShapeWriter PSW = new PointShapeWriter(File);

      PSW.WritePointShape(10, 20);
      PSW.WritePointShape(20, 30);
      PSW.WritePointShape(30, 40);

      DataTable DT = new DataTable();
      DT.Columns.Add("Name", typeof(string));
      DT.Rows.Add(new object[]{"point1"});
      DT.Rows.Add(new object[]{"point2"});
      DT.Rows.Add(new object[]{"point3"});

      PSW.Data.WriteDate(DT);
      PSW.Dispose();


      PointShapeReader PSR = new PointShapeReader(File);
      double x;
      double y;

      DataTable DTread = PSR.Data.Read();

      foreach (DataRow dr in DTread.Rows)
      {
        Console.WriteLine(dr[0].ToString());
        PSR.ReadNext(out x, out y);
        Console.WriteLine(x.ToString() + "   " + y.ToString());

      }
    }

  }
}
