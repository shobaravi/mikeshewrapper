using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MikeSheWrapper.Tools;
using MikeSheWrapper.InputDataPreparation;

using NUnit.Framework;

namespace MikeSheWrapper.InputDataPreparation.UnitTest
{
  [TestFixture]
  public class ObservationWellTest
  {
    [Test]
    public void SortTest()
    {
      ObservationWell OW = new ObservationWell("test");

      OW.Observations.Add(new TimeSeriesEntry(new DateTime(2000, 1, 1), 10));
      OW.Observations.Add(new TimeSeriesEntry(new DateTime(1999, 1, 1), 15));

      Assert.AreEqual(10, OW.Observations[0].Value);

      OW.Observations.Sort();
      Assert.AreEqual(15, OW.Observations[0].Value);


      DateTime start = new DateTime(1999, 1, 1);
      DateTime end = new DateTime(2000, 1, 1);

      Func<TimeSeriesEntry, bool> InBetween = a => a.Time >= start & a.Time <= end;

      var query = OW.Observations.Where(a => InBetween(a));

      foreach (TimeSeriesEntry tse in query)
        Console.WriteLine(tse.Time);


      var query2 = from entry in OW.Observations where entry.Time >= start & entry.Time <= end select entry;

      double d = query2.Sum(new Func<TimeSeriesEntry, double>(a => a.Value));


      foreach (TimeSeriesEntry tse in query2)
        Console.WriteLine(tse.Time);


    }
  }
}
