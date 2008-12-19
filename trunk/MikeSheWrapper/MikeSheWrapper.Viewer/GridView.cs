using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MikeSheWrapper;

namespace MikeSheWrapper.Viewer
{
  public partial class GridView : Form
  {
    private Model _mshe;
    public GridView()
    {
      InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      if (openFileDialog1.ShowDialog() == DialogResult.OK)
      {
        textBox1.Text = openFileDialog1.FileName;
        _mshe = new Model(openFileDialog1.FileName);
      }
    }

    private void button2_Click(object sender, EventArgs e)
    {
      int column = int.Parse( dataGridView1.CurrentRow.Cells["Column"].Value.ToString());
      int row = int.Parse(dataGridView1.CurrentRow.Cells["Row"].Value.ToString());
      int layer = int.Parse(dataGridView1.CurrentRow.Cells["Layer"].Value.ToString());
      int TimeStep = int.Parse(dataGridView1.CurrentRow.Cells["TimeStep"].Value.ToString());

      //MikeShe numbering
      if (radioButton1.Checked)
      {
        column--;
        row--;
        layer = _mshe.Processed.HorizontalConductivity.Data.LayerCount - layer;
      }

      dataGridView1.CurrentRow.Cells["Head"].Value  = _mshe.Results.Heads.TimeData(TimeStep)[row, column, layer];
      dataGridView1.CurrentRow.Cells["DrainFlow"].Value = _mshe.Results.SZDrainageFlow.TimeData(TimeStep)[row, column, layer];
      dataGridView1.CurrentRow.Cells["RiverFlow"].Value = _mshe.Results.SZExchangeFlowWithRiver.TimeData(TimeStep)[row, column, layer];
      dataGridView1.CurrentRow.Cells["Extraction"].Value = _mshe.Results.GroundWaterExtraction.TimeData(TimeStep)[row, column, layer];

      dataGridView1.CurrentRow.Cells["CellBottom"].Value = _mshe.GridInfo.LowerLevelOfComputationalLayers.Data[row, column, layer];
      dataGridView1.CurrentRow.Cells["CellTop"].Value = _mshe.GridInfo.UpperLevelOfComputationalLayers.Data[row, column, layer];

      dataGridView1.CurrentRow.Cells["K_hor"].Value = _mshe.Processed.HorizontalConductivity.Data[row, column, layer];
      dataGridView1.CurrentRow.Cells["K_ver"].Value = _mshe.Processed.VerticalConductivity.Data[row, column, layer];
      dataGridView1.CurrentRow.Cells["CellThickness"].Value = _mshe.GridInfo.ThicknessOfComputationalLayers.Data[row, column, layer]; 

    }
  }
}
