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
    Reader R;

    [SetUp]
    public void ConstructTest()
    {
      R = new Reader(@"..\..\..\TestData\AlbertslundPcJupiter.mdb");
    }

    [TearDown]
    public void DisposeTest()
    {
      R.Dispose();
    }

    public void DobbeltFillTest()
    {
      
    }

    

    [Test]
    public void TableMergeTest()
    {
      NovanaTables.IntakeCommonDataTable DT = new NovanaTables.IntakeCommonDataTable();
      NovanaTables.PejlingerDataTable DT2 = new NovanaTables.PejlingerDataTable();


      NovanaTables.IntakeCommonRow dr = DT.NewIntakeCommonRow();
      dr.NOVANAID = "boring 1";
      dr.JUPKOTE = 10;

      DT.Rows.Add(dr);

      NovanaTables.PejlingerRow dr1 = DT2.NewPejlingerRow();
      dr1.NOVANAID= "boring2";
      DT2.Rows.Add(dr1);
      NovanaTables.PejlingerRow dr2 = DT2.NewPejlingerRow();
      dr2.NOVANAID = "boring 1";
      DT2.Rows.Add(dr2);

      int n = dr.Table.Columns.Count;
      DT.Merge(DT2);
      n = dr.Table.Columns.Count;
      
     dr["AKTDAGE"] = 2;



    }



    [Test]
    public void ReadExtractionsTest()
    {
      Dictionary<string, IWell> Wells = new Dictionary<string, IWell>();

      var Anlaeg = R.Extraction(Wells);

      Assert.AreEqual(4, Anlaeg.Count(x => x.PumpingIntakes.Count == 0));

      R.AddDataForNovanaExtraction(Anlaeg,DateTime.MinValue, DateTime.MaxValue);

    }

    [Ignore]
    [Test]
    public void ReadRibeTest()
    {
      
      Dictionary<string, IWell> Wells = new Dictionary<string, IWell>();
      Reader R = new Reader(@"..\..\..\mcribe.mdb");
      var Anlaeg = R.Extraction(Wells);


    }

    [Test]
    public void WellsForNovanaTest()
    {

      Dictionary<string, IWell> Wells = R.WellsForNovana(true, true, true);
      List<JupiterIntake> Intakes = new List<JupiterIntake>();

      foreach (IWell w in Wells.Values)
      {
        foreach (JupiterIntake JI in w.Intakes)
          Intakes.Add(JI);
      }
      R.AddDataForNovanaPejl(Intakes);

    }
  }
}