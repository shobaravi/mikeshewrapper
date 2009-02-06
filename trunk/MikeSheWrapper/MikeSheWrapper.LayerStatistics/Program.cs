using System;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.Data;
using System.IO;

using MikeSheWrapper;
using MikeSheWrapper.Tools;
using MikeSheWrapper.InputDataPreparation;

namespace MikeSheWrapper.LayerStatistics
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		public static void Main(string[] args)
		{
      
      DateTime Start = DateTime.Now;
//      try
      {
        MikeSheGridInfo _grid = null;
        Results _res = null;
        string ObsFileName;
        DateTime SimulationStart= DateTime.MaxValue;

        if (args.Length == 2)
        {
          Model MS = new Model(args[0]);
          _grid = MS.GridInfo;
          _res = MS.Results;
          ObsFileName = args[1];
        }
        else
        {
          Configuration cf = Configuration.ConfigurationFactory(args[0]);
          
          _grid = new MikeSheGridInfo(cf.PreProcessedDFS3, cf.PreProcessedDFS2);

          if (cf.HeadItemText!=null)
            Results.HeadElevationString = cf.HeadItemText;

          _res = new Results(cf.ResultFile, _grid);

          if (_res.Heads==null)
            throw new Exception("Heads could not be found. Check that item: \"" + Results.HeadElevationString + "\" exists in + " + cf.ResultFile);

          ObsFileName = cf.ObservationFile;
        }

        HeadObservations HO = new HeadObservations();
        InputOutput IO = new InputOutput(_grid.NumberOfLayers);

        IO.ReadFromLSText(ObsFileName, HO);

        TimeSpan ReadInput = Start.Subtract(DateTime.Now);
        Start = DateTime.Now;

        int NLay = _grid.NumberOfLayers;
        double [] ME = new double[NLay];
        double [] RMSE = new double[NLay];
        int [] ObsUsed = new int[NLay];
        int [] ObsTotal = new int[NLay];

        //Initialiserer
        for (int i=0;i<NLay;i++)
        {
          ME[i]       = 0;
          RMSE[i]     = 0;
          ObsUsed[i]  = 0;
          ObsTotal[i] = 0;
        }

        HO.SelectByMikeSheModelArea(_grid);

        foreach (ObservationWell W in HO.WorkingList)
        {
          if (W.Layer == -3)
          {
            W.Layer = _grid.GetLayer(W.Column, W.Row, W.Z);
          }
          else
          {
            W.Z = _grid.LowerLevelOfComputationalLayers.Data[W.Row, W.Column, W.Layer] + 0.5 * _grid.ThicknessOfComputationalLayers.Data[W.Row, W.Column, W.Layer];
          }
        }


        HO.GetSimulatedValuesFromGridOutput(_res, _grid);
      
        //Samler resultaterne for hver lag
        foreach (ObservationWell OW in HO.WorkingList)
        {
          foreach (ObservationEntry TSE in OW.Observations)
          {
            if (TSE.SimulatedValueCell == _res.DeleteValue)
            {
              ObsTotal[OW.Layer - 1]++;
            }
            else
            {
              ME[OW.Layer ] += TSE.ME;
              RMSE[OW.Layer] += TSE.RMSE;
              ObsUsed[OW.Layer]++;
              ObsTotal[OW.Layer]++;
            }
          }
        }

        for (int i=0;i<NLay;i++)
        {
          ME[i]   = ME[i]/ObsUsed[i];
          RMSE[i] = Math.Pow(RMSE[i]/ObsUsed[i], 0.5);
        }
        TimeSpan Calculation = Start.Subtract(DateTime.Now);
        Start = DateTime.Now;

        IO.WriteObservations(HO);
        IO.WriteLayers(ME,RMSE,ObsUsed,ObsTotal);

        TimeSpan WriteOutput = Start.Subtract(DateTime.Now);

        _grid.Dispose();
        _res.Dispose();

        //Console.WriteLine("Reading of input:" + ReadInput.TotalSeconds + " s");
        //Console.WriteLine("Calculation:" + Calculation.TotalSeconds + " s");
        //Console.WriteLine("Writing of output:" + WriteOutput.TotalSeconds + " s");
        //Console.ReadLine();

      }
      //catch (Exception e)
      //{
      //  MessageBox.Show("Der er opstået en fejl af typen: " + e.Message);
      //} 
		}
	}
}
