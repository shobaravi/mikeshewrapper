using System;
using System.Collections;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Xml.Serialization;

using MikeSheWrapper;
using MikeSheWrapper.Tools;
using MikeSheWrapper.InputDataPreparation;

namespace MikeSheWrapper.LayerStatistics
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
      try
      {
        MikeSheGridInfo _grid;
        Results _res;
        string ObsFileName;

        if (args.Length == 2)
        {
          Model MS = new Model(args[0]);
          _grid = MS.GridInfo;
          _res = MS.Results;
          ObsFileName = args[1];
        }
        else
        {
          XmlSerializer x = new XmlSerializer(typeof (Configuration));
          Configuration cf = (Configuration) x.Deserialize(new FileStream(args[0], FileMode.Open));
          _grid = new MikeSheGridInfo(cf.PreProcessedDFS3, cf.PreProcessedDFS2);
          _res = new Results(cf.ResultFile, _grid);
          ObsFileName = cf.ObservationFile;
        }

        HeadObservations HO = new HeadObservations();
        InputOutput IO = new InputOutput();

        IO.ReadFromLSText(ObsFileName, HO);

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
        HO.StatisticsFromGridOutput(_res, _grid);
      
        //Samler resultaterne for hver lag
        foreach (ObservationWell OW in HO.WorkingList)
        {
          foreach (TimeSeriesEntry TSE in OW.Observations)
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


        IO.WriteObservations(HO);
        IO.WriteLayers(ME,RMSE,ObsUsed,ObsTotal);
      }
      catch (Exception e)
      {
        MessageBox.Show("Der er opst�et en fejl af typen: " + e.Message);
      } 
		}
	}
}
