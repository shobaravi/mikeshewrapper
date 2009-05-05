using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

using MikeSheWrapper.DFS;

namespace MikeSheWrapper.DFS.UnitTest
{
  [TestFixture]
  public class Res11Test
  {

    [Test]
    public void ReadTest()
    {
      DFS0 d = new DFS0(@"C:\Program Files\DHI\MIKEZero\Examples\MIKE_11\AutoCal\Example3\ADTEST3ADAdd_REF.res11");
    }

  }
}
