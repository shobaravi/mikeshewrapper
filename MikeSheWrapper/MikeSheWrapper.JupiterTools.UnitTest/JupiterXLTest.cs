using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MikeSheWrapper.JupiterTools;
using NUnit.Framework;

namespace MikeSheWrapper.JupiterTools.UnitTest
{
  [TestFixture]
  public class JupiterXLTest
  {
    [Test]
    public void DobbletFillTest()
    {
      JupiterXL JXL = new JupiterXL(@"..\..\..\TestData\AlbertslundPcJupiter.mdb");
      JXL.ReadWells(true);

      Assert.IsTrue(JXL.BOREHOLE.First().IsUSENull());

      JXL.ReadWells(false);
      Assert.IsFalse(JXL.BOREHOLE.First().IsUSENull());
    }
  }
}
