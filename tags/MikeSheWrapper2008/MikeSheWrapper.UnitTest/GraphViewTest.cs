using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using MikeSheWrapper.Viewer;
using MikeSheWrapper.Tools;
using MikeSheWrapper.JupiterTools;

namespace MikeSheWrapper.UnitTest
{
  [TestFixture]
  public class GraphViewTest
  {

    [Ignore]
    [Test]
    public void ViewTest()
    {
      Form1 f = new Form1();
      Reader R = new Reader(@"..\..\..\TestData\AlbertslundPcJupiter.mdb");

      Dictionary<string, IWell> Wells = R.WellsForNovana(false, true, false);
      List<IIntake> Intakes = new List<IIntake>();
      
      foreach (IWell W in Wells.Values)
         Intakes.AddRange(W.Intakes);


      f.Intakes = Intakes;

      f.ShowDialog();

    }
  }
}
