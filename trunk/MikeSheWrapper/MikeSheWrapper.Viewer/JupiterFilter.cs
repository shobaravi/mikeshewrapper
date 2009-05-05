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
  public partial class JupiterFilter : Form
  {
    public JupiterFilter()
    {
      InitializeComponent();
    }

    public bool ReadWells
    {
      set
      {
        checkBoxWell.Checked = value;
      }
    }

   
    public bool ReadPejlinger
    {
      get
      {
        return checkBoxPejlinger.Checked;
      }
    }

    public bool ReadChemistry
    {
      get
      {
        return checkBoxKemi.Checked;
      }
    }

    public bool ReadExtration
    {
      get
      {
        return checkBoxIndvinding.Checked;
      }
    }

    public bool ReadLithology
    {
      get
      {
        return checkBoxLitologi.Checked;
      }
    }

    private void button1_Click(object sender, EventArgs e)
    {
      this.Close();
      this.DialogResult = DialogResult.OK;
    }

    private void button2_Click(object sender, EventArgs e)
    {
      this.Close();
      this.DialogResult = DialogResult.Cancel;
    }
  }
}
