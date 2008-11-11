using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

namespace MikeSheWrapper.InputFiles.UnitTest
{
  [TestFixture]
  public class SheFileTest
  {

    private SheFile _she;

    [SetUp]
    public void ConstructTest()
    {
      _she = new SheFile(@"..\..\..\TestData\TestModel.she");
    }


    [Test]
    public void ReadTest()
    {
      Assert.AreEqual(1, _she.MIKESHE_FLOWMODEL.SimSpec.ModelComp.WM);

      Assert.AreEqual(Path.GetFullPath(@"..\..\..\TestData\Model Domain and Grid.dfs2"), _she.MIKESHE_FLOWMODEL.Catchment.DFS_2D_DATA_FILE.FILE_NAME);

      Assert.AreEqual(3, _she.MIKESHE_FLOWMODEL.LandUse.CommandAreas.CommandAreas1.Count);
      Assert.AreEqual("Grid code = 4", _she.MIKESHE_FLOWMODEL.LandUse.CommandAreas.CommandAreas1[2].AreaName);

    }

    [Test]
    public void ModifyTest()
    {

      _she.MIKESHE_FLOWMODEL.LandUse.CommandAreas.AddNewCommandArea();
      _she.SaveAs(@"..\..\..\TestData\TestModel_changed.she");

    }

    [Test]
    public void WriteTest()
    {
      _she.MIKESHE_FLOWMODEL.SimSpec.ModelComp.WM = 0;
      _she.SaveAs(@"..\..\..\TestData\TestModel_changed.she");
    }

  }
}
