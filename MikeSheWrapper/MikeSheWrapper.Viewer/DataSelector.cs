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
    private DataTable _data;

    /// <summary>
    /// Gets the selected rows. Returns null until the ok-button is pressed
    /// </summary>
    public DataRow[] SelectedRows { get; private set; }


    /// <summary>
    /// Returns the selected rows.
    /// </summary>
    /// <returns></returns>
    private bool TrySelectRows()
    {
      try
      {
        SelectedRows = _data.Select(SelectString);
      }
      catch (InvalidExpressionException EE)
      {
        MessageBox.Show(EE.Message, "Invalid filter expression");
        return false;
      }
      return true;
    }




    public DataSelector(DataTable Data)
    {
      InitializeComponent();

      _data = Data;
      foreach (DataColumn DC in _data.Columns)
        listBox1.Items.Add(DC.ColumnName);


      this.EqualButton.Click += new System.EventHandler(this.EqualButton_Click);
      this.buttonNE.Click += new System.EventHandler(this.EqualButton_Click);
      this.buttonLike.Click += new System.EventHandler(this.EqualButton_Click);
     
      this.buttonGT.Click += new System.EventHandler(this.EqualButton_Click);
      this.buttonGE.Click += new System.EventHandler(this.EqualButton_Click);
      this.buttonAnd.Click += new System.EventHandler(this.EqualButton_Click);

      this.buttonLT.Click += new System.EventHandler(this.EqualButton_Click);
      this.buttonLE.Click += new System.EventHandler(this.EqualButton_Click);
      this.buttonOr.Click += new System.EventHandler(this.EqualButton_Click);

      this.button_.Click += new System.EventHandler(this.EqualButton_Click);
      this.buttonParanteses.Click += new System.EventHandler(this.EqualButton_Click);
      this.buttonNot.Click += new System.EventHandler(this.EqualButton_Click);

      this.buttonPercent.Click += new System.EventHandler(this.EqualButton_Click);
      this.buttonLs.Click += new System.EventHandler(this.EqualButton_Click);

      this.listBox1.DoubleClick += new EventHandler(listBox1_DoubleClick);
      this.listBoxUniqueValues.DoubleClick += new EventHandler(listBoxUniqueValues_DoubleClick);
    }


    /// <summary>
    /// Gets the created string
    /// </summary>
    public string SelectString
    {
      get
      {
        return richTextBoxSelectString.Text;
      }
    }

    /// <summary>
    /// Sets the array of strings in the listbox
    /// </summary>
    public string[] Fields
    {
      set
      {
        listBox1.Items.AddRange(value);
      }
    }

    /// <summary>
    /// Inserts the text at the position of the cursor
    /// </summary>
    /// <param name="text"></param>
    private void InsertText(string text)
    {
      int k = richTextBoxSelectString.SelectionStart;

      richTextBoxSelectString.Text = richTextBoxSelectString.Text.Insert(k," "+ text+ " ");

      richTextBoxSelectString.SelectionStart = k + text.Length+2;
      richTextBoxSelectString.Focus();

    }

    #region EventHandlers

    /// <summary>
    /// Closes the form if the select string is ok
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OkButton_Click(object sender, EventArgs e)
    {
      if (TrySelectRows())
      {
        DialogResult = DialogResult.OK;
        this.Close();
      }
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

    void listBox1_DoubleClick(object sender, EventArgs e)
    {
      InsertText(((ListBox)sender).SelectedItem.ToString());
    }

    void listBoxUniqueValues_DoubleClick(object sender, EventArgs e)
    {
      InsertText("'" + ((ListBox)sender).SelectedItem.ToString() + "'");
    }

    private void EqualButton_Click(object sender, EventArgs e)
    {
      InsertText(((Button)sender).Text);
    }

    private void button1_Click(object sender, EventArgs e)
    {
      if (_data != null & listBox1.SelectedItem!=null)
      {
        DataTable DTDistinct = DataSetHelper.SelectDistinct(_data, listBox1.SelectedItem.ToString());
        listBoxUniqueValues.Items.Clear();
        foreach (DataRow DR in DTDistinct.Rows)
        listBoxUniqueValues.Items.Add(DR[0]);
      }
    }

    #endregion
  }
}
