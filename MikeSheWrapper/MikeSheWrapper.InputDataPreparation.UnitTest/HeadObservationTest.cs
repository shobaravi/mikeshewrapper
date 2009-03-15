using System;
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
      JupiterTools.Reader R = new Reader(@"..\..\..\TestData\AlbertslundPcJupiter.mdb");
      Dictionary<string, ObservationWell> Wells = R.Wells();
      R.Waterlevels(Wells);

      Assert.AreEqual(747, Wells.Count);

      HO.WriteToDfs0(@"..\..\..\TestData\TidsSerier", Wells.Values, new DateTime(2003, 1, 1), new DateTime(2009, 1, 1));


    }

    [Test]
    public void SelectByMikeSheModelAreaTest()
    {
      HO.Wells.Add("well1", new ObservationWell("well1", 10000, 10000));
      HO.Wells.Add("well2", new ObservationWell("well2", 250, 250));
      HO.Wells.Add("well3", new ObservationWell("well3", 300, 300));

      Model M = new Model(@"..\..\..\TestData\TestModel.she");

      var SelectedWells = HeadObservations.SelectByMikeSheModelArea(M.GridInfo, HO.Wells);

      Assert.AreEqual(2, SelectedWells.Count());


      M.Dispose();
    }



    [Test]
    public void ReadInFromMsheModel()
    {
      HO.Wells.Clear();
      Model M = new Model(@"..\..\..\TestData\TestModel.she");
      HO.ReadInDetailedTimeSeries(M);
      HO.GetSimulatedValuesFromDetailedTSOutput(@"..\..\..\TestData\TestModel.she - Result Files\TestModelDetailedTS_SZ.dfs0");
      Assert.AreEqual(2, HO.WorkingList.Count);
      M.Dispose();
    }

  }
}
