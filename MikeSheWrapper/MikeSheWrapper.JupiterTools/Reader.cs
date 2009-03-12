using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MikeSheWrapper.Tools;

namespace MikeSheWrapper.JupiterTools
{
  public class Reader
  {

    /// <summary>
    /// Read in water levels from a Jupiter access database. 
    /// If CreateWells is true a new well is created if it does not exist in the list.
    /// Entries with blank dates of waterlevels are skipped.
    /// </summary>
    /// <param name="DataBaseFile"></param>
    /// <param name="CreateWells"></param>
    public static void Waterlevels(string DataBaseFile, bool CreateWells, Dictionary<string, ObservationWell> Wells)
    {
      JupiterXL JXL = new JupiterXL();

      JXL.ReadWaterLevels(DataBaseFile);

      foreach (var WatLev in JXL.WATLEVEL)
      {
        ObservationWell CurrentWell;

        //Builds the unique well ID
        string well = WatLev.BOREHOLENO.Replace(" ", "") + "_" + WatLev.INTAKENO;

        //Find the well in the dictionary
        if (!Wells.TryGetValue(well, out CurrentWell))
        {
          //Create the well if not found
          if (CreateWells)
          {
            CurrentWell = new ObservationWell(well);
            Wells.Add(well, CurrentWell);
          }
        }
        //If the well has been found or is created fill in the observations
        if (CurrentWell != null)
          FillInWaterLevel(CurrentWell, WatLev);

      }
    }

    /// <summary>
    /// Put the observation in the well
    /// </summary>
    /// <param name="CurrentWell"></param>
    /// <param name="WatLev"></param>
    private static void FillInWaterLevel(ObservationWell CurrentWell, JupiterXL.WATLEVELRow WatLev)
    {
      if (!WatLev.IsTIMEOFMEASNull())
        if (!WatLev.IsWATLEVMSLNull())
          CurrentWell.Observations.Add(new ObservationEntry(WatLev.TIMEOFMEAS, WatLev.WATLEVMSL));
    }

    public static void Extraction(string DataBaseFile, Dictionary<int, Plant> Plants, Dictionary<string, Well> Wells)
    {
      JupiterXL JXL = new JupiterXL();
      JXL.ReadExtractions(DataBaseFile);

      Well CurrentWell;
      Plant CurrentPlant;

      foreach (var Anlaeg in JXL.DRWPLANT)
      {
        if (!Plants.TryGetValue(Anlaeg.PLANTID, out CurrentPlant))
        {
          CurrentPlant = new Plant(Anlaeg.PLANTID);
          Plants.Add(CurrentPlant.IDNumber, CurrentPlant);
        }

        if (!Anlaeg.IsPLANTNAMENull())
          CurrentPlant.Name = Anlaeg.PLANTNAME;

        if (!Anlaeg.IsPLANTADDRESSNull())
          CurrentPlant.Address = Anlaeg.PLANTADDRESS;

        if (!Anlaeg.IsPLANTPOSTALCODENull())
          CurrentPlant.PostalCode = Anlaeg.PLANTPOSTALCODE;

        if (!Anlaeg.IsPERMITDATENull())
          CurrentPlant.PermitDate = Anlaeg.PERMITDATE;

        if (!Anlaeg.IsPERMITEXPIREDATENull())
          CurrentPlant.PermitExpiryDate = Anlaeg.PERMITEXPIREDATE;

        if (!Anlaeg.IsPERMITAMOUNTNull())
          CurrentPlant.Permit = Anlaeg.PERMITAMOUNT;

        //Loop the intakes
        foreach (var Intake in Anlaeg.GetDRWPLANTINTAKERows())
        {
          string NovanaID = Intake.BOREHOLENO.Replace(" ", "") + "_" + Intake.INTAKENO;
          if (!Wells.TryGetValue(NovanaID, out CurrentWell))
          {
            CurrentWell = new Well(NovanaID);
            Wells.Add(NovanaID, CurrentWell);
          }
          CurrentPlant.PumpingWells.Add(CurrentWell);
        }

        //Loop the extractions
        foreach (var Ext in Anlaeg.GetWRRCATCHMENTRows())
        {
          if (!Ext.IsAMOUNTNull())
            CurrentPlant.Extractions.Add(new TimeSeriesEntry(Ext.STARTDATE, Ext.AMOUNT));
          if (!Ext.IsSURFACEWATERVOLUMENull())
            CurrentPlant.SurfaceWaterExtrations.Add(new TimeSeriesEntry(Ext.STARTDATE, Ext.SURFACEWATERVOLUME));
        }
      }
    }


