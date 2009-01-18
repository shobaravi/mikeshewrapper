using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MikeSheWrapper;
using MikeSheWrapper.JupiterTools;
using MikeSheWrapper.JupiterTools.JupiterXLTableAdapters;

using MathNet.Numerics.Statistics;

namespace GeologyChecker
{
  /// <summary>
  /// Small program that collects the hydraulic conductivity from a MikeShe setup for all soil samples in the JUPITER database
  /// Writes a text-file with the averages and standard deviation of the logarithm of the hydraulic conductivities
  /// for all Rock types.
  /// </summary>
  class Program
  {
    static void Main(string[] args)
    {
      Dictionary<string, Accumulator> vals = new Dictionary<string, Accumulator>();

      Model m = new Model(args[0]);

      JupiterXL JXL = new JupiterXL();
      JXL.PartialReadOfWells(args[1]);

      LITHSAMPTableAdapter LTA = new LITHSAMPTableAdapter();
      LTA.Connection.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + args[1] + ";Persist Security Info=False";

      LTA.FillByOnlyRock(JXL.LITHSAMP);

      foreach (var geo in JXL.LITHSAMP)
      {
        if (!geo.IsROCKSYMBOLNull() && !geo.IsBOTTOMNull() && !geo.IsTOPNull() && geo.ROCKSYMBOL.Trim() != "")
        {
          var Boring = JXL.BOREHOLE.FindByBOREHOLENO(geo.BOREHOLENO);

          if (Boring != null && !Boring.IsXUTMNull() && !Boring.IsYUTMNull())
          {
            int Column = m.GridInfo.GetColumnIndex(Boring.XUTM);
            int row = m.GridInfo.GetRowIndex(Boring.YUTM);
            if (row >= 0 & Column >= 0)
            {
              int Layer = m.GridInfo.GetLayer(Column, row, m.GridInfo.SurfaceTopography.Data[row, Column] - (geo.TOP + geo.BOTTOM) / 2);
              if (Layer >= 0)
              {
                Accumulator Ledningsevner;
                if (!vals.TryGetValue(geo.ROCKSYMBOL, out Ledningsevner))
                {
                  Ledningsevner = new Accumulator();
                  vals.Add(geo.ROCKSYMBOL, Ledningsevner);
                }
                Ledningsevner.Add(Math.Log10(m.Processed.HorizontalConductivity.Data[row, Column, Layer]));
              }

            }
          }
        }
      }//End of loop

      Accumulator all = new Accumulator();
      for (int lay = 0; lay < m.GridInfo.NumberOfLayers; lay++)
        for (int row = 0; row < m.GridInfo.NumberOfRows; row++)
          for (int col = 0; col < m.GridInfo.NumberOfColumns; col++)
            if (m.GridInfo.ModelDomainAndGrid.Data[row, col] == 1)
              all.Add(Math.Log10(m.Processed.HorizontalConductivity.Data[row, col, lay]));

      vals.Add("All", all);


      using (StreamWriter sw = new StreamWriter(@"F:\temp\out.txt", false, Encoding.Default))
      {
        sw.WriteLine("Rocksymbol\tNoOfEntries\tMean\tVariance\tStandard Deviation");
        foreach (KeyValuePair<string, Accumulator> KVP in vals.OrderByDescending((acc) => acc.Value.Count))
        {
          sw.WriteLine(KVP.Key + "\t" + KVP.Value.Count + "\t" + KVP.Value.Mean + "\t" + KVP.Value.Variance + "\t" + KVP.Value.Sigma);
        }
      }
      JXL.Dispose();
    }
  }
}
