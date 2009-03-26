using System;
using System.Data;
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
    /// Disposes the dataset and closes the connection to the database.
    /// </summary>
    public void Dispose()
    {
      JXL.Dispose();
    }

    /// <summary>
    /// Read in water levels from a Jupiter access database. 
    /// Entries with blank dates of waterlevels are skipped.
    /// </summary>
    /// <param name="DataBaseFile"></param>
    /// <param name="CreateWells"></param>
    public void Waterlevels(Dictionary<string, IWell> Wells)
    {
      JXL.ReadWaterLevels();

      foreach (var WatLev in JXL.WATLEVEL)
      {
        IWell CurrentWell;

        //Find the well in the dictionary
        if (!Wells.TryGetValue(WatLev.BOREHOLENO, out CurrentWell))
        {
          IIntake I = CurrentWell.Intakes.First(var => var.IDNumber == WatLev.INTAKENO);
          if(I!=null)
            FillInWaterLevel(I, WatLev);
        }
      }
    }

    /// <summary>
    /// Put the observation in the well
    /// </summary>
    /// <param name="CurrentWell"></param>
    /// <param name="WatLev"></param>
    private void FillInWaterLevel(IIntake CurrentIntake, JupiterXL.WATLEVELRow WatLev)
    {
      if (!WatLev.IsTIMEOFMEASNull())
        if (!WatLev.IsWATLEVMSLNull())
          CurrentIntake.Observations.Add(new ObservationEntry(WatLev.TIMEOFMEAS, WatLev.WATLEVMSL));
    }


    /// <summary>
    /// Read Extractions
    /// </summary>
    /// <param name="Plants"></param>
    /// <param name="Wells"></param>
    public IEnumerable<Plant> Extraction(Dictionary<string, IWell> Wells)
    {
      List<Plant> Plants = new List<Plant>();

      JXL.ReadExtractions();


      IWell CurrentWell;
      IIntake CurrentIntake;
      Plant CurrentPlant;

      foreach (var Anlaeg in JXL.DRWPLANT)
      {
          CurrentPlant = new Plant(Anlaeg.PLANTID);
          Plants.Add(CurrentPlant);

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

          CurrentIntake = CurrentWell.Intakes.FirstOrDefault(var => var.IDNumber == IntakeData.INTAKENO);
          if (CurrentIntake ==null)
            CurrentIntake = CurrentWell.AddNewIntake(IntakeData.INTAKENO);
          
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
      return Plants;
    }


    /// <summary>
    /// Reads in all wells from a Jupiter database. 
    /// Only reads geographical information and location of Intakes and screen
    /// </summary>
    /// <param name="DataBaseFile"></param>
    public Dictionary<string, IWell> Wells()
    {
      Dictionary<string, IWell> Wells = new Dictionary<string, IWell>();
      //Construct the data set
      JXL.ReadWells(true);

      Well CurrentWell;
      IIntake CurrentIntake;

      //Loop the wells
      foreach (var Boring in JXL.BOREHOLE)
      {
        CurrentWell = new Well(Boring.BOREHOLENO);
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
          CurrentIntake = CurrentWell.Intakes.FirstOrDefault(var => var.IDNumber == IntakeData.INTAKENO);
          if (CurrentIntake == null)
            CurrentIntake = CurrentWell.AddNewIntake(IntakeData.INTAKENO);


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

    /// <summary>
    /// Fills the data row with entries common for Intake and Extractions.
    /// </summary>
    /// <param name="CurrentIntake"></param>
    private void AddCommonDataForNovana(JupiterIntake CurrentIntake)
    {
      JupiterWell CurrentWell;

      NovanaTables.IntakeCommonRow CurrentRow = (NovanaTables.IntakeCommonRow)CurrentIntake.Data;

      CurrentWell = (JupiterWell)CurrentIntake.well;

      CurrentRow.NOVANAID = CurrentWell.ID.Replace(" ", "") + "_" + CurrentIntake.ToString();

      CurrentRow.XUTM = CurrentWell.X;
      CurrentRow.YUTM = CurrentWell.Y;

      //Make sure all the necessary data have been read.
      if (JXL.ReducedRead)
        JXL.ReadWells(false);
      if (JXL.LITHSAMP.Count == 0)
        JXL.ReadInLithology();

      var BoringsData = JXL.BOREHOLE.FindByBOREHOLENO(CurrentWell.ID);
      var IntakeData = BoringsData.GetINTAKERows().First(var => var.INTAKENO == CurrentIntake.IDNumber);

      CurrentRow.JUPKOTE = BoringsData.ELEVATION;
      CurrentRow.BOREHOLENO = BoringsData.BOREHOLENO;
      CurrentRow.INTAKENO = CurrentIntake.IDNumber;
      CurrentRow.LOCATION = BoringsData.LOCATION;

      CurrentRow.ANTINT_B = CurrentWell.Intakes.Count();


      if (!BoringsData.IsDRILENDATENull())
        CurrentRow.DRILENDATE = BoringsData.DRILENDATE;

      if (!BoringsData.IsABANDONDATNull())
        CurrentRow.ABANDONDAT = BoringsData.ABANDONDAT;

      CurrentRow.ABANDCAUSE = BoringsData.ABANDCAUSE;
      CurrentRow.DRILLDEPTH = BoringsData.DRILLDEPTH;

      //Assumes that the string no from the intake identifies the correct Casing
      foreach (var Casing in BoringsData.GetCASINGRows())
      {
        if (!IntakeData.IsSTRINGNONull() & !Casing.IsSTRINGNONull())
          if (IntakeData.STRINGNO == Casing.STRINGNO & !Casing.IsBOTTOMNull())
            CurrentRow.CASIBOT = Casing.BOTTOM;
      }

      CurrentRow.PURPOSE = BoringsData.PURPOSE;
      CurrentRow.USE = BoringsData.USE;
      if (CurrentIntake.ScreenTop.Count > 0)
        CurrentRow.INTAKETOP = CurrentIntake.ScreenTop.Min();
      if (CurrentIntake.ScreenBottom.Count > 0)
        CurrentRow.INTAKEBOT = CurrentIntake.ScreenBottom.Max();

      //Takes the minimum of all non-null dates
      IEnumerable<JupiterXL.SCREENRow> NonNullList = IntakeData.GetSCREENRows().Where(x => !x.IsSTARTDATENull());
      if (NonNullList.Count() > 0)
        CurrentRow.INTSTDATE2 = NonNullList.Min(x => x.STARTDATE);

      //Takes the maximum of all non-null dates
      NonNullList = IntakeData.GetSCREENRows().Where(x => !x.IsENDDATENull());
      if (NonNullList.Count() > 0)
        CurrentRow.INTENDATE2 = NonNullList.Max(x => x.ENDDATE);

      CurrentRow.RESROCK = IntakeData.RESERVOIRROCK;

      if (CurrentWell.LithSamples.Count != 0)
      {
        CurrentWell.LithSamples.Sort();
        CurrentRow.BOTROCK = CurrentWell.LithSamples[CurrentWell.LithSamples.Count - 1].RockSymbol;
      }
      else
        CurrentRow.BOTROCK = "999";
    }

    public IEnumerable<JupiterIntake> AddDataForNovanaExtraction(IEnumerable<Plant> Plants, DateTime StartDate, DateTime EndDate)
    {
      NovanaTables.IntakeCommonDataTable DT2 = new NovanaTables.IntakeCommonDataTable();
      NovanaTables.IndvindingerDataTable DT1 = new NovanaTables.IndvindingerDataTable();
      NovanaTables.IndvindingerRow CurrentRow;

      List<JupiterIntake> _intakes = new List<JupiterIntake>();

      //Loop the plants
      foreach (Plant P in Plants)
        //Loop the wells
        foreach (IWell IW in P.PumpingWells)
        {
          //Construct a JupiterWell
          JupiterWell Jw = new JupiterWell(IW);
          //Loop the intakes
          foreach (IIntake I in Jw.Intakes)
          {
            JupiterIntake CurrentIntake = I as JupiterIntake;
            CurrentIntake.Data = DT2.NewIntakeCommonRow();
            //Read generic data
            AddCommonDataForNovana(CurrentIntake);
            DT2.Rows.Add(CurrentIntake.Data);
            CurrentRow = DT1.NewIndvindingerRow();

            //Construct novana id
            string NovanaID = P.IDNumber + "_" + CurrentIntake.well.ID.Replace(" ", "") + "_" + CurrentIntake.IDNumber;

            CurrentRow.NOVANAID = NovanaID;
            CurrentIntake.Data["NOVANAID"] = NovanaID;
            CurrentRow.PLANTID = P.IDNumber;
            CurrentRow.PLANTNAME = P.Name;

            //Get additional data about the plant from the dataset
            var anlaeg = JXL.DRWPLANT.FindByPLANTID(P.IDNumber);
            CurrentRow.NYKOMNR = anlaeg.MUNICIPALITYNO2007;
            CurrentRow.KOMNR = anlaeg.MUNICIPALITYNO;
            CurrentRow.ATYP = anlaeg.PLANTTYPE;
            CurrentRow.ANR = anlaeg.SERIALNO;
            CurrentRow.UNR = anlaeg.SUBNO;

            if (anlaeg.IsXUTMNull())
              CurrentRow.ANLUTMX = 0;
            else
              CurrentRow.ANLUTMX = anlaeg.XUTM;

            if (anlaeg.IsYUTMNull())
              CurrentRow.ANLUTMY = 0;
            else
              CurrentRow.ANLUTMY = anlaeg.YUTM;
  
            CurrentRow.VIRKTYP = anlaeg.COMPANYTYPE;
            CurrentRow.ACTIVE = anlaeg.ACTIVE;

            if (P.Extractions.Count > 0)
            {
              CurrentRow.MEANINDV = P.Extractions.Where(var => var.Time >= StartDate && var.Time <= EndDate).Average(var => var.Value);
              CurrentRow.NYINDVAAR = P.Extractions.Find(var => var.Value > 0).Time.Year;
              CurrentRow.NYIND = P.Extractions.Find(var => var.Value > 0).Value;
            }
            CurrentRow.ANTINT_A = P.PumpingIntakes.Count;
            CurrentRow.ANTBOR_A = P.PumpingWells.Count;

            DT1.Rows.Add(CurrentRow);
            _intakes.Add(CurrentIntake);
          }
        }
      DT2.Merge(DT1);
      return _intakes;
    }


    public void AddDataForNovanaPejl(IEnumerable<JupiterIntake> Intakes)
    {
      NovanaTables.PejlingerDataTable DT1 = new NovanaTables.PejlingerDataTable();
      NovanaTables.PejlingerRow CurrentRow;

      NovanaTables.IntakeCommonDataTable DT2 = new NovanaTables.IntakeCommonDataTable();

      foreach (JupiterIntake CurrentIntake in Intakes)
      {
        CurrentIntake.Data = DT2.NewIntakeCommonRow();
        AddCommonDataForNovana(CurrentIntake);
        DT2.Rows.Add(CurrentIntake.Data);
        CurrentRow = DT1.NewPejlingerRow();
        CurrentRow.NOVANAID = CurrentIntake.Data["NOVANAID"].ToString();

        DT1.Rows.Add(CurrentRow);

        //Create statistics on water levels
        CurrentRow.ANTPEJ = CurrentIntake.Observations.Count;
        if (CurrentRow.ANTPEJ > 0)
        {
          //Fra WatLevel          
          CurrentRow.REFPOINT = JXL.WATLEVEL.FindByBOREHOLENOWATLEVELNO(CurrentIntake.well.ID, 1).REFPOINT;

          CurrentRow.MINDATO = CurrentIntake.Observations.Min(x => x.Time);
          CurrentRow.MAXDATO = CurrentIntake.Observations.Max(x => x.Time);
          CurrentRow.AKTAAR = CurrentRow.MAXDATO.Year - CurrentRow.MINDATO.Year + 1;
          CurrentRow.AKTDAGE = CurrentRow.MAXDATO.Subtract(CurrentRow.MINDATO).Days + 1;
          CurrentRow.PEJPRAAR = CurrentRow.ANTPEJ / CurrentRow.AKTAAR;
          CurrentRow.MAXPEJ = CurrentIntake.Observations.Max(num => num.Value);
          CurrentRow.MINPEJ = CurrentIntake.Observations.Min(num => num.Value);
          CurrentRow.MEANPEJ = CurrentIntake.Observations.Average(num => num.Value);
        }
      }
      DT2.Merge(DT1);
    }


    public Dictionary<string, IWell> WellsForNovana(bool Lithology, bool WaterLevel, bool Chemistry)
    {
      Dictionary<string, IWell> Wells = new Dictionary<string, IWell>();
      //Construct the data set
      JXL.ReadWells(false);
      if (Lithology)
        JXL.ReadInLithology();
      if (WaterLevel)
        JXL.ReadWaterLevels();
      if (Chemistry)
        JXL.ReadInChemistrySamples();

      JupiterWell CurrentWell;
      IIntake CurrentIntake;

      //Loop the wells
      foreach (var Boring in JXL.BOREHOLE)
      {
        CurrentWell = new JupiterWell(Boring.BOREHOLENO);
        Wells.Add(Boring.BOREHOLENO, CurrentWell);

        if (!Boring.IsXUTMNull())
            CurrentWell.X = Boring.XUTM;
          else //If no x set x to 0!
            CurrentWell.X = 0;

          if (!Boring.IsYUTMNull())
            CurrentWell.Y = Boring.YUTM;
          else
            CurrentWell.Y = 0;

          CurrentWell.Description = Boring.LOCATION;
          CurrentWell.Terrain = Boring.ELEVATION;

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
              C.CompoundName = JXL.COMPOUNDLIST.FindByCOMPOUNDNO(C.CompoundNo).LONG_TEXT;
              CurrentWell.ChemSamples.Add(C);
            }
          }

        //Loop the intakes
        foreach (var Intake in Boring.GetINTAKERows())
        {
          CurrentIntake = CurrentWell.AddNewIntake(Intake.INTAKENO);

          //Loop the screens. One intake can in special cases have multiple screens
          foreach (var Screen in Intake.GetSCREENRows())
          {
              CurrentIntake.ScreenTop.Add(Screen.TOP);
              CurrentIntake.ScreenBottom.Add(Screen.BOTTOM);
          }//Screen loop


          //Read in the water levels
          foreach (var WatLev in Intake.GetWATLEVELRows())
          {
            FillInWaterLevel(CurrentIntake, WatLev);
          }         

        }//Intake loop

      }//Bore loop

      return Wells;
    }

  }
}
