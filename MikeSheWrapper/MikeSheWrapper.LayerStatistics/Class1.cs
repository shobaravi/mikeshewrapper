using System;
using System.Collections;
using System.Data;
using System.IO;

using MikeSheWrapper;

namespace MikeSheWrapper.LayerStatistics
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class Class1
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
      try
      {
        Model MS = new Model(args[0]);     
        WellReader WR = new WellReader(args[1]);

        ArrayList Wells = new ArrayList();
        ArrayList Observations = new ArrayList();
        //Indlser boringerne fra filen med observationer
        WR.read(Wells, Observations,MS);

        int NLay = MS.Processed.ThicknessOfComputationalLayers.Data.LayerCount;
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
      
        //Foretager beregningerne
        foreach(Observation O in Observations)
        {
          O.calculate();
        }
        //Samler resultaterne for hver lag
        foreach(Observation O in Observations)
        {
          if (O.ME == -9999.0)
          {
            //Uden for modelomr�det
          }  
          else if(O.SimValueCell == MS.getDeleteValue(MSHE.gridkeys.Potential))
          {
            ObsTotal[O.Layer-1]++;
          }
          else
          {
            ME[O.Layer-1] += O.ME;
            RMSE[O.Layer-1] += O.RMSE;
            ObsUsed[O.Layer-1]++;
            ObsTotal[O.Layer-1]++;

          }
        }

        for (int i=0;i<NLay;i++)
        {
          ME[i]   = ME[i]/ObsUsed[i];
          RMSE[i] = Math.Pow(RMSE[i]/ObsUsed[i], 0.5);
        }


        string path=Path.GetDirectoryName(args[1]);
        string FileName=Path.GetFileNameWithoutExtension(args[1]);
        string FilePreName=Path.Combine(path,FileName);

        FilesWriter FW=new FilesWriter(FilePreName);
        FW.WriteObservations(Observations);
        FW.WriteLayers(ME,RMSE,ObsUsed,ObsTotal);
      }
      catch (Exception e)
      {
        MessageBox.Show("Der er opst�et en fejl af typen: " + e.Message);
      } 
		}
	}
}
