using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MikeSheWrapper.Tools;


namespace QStationReader
{
  public class QStation
  {
    string _name;
    double _uTMX;
    double _uTMY;
    double _area;
    string _dmuMaalerNr;
    int _dmuStedNr;
    int _dmuStationsNr;
    private List<TimeSeriesEntry> _discharge = new List<TimeSeriesEntry>();

    public List<TimeSeriesEntry> Discharge
    {
        get { return _discharge; }
    }

    public QStation()
    {


    }


    /// <summary>
    /// Reads all station data from the text stream
    /// </summary>
    /// <param name="SR"></param>
    public void ReadEntryFromText(StreamReader SR)
    {
      string[] line = SR.ReadLine().Split(':');
      if (line.Length!=1)
        int.TryParse(line[1].Trim(), out _dmuStationsNr);

      line = SR.ReadLine().Split(':');
      if (line.Length != 1)
        int.TryParse(line[1].Trim(), out _dmuStedNr);

      _dmuMaalerNr = SR.ReadLine().Split(':')[1].Trim();

      _name = SR.ReadLine().Split(':')[1].Trim();

      line = SR.ReadLine().Split(',');
      _uTMX = double.Parse(line[0].Split('=')[1].Trim());
      _uTMY = double.Parse(line[1].Split('=')[1].Trim());

      line = SR.ReadLine().Split(':');
      _area = double.Parse(line[1].Trim().Split(' ')[0].Trim());

      SR.ReadLine();
      SR.ReadLine();

      line = SR.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

      while ( line.Length != 0 & !SR.EndOfStream )
      {
        
          DateTime d = new DateTime(int.Parse(line[0]),int.Parse(line[1]),int.Parse(line[2]),int.Parse(line[3]),int.Parse(line[4]),0);
        _discharge.Add(new TimeSeriesEntry(d, double.Parse(line[5])));

        line = SR.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
      }

    }

    /// <summary>
    /// Gets and sets the x-coodinate
    /// </summary>
    public double UTMX
    {
      get { return _uTMX; }
      set { _uTMX = value; }
    }

    /// <summary>
    /// Gets and sets the y-coodinate
    /// </summary>
    public double UTMY
    {
      get { return _uTMY; }
      set { _uTMY = value; }
    }

    public double Area
    {
      get { return _area; }
      set { _area = value; }
    }

    public string Name
    {
      get { return _name; }
      set { _name = value; }
    }

    public int DmuStedNr
    {
      get { return _dmuStedNr; }
      set { _dmuStedNr = value; }
    }

    public int DmuStationsNr
    {
      get { return _dmuStationsNr; }
      set { _dmuStationsNr = value; }
    }

    public string DmuMaalerNr
    {
      get { return _dmuMaalerNr; }
      set { _dmuMaalerNr = value; }
    }

  }
}
