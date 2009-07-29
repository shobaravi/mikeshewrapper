using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using MikeSheWrapper.DFS;
using MathNet.Numerics.LinearAlgebra;

namespace MikeSheWrapper.DFS.UnitTest
{
  [TestFixture]
  public class WriterTest
  {
    

    [Test]
    public void FirstTest()
    {

      DFS2 outdata = new DFS2(@"..\..\..\TestData\simpelmatrixKopi.dfs2");

      Matrix M = outdata.GetData(0, 1);
      Matrix M2;

      M[2, 2] = 2000;

      for (int i = 0; i < 10; i++)
      {
        outdata.SetData(i + 8, 2, M);
        M2 = outdata.GetData(i + 8, 2);
        Assert.IsTrue(M.Equals(M2));
      }

      DateTime d = new DateTime(1950, 1, 1);

      string dd = d.ToShortDateString();
      outdata.TimeOfFirstTimestep = new DateTime(1950, 1, 1);
      outdata.TimeStep = new TimeSpan(20, 0, 0, 0);
      
      outdata.Dispose();

    }
  }
}
