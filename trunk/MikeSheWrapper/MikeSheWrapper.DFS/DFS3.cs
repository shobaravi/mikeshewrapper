using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MikeSheWrapper.Tools;

namespace MikeSheWrapper.DFS
{
  public class DFS3:DFS
  {
    public DFS3(string DFSFileName):base(DFSFileName)
    {
    }


    public Matrix3d GetData(int TimeStep, int Item)
    {
      readNextItemTimeStep(TimeStep, Item);

      Matrix3d _data = new Matrix3d(_numberOfRows, _numberOfColumns, _numberOfLayers);
      int m = 0;
      for (int k = 0; k < _numberOfLayers; k++)
        for (int i = 0; i < _numberOfRows; i++)
          for (int j = 0; j < _numberOfColumns; j++)
          {
            _data[i, j, k] = (double) dfsdata[m];
            m++;
          }
      return _data;
    }

    /// <summary>
    /// Gets the number of Layers
    /// </summary>
    public int NumberOfLayers
    {
      get
      {
        return _numberOfLayers;
      }
    }

    /// <summary>
    /// Gets the number of columns
    /// </summary>
    public int NumberOfColumns
    {
      get
      {
        return _numberOfColumns;
      }
    }

    /// <summary>
    /// Gets the number of rows
    /// </summary>
    public int NumberOfRows
    {
      get
      {
        return _numberOfRows;
      }
    }
  }
}
