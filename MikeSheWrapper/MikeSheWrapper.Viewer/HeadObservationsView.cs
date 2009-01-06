using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MikeSheWrapper.InputDataPreparation;
using MikeSheWrapper.Tools;

namespace MikeSheWrapper.Viewer
{
  public partial class HeadObservationsView : Form
  {
    private HeadObservations HO;


    public HeadObservationsView()
    {
      InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      if (openFileDialog1.ShowDialog() == DialogResult.OK)
      {
        textBox2.Text = openFileDialog1.FileName;
        if (Path.GetExtension(textBox2.Text).Equals(".shp", StringComparison.OrdinalIgnoreCase))
        {
          PointShapeReader SR = new PointShapeReader(textBox2.Text);

          DataSelector DS = new DataSelector();

          DS.Dt = SR.Data.Read();

          if (DS.ShowDialog() == DialogResult.OK)
          {
            HO = new HeadObservations();
            HO.FillInFromNovanaShape(DS.Dt.Select(DS.SelectString));
          }
          else
          {
            textBox2.Text = "";
            return;
          }
        }
        else
        {
          HO = new HeadObservations(textBox2.Text);
        }

        textBox1.Text = HO.Wells.Count.ToString();
        listBox1.Items.AddRange(HO.WorkingList.ToArray());
        textBox4.Text = listBox1.Items.Count.ToString();
      }      
    }

    private void button2_Click(object sender, EventArgs e)
    {
      if (openFileDialog2.ShowDialog() == DialogResult.OK)
      {
        HO.ReadWaterlevelsFromJupiterAccess(openFileDialog2.FileName, false);
        textBoxObsFile.Text = openFileDialog2.FileName;
      }
    }


    private void button3_Click(object sender, EventArgs e)
    {
      int Min = int.Parse(MinNumber.Text);
      listBox1.Items.Clear();
      if (radioButtonMin.Checked)
        listBox1.Items.AddRange(HO.WorkingList.Where(w => HO.NosInBetween(w, dateTimePicker1.Value, dateTimePicker2.Value, Min)).ToArray());
      else
        listBox1.Items.AddRange(HO.WorkingList.Where(w => !HO.NosInBetween(w, dateTimePicker1.Value, dateTimePicker2.Value, Min)).ToArray());
  
      textBox4.Text = listBox1.Items.Count.ToString();
    }

    private void button4_Click(object sender, EventArgs e)
    {
      if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
      {
        HO.WriteToDfs0(folderBrowserDialog1.SelectedPath, listBox1.Items.Cast<ObservationWell>(), dateTimePicker1.Value, dateTimePicker2.Value);
        HO.WriteToMikeSheModel(Path.Combine(folderBrowserDialog1.SelectedPath, "DetailedTimeSeriesImport.txt"));
      }
    }

    private void button5_Click(object sender, EventArgs e)
    {
      if (saveFileDialog1.ShowDialog() == DialogResult.OK)
      {
        HO.WriteNovanaShape(saveFileDialog1.FileName, listBox1.Items.Cast<ObservationWell>(), dateTimePicker1.Value, dateTimePicker2.Value);
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
        HO.SelectByMikeSheModelArea(new Model(OpenSheFileForSelection.FileName).GridInfo);
        textBoxMikeSHe.Text = OpenSheFileForSelection.FileName;
      }
    }

    private void buttonLSFile_Click(object sender, EventArgs e)
    {
      if (saveFileDialog1.ShowDialog() == DialogResult.OK)
      {
        bool WriteAll = (DialogResult.Yes==MessageBox.Show("Write all values for individual time series?", "Average or all?", MessageBoxButtons.YesNo));
        HO.WriteToLSInput(saveFileDialog1.FileName, listBox1.Items.Cast<ObservationWell>(), dateTimePicker1.Value, dateTimePicker2.Value, WriteAll);
      }
    }

    private void button1_Click_1(object sender, EventArgs e)
    {
      Preview pr = new Preview();
      
      pr.Show();
    }
  }
}
