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
  public partial class DataSelector : Form
  {
    DataTable _dt;

    public DataTable Dt
    {
      get { return _dt; }
      set { _dt = value;
      foreach (DataColumn DC in _dt.Columns)
        listBox1.Items.Add(DC.ColumnName);
      }
    }

    public DataSelector()
    {
      InitializeComponent();
      this.listBox1.DoubleClick += new EventHandler(listBox1_DoubleClick);
      this.listBoxUniqueValues.DoubleClick += new EventHandler(listBoxUniqueValues_DoubleClick);
    }

    void listBox1_DoubleClick(object sender, EventArgs e)
    {
      richTextBoxSelectString.Text += ((ListBox)sender).SelectedItem.ToString();
    }

    void listBoxUniqueValues_DoubleClick(object sender, EventArgs e)
    {
      richTextBoxSelectString.Text += "'" + ((ListBox)sender).SelectedItem.ToString() + "'";
    }

    public string SelectString
    {
      get
      {
        return richTextBoxSelectString.Text;
      }
    }

    public string[] Fields
    {
      set
      {
        listBox1.Items.AddRange(value);
      }
    }


    private void OkButton_Click(object sender, EventArgs e)
    {
      DialogResult = DialogResult.OK;
      this.Close();
    }

    private void CancelButton_Click(object sender, EventArgs e)
    {
      DialogResult = DialogResult.Cancel;
      this.Close();
    }

    private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
      listBoxUniqueValues.Items.Clear();
    }

    private void EqualButton_Click(object sender, EventArgs e)
    {
      richTextBoxSelectString.Text += "=";
    }

    private void button2_Click(object sender, EventArgs e)
    {
    }

    private void button1_Click(object sender, EventArgs e)
    {
      if (_dt != null)
      {
        DataTable DTDistinct = DataSetHelper.SelectDistinct(_dt, listBox1.SelectedItem.ToString());
        listBoxUniqueValues.Items.Clear();
        foreach (DataRow DR in DTDistinct.Rows)
        listBoxUniqueValues.Items.Add(DR[0]);
      }
    }
  }
}
