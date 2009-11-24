using System;
using System.IO;
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
    [Test]
    public void SerializeTest()
    {
      Configuration Cf = new Configuration();

      Cf.BottomHeader = "BOTTOM";
      Cf.TopHeader = "TOP";
      Cf.XHeader = "XUTM";
      Cf.YHeader = "YUTM";
      Cf.IdHeader = "XUTM";
      Cf.SheFile = Path.GetFullPath(@"..\..\..\TestData\TestModel.she");
      Cf.MaxDepthHeader = "BOTTOM";
      Cf.MaxRateHeader = "XUTM";

      Cf.WellShapeFile = Path.GetFullPath(@"..\..\..\TestData\commandareas.shp");
      Cf.DeleteWellsAfterRun = false;

      XmlSerializer x = new XmlSerializer(Cf.GetType());
      System.IO.FileStream file =new System.IO.FileStream(@"..\..\..\TestData\IrrigationConfiguration.xml", System.IO.FileMode.Create);

      x.Serialize(file, Cf);
      file.Dispose();
      
    }

    [Test]
    public void RunTest()
    {
      XmlSerializer x = new XmlSerializer(typeof(Configuration));

      string xmlFileName = @"..\..\..\TestData\IrrigationConfiguration.xml";
      Configuration Cf;
        using (FileStream fs =new System.IO.FileStream(xmlFileName, System.IO.FileMode.Open))
          Cf = (Configuration)x.Deserialize(fs);
     
      Controller C = new Controller(Cf);

      C.Run();

    }

    [Test]
    public void RunTest2()
    {
      Program.Main(new string[]{@"..\..\..\TestData\IrrigationConfiguration.xml"});

      Program.Main(new string[] { Path.GetFullPath(@"..\..\..\TestData\TestModel.she"), @"..\..\..\TestData\IrrigationConfiguration.xml" });
      Program.Main(new string[] { @"..\..\..\TestData\IrrigationConfiguration.xml", Path.GetFullPath(@"..\..\..\TestData\TestModel.she") });
    }
    
  }
}
