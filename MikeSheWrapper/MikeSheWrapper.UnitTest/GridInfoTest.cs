﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MikeSheWrapper;
using NUnit.Framework;

namespace MikeSheWrapper.UnitTest
{
  [TestFixture]
  public class GridInfoTest
  {

    [Test]
    public void GetIndexTest()
    {
      Model mshe = new Model(@"F:\Novana\Novomr4\Result\omr4_jag_UZ.SHE");

      int Column;
      int Row;

      Assert.IsTrue(mshe.GridInfo.GetIndex(518406,6148241,out Column, out Row));
      Assert.AreEqual(159, Column);
      Assert.AreEqual(153, Row);    

    }

    [Test]
    public void EqualsTest()
    {

      MikeSheGridInfo M1 = new MikeSheGridInfo(@"..\..\..\TestData\TestModel.she - Result Files\TestModel_PreProcessed_3DSZ.DFS3", @"..\..\..\TestData\TestModel.she - Result Files\TestModel_PreProcessed.DFS2");
      MikeSheGridInfo M2 = new MikeSheGridInfo(@"..\..\..\TestData\TestModel.she - Result Files\TestModel_PreProcessed_3DSZ.DFS3", @"..\..\..\TestData\TestModel.she - Result Files\TestModel_PreProcessed.DFS2");

      Assert.IsTrue(M1.Equals(M2));
      M1.Dispose();
      M2.Dispose();
    }

  }
}
