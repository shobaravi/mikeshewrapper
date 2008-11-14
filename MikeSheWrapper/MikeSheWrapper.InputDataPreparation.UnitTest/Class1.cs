using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using System.Xml.Serialization;


namespace MikeSheWrapper.InputDataPreparation.UnitTest
{
  [TestFixture]
  public class Class1
  {
    [Test]
    public void Dummy()
    {
      Configuration cf = new Configuration();

      cf.ObservationFile = @"F:\Jacob\MikeSheWrapper\TestData\LayerStatistics\novomr1_pejl90-05_sort1.txt";
      cf.PreProcessedDFS2 = @"F:\Jacob\MikeSheWrapper\TestData\LayerStatistics\Novomr1_26mar08_PreProcessed.DFS2";
      cf.PreProcessedDFS3 = @"F:\Jacob\MikeSheWrapper\TestData\LayerStatistics\Novomr1_26mar08_PreProcessed_3DSZ.DFS3";
      cf.ResultFile = @"F:\Jacob\MikeSheWrapper\TestData\LayerStatistics\Novomr1_26mar08_3DSZ.dfs3";

      XmlSerializer x = new XmlSerializer(typeof(Configuration));
      x.Serialize(new FileStream(@"F:\Jacob\MikeSheWrapper\TestData\LayerStatistics\conf.xml", FileMode.Create), cf);

    }
  }
}
