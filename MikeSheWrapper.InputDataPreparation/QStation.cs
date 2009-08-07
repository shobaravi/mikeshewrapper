using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DHI.TimeSeries;
using DHI.Generic.MikeZero;


namespace QStationer
{
  public class QStation
  {
    string _name;
    double _uTMX;
    double _uTMY;
    double _area;
    int _dmuMaalerNr;
    int _dmuStedNr;
    int _dmuStationsNr;
    ITSObject _data;
    TSItem _qItem;

    public QStation()
    {
      _data = new TSObjectClass();
      _qItem = new TSItemClass();
      _qItem.DataType = ItemDataType.Type_Float;
      _qItem.ValueType = ItemValueType.Instantaneous;

      _qItem.EumType = 2;
      _qItem.EumUnit = 1;

      _qItem.Name = "Discharge";

      _data.Add(_qItem);

    }

    /// <summary>
    /// Writes a .dfs0 file with the discharge data
    /// </summary>
    /// <param name="DFS0FileName"></param>
    public void WriteToDfs0(string DFS0FileName)
    {
      _data.Connection.FilePath = DFS0FileName;
      _data.Connection.Save();
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

      line = SR.ReadLine().Split(':');
      if (line.Length != 1)
        int.TryParse(line[1].Trim(),out _dmuMaalerNr);


      _name = SR.ReadLine().Split(':')[1].Trim();

      line = SR.ReadLine().Split(',');
      _uTMX = double.Parse(line[0].Split('=')[1].Trim());
      _uTMY = double.Parse(line[1].Split('=')[1].Trim());

      line = SR.ReadLine().Split(':');
      _area = double.Parse(line[1].Trim().Split(' ')[0].Trim());

      SR.ReadLine();
      SR.ReadLine();

      int NumberOfTimeSteps=0;
      line = SR.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

      while ( line.Length != 0 & !SR.EndOfStream )
      {
        NumberOfTimeSteps++;

        _data.Time.AddTimeSteps(1);
        _data.Time.SetTimeForTimeStepNr(NumberOfTimeSteps,new DateTime(int.Parse(line[0]),int.Parse(line[1]),int.Parse(line[2]),int.Parse(line[3]),int.Parse(line[4]),0));
        _qItem.SetDataForTimeStepNr(NumberOfTimeSteps, float.Parse(line[5]));

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

    public int DmuMaalerNr
    {
      get { return _dmuMaalerNr; }
      set { _dmuMaalerNr = value; }
    }

  }
}
