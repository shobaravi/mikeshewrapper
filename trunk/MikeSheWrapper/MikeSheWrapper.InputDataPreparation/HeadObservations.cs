using System;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;

using MathNet.Numerics.LinearAlgebra;

using MikeSheWrapper.Tools;
using MikeSheWrapper.DFS;
using MikeSheWrapper.JupiterTools;

using DHI.TimeSeries;
using DHI.Generic.MikeZero.DFS;

namespace MikeSheWrapper.InputDataPreparation
{
  public class HeadObservations
  {
    
    //Object used for thread safety. What happens if two instances are running in the same process?
    private static object _lock = new object();

    /// <summary>
    /// Function that returns true if a time series entry is between the two dates
    /// </summary>
    public static Func<TimeSeriesEntry, DateTime, DateTime, bool> InBetween = (TSE, Start, End) => TSE.Time >= Start & TSE.Time < End;


    /// <summary>
    /// Function that returns true if an Intake has more than Count observations in the period between Start and End
    /// </summary>
    public static Func<IIntake, DateTime, DateTime, int, bool> NosInBetween = (OW, Start, End, Count) => Count <= OW.Observations.Distinct().Count(num => InBetween(num, Start, End));


    public HeadObservations()
    { }


    /// <summary>
    /// Obsolete!
    /// </summary>
    /// <param name="FileName"></param>
    public HeadObservations(string FileName)
    {
      switch (Path.GetExtension(FileName))
      {
        case ".she":
          ReadInDetailedTimeSeries(new Model(FileName));
          break;
        case ".mdb":
//          ReadWellsFromJupiter(FileName);
          break;
        case ".shp":
          ReadFromShape(FileName,"");
          break;
        default:
          break;
      }
    }
 
    /// <summary>
    /// Select the wells that are inside the model area. Does not look at the 
    /// z - coordinate
    /// </summary>
    /// <param name="MikeShe"></param>
    public static IEnumerable<MikeSheWell> SelectByMikeSheModelArea(MikeSheGridInfo Grid, IEnumerable<MikeSheWell> Wells)
    {
      int Column;
      int Row;
      foreach (MikeSheWell W in Wells)
      {
        //Gets the index and sets the column and row
        if (Grid.GetIndex(W.X, W.Y, out Column, out Row))
        {
          W.Column = Column;
          W.Row = Row;
          yield return W;
        }
      }
    }

    /// <summary>
    /// Writes a textfile that can be used for importing detailed timeseries output
    /// Depth is calculated as the midpoint of the lowest screen
    /// </summary>
    /// <param name="TxtFileName"></param>
    public static void WriteToMikeSheModel(string OutputPath, IEnumerable<IIntake> SelectedIntakes, DateTime Start, DateTime End)
    {

      StreamWriter Sw2 = new StreamWriter(Path.Combine(OutputPath, "WellsWithMissingInfo.txt"), false, Encoding.Default);

      using (StreamWriter SW = new StreamWriter(Path.Combine(OutputPath,"DetailedTimeSeriesImport.txt"), false, Encoding.Default))
      {
        foreach (IIntake I in SelectedIntakes)
        {
          //If there is no screen information we cannot use it. 
          if (I.Screens.Count == 0)
            Sw2.WriteLine("Well: " + I.well.ID + "\tIntake: " + I.IDNumber + "\tError: Missing info about screen depth");
          else
          {
            int NoOfObs = I.Observations.Count(TSE => InBetween(TSE, Start, End));
            //          if (W.Dfs0Written)
            SW.WriteLine(I.ToString() + "\t101\t1\t" + I.well.X + "\t" + I.well.Y + "\t" + PointInScreen(I) + "\t1\t" + I.ToString() + "\t1 \t" + NoOfObs);
            //When is this necessary
            //        else  
            //        SW.WriteLine(W.ID + "\t101\t1\t" + W.X + "\t" + W.Y + "\t" + W.Depth + "\t0\t \t ");
          }
        }
      }
      Sw2.Dispose();
    }

  
    /// <summary>
    /// Finds a well based on the ID in the detailed SZ output dfs0. When a well is found it is added to the workinglist
    /// The working list should be cleared before entering this method
    /// </summary>
    /// <param name="DFS0FileName"></param>
    public static void GetSimulatedValuesFromDetailedTSOutput(string DFS0FileName, List<IIntake> Intakes)
    {
      DFS0 _data = new DFS0(DFS0FileName);

      IIntake OW;
      int item=1;

      //Loop all Items
      foreach (string DI in _data.ItemNames)
      {
        OW = Intakes.Find(var => var.ToString() == DI);
        if (OW!=null)
        {
          //Loop the observations
          foreach (ObservationEntry TSE in OW.Observations)
            TSE.SimulatedValue = _data.GetData(TSE.Time, item);
        }
        item++;
      }
    }


