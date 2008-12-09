using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MikeSheWrapper.Tools
{
  public class DBFWriter : DBF
  {
    public DBFWriter(string FileName)
      : base(FileName)
    { }

    /// <summary>
    /// Writes the data corresponding to the shapes. 
    /// Note that data rows should have the same order as the points were made in.
    /// Nothing is actually written until Flush or Dispose are called.
    /// </summary>
    /// <param name="DT"></param>
    public void WriteDate(DataTable DT)
    {
      _data = DT;
    }

    /// <summary>
    /// Writes a datarow
    /// </summary>
    /// <param name="DR"></param>
    public void WriteData(DataRow DR)
    {
      if (_data == null)
        _data = new DataTable();
      _data.Rows.Add(DR);
    }

    public void Flush()
    {
      if ((int)_dbfPointer == 0)
        CreateBDF();

      //Now fill in the data
      for (int j = 0; j < _data.Columns.Count; j++)
      {
        //double data
        if (_data.Columns[j].DataType == typeof(double))
        {
          for (int i = 0; i < _data.Rows.Count; i++)
          {
            if (_data.Rows[i][j]!=DBNull.Value)
              ShapeLib.DBFWriteDoubleAttribute(_dbfPointer, i, j , (double)_data.Rows[i][j]);
          }
        }
        // int data
        else if (_data.Columns[j].DataType == typeof(int))
        {
          for (int i = 0; i < _data.Rows.Count; i++)
          {
            ShapeLib.DBFWriteIntegerAttribute(_dbfPointer, i, j, (int)_data.Rows[i][j]);
          }
        }
        //string data
        else if (_data.Columns[j].DataType == typeof(string))
        {
          for (int i = 0; i < _data.Rows.Count; i++)
          {
            int ok =ShapeLib.DBFWriteStringAttribute(_dbfPointer, i, j, _data.Rows[i][j].ToString());
          }
        }
        //DateTime data
        else if (_data.Columns[j].DataType == typeof(DateTime))
        {
          for (int i = 0; i < _data.Rows.Count; i++)
          {
            ShapeLib.DBFWriteDateAttribute(_dbfPointer, i, j,(DateTime) _data.Rows[i][j]);
          }
        }
        else if(_data.Columns[j].DataType == typeof(bool))
        {
          for (int i = 0; i < _data.Rows.Count; i++)
          {
            ShapeLib.DBFWriteLogicalAttribute(_dbfPointer, i, j, (bool)_data.Rows[i][j]);
          }
        }

      }
      _data.Clear();
    }

    /// <summary>
    /// Flushes and disposes.
    /// </summary>
    public override void Dispose()
    {
      Flush();
      base.Dispose();
    }

    /// <summary>
    /// Returns the number of digits before and after the point
    /// </summary>
    /// <param name="tal"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Returns the number of digits in an integer
    /// </summary>
    /// <param name="tal"></param>
    /// <returns></returns>
    private int GetPrecision(int tal)
    {
      return tal.ToString().Length;
    }

    private void CreateBDF()
    {
      _dbfPointer = ShapeLib.DBFCreate(_filename);

      int DigitsBeforePoint, DigitsAfterPoint;
      int[] Precision = new int[2];

      //Make the dbf file one attribute at the time
      for (int j = 0; j < _data.Columns.Count; j++)
      {
        //double -attribute
        if (_data.Columns[j].DataType == typeof(double))
        {
          DigitsBeforePoint = 0;
          DigitsAfterPoint = 0;

          //Loop to find precision
          for (int i = 0; i < _data.Rows.Count; i++)
          {
            //Don't try if no data
            if (_data.Rows[i][j] != DBNull.Value)
            {
              Precision = GetPrecision((float)(double)_data.Rows[i][j]);
              DigitsBeforePoint = Math.Max(Precision[0], DigitsBeforePoint);
              DigitsAfterPoint = Math.Max(Precision[1], DigitsAfterPoint);
            }
          }
          ShapeLib.DBFAddField(_dbfPointer, _data.Columns[j].Caption, ShapeLib.DBFFieldType.FTDouble, DigitsBeforePoint + DigitsAfterPoint + 1, DigitsAfterPoint);
        }

        //String attribute
        else if (_data.Columns[j].DataType == typeof(string))
        {
          int width = 1;
          //Loop to find cell width.
          for (int i = 0; i < _data.Rows.Count; i++)
          {
            width = Math.Max(width, (_data.Rows[i][j]).ToString().Length);
          }

          ShapeLib.DBFAddField(_dbfPointer, _data.Columns[j].Caption, ShapeLib.DBFFieldType.FTString, width, 0);
        }

        //int attribute
        else if (_data.Columns[j].DataType == typeof(int))
        {
          int width = 1;
          for (int i = 0; i < _data.Rows.Count; i++)
          {
            //Loop to find precision
            width = Math.Max(width, GetPrecision((int)_data.Rows[i][j]));
          }
          ShapeLib.DBFAddField(_dbfPointer, _data.Columns[j].Caption, ShapeLib.DBFFieldType.FTInteger, width, 0);
        }
        else if (_data.Columns[j].DataType == typeof(bool))
        {
          ShapeLib.DBFAddField(_dbfPointer, _data.Columns[j].Caption, ShapeLib.DBFFieldType.FTLogical, 1, 0);
        }
        else if (_data.Columns[j].DataType == typeof(DateTime))
        {
          ShapeLib.DBFAddField(_dbfPointer, _data.Columns[j].Caption, ShapeLib.DBFFieldType.FTDate, 8, 0);
        }
      }
    }


  }
}
