using MikeSheWrapper.JupiterTools.JupiterXLTableAdapters;

namespace MikeSheWrapper.JupiterTools
{


  public partial class JupiterXL
  {

    private string ConnectionString;

    public JupiterXL(string DataBaseFileName)
    {
      ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + DataBaseFileName + ";Persist Security Info=False";

    }

    /// <summary>
    /// Reads in groundwater chemistry
    /// </summary>
    /// <param name="DataBaseFileName"></param>
    public void ReadInChemistrySamples()
    {
      GRWCHEMSAMPLETableAdapter GSA = new GRWCHEMSAMPLETableAdapter();
      GSA.Connection.ConnectionString = ConnectionString;
      GSA.Fill(GRWCHEMSAMPLE);

      COMPOUNDLISTTableAdapter CTA = new COMPOUNDLISTTableAdapter();
      CTA.Connection.ConnectionString = ConnectionString;
      CTA.Fill(COMPOUNDLIST);

      GRWCHEMANALYSISTableAdapter GTA = new GRWCHEMANALYSISTableAdapter();
      GTA.Connection.ConnectionString = ConnectionString;
      GTA.Fill(GRWCHEMANALYSIS);
    }
    
    /// <summary>
    /// Reads in data required for the NOVANA project.
    /// </summary>
    /// <param name="DataBaseFileName"></param>
    public void PartialReadOfWells()
    {

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

    public void ReadInTotalWellsForNovana()
    {
      //Read in boreholes through table adapter
      BOREHOLETableAdapter BTA = new BOREHOLETableAdapter();
      BTA.Connection.ConnectionString = ConnectionString;
      BTA.Fill(BOREHOLE);

      //Read in Intakes through table adapter
      INTAKETableAdapter ITA = new INTAKETableAdapter();
      ITA.Connection.ConnectionString = ConnectionString;
      ITA.Fill(INTAKE);

      //Read in Screens throug the table adapter
      SCREENTableAdapter STA = new SCREENTableAdapter();
      STA.Connection.ConnectionString = ConnectionString;
      STA.Fill(SCREEN);

      //Read in Casings through the table adapter
      CASINGTableAdapter CTA = new CASINGTableAdapter();
      CTA.Connection.ConnectionString = ConnectionString;
      CTA.Fill(CASING);

    }

    /// <summary>
    /// Read the data from the Lithsample table.
    /// </summary>
    /// <param name="DataBaseFileName"></param>
    public void ReadInLithology()
    {
      LITHSAMPTableAdapter LTA = new LITHSAMPTableAdapter();
      LTA.Connection.ConnectionString = ConnectionString;
      LTA.Fill(LITHSAMP);
    }

    /// <summary>
    /// Reads in the waterlevels from the database using the FillByNovana method. 
    /// Only necessary fields are read.
    /// </summary>
    /// <param name="DataBaseFileName"></param>
    public void ReadWaterLevels()
    {
      WATLEVELTableAdapter WTA = new WATLEVELTableAdapter();
      WTA.Connection.ConnectionString = ConnectionString;
      WTA.FillByNovana(WATLEVEL);
    }

    /// <summary>
    /// Read in plants and  related intakes  and extration
    /// Tables DRWPLANT, DRWPLANTINTAKE, WRRCATHCHMENT are filled.
    /// </summary>
    /// <param name="DataBaseFileName"></param>
    public void ReadExtractions()
    {
      DRWPLANTTableAdapter DTA = new DRWPLANTTableAdapter();
      DTA.Connection.ConnectionString = ConnectionString;
      DTA.Fill(DRWPLANT);

      DRWPLANTINTAKETableAdapter DTIA = new DRWPLANTINTAKETableAdapter();
      DTIA.Connection.ConnectionString = ConnectionString;
      DTIA.Fill(DRWPLANTINTAKE);

      WRRCATCHMENTTableAdapter WTA = new WRRCATCHMENTTableAdapter();
      WTA.Connection.ConnectionString = ConnectionString;
      WTA.Fill(WRRCATCHMENT);
    }
  }
}