    /// <summary>
    /// 4-point bilinear interpolation is used to get the value in a point.
    /// </summary>
    /// <param name="MSheResults"></param>
    /// <param name="GridInfo"></param>
    public static void GetSimulatedValuesFromGridOutput(Results MSheResults, MikeSheGridInfo GridInfo, MikeSheWell Well)
    {
      foreach(Intake I in Well.Intakes)
        foreach (ObservationEntry TSE in I.Observations)
        {
          if (Well.Layer >= 0)
          {
            Matrix M = MSheResults.PhreaticHead.TimeData(TSE.Time)[Well.Layer];

            TSE.SimulatedValueCell = M[Well.Row, Well.Column];
            //Interpolates in the matrix
            TSE.SimulatedValue = GridInfo.Interpolate(Well.X, Well.Y, Well.Layer, M, out TSE.DryCells, out TSE.BoundaryCells);
          }
        }
    }


#region Population Methods

    /// <summary>
    /// Reads in the wells defined in detailed timeseries input section
    /// </summary>
    /// <param name="Mshe"></param>
    public static IEnumerable<IWell> ReadInDetailedTimeSeries(Model Mshe)
    {
      MikeSheWell CurrentWell;
      IIntake CurrentIntake;
      TSObject _tso = null;

      foreach (MikeSheWrapper.InputFiles.Item_11 dt in Mshe.Input.MIKESHE_FLOWMODEL.StoringOfResults.DetailedTimeseriesOutput.Item_1s)
      {
        CurrentWell = new MikeSheWell(dt.Name);
        CurrentWell.X = dt.X;
        CurrentWell.Y = dt.Y;
        CurrentWell.Depth = dt.Z;
        CurrentWell.UsedForExtraction = false;

        //Read in observations if they are included
        if (dt.InclObserved == 1)
        {
          CurrentIntake = CurrentWell.AddNewIntake(1);

          if (_tso == null || _tso.Connection.FilePath != dt.TIME_SERIES_FILE.FILE_NAME)
          {
            _tso = new TSObjectClass();
            _tso.Connection.FilePath = dt.TIME_SERIES_FILE.FILE_NAME;
            _tso.Connection.Open();
          }

          //Loop the observations and add
          for (int i = 1; i <= _tso.Time.NrTimeSteps; i++)
          {
            CurrentIntake.Observations.Add(new ObservationEntry((DateTime)_tso.Time.GetTimeForTimeStepNr(i), (float)_tso.Item(dt.TIME_SERIES_FILE.ITEM_NUMBERS).GetDataForTimeStepNr(i)));
          }
        }
        yield return CurrentWell;
      }
    }


        /// <summary>
    /// Creates wells from DataRows based on ShapeReaderConfiguration
    /// </summary>
    /// <param name="DS"></param>
    /// <param name="SRC"></param>
    public static void FillInFromNovanaShape(DataRow[] DS, ShapeReaderConfiguration SRC, Dictionary<string, IWell> Wells)
    {
      FillInFromNovanaShape(DS, SRC, Wells, null);
    }



