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
  public partial class Preview : Form
  {
    public Preview(DataTable DT)
    {
      InitializeComponent();
      dataGridView1.DataSource = null;
      dataGridView1.DataSource = DT;
    
    }
  }
}
