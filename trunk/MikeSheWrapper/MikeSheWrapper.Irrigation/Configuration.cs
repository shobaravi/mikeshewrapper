using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MikeSheWrapper.Irrigation
{
  [Serializable]
  public class Configuration
  {
    private string _sheFile;
    private string _wellShapeFile;
    private string _xHeader;
    private string _yHeader;
    private string _topHeader;
    private string _bottomHeader;
    private string _idNumberHeader;
    private string _idHeader;
    private string _maxDepthHeader;
    private string _maxRateHeader;

    public string SheFile
    {
      get { return _sheFile; }
      set { _sheFile = value; }
    }

    public string WellShapeFile
    {
      get { return _wellShapeFile; }
      set { _wellShapeFile = value; }
    }

    public string XHeader
    {
      get { return _xHeader; }
      set { _xHeader = value; }
    }

    public string YHeader
    {
      get { return _yHeader; }
      set { _yHeader = value; }
    }

    public string TopHeader
    {
      get { return _topHeader; }
      set { _topHeader = value; }
    }


    public string BottomHeader
    {
      get { return _bottomHeader; }
      set { _bottomHeader = value; }
    }

    public string MaxRateHeader
    {
      get { return _maxRateHeader; }
      set { _maxRateHeader = value; }
    }

    public string MaxDepthHeader
    {
      get { return _maxDepthHeader; }
      set { _maxDepthHeader = value; }
    }

    public string IdHeader
    {
      get { return _idHeader; }
      set { _idHeader = value; }
    }

    public string IdNumberHeader
    {
      get { return _idNumberHeader; }
      set { _idNumberHeader = value; }
    }
  }
}