    /// <summary>
    /// Creates wells from DataRows based on ShapeReaderConfiguration
    /// </summary>
    /// <param name="DS"></param>
    /// <param name="SRC"></param>
    public static void FillInFromNovanaShape(DataRow[] DS, ShapeReaderConfiguration SRC, Dictionary<string, IWell> Wells, Dictionary<int, Plant> Plants)
    {
      bool ReadPumpActivity = false;
      bool ReadPlants = false;
      bool ReadLayer = false;
      if (DS.First().Table.Columns.Contains(SRC.FraAArHeader) & DS.First().Table.Columns.Contains(SRC.TilAArHeader))
        ReadPumpActivity = true;

      if (DS.First().Table.Columns.Contains(SRC.LayerHeader))
        ReadLayer = true;


      if (Plants != null)
        if (DS.First().Table.Columns.Contains(SRC.PlantIDHeader))
          ReadPlants = true;

      IWell CurrentWell;
      IIntake CurrentIntake;
      foreach (DataRow DR in DS)
      {
        //Find the well in the dictionary
        if (!Wells.TryGetValue((string)DR[SRC.WellIDHeader], out CurrentWell))
        {
          //Add a new well if it was not found
          CurrentWell = new Well(DR[SRC.WellIDHeader].ToString());
          CurrentWell.UsedForExtraction = true;
          Wells.Add(CurrentWell.ID, CurrentWell);
        }

        int intakeno = Convert.ToInt32(DR[SRC.IntakeNumber]);

        CurrentIntake = CurrentWell.Intakes.FirstOrDefault(var => var.IDNumber == intakeno);
        
        if (CurrentIntake==null)
          CurrentIntake = CurrentWell.AddNewIntake(intakeno);

        if (ReadLayer)
          if (!Convert.IsDBNull(DR[SRC.LayerHeader]))
            CurrentIntake.Layer = Convert.ToInt32(DR[SRC.LayerHeader]);

        if (ReadPlants)
        {
          Plant CurrentPlant;
          int PlantID = Convert.ToInt32(DR[SRC.PlantIDHeader]);
          if (!Plants.TryGetValue(PlantID, out CurrentPlant))
          {
            CurrentPlant = new Plant(PlantID);
            Plants.Add(PlantID, CurrentPlant);
          }
          PumpingIntake CurrentPumpingIntake = new PumpingIntake(CurrentIntake);
          CurrentPlant.PumpingIntakes.Add(CurrentPumpingIntake);
          if (ReadPumpActivity)
          {
            CurrentPumpingIntake.Start = new DateTime(Convert.ToInt32(DR[SRC.FraAArHeader]), 1, 1);
            CurrentPumpingIntake.End = new DateTime(Convert.ToInt32(DR[SRC.TilAArHeader]), 12, 31);
          }
        }
        CurrentWell.X = Convert.ToDouble(DR[SRC.XHeader]);
        CurrentWell.Y = Convert.ToDouble(DR[SRC.YHeader]);
        CurrentWell.Terrain = Convert.ToDouble(DR[SRC.TerrainHeader]);
        Screen CurrentScreen = new Screen(CurrentIntake);
        CurrentScreen.BottomAsKote = Convert.ToDouble(DR[SRC.BOTTOMHeader]);
        CurrentScreen.TopAsKote = Convert.ToDouble(DR[SRC.TOPHeader]);
      }
    }

    /// <summary>
    /// Reads in observations from a shape file
    /// </summary>
    /// <param name="ShapeFileName"></param>
    public void ReadFromShape(string ShapeFileName, string SelectString)
    {
      PointShapeReader SR = new PointShapeReader(ShapeFileName);

      DataTable DT = SR.Data.Read();

     // FillInFromNovanaShape(DT.Select(SelectString));
    }

#endregion


    /// <summary>
    /// Returns a point in the screen in meters below surface. To be used for detailed time series output and layerstatistics.
    /// </summary>
    /// <param name="Intake"></param>
    /// <returns></returns>
    private static double PointInScreen(IIntake Intake)
    {
      double top = Intake.Screens.Min(var => var.DepthToTop);
      double bottom = Intake.Screens.Max(var => var.DepthToBottom);

      if (top == -999)
        return bottom -1;
      else if (bottom == -999)
        return top +1;
      else
       return (top + bottom)/2;
    }

