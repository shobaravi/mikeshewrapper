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
    protected int _recordPointer = 0;
    protected int _noOfEntries;
    protected string _fileName;


    public virtual void Dispose()
    {
 
      ShapeLib.SHPClose(_shapePointer);

    }
  }
}
