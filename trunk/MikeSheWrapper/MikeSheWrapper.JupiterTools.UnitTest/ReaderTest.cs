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
    }



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