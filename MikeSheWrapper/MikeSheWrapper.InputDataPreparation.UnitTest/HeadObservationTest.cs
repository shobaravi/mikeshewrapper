﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using MikeSheWrapper;
using MikeSheWrapper.Tools;
using MikeSheWrapper.JupiterTools;
using MikeSheWrapper.InputDataPreparation;


namespace MikeSheWrapper.InputDataPreparation.UnitTest
{
  [TestFixture]
  public class HeadObservationTest
  {
    HeadObservations HO = new HeadObservations();

    [Test]
    public void ReadAllAndWriteDFS0()
    {
      JupiterTools.Reader.Wells(@"..\..\..\TestData\AlbertslundPcJupiter.mdb", HO.Wells);
      JupiterTools.Reader.Waterlevels(@"..\..\..\TestData\AlbertslundPcJupiter.mdb", false, HO.Wells);

      Assert.AreEqual(747, HO.Wells.Count);

      HO.WriteToDfs0(@"..\..\..\TestData\TidsSerier", HO.WorkingList, new DateTime(2003, 1, 1), new DateTime(2009, 1, 1));


    }

    [Test]
    public void SelectByMikeSheModelAreaTest()
    {
      HO.Wells.Add("well1", new ObservationWell("well1", 10000, 10000));
      HO.Wells.Add("well2", new ObservationWell("well2", 250, 250));
      HO.Wells.Add("well3", new ObservationWell("well3", 300, 300));

      Model M = new Model(@"..\..\..\TestData\TestModel.she");

      HO.SelectByMikeSheModelArea(M.GridInfo);

      Assert.AreEqual(2, HO.WorkingList.Count);
      Assert.AreEqual("well2", HO.WorkingList[0].ID);

      M.Dispose();
    }



    [Test]
    public void ReadInFromMsheModel()
    {
      Model M = new Model(@"..\..\..\TestData\TestModel.she");
      HO.ReadInDetailedTimeSeries(M);
      HO.GetSimulatedValuesFromDetailedTSOutput(@"..\..\..\TestData\TestModel.she - Result Files\TestModelDetailedTS_SZ.dfs0");
      Assert.AreEqual(2, HO.WorkingList.Count);
      M.Dispose();
    }

  }
}
