using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DHI.TimeSeries;

namespace QStationer
{
  class Program
  {
    static void Main(string[] args)
    {

      List<QStation> _stations = new List<QStation>();
      string TextFileName = @"F:\DHI\Data\Novana\Novomr4\Time\Obs\Q-data\jylland1.txt";


      using (StreamReader SR = new StreamReader(TextFileName,Encoding.Default))
      {
     
        string line;
        while (!SR.EndOfStream)
        {
          line = SR.ReadLine();
          if (line.Equals("*"))
          {
            QStation qs = new QStation();
            qs.ReadEntryFromText(SR);
            _stations.Add(qs);
            qs.WriteToDfs0(Path.Combine(Path.GetDirectoryName(TextFileName), qs.DmuStationsNr + ".dfs0"));
          }
        }
      }
    }
  }
}
