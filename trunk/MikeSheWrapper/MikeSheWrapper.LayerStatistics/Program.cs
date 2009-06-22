using System;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data;
using System.IO;

using MathNet.Numerics.LinearAlgebra;

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
      try
      {
        MikeSheGridInfo _grid = null;
        Results _res = null;
        string ObsFileName;

        //Input is a -she-file and an observation file
        if (args.Length == 2)
        {
          Model MS = new Model(args[0]);
          _grid = MS.GridInfo;
          _res = MS.Results;
          ObsFileName = args[1];
        }
        //Input is an .xml-file
        else if (args.Length == 1)
        {
          Configuration cf = Configuration.ConfigurationFactory(args[0]);

          _grid = new MikeSheGridInfo(cf.PreProcessedDFS3, cf.PreProcessedDFS2);

          if (cf.HeadItemText != null)
            _res = new Results(cf.ResultFile, _grid, cf.HeadItemText);
          else
            _res = new Results(cf.ResultFile, _grid);

          if (_res.Heads == null)
            throw new Exception("Heads could not be found. Check that item: \"" + _res.HeadElevationString + "\" exists in + " + cf.ResultFile);

          ObsFileName = cf.ObservationFile;
        }
        else
        {
          OpenFileDialog Ofd = new OpenFileDialog();

          Ofd.Filter = "Known file types (*.she)|*.she";
          Ofd.ShowReadOnly = true;
          Ofd.Title = "Select a MikeShe setup file";
          

          if (DialogResult.OK == Ofd.ShowDialog())
          {
            Model MS = new Model(Ofd.FileName);
            _grid = MS.GridInfo;
            _res = MS.Results;
            Ofd.Filter = "Known file types (*.txt)|*.txt";
            Ofd.ShowReadOnly = true;
            Ofd.Title = "Select a LayerStatistics setup file";

            if (DialogResult.OK == Ofd.ShowDialog())
              ObsFileName = Ofd.FileName;
            else
              return;
          }
          else
            return;
        }

        InputOutput IO = new InputOutput(_grid.NumberOfLayers);

        //Read in the wells
        Dictionary<string, MikeSheWell> Wells = IO.ReadFromLSText(ObsFileName);

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

        //Only operate on wells within the mikeshe area
        var SelectedWells = HeadObservations.SelectByMikeSheModelArea(_grid, Wells.Values);

        //Loops the wells that are within the model area
        foreach (MikeSheWell W in SelectedWells)
        {
          if (W.Layer == -3)
          {

            W.Layer = _grid.GetLayerFromDepth(W.Column, W.Row, W.Depth );
          }
          else
          {
            W.Depth =_grid.SurfaceTopography.Data[W.Row,W.Column]- (_grid.LowerLevelOfComputationalLayers.Data[W.Row, W.Column, W.Layer] + 0.5 * _grid.ThicknessOfComputationalLayers.Data[W.Row, W.Column, W.Layer]);
          }

          //Henter de simulerede værdier
          HeadObservations.GetSimulatedValuesFromGridOutput(_res, _grid, W);

          //Samler resultaterne for hver lag
          foreach (ObservationEntry TSE in W.Intakes.First().Observations)
          {
            if (TSE.SimulatedValueCell == _res.DeleteValue)
            {
              ObsTotal[W.Layer - 1]++;
            }
            else
            {
              ME[W.Layer] += TSE.ME;
              RMSE[W.Layer] += TSE.RMSE;
              ObsUsed[W.Layer]++;
              ObsTotal[W.Layer]++;
            }
          }
        }

        //Divide with the number of observations.
        for (int i=0;i<NLay;i++)
        {
          ME[i]   = ME[i]/ObsUsed[i];
          RMSE[i] = Math.Pow(RMSE[i]/ObsUsed[i], 0.5);
        }

        //Write output
        IO.WriteObservations(SelectedWells);
        IO.WriteLayers(ME,RMSE,ObsUsed,ObsTotal);

        //Dispose MikeShe
        _grid.Dispose();
        _res.Dispose();


      }
      catch (Exception e)
      {
        MessageBox.Show("Der er opstået en fejl af typen: " + e.Message);
      } 
		}
	}
}
