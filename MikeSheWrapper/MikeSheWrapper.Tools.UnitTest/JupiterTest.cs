using System;
using System.Data;
using System.Data.OleDb;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MikeSheWrapper.Tools;
using NUnit.Framework;


namespace MikeSheWrapper.Tools
{
  [TestFixture]
  public class JupiterTest
  {
    [Test]
    public void ConstructorTest()
    {
      JupiterXL jxl = new JupiterXL();
      JupiterXLTableAdapters.BOREHOLETableAdapter ts = new MikeSheWrapper.Tools.JupiterXLTableAdapters.BOREHOLETableAdapter();
      DateTime start = DateTime.Now;

      ts.FillByNovana(jxl.BOREHOLE);


      foreach (var m in jxl.BOREHOLE)
        Console.WriteLine(m.BOREHOLENO);

      TimeSpan l1 = start.Subtract(DateTime.Now);

      start = DateTime.Now;
      DataTable DT = new DataTable();


      string DataBaseFile = @"F:\Jacob\Pejlinger\herning.mdb";

      string databaseConnection = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DataBaseFile + ";Persist Security Info=False";

      OleDbConnection dbconnection = new OleDbConnection(databaseConnection);
      dbconnection.Open();

      //string select = "SELECT     BOREHOLENO, DRILLDEPTH, ELEVATION,  LOCATION, COMMENTS, XUTM, YUTM FROM borehole";
      string select = "SELECT * FROM borehole";

      OleDbDataAdapter da = new OleDbDataAdapter(select, databaseConnection);

      da.Fill(DT);
      TimeSpan l2 = start.Subtract(DateTime.Now);

      select = "SELECT boreholeno, intakeno, screenno FROM screen";
      da = new OleDbDataAdapter(select, databaseConnection);
      da.Fill(jxl.SCREEN);

      select = "SELECT boreholeno, intakeno FROM intake";
      da = new OleDbDataAdapter(select, databaseConnection);
      da.Fill(jxl.INTAKE);


      for (int i = 0; i < jxl.BOREHOLE.Rows.Count;i++ )
      {
        for (int j= 0;j<jxl.BOREHOLE[i].GetINTAKERows().Length;j++)
          for (int k = 0; k < jxl.BOREHOLE[i].GetINTAKERows()[j].GetSCREENRows().Length; k++)
            if (j>0)
             Console.WriteLine(jxl.BOREHOLE[i].GetINTAKERows()[j].GetSCREENRows()[k].BOREHOLENO);
      }
    }
  }
}
