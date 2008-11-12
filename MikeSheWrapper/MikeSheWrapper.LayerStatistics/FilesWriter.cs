using System;
using System.IO;
using System.Text;
using System.Collections;

namespace MikeSheWrapper.LayerStatistics

{
	/// <summary>
	/// 
	/// </summary>
	public class FilesWriter
	{
    private string FilePreName;
		public FilesWriter(string FilePreName)
		{
			this.FilePreName=FilePreName;
		}
    /// <summary>
    /// Skriver en fil med alle observationsdata
    /// </summary>
    /// <param name="Observations"></param>
		public void WriteObservations(ArrayList Observations)
		{
			using(StreamWriter sw=new StreamWriter(FilePreName+ "_observations.txt"))
			{
				sw.WriteLine("OBS_ID\tX\tY\tZ\tLAYER\tOBS_VALUE\tDATO\tSIM_VALUE_INTP\tSIM_VALUE_CELL\tME\tME^2\t#DRY_CELLS\t#BOUNDARY_CELLS\tCOLUMN\tROW");
				foreach(Observation O in Observations)
					sw.WriteLine(O.ToString());
			}
		}
    /// <summary>
    /// Skriver 3 filer med beregnede værdier for hvert lag
    /// </summary>
    /// <param name="ME"></param>
    /// <param name="RMSE"></param>
    /// <param name="ObsUsed"></param>
    /// <param name="ObsTotal"></param>
    public void WriteLayers(double[] ME, double[] RMSE, int[] ObsUsed,int [] ObsTotal)
    {
      using(StreamWriter sw=new StreamWriter(FilePreName+ "_ME.txt"))
      {
        foreach (double me in ME)
        {
          sw.WriteLine(me.ToString());
        }
      }
      using(StreamWriter sw=new StreamWriter(FilePreName+ "_RMSE.txt"))
      {
        foreach (double rmse in RMSE)
        {
          sw.WriteLine(rmse.ToString());
        }
      }
      using(StreamWriter sw=new StreamWriter(FilePreName+ "_layers.txt"))
      {
        //Writes the headline
        sw.WriteLine("Layer\tRMSE\tME\t#obs used\tobs total");
        for (int i=0;i<ME.Length;i++)
        {
          StringBuilder str=new StringBuilder();
          str.Append((i+1) +"\t");
          str.Append(RMSE[i] +"\t");
          str.Append(ME[i] +"\t");
          str.Append(ObsUsed[i] +"\t");
          str.Append(ObsTotal[i] +"\t");
          sw.WriteLine(str.ToString());
        }
        }





    }
	}
}
