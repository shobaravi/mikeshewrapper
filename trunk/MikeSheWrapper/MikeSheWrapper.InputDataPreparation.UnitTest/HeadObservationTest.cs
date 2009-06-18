using System;
using System.IO;
using System.Xml.Serialization;
using System.Data;
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
      R.Waterlevels(Wells);

      List<IIntake> Intakes= new List<IIntake>();

      foreach (IWell W in Wells.Values)
        foreach(IIntake I in W.Intakes)
          Intakes.Add(I);

      Assert.AreEqual(747, Intakes.Count);

      foreach(IIntake I in Intakes)
      HeadObservations.WriteToDfs0(@"..\..\..\TestData\TidsSerier", I, new DateTime(2003, 1, 1), new DateTime(2009, 1, 1));


    }

    [Test]
    public void ReadExtractionsAndWrite()
    {
      JupiterTools.Reader R = new Reader(@"..\..\..\TestData\AlbertslundPcJupiter.mdb");
      Dictionary<string, IWell> Wells = R.WellsForNovana(false, false, false);
      var Plants = R.ReadPlants(Wells);
      R.FillInExtraction(Plants);

      HeadObservations.WriteExtractionDFS0(@"..\..\..\TestData\", Plants.Values.Where(var => var.Extractions.Count > 0), new DateTime(2000, 1, 1), new DateTime(2006, 1, 1));

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
    public void ReadInFromShapeTest()
    {
      PointShapeReader PSr = new PointShapeReader(@"..\..\..\TestData\AlbertslundExtraction.shp");
      DataTable DTWells = PSr.Data.Read();

      PSr.Dispose();

      PSr = new PointShapeReader(@"..\..\..\TestData\AlbertslundObservation.shp");
      DataTable DTWellsObs = PSr.Data.Read();

      PSr.Dispose();


      ShapeReaderConfiguration SRC2;
      XmlSerializer x = new XmlSerializer(typeof(ShapeReaderConfiguration));
      using (FileStream fs = new FileStream(@"..\..\..\ThirdpartyBinaries\ShapeReaderConfig.xml", FileMode.Open))
      {
        SRC2 = (ShapeReaderConfiguration)x.Deserialize(fs);
      }



      Dictionary<string, IWell> Wells1 = new Dictionary<string, IWell>();
        HeadObservations.FillInFromNovanaShape(DTWells.Select(""), SRC2, Wells1);
      Assert.AreEqual(242, Wells1.Count());

      Dictionary<string, IWell> Wells3 = new Dictionary<string, IWell>();
        HeadObservations.FillInFromNovanaShape(DTWellsObs.Select(""), SRC2, Wells3);
      Assert.AreEqual(308, Wells3.Count());

      SRC2.FraAArHeader="DUMMY";

      Dictionary<string, IWell> Wells2 = new Dictionary<string, IWell>();
        HeadObservations.FillInFromNovanaShape(DTWells.Select(""), SRC2, Wells2);




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
