using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace MikeSheWrapper.Tools
{
  public abstract class Shape
  {
    protected IntPtr _shapePointer;
    protected IntPtr _dbfPointer;
    protected DataTable _data;
    protected int _recordPointer = 0;
    protected int _noOfEntries;

    protected Dictionary<string, DBFEntry> _columns;

    public Dictionary<string, DBFEntry> Columns
    {
      get { return _columns; }
    }


  }
}