    /// <summary>
    /// Write a text-file that can be used by LayerStatistics.
    /// </summary>
    /// <param name="FileName"></param>
    /// <param name="SelectedWells"></param>
    /// <param name="Start"></param>
    /// <param name="End"></param>
    /// <param name="AllObs"></param>
    public static void WriteToLSInput(string FileName, IEnumerable<IIntake> SelectedIntakes, DateTime Start, DateTime End, bool AllObs)
    {
      using (StreamWriter SW = new StreamWriter(FileName, false, Encoding.Default))
      {
        if (AllObs)
          SW.WriteLine("NOVANAID\tXUTM\tYUTM\tDEPTH\tPEJL\tDATO\tBERELAG");
        else
          SW.WriteLine("NOVANAID\tXUTM\tYUTM\tDEPTH\tMEANPEJ\tMAXDATO\tBERELAG");

          foreach (IIntake I in SelectedIntakes.Where(var=>var.Screens.Count>0))
          {
            List<ObservationEntry> SelectedObs = I.Observations.Where(TSE => InBetween(TSE, Start, End)).ToList<ObservationEntry>();

            SelectedObs.Sort();

            StringBuilder S = new StringBuilder();
     
              S.Append(I.ToString() + "\t" + I.well.X + "\t" + I.well.Y + "\t" + PointInScreen(I) + "\t");
            
            if (AllObs)
              foreach (ObservationEntry TSE in SelectedObs)
              {
                StringBuilder ObsString = new StringBuilder(S.ToString());
                ObsString.Append(TSE.Value + "\t" + TSE.Time.ToShortDateString());
                if (I.Layer != null)
                  ObsString.Append("\t" + I.Layer.ToString());
                SW.WriteLine(ObsString.ToString());
              }
            else
            {
                if (SelectedObs.Count > 0)
                {
                    S.Append(SelectedObs.Average(num => num.Value).ToString() + "\t");
                    S.Append(SelectedObs.Max(num => num.Time).ToShortDateString());
                    if (I.Layer != null)
                      S.Append("\t" +I.Layer.ToString());
                    SW.WriteLine(S.ToString());
                }
            }
          }
      }
    }
    


    /// <summary>
    /// Writes dfs0 files with head observations for the SelectedIntakes
    /// Only includes data within the period bounded by Start and End
    /// </summary>
    /// <param name="OutputPath"></param>
    public static void WriteToDfs0(string OutputPath, IIntake Intake, DateTime Start, DateTime End)
    {
      //Create the TSObject
      TSObject _tso = new TSObjectClass();
      TSItem _item = new TSItemClass();
      _item.DataType = ItemDataType.Type_Float;
      _item.ValueType = ItemValueType.Instantaneous;
      _item.EumType = 171;
      _item.EumUnit = 1;
      _item.Name = "Head";
      _tso.Add(_item);

      DateTime _previousTimeStep = DateTime.MinValue;

      //Select the observations
      List<ObservationEntry> SelectedObs = Intake.Observations.Where(TSE => InBetween(TSE, Start, End)).ToList<ObservationEntry>();

      SelectedObs.Sort();

      for (int i = 0; i < SelectedObs.Count; i++)
      {
        //Only add the first measurement of the day
        if (SelectedObs[i].Time != _previousTimeStep)
        {
          _tso.Time.AddTimeSteps(1);
          _tso.Time.SetTimeForTimeStepNr(i + 1, SelectedObs[i].Time);
          _item.SetDataForTimeStepNr(i + 1, (float)SelectedObs[i].Value);
        }
      }

      //Now write the DFS0.
      if (_tso.Time.NrTimeSteps != 0)
      {
        _tso.Connection.FilePath = Path.Combine(OutputPath, Intake.ToString() + ".dfs0");
        _tso.Connection.Save();
      }
    }

