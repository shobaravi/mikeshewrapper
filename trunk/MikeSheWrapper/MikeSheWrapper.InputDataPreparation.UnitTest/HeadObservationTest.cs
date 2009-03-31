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

    [Test]
    public void ReadAllAndWriteDFS0()
    {
      JupiterTools.Reader R = new Reader(@"..\..\..\TestData\AlbertslundPcJupiter.mdb");
      Dictionary<string, IWell> Wells = R.Wells();
      R.Waterlevels(Wells, false);

      List<IIntake> Intakes= new List<IIntake>();

      foreach (IWell W in Wells.Values)
        foreach(IIntake I in W.Intakes)
          Intakes.Add(I);

      Assert.AreEqual(747, Intakes.Count);

      HeadObservations.WriteToDfs0(@"..\..\..\TestData\TidsSerier", Intakes, new DateTime(2003, 1, 1), new DateTime(2009, 1, 1));


    }

    [Test]
    public void ReadExtractionsAndWrite()
    {
      JupiterTools.Reader R = new Reader(@"..\..\..\TestData\AlbertslundPcJupiter.mdb");
      Dictionary<string, IWell> Wells = R.WellsForNovana(false, false, false);
      var Plants = R.Extraction(Wells).Where(var => var.Extractions.Count>0);



      HeadObservations.WriteExtractionDFS0(@"..\..\..\TestData\", Plants , new DateTime(2000, 1, 1), new DateTime(2006, 1, 1));

    }

    [Test]
    public void SelectByMikeSheModelAreaTest()
    {
      List<MikeSheWell> Wells = new List<MikeSheWell>();
      Wells.Add(new MikeSheWell("well1", 10000, 10000));
      Wells.Add(new MikeSheWell("well2", 250, 250));
      Wells.Add(new MikeSheWell("well3", 300, 300));

      Model M = new Model(@"..\..\..\TestData\TestModel.she");

      var SelectedWells = HeadObservations.SelectByMikeSheModelArea(M.GridInfo, Wells);

      Assert.AreEqual(2, SelectedWells.Count());


      M.Dispose();
    }



    [Test]
    public void ReadInFromMsheModel()
    {
      List<IIntake> Intakes = new List<IIntake>();
      Model M = new Model(@"..\..\..\TestData\TestModel.she");
      foreach (IWell IW in HeadObservations.ReadInDetailedTimeSeries(M))
        foreach (IIntake I in IW.Intakes)
          Intakes.Add(I);

      HeadObservations.GetSimulatedValuesFromDetailedTSOutput(@"..\..\..\TestData\TestModel.she - Result Files\TestModelDetailedTS_SZ.dfs0", Intakes);
      Assert.AreEqual(2, Intakes.Count);
      M.Dispose();
    }

  }
}
