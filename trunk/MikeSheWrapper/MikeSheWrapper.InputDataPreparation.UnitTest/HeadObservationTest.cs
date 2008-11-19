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
    


    [SetUp]
    public void ConstructTest()
    {
      HO.ReadFromShape(@"F:\Jacob\Pejlinger\novomr456_pejle_ks.shp");
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

      var query = HO.Wells.Values.Where(w => HO.NosInBetween(w, new DateTime(1990, 1, 1), new DateTime(2000, 1, 1), 10));



    }


    [Test]
    public void StatisticsFromDFS0Test()
    {
      HO.StatisticsFromDetailedTSOutput(@"F:\Novana\Novomr4\Result\omr4_jag_UZ_ts.SHE - Result Files\omr4_jag_UZ_tsDetailedTS_SZ.dfs0");

    }


  }
}
