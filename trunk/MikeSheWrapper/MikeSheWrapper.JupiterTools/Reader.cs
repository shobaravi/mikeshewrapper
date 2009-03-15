using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MikeSheWrapper.Tools;

namespace MikeSheWrapper.JupiterTools
{
  public class Reader
  {

    private JupiterXL JXL;

    public Reader(string DataBaseFile)
    {
      JXL = new JupiterXL(DataBaseFile);
    }


    /// <summary>
    /// Read in water levels from a Jupiter access database. 
    /// Entries with blank dates of waterlevels are skipped.
    /// </summary>
    /// <param name="DataBaseFile"></param>
    /// <param name="CreateWells"></param>
    public void Waterlevels(Dictionary<string, Well> Wells)
    {
      JXL.ReadWaterLevels();

      foreach (var WatLev in JXL.WATLEVEL)
      {
        Well CurrentWell;

        //Find the well in the dictionary
        if (!Wells.TryGetValue(WatLev.BOREHOLENO, out CurrentWell))
        {
          if (CurrentWell.Intakes.Count>= WatLev.INTAKENO)
            FillInWaterLevel(CurrentWell.Intakes[WatLev.INTAKENO-1], WatLev);
        }
      }
    }

    /// <summary>
    /// Put the observation in the well
    /// </summary>
    /// <param name="CurrentWell"></param>
    /// <param name="WatLev"></param>
    private void FillInWaterLevel(Intake CurrentIntake, JupiterXL.WATLEVELRow WatLev)
    {
      if (!WatLev.IsTIMEOFMEASNull())
        if (!WatLev.IsWATLEVMSLNull())
          CurrentIntake.Observations.Add(new ObservationEntry(WatLev.TIMEOFMEAS, WatLev.WATLEVMSL));
    }

