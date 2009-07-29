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
      //DFS2 d = new DFS2(@"D:\Udvikling\MikeSheWrapper\MikeSheWrapper\TestData\Layer Statistics\Novomr1_inv_PreProcessed.dfs2");

      _simpleDfs = new DFS2(@"..\..\..\TestData\simpelmatrix.dfs2");
      _dfs = new DFS3(@"..\..\..\TestData\omr4_jag_3DSZ.dfs3");

    }


    [Test]
    public void OpenTwiceTest()
    {
      DFS2 dfs = new DFS2(@"..\..\..\TestData\Layer Statistics\Novomr1_inv_PreProcessed.DFS2");
      
      List<DFS2> _files = new List<DFS2>();
      for (int i=0;i<100;i++)
      {
        _files.Add(new DFS2(@"..\..\..\TestData\Layer Statistics\Novomr1_inv_PreProcessed.DFS2"));
        Matrix M = _files[i].GetData(0,1);
      }

      int k = 0;
      DFS2.MaxEntriesInBuffer = 5;

      for (int i = 1; i < dfs.ItemNames.Count(); i++)
      {
        Matrix M = dfs.GetData(0, i);
      }

    }

    [TearDown]
    public void Destruct()
    {
      _dfs.Dispose();
      _simpleDfs.Dispose();
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
    }
  }
}
