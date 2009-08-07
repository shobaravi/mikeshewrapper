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
    Dictionary<int, Plant> DPlants;
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
      openFileDialog2.Filter = "Known file types (*.mdb)|*.mdb";
      this.openFileDialog2.ShowReadOnly = true;
      this.openFileDialog2.Title = "Select an Access file with data in JupiterXL format";

      if (openFileDialog2.ShowDialog() == DialogResult.OK)
      {
        JupiterFilter jd = new JupiterFilter();

        jd.ReadWells = (Wells == null);

        string FileName = openFileDialog2.FileName;
        if (DialogResult.OK == jd.ShowDialog())
        {
          JupiterReader = new Reader(FileName);

          if (Wells == null)
          {
            Wells = JupiterReader.WellsForNovana(jd.ReadLithology, jd.ReadPejlinger, jd.ReadChemistry);
          }
          else
          {
              if (jd.ReadPejlinger)
              {
                  JupiterReader.Waterlevels(Wells);
              }
          }

          if (jd.ReadExtration)
          {
            if (DPlants == null)
              DPlants = JupiterReader.ReadPlants(Wells);
  
            JupiterReader.FillInExtraction(DPlants);
            buttonNovanaExtract.Enabled = true;
            buttonMsheExt.Enabled = true;
          }
          if (jd.ReadPejlinger)
          {
              buttonNovanaShape.Enabled = true;
              buttonLSFile.Enabled = true;
              buttonMSheObs.Enabled = true;
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
      {
        listBoxWells.Items.Clear();
        listBoxWells.Items.AddRange(Wells.Values.OrderBy(var => var.ID).ToArray());
        textBoxWellsNumber.Text = listBoxWells.Items.Count.ToString();

        Intakes = new List<IIntake>();
        foreach (IWell W in Wells.Values)
           Intakes.AddRange(W.Intakes);

        listBoxIntakes.Items.Clear();
        listBoxIntakes.Items.AddRange(Intakes.OrderBy(var=>var.ToString()).ToArray());
        textBox4.Text = listBoxIntakes.Items.Count.ToString();

      }

      if (DPlants != null)
      {
        listBoxAnlaeg.Items.Clear();
        listBoxAnlaeg.Items.AddRange(DPlants.Values.OrderBy(var => var.ToString()).ToArray());
        radioButton2.Enabled = true;
        textBoxPlantCount.Text = listBoxAnlaeg.Items.Count.ToString();
      }
    }

    /// <summary>
    /// Opens a point shape
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void button2_Click(object sender, EventArgs e)
    {
        openFileDialog2.Filter = "Known file types (*.shp)|*.shp";
        this.openFileDialog2.ShowReadOnly = true;
        this.openFileDialog2.Title = "Select a shape file with data for wells or intakes";

        if (openFileDialog2.ShowDialog() == DialogResult.OK)
        {
            string FileName = openFileDialog2.FileName;

            PointShapeReader SR = new PointShapeReader(FileName);

            DataTable FullDataSet = SR.Data.Read();
            //Launch a data selector
            DataSelector DS = new DataSelector(FullDataSet);

            if (DS.ShowDialog() == DialogResult.OK)
            {
                if (ShpConfig == null)
                {
                    XmlSerializer x = new XmlSerializer(typeof(ShapeReaderConfiguration));
                    string InstallationPath = Path.GetDirectoryName(this.GetType().Assembly.Location);
                    string config = Path.Combine(InstallationPath, "ShapeReaderConfig.xml");
                    using (FileStream fs = new FileStream(config, FileMode.Open))
                    {
                        ShpConfig = (ShapeReaderConfiguration)x.Deserialize(fs);

                        if (CheckColumn(FullDataSet, ShpConfig.WellIDHeader, config))
                            if (CheckColumn(FullDataSet, ShpConfig.IntakeNumber, config))
                                if (CheckColumn(FullDataSet, ShpConfig.XHeader, config))
                                    if (CheckColumn(FullDataSet, ShpConfig.YHeader, config))
                                        if (CheckColumn(FullDataSet, ShpConfig.TOPHeader, config))
                                          if (CheckColumn(FullDataSet, ShpConfig.BOTTOMHeader, config))
                                          {
                                            Wells = new Dictionary<string, IWell>();
                                            if (FullDataSet.Columns.Contains(ShpConfig.PlantIDHeader))
                                            {
                                              DPlants = new Dictionary<int, Plant>();
                                              HeadObservations.FillInFromNovanaShape(DS.SelectedRows, ShpConfig, Wells, DPlants);
                                            }
                                            else
                                              HeadObservations.FillInFromNovanaShape(DS.SelectedRows, ShpConfig, Wells);
                                            UpdateListsAndListboxes();
                                          }
                    }
                }
            }
            SR.Dispose();
        }
    }

    private bool CheckColumn(DataTable DT, string ColumnName, string config)
    {
      if (!DT.Columns.Contains(ColumnName))
      {
        MessageBox.Show("The column: " + ColumnName + " is not found in the shape file. Check that the column names defined in: " + config + " corresponds to the shape file you are trying to load", "Error loading data from shape file");
        return false;
      }
      return true;
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
          listBoxIntakes.Items.AddRange(Intakes.Where(w => HeadObservations.NosInBetween(w, dateTimePicker1.Value, dateTimePicker2.Value, Min)).OrderBy(var => var.ToString()).ToArray());
        else
          listBoxIntakes.Items.AddRange(Intakes.Where(w => !HeadObservations.NosInBetween(w, dateTimePicker1.Value, dateTimePicker2.Value, Min)).OrderBy(var => var.ToString()).ToArray());
      }
      textBox4.Text = listBoxIntakes.Items.Count.ToString();
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


  
    private void WriteNovanaShape(object sender, EventArgs e)
    {
      if (saveFileDialog1.ShowDialog() == DialogResult.OK)
      {
        JupiterReader.AddDataForNovanaPejl(listBoxIntakes.Items.Cast<JupiterIntake>());
        HeadObservations.WriteShapeFromDataRow(saveFileDialog1.FileName, listBoxIntakes.Items.Cast<JupiterIntake>());
      }
    }


    private void listBoxWells_SelectedIndexChanged(object sender, EventArgs e)
    {
      propertyWells.SelectedObject = listBoxWells.SelectedItem;
    }

    private void listBoxAnlaeg_SelectedIndexChanged_1(object sender, EventArgs e)
    {
      if (radioButton2.Checked)
        ListWellsAttachedToPlant();
      propertyGridPlants.SelectedObject = listBoxAnlaeg.SelectedItem;
    }

    private void ListWellsAttachedToPlant()
    {
      listBoxWells.Items.Clear();
      if (radioButton2.Checked)
      {
        if (listBoxAnlaeg.SelectedItem!=null)
          listBoxWells.Items.AddRange(((Plant)listBoxAnlaeg.SelectedItem).PumpingWells.OrderBy(var => var.ID).ToArray());
      }
      else
      {
        listBoxWells.Items.AddRange(Wells.Values.OrderBy(var=>var.ID).ToArray());
      }

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

      if (DPlants != null)
      {
        listBoxAnlaeg.Items.Clear();

        if (MinVal == 0)
          listBoxAnlaeg.Items.AddRange(DPlants.Values.OrderBy(var => var.Name).ToArray());
        else
        {
          List<Plant> Slist = new List<Plant>();
          foreach (Plant P in DPlants.Values)
          {
            if (P.Extractions.Count > 0)
            {
              var ReducedList = P.Extractions.Where(var2 => HeadObservations.InBetween(var2, dateTimeStartExt.Value, dateTimeEndExt.Value));
              if (ReducedList.Count() > 0)
                if (ReducedList.Average(var => var.Value) >= MinVal)
                  Slist.Add(P);
            }
          }
          listBoxAnlaeg.Items.AddRange(Slist.OrderBy(var => var.ToString()).ToArray());          
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
        IEnumerable<Plant> plants =listBoxAnlaeg.Items.Cast<Plant>();
        IEnumerable<JupiterIntake> intakes = JupiterReader.AddDataForNovanaExtraction(plants, dateTimeStartExt.Value, dateTimeEndExt.Value);
        HeadObservations.WriteShapeFromDataRow(saveFileDialog1.FileName, intakes);

        IEnumerable<Plant> PlantWithoutIntakes = plants.Where(var => var.PumpingIntakes.Count == 0);
        if (PlantWithoutIntakes.Count()>0)
          if (DialogResult.Yes == MessageBox.Show("The list contains plants with no intakes attached. Should these be written to a new shape-file?", "Plants without intakes!", MessageBoxButtons.YesNo))
          {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                NovanaTables.IndvindingerDataTable dt = JupiterReader.FillPlantData(PlantWithoutIntakes, dateTimeStartExt.Value, dateTimeEndExt.Value);
                PointShapeWriter PSW = new PointShapeWriter(saveFileDialog1.FileName);
                PSW.WritePointShape(dt, dt.ANLUTMXColumn.ColumnName, dt.ANLUTMYColumn.ColumnName);
                PSW.Dispose();
            }

          }
      }

    }

    private void radioButton1_CheckedChanged(object sender, EventArgs e)
    {
      ListWellsAttachedToPlant();
    }

    private void radioButtonMax_CheckedChanged(object sender, EventArgs e)
    {
      SelectObservations();
    }

    private void buttonReadMshe_Click(object sender, EventArgs e)
    {
      openFileDialog2.Filter = "Known file types (*.she)|*.she";
      openFileDialog2.ShowReadOnly = true;
      openFileDialog2.Title = "Select a .she file with MikeShe setup";

      if (openFileDialog2.ShowDialog() == DialogResult.OK)
      {
        Model M = new Model(openFileDialog2.FileName);

        //Wells have already been read. Make a model domain selection
        if (Wells != null)
        {
          int Column;
          int Row;
          List<IWell> WellsOutSideModelDomain = new List<IWell>();
          foreach (IWell W in Wells.Values)
          {
            if (!M.GridInfo.GetIndex(W.X, W.Y, out Column, out Row))
              WellsOutSideModelDomain.Add(W);
          }

          if (WellsOutSideModelDomain.Count > 0)
          {
            if (DialogResult.Yes == MessageBox.Show(WellsOutSideModelDomain.Count + " wells found outside horizontal MikeShe model domain.\n Remove these wells from list?", "Wells outside model domain", MessageBoxButtons.YesNo))
            {
              foreach (IWell W in WellsOutSideModelDomain)
              {
                Wells.Remove(W.ID);
              }
            }
          }
        }
        else
        {
          Wells = new Dictionary<string, IWell>();
          foreach (IWell W in HeadObservations.ReadInDetailedTimeSeries(M))
            Wells.Add(W.ID, W);
        }
        UpdateListsAndListboxes();
      }
    }

    private void buttonMsheExt_Click(object sender, EventArgs e)
    {
      if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
      {
          HeadObservations.WriteExtractionDFS0(folderBrowserDialog1.SelectedPath, listBoxAnlaeg.Items.Cast<Plant>(), dateTimeStartExt.Value, dateTimeEndExt.Value);
      }

    }

    private void buttonLSFile_Click_1(object sender, EventArgs e)
    {
        if (saveFileDialog1.ShowDialog() == DialogResult.OK)
        {
            bool WriteAll = (DialogResult.Yes == MessageBox.Show("Press \"Yes\" if you want to write all values for individual time series.\nPress \"No\" if you want to write the average value of the time series.", "Average or all?", MessageBoxButtons.YesNo));
            HeadObservations.WriteToLSInput(saveFileDialog1.FileName, listBoxIntakes.Items.Cast<IIntake>(), dateTimePicker1.Value, dateTimePicker2.Value, WriteAll);
        }

    }

    private void buttonMSheObs_Click(object sender, EventArgs e)
    {
        if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
        {
            IEnumerable<IIntake> SelectedWells = listBoxIntakes.Items.Cast<IIntake>();
          
            int TotalDfs0 = SelectedWells.Count();
            progressBar1.Maximum = TotalDfs0;
            progressBar1.Value = 0;
            progressBar1.Visible = true;
            labelProgBar.Visible = true;
            this.Refresh();
          foreach (IIntake I in SelectedWells)
            {
              progressBar1.Value++;
              HeadObservations.WriteToDfs0(folderBrowserDialog1.SelectedPath, I, dateTimePicker1.Value, dateTimePicker2.Value);
            }

          HeadObservations.WriteToMikeSheModel(folderBrowserDialog1.SelectedPath, SelectedWells, dateTimePicker1.Value, dateTimePicker2.Value);
            HeadObservations.WriteToDatFile(Path.Combine(folderBrowserDialog1.SelectedPath, "Timeseries.dat"), SelectedWells, dateTimePicker1.Value, dateTimePicker2.Value);
        }
        progressBar1.Visible = false;
        labelProgBar.Visible = false;

    }
  }
}
