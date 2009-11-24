using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;


using DHI.TimeSeries;

using MikeSheWrapper.Tools;

namespace QStationReader
{
    class Program
    {
      [STAThread]
        static void Main(string[] args)
        {
            string TextFileName="";
            string dfs0FileName="";
            if (args.Length == 0)
            {
              OpenFileDialog OFD = new OpenFileDialog();
              OFD.Title = "Select a text file with discharge data";
              if (DialogResult.OK == OFD.ShowDialog())
                TextFileName = OFD.FileName;
              else
                return;

              SaveFileDialog SFD = new SaveFileDialog();
              SFD.Title = "Select a .dfs0 file or give a new name";
              SFD.Filter = "Known file types (*.dfs0)|*.dfs0";
              SFD.OverwritePrompt = false;
              if (DialogResult.OK == SFD.ShowDialog())
                dfs0FileName = SFD.FileName;
              else 
                return;
            }
            else
            {
                TextFileName = args[0];
                dfs0FileName = args[1];
            }

            if (args.Length > 2 || !File.Exists(TextFileName))
            {
                if (DialogResult.Cancel ==
                    MessageBox.Show("This program needs two file names as input. If the file names contain spaces the filename should be embraced by \"\". \n Are these file names correct:? \n" + TextFileName + "\n" + dfs0FileName, "Two many arguments!", MessageBoxButtons.OKCancel))
                    return;
            }


            List<QStation> _stations = new List<QStation>();
            //Loop to read the Q-stations.
            using (StreamReader SR = new StreamReader(TextFileName, Encoding.Default))
            {
                string line;
                while (!SR.EndOfStream)
                {
                    line = SR.ReadLine();
                    if (line.Equals("*"))
                    {
                        QStation qs = new QStation();
                        qs.ReadEntryFromText(SR);
                        _stations.Add(qs);
                    }
                }
            }

            TSObject _data = new TSObjectClass();
            _data.Connection.FilePath = dfs0FileName;
            TSItem I = null;

            //Append to existing file
            if (File.Exists(dfs0FileName))
                _data.Connection.Open();
            else
            {
                //Create new .dfs0-file and list of q-stations
                using (StreamWriter SW = new StreamWriter(Path.Combine(Path.GetDirectoryName(dfs0FileName), "DetailedTimeSeriesImport.txt"),false,Encoding.Default)) 
                {
                    int k = 1;
                    foreach (var qs in _stations)
                    {
                        //Build the TSITEMS
                        I = new TSItemClass();
                        I.DataType = ItemDataType.Type_Float;
                        I.ValueType = ItemValueType.Instantaneous;
                        I.EumType = 2;
                        I.EumUnit = 1;

                        //Provide an ITEM name following the convention by Anker
                        if (qs.DmuMaalerNr != "")
                            I.Name = qs.DmuMaalerNr;
                        else
                            I.Name = qs.DmuStationsNr.ToString();

                        _data.Add(I);
                        SW.WriteLine(I.Name + "\t" + qs.UTMX + "\t" + qs.UTMY + "\t" + k);
                        k++;
                    }
                }
            }

            // 12 hours have been added in dfs0!
            DateTime LastTimeStep = ((DateTime)_data.Time.EndTime).Subtract(new TimeSpan(12,0,0));

            int TSCount = _data.Time.NrTimeSteps;
            int count;

            DateTime CurrentLastTimeStep = LastTimeStep;

            bool ItemFound = true;

            //Loop the stations from the text-file
            foreach (var qs in _stations)
            {
                qs.Discharge.Sort();
                //See if the station has newer data
                if (qs.Discharge.Last().Time > LastTimeStep)
                {
                    //Find the ITEM. First by DMUMAALERNR
                    try
                    {
                        I = _data.Item(qs.DmuMaalerNr);
                        ItemFound = true;
                    }
                    catch (ArgumentException E)
                    {
                        //Then by DMUSTATIONSNR
                        try
                        {
                            I = _data.Item(qs.DmuStationsNr.ToString());
                            ItemFound = true;
                        }
                        catch (ArgumentException E2)
                        {
                            Console.WriteLine("DMU MÅLER Nr: " + qs.DmuMaalerNr + " eller DMU sted nr: " + qs.DmuStationsNr + " blev ikke fundet i dfs0.filen"); 
                            ItemFound = false;
                        }
                    }

                    //Write to the item if it exists
                    if (ItemFound)
                    {
                        //Start at the last entry of the original
                        int index = qs.Discharge.FindIndex(var => var.Time > LastTimeStep);
                        count = 0;

                        //Loop all the entries
                        for (int i = index; i < qs.Discharge.Count; i++)
                        {
                            count++;
                            //Check if it is necessary to add timesteps to the tsobject
                            if (qs.Discharge[i].Time > CurrentLastTimeStep)
                            {
                                CurrentLastTimeStep = qs.Discharge[i].Time;
                                _data.Time.AddTimeSteps(1);
                                // 12 hours have been added in dfs0!
                                _data.Time.SetTimeForTimeStepNr(TSCount + count, qs.Discharge[i].Time.AddHours(12));
                            }
                            //Get the index in the time series. We cannot be sure that the times are equidistant in the textfile
                            int tsn = _data.Time.GetTimeStepNrAfter(qs.Discharge[i].Time);
                            I.SetDataForTimeStepNr(tsn, (float)qs.Discharge[i].Value);
                        }
                    }
                }
            }
            _data.Connection.Save();
        }
    }
}
