using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using MikeSheWrapper;
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
      HO.SelectByMikeSheModelArea(new Model(@"F:\DHI\Data\Novana\Novomr4\Result\omr4_jag_UZ.SHE"));
      
    }




  }
}
