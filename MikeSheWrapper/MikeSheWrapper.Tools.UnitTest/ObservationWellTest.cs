using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MikeSheWrapper.Tools;

using NUnit.Framework;

namespace MikeSheWrapper.Tools.UnitTest
{
  [TestFixture]
  public class ObservationWellTest
  {
    private static Func<ObservationEntry, DateTime, DateTime, bool> InBetween = (TSE, Start, End) => TSE.Time >= Start & TSE.Time < End;

    [Test]
    public void GroupByTest()
    {
      ObservationWell OW = new ObservationWell("test");

      OW.Observations.Add(new ObservationEntry(new DateTime(2000, 1, 1), 10));
      OW.Observations.Add(new ObservationEntry(new DateTime(1999, 1, 1), 10));
      OW.Observations.Add(new ObservationEntry(new DateTime(1999, 1, 1), 5));
      OW.Observations.Add(new ObservationEntry(new DateTime(1999, 1, 1), 15));

      OW.Observations.Sort();

      Assert.IsTrue(OW.Observations[1].Equals(OW.Observations[2]));

      int kk = OW.Observations.Distinct().Count(W => InBetween(W, DateTime.MinValue, DateTime.MaxValue)); ;
                 //   group obs by obs.Time into g 

      Assert.AreEqual(2, OW.Observations.Distinct().Count());

      //foreach (var W in grouped)
      //{
      //  foreach (TimeSeriesEntry TSE in W)
      //    Console.WriteLine(TSE.ToString());
      //}

      foreach (var W in OW.Observations.Where(v => InBetween(v, new DateTime(1998,2,2), DateTime.MaxValue)).GroupBy(o => o.Time))
      {
        Console.WriteLine("newline");
        int k = 0;
        foreach (ObservationEntry TSE in W)
        {
          if(k==0)
          Console.WriteLine(TSE.ToString());
          k++;
        }
      }

    }



    [Test]
    public void SortTest()
    {
      ObservationWell OW = new ObservationWell("test");

      OW.Observations.Add(new ObservationEntry(new DateTime(2000, 1, 1), 10));
      OW.Observations.Add(new ObservationEntry(new DateTime(1999, 1, 1), 15));

      Assert.AreEqual(10, OW.Observations[0].Value);

      OW.Observations.Sort();
      Assert.AreEqual(15, OW.Observations[0].Value);


      DateTime start = new DateTime(1999, 1, 1);
      DateTime end = new DateTime(2000, 1, 1);

      Func<ObservationEntry, bool> InBetween = a => a.Time >= start & a.Time <= end;

      var query = OW.Observations.Where(a => InBetween(a));

      foreach (ObservationEntry tse in query)
        Console.WriteLine(tse.Time);


      var query2 = from entry in OW.Observations where entry.Time >= start & entry.Time <= end select entry;

      double d = query2.Sum(new Func<ObservationEntry, double>(a => a.Value));


      foreach (ObservationEntry tse in query2)
        Console.WriteLine(tse.Time);


    }
  }
}
