using MikeSheWrapper.Tools.JupiterXLTableAdapters;

namespace MikeSheWrapper.Tools {
    
    
    public partial class JupiterXL {

      /// <summary>
      /// Reads in data required for the NOVANA project.
      /// </summary>
      /// <param name="DataBaseFileName"></param>
      public void ReadInNovanaWells(string DataBaseFileName)
      {
        string ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DataBaseFileName + ";Persist Security Info=False";

        //Read in boreholes through table adapter
        BOREHOLETableAdapter BTA = new BOREHOLETableAdapter();
        BTA.Connection.ConnectionString = ConnectionString;
        BTA.FillByNovana(BOREHOLE);

        //Read in Intakes through table adapter
        INTAKETableAdapter ITA = new INTAKETableAdapter();
        ITA.Connection.ConnectionString = ConnectionString;
        ITA.FillByNovana(INTAKE);

        //Read in Screens throug the table adapter
        SCREENTableAdapter STA = new SCREENTableAdapter();
        STA.Connection.ConnectionString = ConnectionString;
        STA.FillByNovana(SCREEN);
      }

      /// <summary>
      /// Reads in the waterlevels from the database using the FillByNovana method. 
      /// Only necessary fields are read.
      /// </summary>
      /// <param name="DataBaseFileName"></param>
      public void ReadWaterLevels(string DataBaseFileName)
      {
        string ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DataBaseFileName + ";Persist Security Info=False";
        WATLEVELTableAdapter WTA = new WATLEVELTableAdapter();
        WTA.Connection.ConnectionString = ConnectionString;
        WTA.FillByNovana(WATLEVEL);
      }


    }
}

namespace MikeSheWrapper.Tools.JupiterXLTableAdapters {
    
    
    public partial class BOREHOLETableAdapter {

    }
}
