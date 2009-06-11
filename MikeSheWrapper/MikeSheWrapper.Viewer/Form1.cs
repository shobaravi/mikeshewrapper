using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MikeSheWrapper.Viewer
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.OK;
      
    }

    private void button2_Click(object sender, EventArgs e)
    {
      this.graphView1.chart1.Invalidate();
      this.graphView1.chart1.DataBind();
    }
  }
}
