using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;


namespace MikeSheWrapper.Irrigation
{
  class Program
  {
    /// <summary>
		/// The main entry point for the application.
		/// </summary>
    [STAThread]
    public static void Main(string[] args)
    {
      

      XmlSerializer x = new XmlSerializer(typeof(Configuration));

      Configuration Cf = (Configuration)x.Deserialize(new System.IO.FileStream(args.Aggregate<string>((a, b) => a + b), System.IO.FileMode.Open));
      Controller C = new Controller(Cf);

      
      C.Run();


    }
  }
}