      /// <summary>
      /// Writes adfs0 with extraction data for each active intake in every plant. 
      /// Also writes the textfile that can be imported by the well editor.
      /// </summary>
      /// <param name="OutputPath"></param>
      /// <param name="Plants"></param>
      /// <param name="Start"></param>
      /// <param name="End"></param>
    public static void WriteExtractionDFS0(string OutputPath, IEnumerable<Plant> Plants, DateTime Start, DateTime End)
    {

      //Create the text file to the well editor.
      StreamWriter Sw = new StreamWriter(Path.Combine(OutputPath, "WellEditorImport.txt"), false, Encoding.Default);
      StreamWriter Sw2 = new StreamWriter(Path.Combine(OutputPath, "WellsWithMissingInfo.txt"), false, Encoding.Default);

      //Create the TSObject
      TSObject _tso = new TSObjectClass();
      string dfs0FileName =Path.Combine(OutputPath, "Extraction.dfs0");
      _tso.Connection.FilePath = dfs0FileName;
      TSItem _item;

      TSObject _tsoStat = new TSObjectClass();
      _tsoStat.Connection.FilePath = Path.Combine(OutputPath, "ExtractionStat.dfs0");
      Dictionary<int, double> Sum = new Dictionary<int, double>();
      Dictionary<int, double> SumSurfaceWater = new Dictionary<int, double>();
      Dictionary<int, double> SumNotUsed = new Dictionary<int, double>();

      int Pcount = 0;
      

      int NumberOfYears = End.Year - Start.Year + 1;

        //Dummy year because of mean step accumulated
      _tso.Time.AddTimeSteps(1);
      _tso.Time.SetTimeForTimeStepNr(1, new DateTime(Start.Year, 1, 1, 0, 0, 0));

      for (int i = 0; i < NumberOfYears; i++)
      {
          _tso.Time.AddTimeSteps(1);
          _tso.Time.SetTimeForTimeStepNr(i + 2, new DateTime(Start.Year + i, 12, 31, 12, 0, 0));

          _tsoStat.Time.AddTimeSteps(1);
          _tsoStat.Time.SetTimeForTimeStepNr(i + 1, new DateTime(Start.Year + i, 12, 31, 12, 0, 0));
          Sum.Add(i, 0);
          SumSurfaceWater.Add(i, 0);
          SumNotUsed.Add(i, 0);
      }

      int itemCount = 1;

      double[] fractions = new double[NumberOfYears];

      //loop the plants
      foreach (Plant P in Plants)
      {
        //Create statistics on surface water for all plants
        for (int i = 0; i < NumberOfYears; i++)
        {
          int k = P.SurfaceWaterExtrations.FindIndex(var => var.Time.Year == Start.Year + i);
          if (k > 0)
            SumSurfaceWater[i] += P.SurfaceWaterExtrations[k].Value;
        }

        //Create statistics for plants without intakes
        if (P.PumpingIntakes.Count == 0)
        {
          //Create statistics on water not assigned
          for (int i = 0; i < NumberOfYears; i++)
          {
            int k = P.Extractions.FindIndex(var => var.Time.Year == Start.Year + i);
            if (k > 0)
              SumNotUsed[i] += P.Extractions[k].Value;
          }
        }
        else
        {
          //Create statistics
          for (int i = 0; i < NumberOfYears; i++)
          {
            //Extractions are not necessarily sorted and the time series may have missing data
            int k = P.Extractions.FindIndex(var => var.Time.Year == Start.Year + i);
            if (k >= 0)
              Sum[i] += P.Extractions[k].Value;
          }
          Pcount++;


          //Calculate the fractions based on how many intakes are active for a particular year.
          for (int i = 0; i < NumberOfYears; i++)
          {
            fractions[i] = 1.0 / P.PumpingIntakes.Count(var => var.Intake.well.UsedForExtraction & var.Start.Year <= Start.Year + i & var.End.Year >= Start.Year + i);
          }

          //Now loop the intakes
          foreach (PumpingIntake PI in P.PumpingIntakes)
          {
            IIntake I = PI.Intake;
            //Is it an extraction well?
            if (I.well.UsedForExtraction)
            {
              //If there is no screen information we cannot use it. 
              if (I.Screens.Count==0)
                Sw2.WriteLine("Well: " + I.well.ID + "\tIntake: " + I.IDNumber + "\tError: Missing info about screen depth");
              else
              {
                //Build novanaid
                string NovanaID = P.IDNumber.ToString() + "_" + I.well.ID.Replace(" ", "") + "_" + I.IDNumber;
                //Build and add new item
                _item = new TSItemClass();
                _item.DataType = ItemDataType.Type_Float;
                _item.ValueType = ItemValueType.Mean_Step_Accumulated;
                _item.EumType = 328;
                _item.EumUnit = 3;
                _item.Name = NovanaID;
                _tso.Add(_item);

                //Loop the years
                for (int i = 0; i < NumberOfYears; i++)
                {
                  //Extractions are not necessarily sorted and the time series may have missing data
                  int k = P.Extractions.FindIndex(var => var.Time.Year == Start.Year + i);

                  //First year should be printed twice
                  if (i == 0)
                  {
                    if (k >= 0 & PI.Start.Year <= Start.Year + i & PI.End.Year >= Start.Year + i)
                      _item.SetDataForTimeStepNr(1, (float)(P.Extractions[k].Value * fractions[i]));
                    else
                      _item.SetDataForTimeStepNr(1, 0F); //Prints 0 if no data available
                  }
                  //If data and the intake is active
                  if (k >= 0 & PI.Start.Year <= Start.Year + i & PI.End.Year >= Start.Year + i)
                    _item.SetDataForTimeStepNr(i + 2, (float)(P.Extractions[k].Value * fractions[i]));
                  else
                    _item.SetDataForTimeStepNr(i + 2, 0F); //Prints 0 if no data available
                }

                //Now add line to text file.
                StringBuilder Line = new StringBuilder();
                Line.Append(NovanaID + "\t");
                Line.Append(I.well.X + "\t");
                Line.Append(I.well.Y + "\t");
                Line.Append(I.well.Terrain + "\t");
                Line.Append("0\t");
                Line.Append(P.IDNumber + "\t");
                Line.Append(I.Screens.Max(var=>var.TopAsKote ) + "\t");
                Line.Append(I.Screens.Min(var => var.BottomAsKote) + "\t");
                Line.Append(1 + "\t");
                Line.Append(dfs0FileName + "\t");
                Line.Append(itemCount);
                Sw.WriteLine(Line.ToString());

                itemCount++;
              }
            }
          }
        }
      }

      TSItem SumItem = new TSItemClass();
      SumItem.DataType = ItemDataType.Type_Float;
      SumItem.ValueType = ItemValueType.Mean_Step_Accumulated;
      SumItem.EumType = 328;
      SumItem.EumUnit = 3;
      SumItem.Name = "Sum";
      _tsoStat.Add(SumItem);

      TSItem MeanItem = new TSItemClass();
      MeanItem.DataType = ItemDataType.Type_Float;
      MeanItem.ValueType = ItemValueType.Mean_Step_Accumulated;
      MeanItem.EumType = 328;
      MeanItem.EumUnit = 3;
      MeanItem.Name = "Mean";
      _tsoStat.Add(MeanItem);

      TSItem SumNotUsedItem = new TSItemClass();
      SumNotUsedItem.DataType = ItemDataType.Type_Float;
      SumNotUsedItem.ValueType = ItemValueType.Mean_Step_Accumulated;
      SumNotUsedItem.EumType = 328;
      SumNotUsedItem.EumUnit = 3;
      SumNotUsedItem.Name = "SumNotUsed";
      _tsoStat.Add(SumNotUsedItem);

      TSItem SumSurfaceWaterItem = new TSItemClass();
      SumSurfaceWaterItem.DataType = ItemDataType.Type_Float;
      SumSurfaceWaterItem.ValueType = ItemValueType.Mean_Step_Accumulated;
      SumSurfaceWaterItem.EumType = 328;
      SumSurfaceWaterItem.EumUnit = 3;
      SumSurfaceWaterItem.Name = "SumSurfaceWater";
      _tsoStat.Add(SumSurfaceWaterItem);

      for (int i = 0; i < NumberOfYears; i++)
      {
          SumItem.SetDataForTimeStepNr(i + 1, (float)Sum[i]);
          MeanItem.SetDataForTimeStepNr(i + 1, (float)Sum[i]/Pcount);
          SumNotUsedItem.SetDataForTimeStepNr(i + 1, (float)SumNotUsed[i]);
          SumSurfaceWaterItem.SetDataForTimeStepNr(i + 1, (float)SumSurfaceWater[i]);
      }

      _tsoStat.Connection.Save();
      _tso.Connection.Save();
      Sw.Dispose();
      Sw2.Dispose();
    }

  

