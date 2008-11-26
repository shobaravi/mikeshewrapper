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

    /// <summary>
    /// Creates a new shape-file and fills in the data from the datatable. First two columns in DT must contain x-y-data.
    /// </summary>
    /// <param name="FileName"></param>
    /// <param name="DT"></param>
    public void Write(string FileName, DataTable DT)
    {
      // create a new point shapefile and return the pointer
      IntPtr hShp = ShapeLib.SHPCreate(FileName, ShapeLib.ShapeType.Point);


      //We make all the shapes
      for (int i = 0; i < DT.Rows.Count; i++)
      {
        IntPtr pshpObj = ShapeLib.SHPCreateSimpleObject(ShapeLib.ShapeType.Point, 1, new double[] { (double)DT.Rows[i][0] }, new double[] { (double)DT.Rows[i][1] }, new double[1]);

        Shape shpObj = new Shape();
        Marshal.PtrToStructure(pshpObj, shpObj);
        ShapeLib.SHPWriteObject(hShp, -1, pshpObj);
        ShapeLib.SHPDestroyObject(pshpObj);
      }



      //Close the shape handle the shp and shx file is now created
      ShapeLib.SHPClose(hShp);

      //Create the DBF-file
      pDbf = ShapeLib.DBFCreate(FileName);

      if ((int)pDbf <= 0)
        throw new Exception("DBF-fil kunne ikke dannes!");

      int DigitsBeforePoint, DigitsAfterPoint;
      int[] Precision = new int[2];


      //Make the dbf file one attribute at the time
      for (int j = 2; j < DT.Columns.Count; j++)
      {

        //double -attribute
        if (DT.Rows[0][j].GetType() == typeof(double))
        {
          DigitsBeforePoint = 0;
          DigitsAfterPoint = 0;
          for (int i = 0; i < DT.Rows.Count; i++)
          {
            // finder præcision af data
            Precision = GetPrecision((float)(double)DT.Rows[i][j]);
            DigitsBeforePoint = Math.Max(Precision[0], DigitsBeforePoint);
            DigitsAfterPoint = Math.Max(Precision[1], DigitsAfterPoint);
          }
          ShapeLib.DBFAddField(pDbf, DT.Columns[j].Caption, ShapeLib.DBFFieldType.FTDouble, DigitsBeforePoint + DigitsAfterPoint + 1, DigitsAfterPoint);
        }

        //String attribute
        else if (DT.Rows[0][j].GetType() == typeof(string))
        {
          int width = 1;
          for (int i = 0; i < DT.Rows.Count; i++)
          {
            // finder præcision af data
            width = Math.Max(width, (DT.Rows[i][j]).ToString().Length);
          }

          ShapeLib.DBFAddField(pDbf, DT.Columns[j].Caption, ShapeLib.DBFFieldType.FTString, width, 0);
        }

        //int attribute
        else if (DT.Rows[0][j].GetType() == typeof(int))
        {
          int width = 1;
          for (int i = 0; i < DT.Rows.Count; i++)
          {
            // finder præcision af data
            width = Math.Max(width, GetPrecision((int)DT.Rows[i][j]));
          }
          ShapeLib.DBFAddField(pDbf, DT.Columns[j].Caption, ShapeLib.DBFFieldType.FTInteger, width, 0);
        }
        else if (DT.Rows[0][j].GetType() == typeof(bool))
        {
          ShapeLib.DBFAddField(pDbf, DT.Columns[j].Caption, ShapeLib.DBFFieldType.FTLogical, 16, 0);
        }
      }

      //Now fill in the data
      for (int j = 2; j < DT.Columns.Count; j++)
      {
        //double data
        if (DT.Rows[0][j].GetType() == typeof(double))
        {
          for (int i = 0; i < DT.Rows.Count; i++)
          {
            ShapeLib.DBFWriteDoubleAttribute(pDbf, i, j - 2, (double)DT.Rows[i][j]);
          }
        }
        // int data
        else if (DT.Rows[0][j].GetType() == typeof(int))
        {
          for (int i = 0; i < DT.Rows.Count; i++)
          {
            ShapeLib.DBFWriteIntegerAttribute(pDbf, i, j - 2, (int)DT.Rows[i][j]);
          }
        }
        //string data
        else if (DT.Rows[0][j].GetType() == typeof(string))
        {
          for (int i = 0; i < DT.Rows.Count; i++)
          {
            ShapeLib.DBFWriteStringAttribute(pDbf, i, j - 2, DT.Rows[i][j].ToString());
          }
        }
      }
      // close the file handle 
      ShapeLib.DBFClose(pDbf);
    }
    //Returnerer hvor mange tal der er før og efter kommaet
    private int[] GetPrecision(float tal)
    {
      int[] prec = new int[2];

      if (!tal.ToString().Equals("NaN"))
      {
        //Den maksimale præcision på en en float er 7.
        string sTal = tal.ToString("0.0000000e-00");

        int eksponent_p = sTal.IndexOf("e");
        int eksponent = Convert.ToInt16(sTal.Substring(sTal.Length - 2));
        if (sTal.Substring(eksponent_p + 1, 1) == "-")
        {
          prec[0] = 1; //tal før kommaet
          prec[1] = eksponent_p + eksponent - 2; //tal efter kommaet
        }
        else
        {
          prec[0] = eksponent + 1; //tal før kommaet
          prec[1] = eksponent_p - 2; //tal efter kommaet
        }
      }
      return prec;
    }

    private int GetPrecision(int tal)
    {
      return tal.ToString().Length;
    }


  }
}
