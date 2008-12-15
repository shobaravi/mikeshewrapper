using System;
using System.Data;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MikeSheWrapper.Tools
{
  public class PointShapeWriter:Shape 
  {
    private DBFWriter _dbf;


    /// <summary>
    /// Creates a new point shape file. Overwrite if it exists.
    /// </summary>
    /// <param name="FileName"></param>
    public PointShapeWriter(string FileName)
    {
      _fileName = FileName;
      _shapePointer = ShapeLib.SHPCreate(_fileName, ShapeLib.ShapeType.Point);

      _dbf = new DBFWriter(FileName);
    }

    /// <summary>
    /// Writes a point
    /// </summary>
    /// <param name="X"></param>
    /// <param name="Y"></param>
    public void WritePointShape(double X, double Y)
    {
      IntPtr obj = ShapeLib.SHPCreateSimpleObject(ShapeLib.ShapeType.Point, 1, new double[] { X }, new double[] { Y }, null);
     // IntPtr obj = ShapeLib.SHPCreateObject(ShapeLib.ShapeType.Point, -1, 0, null, null, 1, new double[] { X }, new double[] { Y }, null, null);

      ShapeLib.SHPWriteObject(_shapePointer, -1, obj);
      ShapeLib.SHPDestroyObject(obj);
      _recordPointer++;
    }

    /// <summary>
    /// Writes a point shape from a data table where x and y coordinates are included.
    /// </summary>
    /// <param name="Data"></param>
    /// <param name="XHeader">Name of the column containing the x-coordinate</param>
    /// <param name="YHeader">Name of the column containing the y-coordinate</param>
    public void WritePointShape(DataTable Data, string XHeader, string YHeader)
    {
      foreach (DataRow Dr in Data)
      {
        WritePointShape((double)Dr[XHeader], (double)Dr[YHeader]);
        _dbf.WriteData(Dr);
      }
    }

    /// <summary>
    /// Gets access to the data. Note that the data in the table should be in the same order as the shapes entered.
    /// </summary>
    public DBFWriter Data
    {
      get { return _dbf; }
    }

    public override void Dispose()
    {
      _dbf.Dispose();
      base.Dispose();
    }

  }
}
