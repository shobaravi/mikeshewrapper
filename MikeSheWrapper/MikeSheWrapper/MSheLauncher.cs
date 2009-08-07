using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DHI.DHIfl;

namespace MikeSheWrapper
{
  public class MSheLauncher
  {
    /// <summary>
    /// Preprocesses and runs Mike She
    /// Note that if the MzLauncher is used it uses the execution engine flags from the .she-file
    /// </summary>
    /// <param name="MsheFileName"></param>
    /// <param name="UseMZLauncher"></param>
    public static void PreprocessAndRun(string MsheFileName, bool UseMZLauncher)
    {

      Process Runner = new Process();

      string path;
      DHIRegistry key = new DHIRegistry(DHIProductAreas.COMMON_COMPONNETS, false);
      key.GetHomeDirectory(out path);
      
      if (UseMZLauncher)
      {
        Runner.StartInfo.FileName = Path.Combine(path,"Mzlaunch.exe");
        Runner.StartInfo.Arguments = Path.GetFullPath(MsheFileName) + " -exit"; 
      }

      else
      {
        Runner.StartInfo.FileName = Path.Combine(path,"Mshe_preprocessor.exe");
        Runner.StartInfo.Arguments = MsheFileName;
        Runner.Start();
        Runner.WaitForExit();
        Runner.StartInfo.FileName = Path.Combine(path,"Mshe_watermovement.exe");
      }
      Runner.Start();
      Runner.WaitForExit();
      Runner.Close();

    }

  }
}
