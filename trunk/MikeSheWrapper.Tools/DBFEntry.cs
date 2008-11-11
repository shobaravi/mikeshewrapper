using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MikeSheWrapper.Tools
{
  public class DBFEntry
  {
    public string name;
    internal ShapeLib.DBFFieldType _dbfType;
    public Type _dotNetType;
    public int _index;
    public int _width = 0;
    public int _decimals = 0;
  }
}
