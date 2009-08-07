using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MikeSheWrapper.Viewer;
using NUnit.Framework;


namespace MikeSheWrapper.UnitTest
{
  [TestFixture]
  public class DataSelectorTest
  {
    [Ignore]
    [Test]
    public void ShowTest()
    {

      DataTable DT = new DataTable();
      DT.Columns.Add("Tekst", typeof(string));

      DT.Rows.Add("Row1");
      DT.Rows.Add("Row1"); 
      DT.Rows.Add("Row3");

      string[] data = new string[] { "felt1", "felt2", "felt3" };


      DataSelector DS = new DataSelector(DT);

      DS.ShowDialog();

      

      Assert.AreEqual("\"felt1\"", DS.SelectString);
      

    }
  }
}