    /// <summary>
    /// Write a specialized output with all observation in one long .dat file
    /// </summary>
    /// <param name="FileName"></param>
    /// <param name="SelectedWells"></param>
    /// <param name="Start"></param>
    /// <param name="End"></param>
    public static void WriteToDatFile(string FileName, IEnumerable<IIntake> SelectedIntakes, DateTime Start, DateTime End)
    {
      StringBuilder S = new StringBuilder();

      using (StreamWriter SW = new StreamWriter(FileName, false, Encoding.Default))
      {
        foreach (Intake I in SelectedIntakes)
        {
          List<ObservationEntry> SelectedObs = I.Observations.Where(TSE => InBetween(TSE, Start, End)).ToList<ObservationEntry>();
          SelectedObs.Sort();
          foreach (ObservationEntry TSE in SelectedObs)
          {
            S.Append(I.ToString() + "    " + TSE.Time.ToString("dd/MM/yyyy hh:mm:ss") + " " + TSE.Value.ToString() + "\n");
          }
        }

        SW.Write(S.ToString());
      }
    }

    /// <summary>
    /// Writes a point shape with entries for each intake in the list. Uses the dataRow as attributes.
    /// </summary>
    /// <param name="FileName"></param>
    /// <param name="Intakes"></param>
    /// <param name="Start"></param>
    /// <param name="End"></param>
    public static void WriteShapeFromDataRow(string FileName, IEnumerable<JupiterIntake> Intakes)
    {
      PointShapeWriter PSW = new PointShapeWriter(FileName);
        foreach (JupiterIntake JI in Intakes)
        {
          PSW.WritePointShape(JI.well.X, JI.well.Y);
          PSW.Data.WriteData(JI.Data);
        }
      PSW.Dispose();

    }

