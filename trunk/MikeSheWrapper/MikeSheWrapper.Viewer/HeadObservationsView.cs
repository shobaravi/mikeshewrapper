using System;
using System.IO;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

using MikeSheWrapper.InputDataPreparation;
using MikeSheWrapper.Tools;
using MikeSheWrapper.JupiterTools;

namespace MikeSheWrapper.Viewer
{
  public partial class HeadObservationsView : Form
  {
    private HeadObservations HO;
    private ShapeReaderConfiguration ShpConfig = null;


    public HeadObservationsView()
    {
      InitializeComponent();
    }

    private void ReadButton_Click(object sender, EventArgs e)
    {
      if (openFileDialog1.ShowDialog() == DialogResult.OK)
      {
        string FileName=openFileDialog1.FileName;
        HO = new HeadObservations();
        textBoxObsFile.Text = "";

        textBox2.Text = FileName;
        
        switch (Path.GetExtension(FileName))
        {
          case ".she":
            HO.ReadInDetailedTimeSeries(new Model(FileName));
            break;
          case ".mdb":
            bool ReadAll = (DialogResult.Yes == MessageBox.Show("Read data for specialized NOVANA output?", "Read in how much data?", MessageBoxButtons.YesNo));
            if (ReadAll)
            {
              //JupiterTools.Reader.WellsForNovana(FileName, HO.Wells);
              buttonNovanaShape.Enabled = true;
            }
            else
            {
              JupiterTools.Reader.Wells(FileName, HO.Wells);
              JupiterTools.Reader.Waterlevels(FileName, false, HO.Wells);
            }
            textBoxObsFile.Text = FileName;
            break;
          case ".shp":
            PointShapeReader SR = new PointShapeReader(textBox2.Text);

            //Launch a data selector
            DataSelector DS = new DataSelector(SR.Data.Read());

            if (DS.ShowDialog() == DialogResult.OK)
            {
              if (ShpConfig == null)
              {
                XmlSerializer x = new XmlSerializer(typeof(ShapeReaderConfiguration));
                string InstallationPath = Path.GetDirectoryName(this.GetType().Assembly.Location);
                string config = Path.Combine(InstallationPath, "config.xml");
                using (FileStream fs = new FileStream(config, FileMode.Open))
                {
                  ShpConfig = (ShapeReaderConfiguration)x.Deserialize(fs);
                }
              }
              HO.FillInFromNovanaShape(DS.SelectedRows, ShpConfig);
              }
            else
            {
              textBox2.Text = "";
              return;
            }
            break;
          default:
            break;
        }

        textBox1.Text = HO.Wells.Count.ToString();
        listBox1.Items.AddRange(HO.WorkingList.ToArray());
        textBox4.Text = listBox1.Items.Count.ToString();
      }      
    }

    /// <summary>
    /// Read in observations
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void button2_Click(object sender, EventArgs e)
    {
      if (openFileDialog2.ShowDialog() == DialogResult.OK)
      {
        Reader.Waterlevels(openFileDialog2.FileName, false, HO.Wells);
        textBoxObsFile.Text = openFileDialog2.FileName;
      }
    }

    /// <summary>
    /// Refesh the sorting
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void button3_Click(object sender, EventArgs e)
    {
      int Min;
      //Anything else than an integer is set to zero
      if (!int.TryParse(MinNumber.Text, out Min))
        Min = 0;

      listBox1.Items.Clear();
      if (radioButtonMin.Checked)
        listBox1.Items.AddRange(HO.WorkingList.Where(w => HO.NosInBetween(w, dateTimePicker1.Value, dateTimePicker2.Value, Min)).ToArray());
      else
        listBox1.Items.AddRange(HO.WorkingList.Where(w => !HO.NosInBetween(w, dateTimePicker1.Value, dateTimePicker2.Value, Min)).ToArray());
  
      textBox4.Text = listBox1.Items.Count.ToString();
    }

    /// <summary>
    /// Write the dfs0s
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void button4_Click(object sender, EventArgs e)
    {
      if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
      {
        IEnumerable<ObservationWell> SelectedWells = listBox1.Items.Cast<ObservationWell>();
        HO.WriteToDfs0(folderBrowserDialog1.SelectedPath, SelectedWells , dateTimePicker1.Value, dateTimePicker2.Value);
        HO.WriteToMikeSheModel(Path.Combine(folderBrowserDialog1.SelectedPath, "DetailedTimeSeriesImport.txt"),SelectedWells);
        HO.WriteToDatFile(Path.Combine(folderBrowserDialog1.SelectedPath, "Timeseries.dat"), SelectedWells, dateTimePicker1.Value, dateTimePicker2.Value);
      }
    }

    /// <summary>
    /// Write to shape
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void button5_Click(object sender, EventArgs e)
    {
      if (saveFileDialog1.ShowDialog() == DialogResult.OK)
      {
        HO.WriteSimpleShape(saveFileDialog1.FileName, listBox1.Items.Cast<ObservationWell>(), dateTimePicker1.Value, dateTimePicker2.Value);
      }
    }

    private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
      propertyGrid1.SelectedObject = listBox1.SelectedItem;
    }

    private void buttonSelectMShe_Click(object sender, EventArgs e)
    {
      if (OpenSheFileForSelection.ShowDialog() == DialogResult.OK)
      {
        //HO.SelectByMikeSheModelArea(new Model(OpenSheFileForSelection.FileName).GridInfo);
        textBoxMikeSHe.Text = OpenSheFileForSelection.FileName;
      }
    }

    private void buttonLSFile_Click(object sender, EventArgs e)
    {
      if (saveFileDialog1.ShowDialog() == DialogResult.OK)
      {
        bool WriteAll = (DialogResult.Yes==MessageBox.Show("Press \"Yes\" if you want to write all values for individual time series.\nPress \"No\" if you want to write the average value of the time series.", "Average or all?", MessageBoxButtons.YesNo));
        HO.WriteToLSInput(saveFileDialog1.FileName, listBox1.Items.Cast<ObservationWell>(), dateTimePicker1.Value, dateTimePicker2.Value, WriteAll);
      }
    }


    private void WriteNovanaShape(object sender, EventArgs e)
    {
      if (saveFileDialog1.ShowDialog() == DialogResult.OK)
      {
        IEnumerable<ObservationWell> wells = listBox1.Items.Cast<ObservationWell>();
        HO.WriteShapeFromDataRow(saveFileDialog1.FileName, wells, dateTimePicker1.Value, dateTimePicker2.Value);
      }

    }
  }
}
