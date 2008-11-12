using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using MikeSheWrapper.Tools;
using MikeSheWrapper.InputDataPreparation;

namespace MikeSheWrapper.Tools.UnitTest
{
  [TestFixture]
  public class HeadObservationTest
  {
    [Test]
    public void ReadAllAndWrite()
    {

      HeadObservations HO = new HeadObservations();


      HO.ReadFromShape(@"F:\Jacob\Pejlinger\novomr456_pejle_ks.shp");


      HO.WriteToDfs0( @"F:\Jacob\Pejlinger\TidsSerier");
    }


  }
}
