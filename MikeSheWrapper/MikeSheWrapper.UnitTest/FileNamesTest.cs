using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MikeSheWrapper;
using NUnit.Framework;


namespace MikeSheWrapper.UnitTest
{
  [TestFixture]
  public class FileNamesTest
  {
    private FileNames _filenames;
    [SetUp]
    public void Load()
    {
      _filenames = new FileNames(@"..\..\..\TestData\TestModelDemo.she");
    }


    [Test]
    public void WellFile()
    {
      Assert.AreEqual(Path.GetFullPath(@"..\..\..\TestData\DemoWells.WEL"), _filenames.WelFileName);
    }

  }
}
