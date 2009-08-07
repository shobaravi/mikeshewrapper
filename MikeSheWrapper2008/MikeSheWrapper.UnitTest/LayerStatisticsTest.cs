using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

using NUnit.Framework;
using MikeSheWrapper.LayerStatistics;

namespace MikeSheWrapper.UnitTest
{
  [TestFixture]
  public class LayerStatisticsTest
  {

    [Test]
    public void TotalTest()
    {
      Program.Main(new string[] { @"..\..\..\TestData\Layer Statistics\conf_mean.xml"});


      //Compare wells
      CompareFiles(@"..\..\..\TestData\Layer Statistics\Kopi af novomr1_pejl90-05_mean_sort1_wells.txt", @"..\..\..\TestData\Layer Statistics\novomr1_pejl90-05_mean_sort1_wells.txt");
      CompareFiles(@"..\..\..\TestData\Layer Statistics\Kopi af novomr1_pejl90-05_mean_sort1_layers.txt", @"..\..\..\TestData\Layer Statistics\novomr1_pejl90-05_mean_sort1_layers.txt");
      CompareFiles(@"..\..\..\TestData\Layer Statistics\Kopi af novomr1_pejl90-05_mean_sort1_me.txt", @"..\..\..\TestData\Layer Statistics\novomr1_pejl90-05_mean_sort1_me.txt");
      CompareFiles(@"..\..\..\TestData\Layer Statistics\Kopi af novomr1_pejl90-05_mean_sort1_rmse.txt", @"..\..\..\TestData\Layer Statistics\novomr1_pejl90-05_mean_sort1_rmse.txt");
      CompareFiles(@"..\..\..\TestData\Layer Statistics\Kopi af novomr1_pejl90-05_mean_sort1_observations.txt", @"..\..\..\TestData\Layer Statistics\novomr1_pejl90-05_mean_sort1_observations.txt");

    }


    private void CompareFiles(string filename1, string filename2)
    {
      string org;
      string newfile;
      using (StreamReader Sr = new StreamReader(filename1))
      {
        org = Sr.ReadToEnd();
      }
      using (StreamReader Sr = new StreamReader(filename2))
      {
        newfile = Sr.ReadToEnd();
      }
      Assert.AreEqual(org, newfile);

    }

    [Test]
    public void MultipleRunsTest()
    {

      for (int i = 0; i < 50; i++)
      {
        Program.Main(new string[] { @"..\..\..\TestData\Layer Statistics\conf_mean.xml" });
      }
    }

   


  }
}
