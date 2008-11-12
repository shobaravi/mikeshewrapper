using System.Collections;
using System;
using System.IO;

using MikeSheWrapper;

/**
 * @(#) WellReader.cs
 */

namespace MikeSheWrapper.LayerStatistics
{
	public class WellReader
	{
		string FileName;
	
	
		public WellReader(string FileName )
		{
      FileNameUtil FN = new FileNameUtil();
			FN.CheckFiles(FileName);
			this.FileName=FileName;
		
		}
	
		public void read(ArrayList Wells, ArrayList Observations, Model MSObject)
		{
			using (StreamReader SR = new StreamReader( FileName ))
			{
				//Reads the HeadLine
				string line=SR.ReadLine();
        double UTMX;
        double UTMY;
        double Z;
        double Potential;
        DateTime Time;
        string[] s;

				while ((line = SR.ReadLine()) != null) 
				{
          s    = line.Split('\t');
          if (s.Length > 5)
          {
            try
            {
              UTMX = double.Parse(s[1]);
              UTMY = double.Parse(s[2]);
              Potential = double.Parse(s[4]);
              Time = DateTime.Parse(s[5]);
          
              if (s.Length > 7 && s[6] != "")
              {
                int Layer = int.Parse(s[6]);
                int Column;
                int Row;
                bool insidedomain = MSObject.Processed.GetIndex(UTMX, UTMY, out Column, out Row);

                if (insidedomain) //What if not?
                {
                  //To dfs layering
                  Layer -= MSObject.Processed.LowerLevelOfComputationalLayers.Data.LayerCount;
                  Z = 0.5 * (MSObject.Processed.LowerLevelOfComputationalLayers.Data[Row, Column, Layer] + MSObject.Processed.UpperLevelOfComputationalLayers.Data[Row, Column, Layer]);
                }
              }
              else
              {
                Z = double.Parse(s[3]);
              }
            }
            catch (FormatException e)
            {
              throw new Exception("Error reading input-file: " + FileName);
            }

            Well W = new Well(s[0], UTMX, UTMY, Z, Potential, Time );
            Wells.Add(W);
            Observations.Add( new Observation( W, MSObject ) );
          }
        }
			}
		}
	
	}
}
