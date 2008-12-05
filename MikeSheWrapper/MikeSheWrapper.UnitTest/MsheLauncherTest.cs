using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using MikeSheWrapper;

namespace MikeSheWrapper.UnitTest
{
  [TestFixture]
  public class MsheLauncherTest
  {
    [Test]
    public void PreProcessTest()
    {
     
      MSheLauncher.PreprocessAndRun(@"F:\Jacob\MikeSheWrapper\TestData\TestModel.she",false);
      Console.WriteLine("simulation finished");
    }
  }
}
