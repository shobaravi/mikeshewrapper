using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MikeSheWrapper.Tools
{
  public class PointShapeWriter:Shape 
  {
    private DBFWriter _dbf;


    public PointShapeWriter(string FileName):base(FileName)
    {
      _dbf = new DBFWriter(FileName);
    }

    public void WritePointShape(double X, double Y)
    {
      if ((int)_shapePointer == 0)
        _shapePointer = ShapeLib.SHPCreate(_filename, ShapeLib.ShapeType.Point);

      IntPtr obj = ShapeLib.SHPCreateSimpleObject(ShapeLib.ShapeType.Point, 1, new double[] { X }, new double[] { Y }, null);
      ShapeLib.SHPWriteObject(_shapePointer, _recordPointer, obj);
      ShapeLib.SHPDestroyObject(obj);
    }

    public DBFWriter Data
    {
      get { return _dbf; }
    }

    public void Dispose()
    {
      _dbf.Dispose();
      base.Dispose();
    }

  }
}
