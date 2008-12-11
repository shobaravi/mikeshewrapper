using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.LinearAlgebra;
using DHI.Generic.MikeZero.DFS;

namespace MikeSheWrapper.DFS
{
  /// <summary>
  /// Provides read access to a .dfs2-file
  /// </summary>
  public class DFS2 : DFS
  {

    public DFS2(string DFSFileName)
      : base(DFSFileName)
    {
      
    }

    /// <summary>
    /// Returns a Matrix with the data for the TimeStep, Item
    /// TimeStep counts from 0, Item from 1.
    /// Lower left in Matrix is (0,0)
    /// </summary>
    /// <param name="TimeStep"></param>
    /// <param name="Item"></param>
    /// <param name="Layer"></param>
    /// <returns></returns>
    public Matrix GetData(int TimeStep, int Item)
    {
      ReadItemTimeStep(TimeStep, Item);
      Matrix _data = new Matrix(_numberOfRows, _numberOfColumns);
      int m = 0;
      for (int i = 0; i < _numberOfRows; i++)
        for (int j = 0; j < _numberOfColumns; j++)
        {
          _data[i, j] = dfsdata[m];
          m++;
        }
      return _data;
    }

    /// <summary>
    /// Gets the data at coordinate set.
    /// Can be faster to use because it does not fill data into the matrix.
    /// </summary>
    /// <param name="UTMX"></param>
    /// <param name="UTMY"></param>
    /// <param name="TimeStep"></param>
    /// <param name="Item"></param>
    /// <returns></returns>
    public double GetData(double UTMX, double UTMY, int TimeStep, int Item)
    {
      ReadItemTimeStep(TimeStep, Item);
      return dfsdata[GetRowIndex(UTMY) * _numberOfColumns + GetColumnIndex(UTMX)];
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

