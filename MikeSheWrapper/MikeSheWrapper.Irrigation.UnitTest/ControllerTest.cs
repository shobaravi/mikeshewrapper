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
      Cf.IdHeader = "ID";
      Cf.SheFile = @"C:\Kode\MikeSheWrapper\TestData\TestModel.she";
      Cf.WellShapeFile = @"C:\Kode\MikeSheWrapper\TestData\commandareas.shp";
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

      Configuration Cf = (Configuration)x.Deserialize(new System.IO.FileStream(xmlFileName, System.IO.FileMode.Open));
     
      Controller C = new Controller(Cf);

      C.Run();

    }

    [Test]
    public void RunTest2()
    {
      Program.Main(new string[]{@"..\..\..\TestData\IrrigationConfiguration.xml"});

      Program.Main(new string[] {@"C:\Kode\MikeSheWrapper\TestData\TestModel.she", @"..\..\..\TestData\IrrigationConfiguration.xml" });
      Program.Main(new string[] {@"..\..\..\TestData\IrrigationConfiguration.xml", @"C:\Kode\MikeSheWrapper\TestData\TestModel.she" });
    }
    
  }
}
