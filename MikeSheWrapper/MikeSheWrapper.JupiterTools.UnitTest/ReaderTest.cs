using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using MikeSheWrapper.Tools;
using MikeSheWrapper.JupiterTools;

namespace MikeSheWrapper.JupiterTools.UnitTest
{
  [TestFixture]
  public class ReaderTest
  {

    [Test]
    public void ReadExtractionsTest()
    {
      Dictionary<int, Plant> Anlaeg = new Dictionary<int, Plant>();
      Dictionary<string, Well> Wells = new Dictionary<string, Well>();
      Reader.Extraction(@"..\..\..\TestData\AlbertslundPcJupiter.mdb", Anlaeg, Wells);

      Assert.AreEqual(4,Anlaeg.Values.Count(x=>x.PumpingWells.Count==0));

    }
  }
}
