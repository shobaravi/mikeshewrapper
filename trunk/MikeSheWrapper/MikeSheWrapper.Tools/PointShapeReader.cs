using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;


namespace MikeSheWrapper.Tools
{
  public class PointShapeReader:Shape
  {



    public PointShapeReader(string FileName)
    {
      // Open shapefile
      _shapePointer = ShapeLib.SHPOpen(FileName, "rb");

      if (dr.Table.Columns[j].ColumnName.Equals("ShapeID"))
      {
        IntPtr pShape = ShapeLib.SHPReadObject(_shapePointer, _recordPointer);
        ShapeLib.SHPObject shpObject = new ShapeLib.SHPObject();
        Marshal.PtrToStructure(pShape, shpObject);
        dr[j] = shpObject.nShapeId;
        ShapeLib.SHPDestroyObject(pShape);
      }
    }



    /// <summary>
    /// Disposes the shapefile
    /// </summary>
    public void Dispose()
    {
      ShapeLib.SHPClose(_shapePointer);
      ShapeLib.DBFClose(_dbfPointer);
    }
  }
}
