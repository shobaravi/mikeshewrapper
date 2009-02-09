using System;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using MikeSheWrapper.Irrigation;

namespace MikeSheWrapper.Irrigation.UnitTest
{
  [TestFixture]
  public class ControllerTest
  {
    [Ignore]
    [Test]
    public void SerializeTest()
    {
      Configuration Cf = new Configuration();

      Cf.BottomHeader = "BUND";
      Cf.TopHeader = "TOP";
      Cf.XHeader = "UTMX";
      Cf.YHeader = "UTMY";
      Cf.IdHeader = "NOVANA_ID";
      Cf.SheFile = @"F:\Jacob\MikeSheWrapper\TestData\TestModel.she";
      Cf.WellShapeFile = @"F:\DHI\Data\Novana\Novomr4\Time\thiesn1_SpatialJoin.shp";

      XmlSerializer x = new XmlSerializer(Cf.GetType());
      x.Serialize(new System.IO.FileStream(@"F:\jacob\out.xml", System.IO.FileMode.Create), Cf);
    }

    [Test]
    public void RunTest()
    {
      XmlSerializer x = new XmlSerializer(typeof(Configuration));
      
      XDocument xd = XDocument.Load(@"..\..\..\TestData\IrrigationConfiguration.xml");

      Configuration Cf = (Configuration)x.Deserialize(new System.IO.FileStream(@"..\..\..\TestData\IrrigationConfiguration.xml", System.IO.FileMode.Open));
      Controller C = new Controller(Cf);

      C.Run();

    }

    [Ignore]
    [Test]
    public void Test1()
    {
      Configuration Cf = new Configuration();

      Cf.BottomHeader = "BUND";
      Cf.TopHeader = "TOP";
      Cf.XHeader = "UTMX";
      Cf.YHeader = "UTMY";
      Cf.IdHeader = "NOVANA_ID";
      Cf.SheFile = @"F:\DHI\Data\Novana\Novomr4\Result\omr4_jag_UZ_irr.SHE";
      Cf.WellShapeFile = @"F:\DHI\Data\Novana\Novomr4\Time\thiesn1_SpatialJoin.shp";

      Controller C = new Controller(Cf);
      C.InsertIrrigationWells();
      C.SaveAs(@"F:\DHI\Data\Novana\Novomr4\Result\omr4_jag_UZ_irr_to.SHE");
    }

  }
}
