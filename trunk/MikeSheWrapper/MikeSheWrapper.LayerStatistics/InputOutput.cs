using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MikeSheWrapper.Tools;
using MikeSheWrapper.InputDataPreparation;

namespace MikeSheWrapper.LayerStatistics
{
  /// <summary>
  /// This class reads the input file and writes the output files for layer statistics. It uses a HeadObservation object
  /// and reads into the Wells and writes from the Working list.
  /// </summary>
  public class InputOutput
  {

    private string _baseOutPutFileName;
    private int _numberOfLayers;

    public InputOutput(int NumberOfLayers)
    {
      _numberOfLayers = NumberOfLayers;
    }

    public string BaseOutPutFileName
    {
      get { return _baseOutPutFileName; }
      set { _baseOutPutFileName = value; }
    }

    /// <summary>
    /// Reads in head observations from txt file with this format.
    /// "WellID X Y Z Head  Date  Layer". Separated with tabs. Layer is optional
    /// </summary>
    /// <param name="LSFileName"></param>
    public void ReadFromLSText(string LSFileName, HeadObservations Obs)
    {
      //Sets the output file name for subsequent writing
      string path = Path.GetDirectoryName(LSFileName);
      string FileName = Path.GetFileNameWithoutExtension(LSFileName);
      _baseOutPutFileName = Path.Combine(path, FileName);

      //Now read the input
      using (StreamReader SR = new StreamReader(LSFileName))
      {
        //Reads the HeadLine
        string line = SR.ReadLine();
        string[] s;
        ObservationWell OW;

        while ((line = SR.ReadLine()) != null)
        {
          s = line.Split('\t');
          if (s.Length > 5)
          {
            try
            {
              //If the well has not already been read in create a new one
              if (!Obs.Wells.TryGetValue(s[0], out OW))
              {
                OW = new ObservationWell(s[0]);
                Obs.Wells.Add(OW.ID, OW);
                OW.X = double.Parse(s[1]);
                OW.Y = double.Parse(s[2]);

                //Layer is provided directly. Calculate Z
                if (s.Length > 7 && s[6] != "")
                {
                  OW.Layer = _numberOfLayers - int.Parse(s[6]);
                }
                //Use the Z-coordinate
                else
                {
                  OW.Z = double.Parse(s[3]);
                }
              }
              //Now add the observation
              OW.Observations.Add(new TimeSeriesEntry(DateTime.Parse(s[5]), double.Parse(s[4])));
            }
            catch (FormatException e)
            {
              throw new Exception("Error reading input-file: " + LSFileName, e);
            }
          }
        }
      }
    }

        /// <summary>
    /// Skriver en fil med alle observationsdata
    /// </summary>
    /// <param name="Observations"></param>
		public void WriteObservations(HeadObservations Obs)
		{
      using (StreamWriter sw = new StreamWriter(_baseOutPutFileName + "_observations.txt"))
			{
				sw.WriteLine("OBS_ID\tX\tY\tZ\tLAYER\tOBS_VALUE\tDATO\tSIM_VALUE_INTP\tSIM_VALUE_CELL\tME\tME^2\t#DRY_CELLS\t#BOUNDARY_CELLS\tCOLUMN\tROW");
        foreach (ObservationWell OW in Obs.WorkingList)
        {
          foreach (TimeSeriesEntry TSE in OW.Observations)
          {
            StringBuilder ObsString=new StringBuilder();
            ObsString.Append(OW.ID +"\t");
            ObsString.Append(OW.X +"\t");
            ObsString.Append(OW.Y +"\t");
            ObsString.Append(OW.Z +"\t");

            ObsString.Append(OW.Layer +"\t");
            ObsString.Append(TSE.Value +"\t");
            ObsString.Append(TSE.Time.ToShortDateString() +"\t");
            ObsString.Append(TSE.SimulatedValueInterpolated +"\t");
            ObsString.Append(TSE.SimulatedValueCell +"\t");
            ObsString.Append(TSE.ME +"\t");
            ObsString.Append(TSE.RMSE +"\t");
            ObsString.Append(TSE.DryCells +"\t");		
            ObsString.Append(TSE.BoundaryCells +"\t");
            //MikeShe numbering is 1-based
            ObsString.Append((OW.Column + 1) +"\t");
            ObsString.Append((OW.Row + 1) + "\t");
            sw.WriteLine(ObsString.ToString());
          }
        }
			}
		}
    /// <summary>
    /// Skriver 3 filer med beregnede værdier for hvert lag
    /// </summary>
    /// <param name="ME"></param>
    /// <param name="RMSE"></param>
    /// <param name="ObsUsed"></param>
    /// <param name="ObsTotal"></param>
    public void WriteLayers(double[] ME, double[] RMSE, int[] ObsUsed, int[] ObsTotal)
    {
      //Writes a file with ME
      using (StreamWriter sw = new StreamWriter(_baseOutPutFileName + "_ME.txt"))
      {
        foreach (double me in ME)
        {
          sw.WriteLine(me.ToString());
        }
      }

      //Writes a file with RMSE
      using (StreamWriter sw = new StreamWriter(_baseOutPutFileName + "_RMSE.txt"))
      {
        foreach (double rmse in RMSE)
        {
          sw.WriteLine(rmse.ToString());
        }
      }

      //Writes a file with a summary for each layer
      using (StreamWriter sw = new StreamWriter(_baseOutPutFileName + "_layers.txt"))
      {
        //Writes the headline
        sw.WriteLine("Layer\tRMSE\tME\t#obs used\tobs total");
        for (int i = 0; i < ME.Length; i++)
        {
          StringBuilder str = new StringBuilder();
          str.Append((i + 1) + "\t");
          str.Append(RMSE[i] + "\t");
          str.Append(ME[i] + "\t");
          str.Append(ObsUsed[i] + "\t");
          str.Append(ObsTotal[i] + "\t");
          sw.WriteLine(str.ToString());
        }
      }


    }


  }
}
