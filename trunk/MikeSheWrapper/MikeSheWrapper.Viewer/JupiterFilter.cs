﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MikeSheWrapper.Viewer
{
  public partial class JupiterFilter : Form
  {
    public JupiterFilter()
    {
      InitializeComponent();
    }

    public bool ReadNovana
    {
      get
      {
        return this.checkBoxNovana.Checked;
      }
    }

    private void button1_Click(object sender, EventArgs e)
    {
      this.Close();
      this.DialogResult = DialogResult.OK;
    }
  }
}
