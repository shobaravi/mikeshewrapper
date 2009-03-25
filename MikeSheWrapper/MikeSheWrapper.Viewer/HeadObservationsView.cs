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
    private JupiterTools.Reader JupiterReader;

    public HeadObservationsView()
    {
      InitializeComponent();
    }

    /// <summary>
    /// Opens a Jupiter database and reads requested data
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ReadButton_Click(object sender, EventArgs e)
    {
      openFileDialog1.Filter = "Known file types (*.mdb)|*.mdb";
      this.openFileDialog1.ShowReadOnly = true;
      this.openFileDialog1.Title = "Select an Access file with data in JupiterXL format";

      if (openFileDialog1.ShowDialog() == DialogResult.OK)
      {
        JupiterFilter jd = new JupiterFilter();
        string FileName = openFileDialog1.FileName;
        if (DialogResult.OK == jd.ShowDialog())
        {
          JupiterReader = new Reader(FileName);

          Wells = JupiterReader.WellsForNovana(jd.ReadLithology, jd.ReadPejlinger, jd.ReadChemistry);

          if (jd.ReadExtration)
          {
            Plants = JupiterReader.Extraction(Wells).ToList<Plant>();
          }

          UpdateListsAndListboxes();
        }
      }
    }

    /// <summary>
    /// Updates the list boxes with data from the lists. Also build the intakes list
    /// </summary>
    private void UpdateListsAndListboxes()
    {
      if (Wells != null)
        listBoxWells.Items.AddRange(Wells.Values.ToArray());

      if (Intakes == null)
        Intakes = new List<IIntake>();

      foreach (IWell W in Wells.Values)
        Intakes.AddRange(W.Intakes);

      listBoxIntakes.Items.AddRange(Intakes.ToArray());

      if (Plants != null)
        listBoxAnlaeg.Items.AddRange(Plants.ToArray());

      textBoxPlantCount.Text = listBoxAnlaeg.Items.Count.ToString();
      textBoxWellsNumber.Text = listBoxWells.Items.Count.ToString();
      textBox4.Text = listBoxIntakes.Items.Count.ToString();

    }

    /// <summary>
    /// Opens a point shape
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void button2_Click(object sender, EventArgs e)
    {
      openFileDialog1.Filter = "Known file types (*.shp)|*.shp";
      this.openFileDialog1.ShowReadOnly = true;
      this.openFileDialog1.Title = "Select a shape file with data for wells or intakes";

      if (openFileDialog1.ShowDialog() == DialogResult.OK)
      {
        string FileName = openFileDialog1.FileName;

        PointShapeReader SR = new PointShapeReader(FileName);

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
              Wells = HeadObservations.FillInFromNovanaShape(DS.SelectedRows, ShpConfig);
            }
          }
        }
      }
    }

         

    /// <summary>
    /// Refesh the sorting
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void SelectObservations()
    {
      int Min;
      //Anything else than an integer is set to zero
      if (!int.TryParse(MinNumber.Text, out Min))
      {
        Min = 0;
        MinNumber.Text = "0";
      }

      if (Intakes != null)
      {
        listBoxIntakes.Items.Clear();
        if (radioButtonMin.Checked)
          listBoxIntakes.Items.AddRange(Intakes.Where(w => HeadObservations.NosInBetween(w, dateTimePicker1.Value, dateTimePicker2.Value, Min)).ToArray());
        else
          listBoxIntakes.Items.AddRange(Intakes.Where(w => !HeadObservations.NosInBetween(w, dateTimePicker1.Value, dateTimePicker2.Value, Min)).ToArray());
      }
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

    private void listBoxIntake_SelectedIndexChanged(object sender, EventArgs e)
    {
      propertyGrid1.SelectedObject = listBoxIntakes.SelectedItem;
    }

    private void buttonSelectMShe_Click(object sender, EventArgs e)
    {
      if (OpenSheFileForSelection.ShowDialog() == DialogResult.OK)
      {
        //HO.SelectByMikeSheModelArea(new Model(OpenSheFileForSelection.FileName).GridInfo);
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
        JupiterReader.AddDataForNovanaPejl(listBoxIntakes.Items.Cast<JupiterIntake>());
        HeadObservations.WriteShapeFromDataRow(saveFileDialog1.FileName, listBoxIntakes.Items.Cast<JupiterIntake>(), dateTimePicker1.Value, dateTimePicker2.Value);
      }

    }


    private void listBoxWells_SelectedIndexChanged(object sender, EventArgs e)
    {
      propertyWells.SelectedObject = listBoxWells.SelectedItem;
    }

    private void listBoxAnlaeg_SelectedIndexChanged_1(object sender, EventArgs e)
    {
      listBoxWells.Items.Clear();
      listBoxWells.Items.AddRange(((Plant)listBoxAnlaeg.SelectedItem).PumpingWells.ToArray());
      propertyGridPlants.SelectedObject = listBoxAnlaeg.SelectedItem;
      textBoxWellsNumber.Text = listBoxWells.Items.Count.ToString();
    }

    private void SelectExtrations()
    {
      double MinVal;
      if (!double.TryParse(textBoxMeanYearlyExt.Text, out MinVal))
      {
        MinVal = 0;
        textBoxMeanYearlyExt.Text = "0";
      }

      if (Plants != null)
      {
        listBoxAnlaeg.Items.Clear();

        if (MinVal == 0)
          listBoxAnlaeg.Items.AddRange(Plants.ToArray());
        else
        {
          foreach (Plant P in Plants)
          {
            if (P.Extractions.Count > 0)
            {
              var ReducedList = P.Extractions.Where(var2 => HeadObservations.InBetween(var2, dateTimeStartExt.Value, dateTimeEndExt.Value));
              if (ReducedList.Count() > 0)
                if (ReducedList.Average(var => var.Value) >= MinVal)
                  listBoxAnlaeg.Items.Add(P);
            }
          }
        }
        textBoxPlantCount.Text = listBoxAnlaeg.Items.Count.ToString();
      }
    }

    private void textBox2_Validating(object sender, CancelEventArgs e)
    {
      SelectExtrations();
    }

    private void textBoxMeanYearlyExt_KeyUp(object sender, KeyEventArgs e)
    {
      if (e.KeyValue == 13)
        SelectExtrations();
    }

    private void MinNumber_Validating(object sender, CancelEventArgs e)
    {
      SelectObservations();
    }

    private void MinNumber_KeyUp(object sender, KeyEventArgs e)
    {
       if (e.KeyValue == 13)
         SelectObservations();
    }

    private void button3_Click(object sender, EventArgs e)
    {
      if (saveFileDialog1.ShowDialog() == DialogResult.OK)
      {
        IEnumerable<JupiterIntake> intakes =  JupiterReader.AddDataForNovanaExtraction(listBoxAnlaeg.Items.Cast<Plant>(), dateTimeStartExt.Value,dateTimeEndExt.Value);
        HeadObservations.WriteShapeFromDataRow(saveFileDialog1.FileName, intakes, dateTimePicker1.Value, dateTimePicker2.Value);
      }

    }
  }
}
