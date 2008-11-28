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
    private DBFReader _data;

    public DBFReader Data
    {
      get { return _data; }
    }



    public PointShapeReader(string FileName)
    {
      _fileName = FileName;
      _data = new DBFReader(FileName);

      // Open shapefile
      _shapePointer = ShapeLib.SHPOpen(FileName, "rb");

//      if (dr.Table.Columns[j].ColumnName.Equals("ShapeID"))
      {
      }
    }

    public void ReadNext(out double X, out double Y)
    {
      IntPtr pShape = ShapeLib.SHPReadObject(_shapePointer, _recordPointer);
      ShapeLib.SHPObject shpObject = new ShapeLib.SHPObject();
      Marshal.PtrToStructure(pShape, shpObject);
      double[] x = new double[shpObject.nVertices];
      Marshal.Copy(shpObject.padfX, x, 0, x.Length);
      double[] y = new double[shpObject.nVertices];
      Marshal.Copy(shpObject.padfX, y, 0, y.Length);

      X= x[0];
      ShapeLib.SHPDestroyObject(pShape);
      Y = y[0];
      _recordPointer++;

    }



    /// <summary>
    /// Disposes the shapefile
    /// </summary>
    public override void  Dispose()
    {
      _data.Dispose();
      base.Dispose();
    }
  }
}
