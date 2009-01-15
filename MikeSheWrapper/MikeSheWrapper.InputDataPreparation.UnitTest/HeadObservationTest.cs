using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using MikeSheWrapper;
using MikeSheWrapper.Tools;
using MikeSheWrapper.InputDataPreparation;

namespace MikeSheWrapper.InputDataPreparation.UnitTest
{
  [TestFixture]
  public class HeadObservationTest
  {
    HeadObservations HO = new HeadObservations();


    [Test]
    public void ReadFromJupiter()
    {
      HO.ReadWellsFromJupiter(@"F:\Jacob\Pejlinger\herning.mdb");
      HO.ReadWaterlevelsFromJupiterAccess(@"F:\Jacob\Pejlinger\herning.mdb", false);
      int NumberOfWells = HO.Wells.Values.Count(w => HO.NosInBetween(w, new DateTime(1990, 1, 1), new DateTime(2000, 1, 1), 10));

    }

    [Ignore]
    [SetUp]
    public void ConstructTest()
    {
     // HO.ReadFromShape(@"F:\Jacob\Pejlinger\novomr456_pejle_ks.shp");
    }

    [Test]
    public void ReadAllAndWrite()
    {
      HO.WriteToDfs0( @"F:\Jacob\Pejlinger\TidsSerier");
    }

    [Test]
    public void DomainAreaTest()
    {
      HO.SelectByMikeSheModelArea(new Model(@"F:\Novana\Novomr4\Result\omr4_jag_UZ.SHE").GridInfo);
      HO.WriteToMikeSheModel(@"F:\Jacob\Pejlinger\DetailedTsimport.txt");
    }

    [Test]
    public void SelectByFunction()
    {
      int NumberOfWells = HO.Wells.Values.Count(w => HO.NosInBetween(w, new DateTime(1990, 1, 1), new DateTime(2000, 1, 1), 10));

      foreach (ObservationWell W in HO.Wells.Values)
      {
        if (W.UniqueObservations.Count > 1)
          Console.WriteLine(W);
      }

      var query = HO.Wells.Values.Where(w => HO.NosInBetween(w, new DateTime(1990, 1, 1), new DateTime(2000, 1, 1), 10));


    }

    [Test]
    public void ReadInFromMsheModel()
    {
      Model M = new Model(@"..\..\..\TestData\TestModel.she");
      HO.ReadInDetailedTimeSeries(M);
      HO.GetSimulatedValuesFromDetailedTSOutput(@"..\..\..\TestData\TestModel.she - Result Files\TestModelDetailedTS_SZ.dfs0");

    }

    [Test]
    public void StatisticsFromDFS0Test()
    {
      HO.GetSimulatedValuesFromDetailedTSOutput(@"F:\Novana\Novomr4\Result\omr4_jag_UZ_ts.SHE - Result Files\omr4_jag_UZ_tsDetailedTS_SZ.dfs0");

    }


    [Test]
    public void SpecialNovanaPrint()
    {
      HO.ReadWellsForNovanaFromJupiter(@"D:\Udvikling\MikeSheWrapper\JupiterData\pcjupiter.mdb");
    }

  }
}