    public static void ReadLithology(string DataBaseFile, Dictionary<string, JupiterWell> Wells)
    {

    }

    /// <summary>
    /// Reads in all wells from a Jupiter database. 
    /// </summary>
    /// <param name="DataBaseFile"></param>
    public static Dictionary<string, ObservationWell> Wells(string DataBaseFile)
    {
      Dictionary<string, ObservationWell> Wells = new Dictionary<string, ObservationWell>();
      //Construct the data set
      JupiterXL JXL = new JupiterXL();
      JXL.PartialReadOfWells(DataBaseFile);

      ObservationWell CurrentWell;
      //Loop the wells
      foreach (var Boring in JXL.BOREHOLE)
      {
        //Loop the intakes
        foreach (var Intake in Boring.GetINTAKERows())
        {
          //Remove spaces and add the intake number to create a unique well ID
          string wellname = Boring.BOREHOLENO.Replace(" ", "") + "_" + Intake.INTAKENO;

          if (!Wells.TryGetValue(wellname, out CurrentWell))
          {
            CurrentWell = new ObservationWell(wellname);
            Wells.Add(wellname, CurrentWell);
          }

          if (!Boring.IsXUTMNull())
            CurrentWell.X = Boring.XUTM;
          if (!Boring.IsYUTMNull())
            CurrentWell.Y = Boring.YUTM;

          CurrentWell.Description = Boring.LOCATION;
          if (!Boring.IsELEVATIONNull())
            CurrentWell.Terrain = Boring.ELEVATION;

          //Loop the screens. One intake can in special cases have multiple screens
          foreach (var Screen in Intake.GetSCREENRows())
          {
            if (!Screen.IsTOPNull())
              CurrentWell.ScreenTop.Add(Screen.TOP);
            if (!Screen.IsBOTTOMNull())
              CurrentWell.ScreenBottom.Add(Screen.BOTTOM);
          }//Screen loop
        }//Intake loop
      }//Bore loop

      return Wells;
    }


