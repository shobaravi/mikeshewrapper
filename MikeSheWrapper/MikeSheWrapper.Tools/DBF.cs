using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MikeSheWrapper.Tools
{
  public abstract class DBF
  {
    protected string _filename;
    protected IntPtr _dbfPointer;
    protected int _recordPointer;
    protected DataTable _data;
    protected Dictionary<string, DBFEntry> _columns;
   

    public Dictionary<string, DBFEntry> Columns
    {
      get { return _columns; }
    }


    public DBF(string FileName)
    {
      _filename = FileName;
    }

    public virtual void Dispose()
    {
      _data.Dispose();
      ShapeLib.DBFClose(_dbfPointer);
    }

  }
}
