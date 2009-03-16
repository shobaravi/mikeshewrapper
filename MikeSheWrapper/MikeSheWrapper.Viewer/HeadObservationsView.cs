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
    private ShapeReaderConfiguration ShpConfig = null;
    private Dictionary<string, IWell> Wells;
    private List<Plant> Plants;
    private List<IIntake> Intakes;

    public HeadObservationsView()
    {
      InitializeComponent();
    }

    private void ReadButton_Click(object sender, EventArgs e)
    {
      if (openFileDialog1.ShowDialog() == DialogResult.OK)
      {
        string FileName=openFileDialog1.FileName;
        textBoxObsFile.Text = "";

        textBox2.Text = FileName;
        
        switch (Path.GetExtension(FileName))
        {
          case ".she":
            foreach(IWell W in HeadObservations.ReadInDetailedTimeSeries(new Model(FileName)))
              Wells.Add(W.ID,W);
            break;
          case ".mdb":
            bool ReadAll = (DialogResult.Yes == MessageBox.Show("Read data for specialized NOVANA output?", "Read in how much data?", MessageBoxButtons.YesNo));
            JupiterTools.Reader R = new Reader(FileName);

            if (ReadAll)
            {
              Wells = R.WellsForNovana();
              Plants = new List<Plant>();
              R.Extraction(Plants, Wells);
              buttonNovanaShape.Enabled = true;
            }
            else
            {
              Wells = R.Wells();
              R.Waterlevels(Wells);
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
              Wells = HeadObservations.FillInFromNovanaShape(DS.SelectedRows, ShpConfig);
              }
            else
            {
              textBox2.Text = "";
              return;
            }
            this.textBoxObsFile.Enabled = true;
            this.buttonReadObs.Enabled = true;
            break;
          default:
            break;
        }

         
        if (Wells!=null)
          listBox1.Items.AddRange(Wells.Values.ToArray());

        if (Plants != null)
          listBoxAnlaeg.Items.AddRange(Plants.ToArray());
        textBox1.Text = listBoxIntakes.Items.Count.ToString();
        textBox4.Text = listBoxIntakes.Items.Count.ToString();
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
        Reader R = new Reader(openFileDialog2.FileName);
        R.Waterlevels(Wells);
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

      listBoxIntakes.Items.Clear();
      if (radioButtonMin.Checked)
        listBoxIntakes.Items.AddRange(Intakes.Where(w => HeadObservations.NosInBetween(w, dateTimePicker1.Value, dateTimePicker2.Value, Min)).ToArray());
      else
        listBoxIntakes.Items.AddRange(Intakes.Where(w => !HeadObservations.NosInBetween(w, dateTimePicker1.Value, dateTimePicker2.Value, Min)).ToArray());
  
      textBox4.Text = listBoxIntakes.Items.Count.ToString();
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
        IEnumerable<IIntake> SelectedWells = listBoxIntakes.Items.Cast<IIntake>();
        HeadObservations.WriteToDfs0(folderBrowserDialog1.SelectedPath, SelectedWells , dateTimePicker1.Value, dateTimePicker2.Value);
        HeadObservations.WriteToMikeSheModel(Path.Combine(folderBrowserDialog1.SelectedPath, "DetailedTimeSeriesImport.txt"), SelectedWells);
        HeadObservations.WriteToDatFile(Path.Combine(folderBrowserDialog1.SelectedPath, "Timeseries.dat"), SelectedWells, dateTimePicker1.Value, dateTimePicker2.Value);
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
        HeadObservations.WriteSimpleShape(saveFileDialog1.FileName, listBoxIntakes.Items.Cast<IIntake>(), dateTimePicker1.Value, dateTimePicker2.Value);
      }
    }

    private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
      propertyGrid1.SelectedObject = listBoxIntakes.SelectedItem;
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
        HeadObservations.WriteToLSInput(saveFileDialog1.FileName, listBoxIntakes.Items.Cast<IIntake>(), dateTimePicker1.Value, dateTimePicker2.Value, WriteAll);
      }
    }


    private void WriteNovanaShape(object sender, EventArgs e)
    {
      if (saveFileDialog1.ShowDialog() == DialogResult.OK)
      {
        HeadObservations.WriteShapeFromDataRow(saveFileDialog1.FileName, listBoxIntakes.Items.Cast<JupiterIntake>(), dateTimePicker1.Value, dateTimePicker2.Value);
      }

    }

    private void HeadObservationsView_Load(object sender, EventArgs e)
    {

    }
  }
}