    public static Dictionary<string, JupiterWell> WellsForNovana(string DataBaseFile)
    {
      Dictionary<string, JupiterWell> Wells = new Dictionary<string, JupiterWell>();
      //Construct the data set
      JupiterXL JXL = new JupiterXL();
      JXL.ReadInTotalWellsForNovana(DataBaseFile);
      JXL.ReadInLithology(DataBaseFile);

      NovanaTables.PejlingerTotalDataTable DT = new NovanaTables.PejlingerTotalDataTable();

      JupiterWell CurrentWell;
      NovanaTables.PejlingerTotalRow CurrentRow;

      

      //Loop the wells
      foreach (var Boring in JXL.BOREHOLE)
      {
        //Loop the intakes
        foreach (var Intake in Boring.GetINTAKERows())
        {
          //Remove spaces and add the intake number to create a unique well ID
          string wellname = Boring.BOREHOLENO.Replace(" ", "") + "_" + Intake.INTAKENO;

          if (!Wells.TryGetValue(wellname, out CurrentWell))
          {
            CurrentWell = new JupiterWell(wellname);
            CurrentWell.Data = DT.NewPejlingerTotalRow();
            DT.Rows.Add(CurrentWell.Data);
            Wells.Add(wellname, CurrentWell);
          }

          CurrentRow = (NovanaTables.PejlingerTotalRow)CurrentWell.Data;

          if (!Boring.IsXUTMNull())
          {
            CurrentWell.X = Boring.XUTM;
            CurrentRow.XUTM = Boring.XUTM;
          }
          else //If no x set x to 0!
          {
            CurrentWell.X = 0;
            CurrentRow.XUTM = 0;
          }

          if (!Boring.IsYUTMNull())
          {
            CurrentWell.Y = Boring.YUTM;
            CurrentRow.YUTM = Boring.YUTM;
          }
          else
          {
            CurrentWell.Y = 0;
            CurrentRow.YUTM = 0;
          }

          CurrentWell.Description = Boring.LOCATION;
          if (!Boring.IsELEVATIONNull())
            CurrentWell.Terrain = Boring.ELEVATION;

          CurrentRow.NOVANAID = wellname;
          //          CurrentRow.BORID = 
          //          CurrentRow.KOORTYPE =
          CurrentRow.JUPKOTE = Boring.ELEVATION;
          CurrentRow.BOREHOLENO = Boring.BOREHOLENO;
          CurrentRow.INTAKENO = Intake.INTAKENO;
          //          CurrentRow.WATLEVELNO =
          CurrentRow.LOCATION = Boring.LOCATION;
          //          CurrentRow.BOTROCK 

          if (!Boring.IsDRILENDATENull())
            CurrentRow.DRILENDATE = Boring.DRILENDATE;

          if (!Boring.IsABANDONDATNull())
            CurrentRow.ABANDONDAT = Boring.ABANDONDAT;
          if (!Boring.IsABANDCAUSENull())
            CurrentRow.ABANDCAUSE = Boring.ABANDCAUSE;
          if (!Boring.IsDRILLDEPTHNull())
            CurrentRow.DRILLDEPTH = Boring.DRILLDEPTH;

          //Assumes that the string no from the intake identifies the correct Casing
          foreach (var Casing in Boring.GetCASINGRows())
          {
            if (!Intake.IsSTRINGNONull() & !Casing.IsSTRINGNONull())
              if (Intake.STRINGNO == Casing.STRINGNO & !Casing.IsBOTTOMNull())
                CurrentRow.CASIBOT = Casing.BOTTOM;
          }

          if (!Boring.IsPURPOSENull())
            CurrentRow.PURPOSE = Boring.PURPOSE;
          if (!Boring.IsUSENull())
            CurrentRow.USE = Boring.USE;

          //Loop the screens. One intake can in special cases have multiple screens
          foreach (var Screen in Intake.GetSCREENRows())
          {
            if (!Screen.IsTOPNull())
              CurrentWell.ScreenTop.Add(Screen.TOP);
            if (!Screen.IsBOTTOMNull())
              CurrentWell.ScreenBottom.Add(Screen.BOTTOM);
          }//Screen loop

          if (CurrentWell.ScreenTop.Count > 0)
            CurrentRow.INTAKETOP = CurrentWell.ScreenTop.Min();
          if (CurrentWell.ScreenBottom.Count > 0)
            CurrentRow.INTAKEBOT = CurrentWell.ScreenBottom.Max();

          //Takes the minimum of all non-null dates
          IEnumerable<JupiterXL.SCREENRow> NonNullList = Intake.GetSCREENRows().Where(x => !x.IsSTARTDATENull());
          if (NonNullList.Count() > 0)
            CurrentRow.INTSTDATE2 = NonNullList.Min(x => x.STARTDATE);

          //Takes the maximum of all non-null dates
          NonNullList = Intake.GetSCREENRows().Where(x => !x.IsENDDATENull());
          if (NonNullList.Count() > 0)
            CurrentRow.INTENDATE2 = NonNullList.Max(x => x.ENDDATE);

          //          CurrentRow.RESROCK=
          //Fra WatLevel          CurrentRow.REFPOINT = 
          CurrentRow.ANTINT_B = Boring.GetINTAKERows().Count();

          //Read in the water levels
          foreach (var WatLev in Intake.GetWATLEVELRows())
          {
            FillInWaterLevel(CurrentWell, WatLev);
          }

          //Create statistics on water levels
          CurrentRow.ANTPEJ = CurrentWell.Observations.Count;
          if (CurrentRow.ANTPEJ > 0)
          {
            CurrentRow.MINDATO = CurrentWell.Observations.Min(x => x.Time);
            CurrentRow.MAXDATO = CurrentWell.Observations.Max(x => x.Time);
            CurrentRow.AKTAAR = CurrentRow.MAXDATO.Year - CurrentRow.MINDATO.Year + 1;
            CurrentRow.AKTDAGE = CurrentRow.MAXDATO.Subtract(CurrentRow.MINDATO).Days + 1;
            CurrentRow.PEJPRAAR = CurrentRow.ANTPEJ / CurrentRow.AKTAAR;
            CurrentRow.MAXPEJ = CurrentWell.Observations.Max(num => num.Value);
            CurrentRow.MINPEJ = CurrentWell.Observations.Min(num => num.Value);
            CurrentRow.MEANPEJ = CurrentWell.Observations.Average(num => num.Value);
          }

          //Loop the lithology
          foreach (var Lith in Boring.GetLITHSAMPRows())
          {
            Lithology L = new Lithology();
            L.Bottom = Lith.BOTTOM;
            L.Top = Lith.TOP;
            L.RockSymbol = Lith.ROCKSYMBOL;
            L.RockType = Lith.ROCKTYPE;
            L.TotalDescription = Lith.TOTALDESCR;
            CurrentWell.LithSamples.Add(L);
          }

          if (CurrentWell.LithSamples.Count != 0)
          {
            CurrentWell.LithSamples.Sort();
            CurrentRow.BOTROCK = CurrentWell.LithSamples[CurrentWell.LithSamples.Count - 1].RockSymbol;
          }
          else
            CurrentRow.BOTROCK = "999";

        }//Intake loop

      }//Bore loop

      return Wells;
    }

  }
}
