using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

using MikeSheWrapper.Tools;


namespace MikeSheWrapper.Viewer
{
  public partial class GraphView : UserControl
  {
    public GraphView()
    {
      InitializeComponent();
    }

    public void AddTimeSeries(string name, List<TimeSeriesEntry> entries)
    {
      Series n = new Series(name);

      n.Name = name;
      n.Points.DataBindXY(entries, "Time", entries,"Value");
      n.ChartType = SeriesChartType.FastPoint;
      this.chart1.Series.Add(n);
      
      this.dataGridView1.DataSource = entries;      
     
    }
  }
}
