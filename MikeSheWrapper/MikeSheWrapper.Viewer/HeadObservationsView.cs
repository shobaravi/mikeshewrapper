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
        HO = new HeadObservations(openFileDialog1.FileName);
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
      listBox1.Items.AddRange(HO.WorkingList.Where(w => HO.NosInBetween(w, dateTimePicker1.Value, dateTimePicker2.Value, Min)).ToArray());
      textBox4.Text = listBox1.Items.Count.ToString();
    }

    private void button4_Click(object sender, EventArgs e)
    {
      if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
      {
        HO.WriteToDfs0(folderBrowserDialog1.SelectedPath);
        HO.WriteToMikeSheModel(Path.Combine(folderBrowserDialog1.SelectedPath, "DetailedTimeSeriesImport.txt"));
      }
    }

    private void button5_Click(object sender, EventArgs e)
    {
      if (saveFileDialog1.ShowDialog() == DialogResult.OK)
      {

      }
    }

    private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
      propertyGrid1.SelectedObject = listBox1.SelectedItem;
    }

  }
}
