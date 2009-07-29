using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using MathNet.Numerics.LinearAlgebra;
using MikeSheWrapper.Tools;
using MikeSheWrapper.DFS;

namespace MikeSheWrapper.DFS.UnitTest
{
  [TestFixture]
  public class DFS3Test
  {
    DFS3 _dfs;


    [SetUp]
    public void ConstructTest()
    {

      _dfs = new DFS3(@"..\..\..\TestData\omr4_jag_3DSZ.dfs3");

    }
       

    [TearDown]
    public void Destruct()
    {
      _dfs.Dispose();
    }


   

    [Test]
    public void StaticTest()
    {
      Assert.AreEqual(460, _dfs.NumberOfColumns);
      Assert.AreEqual(196, _dfs.NumberOfRows);
      Assert.AreEqual(18, _dfs.NumberOfLayers);
    }

    [Test]
    public void IndexTest()
    {
      //Left and right
      Assert.AreEqual(-1, _dfs.GetColumnIndex(0));
      Assert.AreEqual(-2, _dfs.GetColumnIndex(double.MaxValue));

      //Over and under
      Assert.AreEqual(-1, _dfs.GetRowIndex(0));
      Assert.AreEqual(-2, _dfs.GetRowIndex(double.MaxValue));

      Assert.AreEqual(0, _dfs.GetColumnIndex(410000));
      Assert.AreEqual(139, _dfs.GetColumnIndex(479336));

      Assert.AreEqual(49, _dfs.GetRowIndex(6128437));
    }



    [Test]
    public void GetTimeTest()
    {
      Assert.AreEqual(new DateTime(1990, 1, 2, 12, 0, 0), _dfs.TimeOfFirstTimestep);
      Assert.AreEqual(new TimeSpan(0, 0, 864000), _dfs.TimeStep);
    }



    [Test]
    public void GetDataTest()
    {
      Matrix3d M = _dfs.GetData(0, 1);
      Assert.AreEqual(6.733541, M[151, 86, 17], 1e-5);
      Assert.AreEqual(13.94974, _dfs.GetData(1, 1)[150, 86, 17], 1e-5);
      Assert.AreEqual(13.7237, _dfs.GetData(0, 1)[150, 86, 17], 1e-5);

    }
  }
}
