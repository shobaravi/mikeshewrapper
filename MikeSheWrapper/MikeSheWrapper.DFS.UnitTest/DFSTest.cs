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
  public class DFSTest
  {
    DFS3 _dfs;
    DFS2 _simpleDfs;


    [SetUp]
    public void ConstructTest()
    {
      _simpleDfs = new DFS2(@"..\..\..\TestData\simpelmatrix.dfs2");
      _dfs = new DFS3(@"..\..\..\TestData\omr4_jag_3DSZ.dfs3");
    }


    [Test]
    public void DFS0Test()
    {
      DFS0 _dfs0 = new DFS0(@"..\..\..\TestData\novomr4_indv_dfs0_ud1.dfs0");
      Assert.AreEqual(33316.7, _dfs0.GetData(0, 1), 1e-1);
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
    public void GetDataTest1()
    {
      Matrix M = _simpleDfs.GetData(0, 1);
      Assert.AreEqual(0, M[0, 0]);
      Assert.AreEqual(1, M[1, 0]);
      Assert.AreEqual(2, M[2, 0]);
      Assert.AreEqual(3, M[0, 1]);

      Assert.AreEqual(10,_simpleDfs.GetData(323, 125, 0, 1) , 1e-5);

    }



    [Test]
    public void GetDataTest()
    {
      Matrix3d M = _dfs.GetData(0, 1);
      Assert.AreEqual(6.733541, M[151, 86, 17], 1e-5);



    }
  }
}
