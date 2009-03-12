using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MikeSheWrapper.DFS
{
  public abstract class DFS2DBase:DFSBase 
  {

    public DFS2DBase(string DFSFileName)
      : base(DFSFileName)
    {
    }

    /// <summary>
    /// Gets the Column index for this coordinate. Lower left is (0,0). 
    /// Returns -1 if UTMY is left of the grid and -2 if it is right.
    /// </summary>
    /// <param name="UTMY"></param>
    /// <returns></returns>
    public int GetColumnIndex(double UTMX)
    {
      //Calculate as a double to prevent overflow errors when casting 
      double ColumnD = Math.Max(-1, Math.Floor((UTMX - (XOrigin - GridSize / 2)) / GridSize));

      if (ColumnD > _numberOfColumns)
        return -2;
      return (int)ColumnD;
    }

    /// <summary>
    /// Gets the Row index for this coordinate. Lower left is (0,0). 
    /// Returns -1 if UTMY is below the grid and -2 if it is above.
    /// </summary>
    /// <param name="UTMY"></param>
    /// <returns></returns>
    public int GetRowIndex(double UTMY)
    {
      //Calculate as a double to prevent overflow errors when casting 
      double RowD = Math.Max(-1, Math.Floor((UTMY - (YOrigin - GridSize / 2)) / GridSize));

      if (RowD > _numberOfRows)
        return -2;
      return (int)RowD;
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


    /// <summary>
    /// Gets the x-coordinate of the grid the center of the lower left
    /// Only valid for DFS2 and DFS3
    /// Remember that MikeShe does not use the center
    /// </summary>
    public double XOrigin
    {
      get
      {
        return _xOrigin;
      }
    }

    /// <summary>
    /// Gets the Y-coordinate of the grid the center of the lower left
    /// Only valid for DFS2 and DFS3
    /// Remember that MikeShe does not use the center
    /// </summary>
    public double YOrigin
    {
      get
      {
        return _yOrigin;
      }
    }

    /// <summary>
    /// Gets the grid size. Only valid for dfs2 and dfs3.
    /// </summary>
    public double GridSize 
    {
      get
      {
        return _gridSize;
      }
    }
  }
}
