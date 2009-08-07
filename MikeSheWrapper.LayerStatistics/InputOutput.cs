using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
    public Dictionary<string, MikeSheWell> ReadFromLSText(string LSFileName)
    {
      Dictionary<string, MikeSheWell> Wells = new Dictionary<string, MikeSheWell>();
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
        MikeSheWell OW;

        while ((line = SR.ReadLine()) != null)
        {
          s = line.Split('\t');

          //Check that s has correct lengt and does not consist of empty entries
          if (s.Length > 5 & s.Aggregate<string>((a,b)=>a+b)!="")
          {
            try
            {
              //If the well has not already been read in create a new one
              if (!Wells.TryGetValue(s[0], out OW))
              {
                OW = new MikeSheWell(s[0]);
                IIntake I = OW.AddNewIntake(1);
                Wells.Add(OW.ID, OW);
                OW.X = double.Parse(s[1]);
                OW.Y = double.Parse(s[2]);

                //Layer is provided directly. Calculate Z
                if (s.Length >= 7 && s[6] != "")
                {
                  OW.Layer = _numberOfLayers - int.Parse(s[6]);
                }
                //Use the Z-coordinate
                else
                {
                  OW.Depth = double.Parse(s[3]);
                  OW.Layer = -3;
                }
              }
              //Now add the observation
              OW.Intakes.First().Observations.Add(new ObservationEntry(DateTime.Parse(s[5]), double.Parse(s[4])));
            }
            catch (FormatException e)
            {
              MessageBox.Show("Error reading this line:\n\n" + line +"\n\nFrom file: "+ LSFileName + "\n\nLine skipped!", "Format error!");
            }
          }
        }
      } //End of streamreader
      return Wells;
    }

        /// <summary>
    /// Skriver en fil med alle observationsdata
    /// </summary>
    /// <param name="Observations"></param>
    public void WriteObservations(IEnumerable<MikeSheWell> Wells)
    {
      StreamWriter sw = new StreamWriter(_baseOutPutFileName + "_observations.txt");
      StreamWriter swell = new StreamWriter(_baseOutPutFileName + "_wells.txt");

      sw.WriteLine("OBS_ID\tX\tY\tDepth\tLAYER\tOBS_VALUE\tDATO\tSIM_VALUE_INTP\tSIM_VALUE_CELL\tME\tME^2\t#DRY_CELLS\t#BOUNDARY_CELLS\tCOLUMN\tROW");
      swell.WriteLine("OBS_ID\tX\tY\tDepth\tLAYER\tME\tME^2");

      foreach (MikeSheWell OW in Wells)
      {
        //Write for each observation
        foreach (ObservationEntry TSE in OW.Intakes.First().Observations)
        {
          StringBuilder ObsString = new StringBuilder();
          ObsString.Append(OW.ID + "\t");
          ObsString.Append(OW.X + "\t");
          ObsString.Append(OW.Y + "\t");
          ObsString.Append(OW.Depth + "\t");

          ObsString.Append((_numberOfLayers - OW.Layer) + "\t");
          ObsString.Append(TSE.Value + "\t");
          ObsString.Append(TSE.Time.ToShortDateString() + "\t");
          ObsString.Append(TSE.SimulatedValue + "\t");
          ObsString.Append(TSE.SimulatedValueCell + "\t");
          ObsString.Append(TSE.ME + "\t");
          ObsString.Append(TSE.RMSE + "\t");
          ObsString.Append(TSE.DryCells + "\t");
          ObsString.Append(TSE.BoundaryCells + "\t");
          ObsString.Append(OW.Column + "\t");
          ObsString.Append(OW.Row + "\t");
          sw.WriteLine(ObsString.ToString());
        }

        //Write for each well
        StringBuilder WellString = new StringBuilder();
        WellString.Append(OW.ID + "\t");
        WellString.Append(OW.X + "\t");
        WellString.Append(OW.Y + "\t");
        WellString.Append(OW.Depth + "\t");
        WellString.Append((_numberOfLayers - OW.Layer) + "\t");
        WellString.Append(OW.Intakes.First().Observations.Average(num => num.ME).ToString() + "\t");
        WellString.Append(OW.Intakes.First().Observations.Average(num => num.RMSE).ToString() + "\t");
        swell.WriteLine(WellString.ToString());
      }
      sw.Flush();
      sw.Dispose();

      swell.Dispose();
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
        //Write backwards because of MSHE Layering
        for (int i = ME.Length -1 ; i >= 0;i--) 
        {
          sw.WriteLine(ME[i].ToString());
        }
      }

      //Writes a file with RMSE
      using (StreamWriter sw = new StreamWriter(_baseOutPutFileName + "_RMSE.txt"))
      {
        //Write backwards because of MSHE Layering
        for (int i = RMSE.Length - 1; i >= 0; i--)
        {
          sw.WriteLine(RMSE[i].ToString());
        }
      }

      //Writes a file with a summary for each layer
      using (StreamWriter sw = new StreamWriter(_baseOutPutFileName + "_layers.txt"))
      {
        //Writes the headline
        sw.WriteLine("Layer\tRMSE\tME\t#obs used\tobs total");
        //Write backwards because of MSHE Layering
        for (int i = ME.Length - 1; i >= 0; i--)
        {
          StringBuilder str = new StringBuilder();
          str.Append((ME.Length - i) + "\t"); //MSHE -layering
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
