using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using MikeSheWrapper.DFS;

namespace MikeSheWrapper.DFS.UnitTest
{
  [TestFixture]
  public class WriterTest
  {
    [Test]
    public void FirstTest()
    {

      DFSWriter outdata = new DFSWriter(@"..\..\..\TestData\simpelmatrixKopi.dfs2");

      float[] data = new float[21];

      for (int i = 0; i < data.Length; i++)
        data[i] = 10;

      outdata.WriteItemTimeStep(0, 1, data);
      outdata.Dispose();


    }
  }
}
