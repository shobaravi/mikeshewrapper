using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MikeSheWrapper
{
  public class MSheLauncher
  {
    public static void PreprocessAndRun(string MsheFileName, bool UseMZLauncher)
    {

      Process Runner = new Process();

      if (UseMZLauncher)
      {

      }

      else
      {
        //This should actually check the path using DHI.Fl, which will be changed in 2009

        Runner.StartInfo.FileName = "Mshe_preprocessor.exe";
        Runner.StartInfo.Arguments = MsheFileName;
        Runner.Start();
        Runner.WaitForExit();
        Runner.StartInfo.FileName = "Mshe_watermovement.exe";
        Runner.Start();
        Runner.WaitForExit();
        Runner.Close();
      }
    }

  }
}
