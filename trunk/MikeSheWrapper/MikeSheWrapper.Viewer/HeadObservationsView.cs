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
      }
      
    }

    private void button2_Click(object sender, EventArgs e)
    {
      if (openFileDialog2.ShowDialog() == DialogResult.OK)
      {
        
        HO.ReadWaterlevelsFromJupiterAccess(openFileDialog2.FileName, false);
      }
    }

    private void monthCalendar2_DateChanged(object sender, DateRangeEventArgs e)
    {
    }

    private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
    {
    }


    private void MinNumber_TextChanged(object sender, EventArgs e)
    {

    }

    private void button3_Click(object sender, EventArgs e)
    {
      int Min = int.Parse(MinNumber.Text);
      int k = HO.Wells.Values.Count(w => HO.NosInBetween(w, dateTimePicker1.Value, dateTimePicker2.Value, Min));
      textBox4.Text = k.ToString();

    }
  }
}