    /// <summary>
    /// Writes the wells to a point shape
    /// Calculates statistics on the observations within the period from start to end
    /// </summary>
    /// <param name="FileName"></param>
    /// <param name="Wells"></param>
    /// <param name="Start"></param>
    /// <param name="End"></param>
    public static void WriteSimpleShape(string FileName, IEnumerable<IIntake> Intakes, DateTime Start, DateTime End)
    {
      PointShapeWriter PSW = new PointShapeWriter(FileName);


      OutputTables NT = new OutputTables();
      OutputTables.PejlingerOutputDataTable PDT = new OutputTables.PejlingerOutputDataTable();

        foreach (Intake I in Intakes)
        {
          List<ObservationEntry> SelectedObs = I.Observations.Where(TSE => InBetween(TSE, Start, End)).ToList<ObservationEntry>();

          PSW.WritePointShape(I.well.X, I.well.Y);

          OutputTables.PejlingerOutputRow PR = PDT.NewPejlingerOutputRow();

          PR.NOVANAID = I.ToString();
          PR.LOCATION = I.well.Description;
          PR.XUTM = I.well.X;
          PR.YUTM = I.well.Y;
          PR.JUPKOTE = I.well.Terrain;

          if (I.Screens.Count > 0)
          {
            PR.INTAKETOP = I.Screens.Min(var => var.DepthToTop);
            PR.INTAKEBOT = I.Screens.Max(var => var.DepthToBottom);
          }

          PR.NUMBEROFOB = SelectedObs.Count;
          if (SelectedObs.Count > 0)
          {
            PR.STARTDATO = SelectedObs.Min(x => x.Time);
            PR.ENDDATO = SelectedObs.Max(x => x.Time);
            PR.MAXOBS = SelectedObs.Max(num => num.Value);
            PR.MINOBS = SelectedObs.Min(num => num.Value);
            PR.MEANOBS = SelectedObs.Average(num => num.Value);
          }
          PDT.Rows.Add(PR);
        }
      

      PSW.Data.WriteDate(PDT);
      PSW.Dispose();
    }
  }
}
