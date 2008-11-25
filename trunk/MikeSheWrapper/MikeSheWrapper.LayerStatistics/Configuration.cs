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
      if (!File.Exists(XmlFileName))
        throw new Exception("Cannot find file: " + XmlFileName);
      XmlSerializer x = new XmlSerializer(typeof(Configuration));
      Configuration cf = (Configuration)x.Deserialize(new FileStream(XmlFileName, FileMode.Open));
      cf._path = Path.GetDirectoryName(XmlFileName);
      return cf;
    }

    [NonSerialized]
    private string _path;
    private string _preProcessedDFS2;
    private string _preProcessedDFS3;
    private string _observationFile;
    private string _resultFile;

    public string PreProcessedDFS3
    {
      get { return Path.Combine(_path, _preProcessedDFS3); }
      set { _preProcessedDFS3 = value; }
    }

    public string ObservationFile
    {
      get { return Path.Combine(_path, _observationFile); }
      set { _observationFile = value; }
    }

    public string ResultFile
    {
      get { return Path.Combine(_path, _resultFile); }
      set { _resultFile = value; }
    }

    public string PreProcessedDFS2
    {
      get { return Path.Combine(_path,_preProcessedDFS2); }
      set { _preProcessedDFS2 = value; }
    }
  }
}
