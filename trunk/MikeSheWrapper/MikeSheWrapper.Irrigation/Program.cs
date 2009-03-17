using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;


namespace MikeSheWrapper.Irrigation
{
  public class Program
  {
    /// <summary>
		/// The main entry point for the application.
		/// </summary>
    [STAThread]
    public static void Main(string[] args)
    {
      
      XmlSerializer x = new XmlSerializer(typeof(Configuration));

      Configuration Cf;
      string xmlfile;
      string shefile;
      if (args.Length == 2)
      {
        if (Path.GetExtension(args[0]).ToLower() == ".xml")
        {
          xmlfile = args[0];
          shefile = args[1];
        }
        else
        {
          xmlfile = args[1];
          shefile = args[0];
        }
        Cf = (Configuration)x.Deserialize(new FileStream(xmlfile, System.IO.FileMode.Open));
        Cf.SheFile = shefile;
      }
      else
        Cf = (Configuration)x.Deserialize(new FileStream(args[0], System.IO.FileMode.Open));
        
           
      
      Controller C = new Controller(Cf);

      
      C.Run();


    }
  }
}
