using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;


namespace MikeSheWrapper.LayerStatistics
{


  [Serializable]
  public class Configuration
  {
    /// <summary>
    /// Returns a configuration object from an xml-file
    /// </summary>
    /// <param name="XmlFileName"></param>
    /// <returns></returns>
    public static Configuration ConfigurationFactory(string XmlFileName)
    {
      FileCheckAndError(XmlFileName);

      XmlSerializer x = new XmlSerializer(typeof(Configuration));
      Configuration cf;
      using (FileStream fs = new FileStream(XmlFileName, FileMode.Open))
      {
        cf = (Configuration)x.Deserialize(fs);
      }

      cf._path = Path.GetDirectoryName(Path.GetFullPath(XmlFileName));
      string path = Directory.GetCurrentDirectory();
      Directory.SetCurrentDirectory(cf._path);

      cf.PreProcessedDFS2 = Path.GetFullPath(cf.PreProcessedDFS2);
      cf.PreProcessedDFS3 = Path.GetFullPath(cf.PreProcessedDFS3);
      cf.ResultFile = Path.GetFullPath(cf.ResultFile);
      cf.ObservationFile = Path.GetFullPath(cf.ObservationFile);
      Directory.SetCurrentDirectory(path);

      FileCheckAndError(cf.PreProcessedDFS2);
      FileCheckAndError(cf.PreProcessedDFS3);
      FileCheckAndError(cf.ResultFile);
      FileCheckAndError(cf.ObservationFile);

      
      
      return cf;
    }

    private static void FileCheckAndError(string path)
    {
      if (!File.Exists(path))
        throw new Exception("Cannot find file: " + path);
    }

    [NonSerialized]
    private string _path;
    private string _preProcessedDFS2;
    private string _preProcessedDFS3;
    private string _observationFile;
    private string _resultFile;
    private string _headItemText;

    public string HeadItemText
    {
      get { return _headItemText; }
      set { _headItemText = value; }
    }

    public string PreProcessedDFS3
    {
      get { return _preProcessedDFS3; }
      set { _preProcessedDFS3 = value; }
    }

    public string ObservationFile
    {
      get { return _observationFile; }
      set { _observationFile = value; }
    }

    public string ResultFile
    {
      get { return _resultFile; }
      set { _resultFile = value; }
    }

    public string PreProcessedDFS2
    {
      get { return _preProcessedDFS2; }
      set { _preProcessedDFS2 = value; }
    }
  }
}
