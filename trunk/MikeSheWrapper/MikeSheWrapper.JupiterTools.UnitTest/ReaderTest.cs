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
      List<Plant> Anlaeg = new List<Plant>();
      Dictionary<string, IWell> Wells = new Dictionary<string, IWell>();
      Reader R = new Reader(@"..\..\..\TestData\AlbertslundPcJupiter.mdb");

      R.Extraction(Anlaeg, Wells);

      Assert.AreEqual(4, Anlaeg.Count(x => x.PumpingIntakes.Count == 0));

    }

    [Test]
    public void ReadRibeTest()
    {
      List<Plant> Anlaeg = new List<Plant>();
      Dictionary<string, IWell> Wells = new Dictionary<string, IWell>();
      Reader R = new Reader(@"..\..\..\mcribe.mdb");
      R.Extraction(Anlaeg, Wells);

    }

    [Test]
    public void WellsForNovanaTest()
    {


      Reader R = new Reader(@"..\..\..\TestData\AlbertslundPcJupiter.mdb");
            Dictionary<string, IWell> Wells = R.WellsForNovana();

    }
  }
}