    public void Extraction(Dictionary<int, Plant> Plants, Dictionary<string, Well> Wells)
    {
      JXL.ReadExtractions();

      Well CurrentWell;
      Intake CurrentIntake;
      Plant CurrentPlant;

      foreach (var Anlaeg in JXL.DRWPLANT)
      {
        if (!Plants.TryGetValue(Anlaeg.PLANTID, out CurrentPlant))
        {
          CurrentPlant = new Plant(Anlaeg.PLANTID);
          Plants.Add(CurrentPlant.IDNumber, CurrentPlant);
        }

          CurrentPlant.Name = Anlaeg.PLANTNAME;

          CurrentPlant.Address = Anlaeg.PLANTADDRESS;

          CurrentPlant.PostalCode = Anlaeg.PLANTPOSTALCODE;

        if (!Anlaeg.IsPERMITDATENull())
          CurrentPlant.PermitDate = Anlaeg.PERMITDATE;

        if (!Anlaeg.IsPERMITEXPIREDATENull())
          CurrentPlant.PermitExpiryDate = Anlaeg.PERMITEXPIREDATE;

          CurrentPlant.Permit = Anlaeg.PERMITAMOUNT;

        //Loop the intakes
        foreach (var IntakeData in Anlaeg.GetDRWPLANTINTAKERows())
        {
          if (!Wells.TryGetValue(IntakeData.BOREHOLENO, out CurrentWell))
          {
            CurrentWell = new Well(IntakeData.BOREHOLENO);
            Wells.Add(IntakeData.BOREHOLENO, CurrentWell);
          }
          CurrentIntake = CurrentWell.Intakes.Find(var => var.IDNumber == IntakeData.INTAKENO);
          if (CurrentIntake ==null)
            CurrentIntake = new Intake(CurrentWell, IntakeData.INTAKENO);
          CurrentPlant.PumpingIntakes.Add(CurrentIntake);
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


    public void ReadLithology(string DataBaseFile, Dictionary<string, JupiterWell> Wells)
    {

    }

    /// <summary>
    /// Reads in all wells from a Jupiter database. 
    /// Only reads geographical information and location of Intakes and screen
    /// </summary>
    /// <param name="DataBaseFile"></param>
    public Dictionary<string, Well> Wells()
    {
      Dictionary<string, Well> Wells = new Dictionary<string, Well>();
      //Construct the data set
      JXL.PartialReadOfWells();

      Well CurrentWell;
      Intake CurrentIntake;

      //Loop the wells
      foreach (var Boring in JXL.BOREHOLE)
      {
        CurrentWell = new ObservationWell(Boring.BOREHOLENO);
        Wells.Add(Boring.BOREHOLENO, CurrentWell);

        if (!Boring.IsXUTMNull())
          CurrentWell.X = Boring.XUTM;
        if (!Boring.IsYUTMNull())
          CurrentWell.Y = Boring.YUTM;

        CurrentWell.Description = Boring.LOCATION;
        if (!Boring.IsELEVATIONNull())
          CurrentWell.Terrain = Boring.ELEVATION;

        //Loop the intakes
        foreach (var IntakeData in Boring.GetINTAKERows())
        {
          CurrentIntake = CurrentWell.Intakes.Find(var => var.IDNumber == IntakeData.INTAKENO);
          if (CurrentIntake == null)
            CurrentIntake = new Intake(CurrentWell, IntakeData.INTAKENO);


          //Loop the screens. One intake can in special cases have multiple screens
          foreach (var Screen in IntakeData.GetSCREENRows())
          {
            if (!Screen.IsTOPNull())
              CurrentIntake.ScreenTop.Add(Screen.TOP);
            if (!Screen.IsBOTTOMNull())
              CurrentIntake.ScreenBottom.Add(Screen.BOTTOM);
          }//Screen loop
        }//Intake loop
      }//Bore loop

      return Wells;
    }


    public Dictionary<string, JupiterWell> WellsForNovana()
    {
      Dictionary<string, JupiterWell> Wells = new Dictionary<string, JupiterWell>();
      //Construct the data set
      JXL.ReadInTotalWellsForNovana();
      JXL.ReadInLithology();

      NovanaTables.PejlingerTotalDataTable DT = new NovanaTables.PejlingerTotalDataTable();

      JupiterWell CurrentWell;
      NovanaTables.PejlingerTotalRow CurrentRow;

      //Loop the wells
      foreach (var Boring in JXL.BOREHOLE)
      {
        CurrentWell = new JupiterWell(Boring.BOREHOLENO);
        Wells.Add(Boring.BOREHOLENO, CurrentWell);
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
          CurrentWell.Terrain = Boring.ELEVATION;



        //Loop the intakes
        foreach (var Intake in Boring.GetINTAKERows())





          CurrentRow.NOVANAID = wellname;
          //          CurrentRow.KOORTYPE =
          CurrentRow.JUPKOTE = Boring.ELEVATION;
          CurrentRow.BOREHOLENO = Boring.BOREHOLENO;
          CurrentRow.INTAKENO = Intake.INTAKENO;
          //          CurrentRow.WATLEVELNO =
          CurrentRow.LOCATION = Boring.LOCATION;

          if (!Boring.IsDRILENDATENull())
            CurrentRow.DRILENDATE = Boring.DRILENDATE;

          if (!Boring.IsABANDONDATNull())
            CurrentRow.ABANDONDAT = Boring.ABANDONDAT;

          CurrentRow.ABANDCAUSE = Boring.ABANDCAUSE;
          CurrentRow.DRILLDEPTH = Boring.DRILLDEPTH;

          //Assumes that the string no from the intake identifies the correct Casing
          foreach (var Casing in Boring.GetCASINGRows())
          {
            if (!Intake.IsSTRINGNONull() & !Casing.IsSTRINGNONull())
              if (Intake.STRINGNO == Casing.STRINGNO & !Casing.IsBOTTOMNull())
                CurrentRow.CASIBOT = Casing.BOTTOM;
          }

            CurrentRow.PURPOSE = Boring.PURPOSE;
            CurrentRow.USE = Boring.USE;

          //Loop the screens. One intake can in special cases have multiple screens
          foreach (var Screen in Intake.GetSCREENRows())
          {
              CurrentWell.ScreenTop.Add(Screen.TOP);
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

          CurrentRow.RESROCK = Intake.RESERVOIRROCK;
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
          
          //Reads in chemistry
          foreach (var Chem in Boring.GetGRWCHEMSAMPLERows())
          {
            foreach (var analysis in Chem.GetGRWCHEMANALYSISRows())
            {
              ChemistrySample C = new ChemistrySample();
              C.SampleDate = Chem.SAMPLEDATE;
              C.CompoundNo = analysis.COMPOUNDNO;
              C.Amount = analysis.AMOUNT;
              C.Unit = analysis.UNIT;
            }
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
