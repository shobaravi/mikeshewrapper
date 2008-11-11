﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using MikeSheWrapper.Tools;

namespace MikeSheWrapper.Tools.UnitTest
{
  [TestFixture]
  public class HeadObservationTest
  {
    [Test]
    public void ReadAllAndWrite()
    {

      HeadObservations HO = new HeadObservations();


      HO.CreateDfs0FromShape(@"F:\Jacob\Pejlinger\novomr456_pejle_ks.shp");


      HO.WriteToDfs0( @"F:\Jacob\Pejlinger\TidsSerier");
    }


  }
}