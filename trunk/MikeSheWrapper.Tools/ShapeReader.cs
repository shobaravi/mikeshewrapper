using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;


namespace MikeSheWrapper.Tools
{
  public class ShapeReader
  {

    private IntPtr _shapePointer;
    private IntPtr _dbfPointer;
    private DataTable _data;
    private int _recordPointer = 0;
    private int _noOfEntries;

    private Dictionary<string, DBFEntry> _columns;

    public Dictionary<string, DBFEntry> Columns
    {
      get { return _columns; }
    }

    public ShapeReader(string FileName)
    {
      // Open shapefile
      _shapePointer = ShapeLib.SHPOpen(FileName, "rb");
      _dbfPointer = ShapeLib.DBFOpen(FileName, "rb");

      _columns = new Dictionary<string, DBFEntry>();

      int NoOfColumns = ShapeLib.DBFGetFieldCount(_dbfPointer);
      _noOfEntries = ShapeLib.DBFGetRecordCount(_dbfPointer);

      for (int i = 0; i < NoOfColumns; i++)
      {
        DBFEntry E = new DBFEntry();
        StringBuilder Name = new StringBuilder();

        E._dbfType = ShapeLib.DBFGetFieldInfo(_dbfPointer, i, Name, ref E._width, ref E._decimals);
        E.name = Name.ToString();
        E._index =i;
         

        //Find the corresponding .NET Type
        switch (E._dbfType)
        {
          case ShapeLib.DBFFieldType.FTDate:
            E._dotNetType = typeof(DateTime);
            break;
          case ShapeLib.DBFFieldType.FTDouble:
            E._dotNetType = typeof(double);
            break;
          case ShapeLib.DBFFieldType.FTInteger:
            E._dotNetType = typeof(int);
            break;
          case ShapeLib.DBFFieldType.FTLogical:
            E._dotNetType = typeof(bool);
            break;
          case ShapeLib.DBFFieldType.FTString:
            E._dotNetType = typeof(string);
            break;
          case ShapeLib.DBFFieldType.FTInvalid:
          default:
            E._dotNetType = typeof(object);
            break;
        }

        _columns.Add(E.name, E);
      }
    }

    private void InitializeDataTable()
    {
      _data = new DataTable();
      _data.Columns.Add("ShapeID", typeof(int));

      foreach(DBFEntry E in _columns.Values)
        _data.Columns.Add(E.name, E._dotNetType);
      
    }

    public DataTable Read()
    {
      if (_data == null)
        InitializeDataTable();

      _recordPointer = 0;
      _data.Clear();

      while (!EndOfData)
      {
        DataRow dr = _data.NewRow();
        ReadNext(dr);
        _data.Rows.Add(dr);
      }
      return _data;
    }

    /// <summary>
    /// Reads the next row of data columns corresponding to the format in the datarow
    /// </summary>
    /// <returns></returns>
    public void ReadNext(DataRow dr)
    {
      for (int j = 0; j < dr.Table.Columns.Count; j++)
      {
        if (dr.Table.Columns[j].ColumnName.Equals("ShapeID"))
        {
          IntPtr pShape = ShapeLib.SHPReadObject(_shapePointer, _recordPointer);
          ShapeLib.SHPObject shpObject = new ShapeLib.SHPObject();
          Marshal.PtrToStructure(pShape, shpObject);
          dr[j] = shpObject.nShapeId;
          ShapeLib.SHPDestroyObject(pShape);
        }
        else
        {
          DBFEntry E = _columns[dr.Table.Columns[j].ColumnName];

          //Find the corresponding .NET Type
          switch (E._dbfType)
          {
            case ShapeLib.DBFFieldType.FTDate:
              dr[j] = ShapeLib.DBFReadDateTimeAttribute(_dbfPointer, _recordPointer, E._index);
              break;
            case ShapeLib.DBFFieldType.FTDouble:
              dr[j] = ShapeLib.DBFReadDoubleAttribute(_dbfPointer, _recordPointer, E._index);
              break;
            case ShapeLib.DBFFieldType.FTInteger:
              dr[j] = ShapeLib.DBFReadIntegerAttribute(_dbfPointer, _recordPointer, E._index);
              break;
            case ShapeLib.DBFFieldType.FTLogical:
              dr[j] = ShapeLib.DBFReadLogicalAttribute(_dbfPointer, _recordPointer, E._index);
              break;
            case ShapeLib.DBFFieldType.FTString:
              dr[j] = ShapeLib.DBFReadStringAttribute(_dbfPointer, _recordPointer, E._index);
              break;
            case ShapeLib.DBFFieldType.FTInvalid:
            default:
              break;
          }
        }
      }
      _recordPointer++;
    }

    /// <summary>
    /// Returns true if the end of the data set have been reached
    /// </summary>
    public bool EndOfData
    {
      get { return _recordPointer >= _noOfEntries; }
    }

    public double ReadDouble(int record, string ColumnName)
    {
      return ShapeLib.DBFReadDoubleAttribute(_dbfPointer, record, _columns[ColumnName]._index);
    }

    public int ReadInt(int record, string ColumnName)
    {
      return ShapeLib.DBFReadIntegerAttribute(_dbfPointer, record, _columns[ColumnName]._index);
    }

    public DateTime ReadDate(int record, string ColumnName)
    {
      return ShapeLib.DBFReadDateTimeAttribute(_dbfPointer, record, _columns[ColumnName]._index);
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
