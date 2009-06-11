using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using MikeSheWrapper.Viewer;
using MikeSheWrapper.Tools;

namespace MikeSheWrapper.UnitTest
{
  [TestFixture]
  public class GraphViewTest
  {

    [Test]
    public void ViewTest()
    {
      Form1 f = new Form1();

      List<TimeSeriesEntry> entries = new List<TimeSeriesEntry>();

      entries.Add(new TimeSeriesEntry(DateTime.Now,1));
      entries.Add(new TimeSeriesEntry(DateTime.Now.AddDays(1),2));

      f.graphView1.AddTimeSeries("navn", entries);
      f.graphView1.AddTimeSeries("navn3", entries);

      f.ShowDialog();

    }
  }
}
