using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MikeSheWrapper.Tools
{
  public class DBFWriter : DBF
  {

    private List<DataRow> _rows = new List<DataRow>();

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

      foreach(DataRow Dr in DT.Rows)
        _rows.Add(Dr);
    }

    /// <summary>
    /// Writes a datarow
    /// </summary>
    /// <param name="DR"></param>
    public void WriteData(DataRow DR)
    {
      _rows.Add(DR);
    }

    public void Flush()
    {
      if ((int)_dbfPointer == 0)
        CreateBDF();

      //Now fill in the data
      for (int j = 0; j < _rows[0].Table.Columns.Count; j++)
      {
        //double data
        if (_rows[0].Table.Columns[j].DataType == typeof(double))
        {
          for (int i = 0; i < _rows.Count; i++)
          {
            if (_rows[i][j]!=DBNull.Value)
              ShapeLib.DBFWriteDoubleAttribute(_dbfPointer, i, j , (double)_rows[i][j]);
          }
        }
        // int data
        else if (_rows[0].Table.Columns[j].DataType == typeof(int))
        {
          for (int i = 0; i < _rows.Count; i++)
          {
            if (_rows[i][j] != DBNull.Value)
              ShapeLib.DBFWriteIntegerAttribute(_dbfPointer, i, j, (int)_rows[i][j]);
          }
        }
        //string data
        else if (_rows[0].Table.Columns[j].DataType == typeof(string))
        {
          for (int i = 0; i < _rows.Count; i++)
          {
            int ok =ShapeLib.DBFWriteStringAttribute(_dbfPointer, i, j, _rows[i][j].ToString());
          }
        }
        //DateTime data
        else if (_rows[0].Table.Columns[j].DataType == typeof(DateTime))
        {
          for (int i = 0; i < _rows.Count; i++)
          {
            if (_rows[i][j] != DBNull.Value)
              ShapeLib.DBFWriteDateAttribute(_dbfPointer, i, j, (DateTime)_rows[i][j]);
          }
        }
        else if (_rows[0].Table.Columns[j].DataType == typeof(bool))
        {
          for (int i = 0; i < _rows.Count; i++)
          {
            ShapeLib.DBFWriteLogicalAttribute(_dbfPointer, i, j, (bool)_rows[i][j]);
          }
        }

      }
      _rows.Clear();
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
      for (int j = 0; j < _rows[0].Table.Columns.Count; j++)
      {
        //double -attribute
        if (_rows[0].Table.Columns[j].DataType == typeof(double))
        {
          DigitsBeforePoint = 0;
          DigitsAfterPoint = 0;

          bool AllNulls = true;
          //Loop to find precision
          for (int i = 0; i < _rows.Count; i++)
          {
            //Don't try if no data
            if (_rows[i][j] != DBNull.Value)
            {
              AllNulls = false;
              Precision = GetPrecision((float)(double)_rows[i][j]);
              DigitsBeforePoint = Math.Max(Precision[0], DigitsBeforePoint);
              DigitsAfterPoint = Math.Max(Precision[1], DigitsAfterPoint);
            }
          }
          //Set default precision
          if (AllNulls)
          {
            DigitsAfterPoint = 5;
            DigitsBeforePoint = 7;
          }
          ShapeLib.DBFAddField(_dbfPointer, _rows[0].Table.Columns[j].Caption, ShapeLib.DBFFieldType.FTDouble, DigitsBeforePoint + DigitsAfterPoint + 1, DigitsAfterPoint);
        }

        //String attribute
        else if (_rows[0].Table.Columns[j].DataType == typeof(string))
        {
          int width = 10;
          //Loop to find cell width.
          for (int i = 0; i < _rows.Count; i++)
          {
            width = Math.Max(width, (_rows[i][j]).ToString().Length);
          }

          ShapeLib.DBFAddField(_dbfPointer, _rows[0].Table.Columns[j].Caption, ShapeLib.DBFFieldType.FTString, width, 0);
        }

        //int attribute
        else if (_rows[0].Table.Columns[j].DataType == typeof(int))
        {
          int width = 5;
          for (int i = 0; i < _rows.Count; i++)
          {
            //Loop to find precision
            //Don't try if no data
            if (_rows[i][j] != DBNull.Value)
              width = Math.Max(width, GetPrecision((int)_rows[i][j]));
          }
          ShapeLib.DBFAddField(_dbfPointer, _rows[0].Table.Columns[j].Caption, ShapeLib.DBFFieldType.FTInteger, width, 0);
        }
        else if (_rows[0].Table.Columns[j].DataType == typeof(bool))
        {
          ShapeLib.DBFAddField(_dbfPointer, _rows[0].Table.Columns[j].Caption, ShapeLib.DBFFieldType.FTLogical, 1, 0);
        }
        else if (_rows[0].Table.Columns[j].DataType == typeof(DateTime))
        {
          ShapeLib.DBFAddField(_dbfPointer, _rows[0].Table.Columns[j].Caption, ShapeLib.DBFFieldType.FTDate, 8, 0);
        }
      }
    }


  }
}
