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
            if (Wells.TryGetValue(WatLev.BOREHOLENO, out CurrentWell))
            {
                IIntake I = CurrentWell.Intakes.FirstOrDefault(var => var.IDNumber == WatLev.INTAKENO);
                if (I != null)
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
        else if (!WatLev.IsWATLEVGRSUNull())
            CurrentIntake.Observations.Add(new ObservationEntry(WatLev.TIMEOFMEAS, CurrentIntake.well.Terrain - WatLev.WATLEVGRSU));

    }


    /// <summary>
    /// Read Extractions
    /// </summary>
    /// <param name="Plants"></param>
    /// <param name="Wells"></param>
    public IEnumerable<Plant> Extraction(Dictionary<string, IWell> Wells)
    {
      List<Plant> Plants = new List<Plant>();
      Dictionary<int, Plant> DPlants = new Dictionary<int, Plant>();

      JXL.ReadExtractions();

      IWell CurrentWell;
      IIntake CurrentIntake=null;
      Plant CurrentPlant;

      List<Tuple<int, Plant>> SubPlants = new List<Tuple<int, Plant>>();


      foreach (var Anlaeg in JXL.DRWPLANT)
      {
        CurrentPlant = new Plant(Anlaeg.PLANTID);
        DPlants.Add(Anlaeg.PLANTID, CurrentPlant);

        CurrentPlant.Name = Anlaeg.PLANTNAME;

        CurrentPlant.Address = Anlaeg.PLANTADDRESS;

        CurrentPlant.PostalCode = Anlaeg.PLANTPOSTALCODE;

        if (!Anlaeg.IsPERMITDATENull())
          CurrentPlant.PermitDate = Anlaeg.PERMITDATE;

        if (!Anlaeg.IsPERMITEXPIREDATENull())
          CurrentPlant.PermitExpiryDate = Anlaeg.PERMITEXPIREDATE;

        CurrentPlant.Permit = Anlaeg.PERMITAMOUNT;

        if (!Anlaeg.IsSUPPLANTNull())
          SubPlants.Add(new Tuple<int, Plant>(Anlaeg.SUPPLANT, CurrentPlant));

        //Loop the intakes. Only add intakes from wells already in table
        foreach (var IntakeData in Anlaeg.GetDRWPLANTINTAKERows())
        {
          if (Wells.TryGetValue(IntakeData.BOREHOLENO, out CurrentWell))
          {
            CurrentIntake = CurrentWell.Intakes.FirstOrDefault(var => var.IDNumber == IntakeData.INTAKENO);
            if (CurrentIntake != null)
            {
              CurrentPlant.PumpingIntakes.Add(CurrentIntake);

              if (!IntakeData.IsSTARTDATENull())
                CurrentIntake.PumpingStart = IntakeData.STARTDATE;
              else
                CurrentIntake.PumpingStart = DateTime.MinValue;

              if (!IntakeData.IsENDDATENull())
                CurrentIntake.PumpingStop = IntakeData.ENDDATE;
              else
                CurrentIntake.PumpingStop = DateTime.MaxValue;
            }
          }
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

      //In ribe amt extractions are in another table

      foreach (var IntExt in JXL.INTAKECATCHMENT)
      {
        Plant P = DPlants[IntExt.DRWPLANTINTAKERow.PLANTID];

        if (!IntExt.IsVOLUMENull())
        {
          if (IntExt.ENDDATE.Year != IntExt.STARTDATE.Year)
            throw new Exception("Volume cover period longer than 1 year)");

          TimeSeriesEntry E = P.Extractions.FirstOrDefault(var => var.Time.Year == IntExt.ENDDATE.Year);
          if (E == null)
            P.Extractions.Add(new TimeSeriesEntry(IntExt.ENDDATE, IntExt.VOLUME));
          else
            E.Value += IntExt.VOLUME;
        }
      }
      
      //Now attach the subplants
      foreach (Tuple<int, Plant> KVP in SubPlants)
      {
        Plant Upper;
        if (DPlants.TryGetValue(KVP.First, out Upper))
        {
          Upper.SubPlants.Add(KVP.Second);
          foreach (IIntake I in KVP.Second.PumpingIntakes)
          {
            IIntake d = Upper.PumpingIntakes.FirstOrDefault(var => var.well.ID == I.well.ID);
            if (d != null)
              Upper.PumpingIntakes.Remove(d);
          }
        }
      }

      return DPlants.Values;
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
      JXL.ReadWells(true, false);

      Well CurrentWell;
      IIntake CurrentIntake;

      //Loop the wells
      foreach (var Boring in JXL.BOREHOLE)
      {
        CurrentWell = new Well(Boring.BOREHOLENO);
        Wells.Add(CurrentWell.ID, CurrentWell);

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
        JXL.ReadWells(false, false);
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


      CurrentRow.CASIBOT = -999;
      CurrentRow.JUPDTMK = -999;
      CurrentRow.EC0 = 0;

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
      else
        CurrentRow.INTAKETOP = -999;
      if (CurrentIntake.ScreenBottom.Count > 0)
        CurrentRow.INTAKEBOT = CurrentIntake.ScreenBottom.Max();
      else
        CurrentRow.INTAKEBOT = -999;

      CurrentRow.INTAKTOPK = -999;
      CurrentRow.INTAKBOTK = -999;

      if (CurrentRow.JUPKOTE != -999)
      {
        if (CurrentRow.INTAKETOP != -999)
          CurrentRow.INTAKTOPK = CurrentRow.JUPKOTE - CurrentRow.INTAKETOP;
        if (CurrentRow.INTAKEBOT != -999)
          CurrentRow.INTAKBOTK = CurrentRow.JUPKOTE - CurrentRow.INTAKEBOT;
      }
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
        CurrentRow.BOTROCK = "-999";
    }

    public IEnumerable<JupiterIntake> AddDataForNovanaExtraction(IEnumerable<Plant> Plants, DateTime StartDate, DateTime EndDate)
    {
      NovanaTables.IntakeCommonDataTable DT2 = new NovanaTables.IntakeCommonDataTable();
      NovanaTables.IndvindingerDataTable DT1 = new NovanaTables.IndvindingerDataTable();
      NovanaTables.IndvindingerRow CurrentRow;

      List<JupiterIntake> _intakes = new List<JupiterIntake>();

      //Make sure all the necessary data have been read.
      if (JXL.ReducedRead)
        JXL.ReadWells(false, false);


      //Loop the plants
      foreach (Plant P in Plants)
      {
        var anlaeg = JXL.DRWPLANT.FindByPLANTID(P.IDNumber);


        //Loop the wells
        foreach (IWell IW in P.PumpingWells)
        {
          var wellData=JXL.BOREHOLE.FindByBOREHOLENO(IW.ID);
          //Construct a JupiterWell
          JupiterWell Jw = new JupiterWell(IW);
          //Loop the intakes
          foreach (IIntake I in Jw.Intakes)
          {
            //If the plant does not use all intakes in a well we should not print it
            if (null!=P.PumpingIntakes.FirstOrDefault(var=> var.IDNumber.Equals(I.IDNumber) & var.well.ID.Equals(Jw.ID)))
            {
              var intakedata = wellData.GetINTAKERows().FirstOrDefault(var => var.INTAKENO == I.IDNumber);
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

              FillPlantDataIntoDataRow(CurrentRow, anlaeg, P, StartDate, EndDate);

              //Aktiv periode
              var plantintake = anlaeg.GetDRWPLANTINTAKERows().FirstOrDefault(var => var.BOREHOLENO == Jw.ID & var.INTAKENO == I.IDNumber);
              NovanaTables.IntakeCommonRow TIC = CurrentIntake.Data as NovanaTables.IntakeCommonRow;

              CurrentRow.FRAAAR = 1000;
              if (!plantintake.IsSTARTDATENull())
              {
                CurrentRow.INTSTDATE = plantintake.STARTDATE;
                CurrentRow.FRAAAR = Math.Max(CurrentRow.FRAAAR, plantintake.STARTDATE.Year);
              }
              if (!wellData.IsDRILENDATENull())
                CurrentRow.FRAAAR = Math.Max(CurrentRow.FRAAAR, wellData.DRILENDATE.Year);

              if (!TIC.IsINTSTDATE2Null())
                CurrentRow.FRAAAR = Math.Max(CurrentRow.FRAAAR, TIC.INTSTDATE2.Year);

              if (CurrentRow.FRAAAR==int.MinValue)
                CurrentRow.FRAAAR = -999;

              CurrentRow.TILAAR = 9999;
              if (!plantintake.IsENDDATENull())
              {
                CurrentRow.INTENDDATE = plantintake.ENDDATE;
                CurrentRow.TILAAR = Math.Min(CurrentRow.TILAAR, plantintake.ENDDATE.Year);
              }
              if (!wellData.IsABANDONDATNull())
                CurrentRow.TILAAR = Math.Min(CurrentRow.TILAAR, wellData.ABANDONDAT.Year);
              if (!TIC.IsINTENDATE2Null())
                CurrentRow.TILAAR = Math.Min(CurrentRow.TILAAR, TIC.INTENDATE2.Year);

              if (CurrentRow.TILAAR == int.MaxValue)
                CurrentRow.TILAAR = -999;

              DT1.Rows.Add(CurrentRow);
              _intakes.Add(CurrentIntake);
            }
          }
        }
      }

      DT2.Merge(DT1);

      return _intakes;
    }

      public NovanaTables.IndvindingerDataTable FillPlantData(IEnumerable<Plant> plants, DateTime StartDate, DateTime EndDate)
      {
          NovanaTables.IndvindingerDataTable DT = new NovanaTables.IndvindingerDataTable();
          NovanaTables.IndvindingerRow CurrentRow;
          JupiterXL.DRWPLANTRow anlaeg;

          foreach (Plant P in plants)
          {
              anlaeg = JXL.DRWPLANT.FindByPLANTID(P.IDNumber);
              CurrentRow = DT.NewIndvindingerRow();
              FillPlantDataIntoDataRow(CurrentRow, anlaeg, P, StartDate, EndDate);
              CurrentRow.NOVANAID = P.IDNumber.ToString();
              DT.AddIndvindingerRow(CurrentRow);
          }
              return DT;
      }

    private void FillPlantDataIntoDataRow(NovanaTables.IndvindingerRow CurrentRow, JupiterXL.DRWPLANTRow anlaeg, Plant P, DateTime StartDate, DateTime EndDate)
    {
      CurrentRow.PLANTID = anlaeg.PLANTID;
      CurrentRow.PLANTNAME = anlaeg.PLANTNAME;

      //Get additional data about the plant from the dataset
      CurrentRow.NYKOMNR = anlaeg.MUNICIPALITYNO2007;
      CurrentRow.KOMNR = anlaeg.MUNICIPALITYNO;
      CurrentRow.ATYP = anlaeg.PLANTTYPE;
      CurrentRow.ANR = anlaeg.SERIALNO;
      CurrentRow.UNR = anlaeg.SUBNO;
      CurrentRow.ANTUNDERA = P.SubPlants.Count;

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

      if (!anlaeg.IsSUPPLANTNull())
        CurrentRow.OVERANL = anlaeg.SUPPLANT;

      var SelectecExtrations = P.Extractions.Where(var => var.Time >= StartDate && var.Time <= EndDate);
      var ActualValue = SelectecExtrations.FirstOrDefault(var => var.Time.Year == EndDate.Year);

      if (P.Extractions.Count > 0)
      {
        if (SelectecExtrations.Count() > 0)
        {
          CurrentRow.MEANINDV = SelectecExtrations.Average(var => var.Value);
          if (ActualValue != null)
            CurrentRow.AKTUELIND = ActualValue.Value;
          else
            CurrentRow.AKTUELIND = 0;
        }
      }
      CurrentRow.ANTINT_A = P.PumpingIntakes.Count;
      CurrentRow.ANTBOR_A = P.PumpingWells.Count;


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
      string[] ExtractionPurpose = new string[]{"C","G","V","VA","VD","VH","VI","VM","VP","VV"};
      string[] ExtractionUse = new string[]{"C","G","V","VA","VD","VH","VI","VM","VP","VV"};

      Dictionary<string, IWell> Wells = new Dictionary<string, IWell>();
      //Construct the data set
      if (WaterLevel)
          JXL.ReadWaterLevels();

      if (Lithology)
        JXL.ReadInLithology();
      if (Chemistry)
        JXL.ReadInChemistrySamples();

        JXL.ReadWells(false, WaterLevel);

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

          CurrentWell.UsedForExtraction = ExtractionPurpose.Contains(Boring.PURPOSE.ToUpper());
          if (ExtractionUse.Contains(Boring.USE.ToUpper()))
            CurrentWell.UsedForExtraction = true;


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
