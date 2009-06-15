using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MikeSheWrapper.Tools;

namespace MikeSheWrapper.Viewer
{
  public partial class Form1 : Form
  {
    private List<IIntake> _intakes;
    private int _currentIndex;

    public Form1()
    {
      InitializeComponent();
    }

    public List<IIntake> Intakes
    {
      set
      {
        _intakes = value;
        CurrentIndex=0;
      }
    }

    public int CurrentIndex
    {
      get
      {
        return _currentIndex;
      }
      set
      {
        _currentIndex = Math.Max(0,(Math.Min(_intakes.Count, value)));
        SetCurrent();
      }
    }


    private void SetCurrent()
    {
      if (_intakes.Count > 0)
      {
        IIntake Current = _intakes[_currentIndex];
        graphView1.ClearView();
        graphView1.AddTimeSeries(Current.well + "_" + Current.IDNumber, Current.Observations);
        propertyGrid1.SelectedObject = _intakes[_currentIndex];
        textBox1.Text = _intakes[_currentIndex].well.ID;
      }
    }

    private void button1_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.OK;
      
    }

    private void button2_Click(object sender, EventArgs e)
    {
      this.graphView1.Refresh();
    }

    private void buttonForward_Click(object sender, EventArgs e)
    {
      CurrentIndex++;
    }

    private void buttonBack_Click(object sender, EventArgs e)
    {
      CurrentIndex--;
    }
  }
}
