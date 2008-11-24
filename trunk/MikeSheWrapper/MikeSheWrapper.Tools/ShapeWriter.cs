using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MikeSheWrapper.Tools
{
  public class ShapeWriter:Shape
  {
    string _filename;

    public ShapeWriter(string FileName)
    {
      _filename = FileName;
      // Open shapefile
    }

    public void WritePointShape(double X, double Y)
    {
      if ((int)_shapePointer == 0)
        _shapePointer = ShapeLib.SHPCreate(_filename, ShapeLib.ShapeType.Point);

      IntPtr obj= ShapeLib.SHPCreateSimpleObject(ShapeLib.ShapeType.Point,1, new double[] { X }, new double[] { Y }, null);
      ShapeLib.SHPWriteObject(_shapePointer, _recordPointer, obj);
      ShapeLib.SHPDestroyObject(obj);
    }

    public void WriteData(DataRow DR)
    {
      if (_data == null)
        _data = new DataTable();
      _data.Rows.Add(DR);
    }

    public void Flush()
    {
      if ((int)_dbfPointer == 0)
        _dbfPointer = ShapeLib.DBFCreate(_filename);




    }

  }
}
