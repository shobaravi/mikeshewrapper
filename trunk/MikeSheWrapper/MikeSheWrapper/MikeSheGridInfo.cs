using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;

using MathNet.Numerics.LinearAlgebra;

using MikeSheWrapper.Interfaces;
using MikeSheWrapper.DFS;

namespace MikeSheWrapper
{
  public class MikeSheGridInfo
  {
    private ProcessedData _pd;
    private double _gridSize;
    private double _xOrigin;
    private double _yOrigin;
    private double _deleteValue;

    private int _numberOfColumns;
    private int _numberOfRows;
    private int _numberOfLayers;

    internal MikeSheGridInfo(ProcessedData PD, DFS3 _dataFile)
    {
      _pd = PD;
      _deleteValue = _dataFile.DeleteValue;
      _gridSize = _dataFile.DynamicItemInfos[0].DX;

      _numberOfRows = _dataFile.DynamicItemInfos[0].YCoords.Length;
      _numberOfColumns = _dataFile.DynamicItemInfos[0].XCoords.Length;
      _numberOfLayers = _dataFile.DynamicItemInfos[0].ZCoords.Length;

      //For MikeShe the origin is lower left whereas it is center of lower left for DFS
      _xOrigin = _dataFile.Longitude;
      _yOrigin = _dataFile.Latitude;
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
      double ColumnD = Math.Max(-1, Math.Floor((UTMX - _xOrigin) / _gridSize));

      if (ColumnD > _numberOfColumns)
        return -2;
      return (int) ColumnD;
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
      double RowD = Math.Max(-1, Math.Floor((UTMY - _yOrigin) / _gridSize));

      if (RowD > _numberOfRows)
        return -2;
      return (int)RowD;
    }


    /// <summary>
    /// Returns the indeces for a set of coordinates.
    /// Necessary to sent the output as par
    /// Returns true if the grid point is within the active domain.
    /// Note that Column and Row may have positive values and still the point is outside of the active domain
    /// Note this will may return a different value than the DFS-file!!!
    /// </summary>
    /// <param name="X"></param>
    /// <param name="Y"></param>
    /// <param name="Column"></param>
    /// <param name="Row"></param>
    public bool GetIndex(double X, double Y, out int Column, out int Row)
    {
      Column = GetColumnIndex(X);
      Row = GetRowIndex(Y);
      if (Column < 0 | Row < 0)
        return false;
      return 1 == _pd.ModelDomainAndGrid.Data[Row, Column];
    }

    /// <summary>
    /// Returns the layer number. Lower layer is 0. 
    /// If -1 is returned Z is above the surface and if -2 is returned Z is below the bottom.
    /// </summary>
    /// <param name="Column"></param>
    /// <param name="Row"></param>
    /// <param name="Z"></param>
    /// <returns></returns>
    public int GetLayer(int Column, int Row, double Z)
    {
      if (Z > _pd.SurfaceTopography.Data[Row, Column])
        return -1;
      else if (Z < _pd.LowerLevelOfComputationalLayers.Data[Row, Column, 0])
        return -2;
      else
      {
        int i = 0;
        while (Z < _pd.LowerLevelOfComputationalLayers.Data[Row, Column, i])
          i++;
        return i - 1;
      }
    }

        /// <summary>
    /// Returns the X-coordinate of the left cell-boundary
    /// </summary>
    /// <param name="Column"></param>
    /// <returns></returns>
    public double GetX(int Column)
    {
      return _xOrigin + _gridSize * Column;
    }
    /// <summary>
    /// Returns the Y-coordinate of the lower cell-boundary
    /// </summary>
    /// <param name="Row"></param>
    /// <returns></returns>
    public double GetY(int Row)
    {
      return _yOrigin + _gridSize * Row;
    }
    /// <summary>
    /// Returns the X-coordinate of the cell-center
    /// </summary>
    /// <param name="Column"></param>
    /// <returns></returns>
    public double GetXCenter(int Column)
    {
      return GetX(Column) + 0.5 * _gridSize;
    }
    /// <summary>
    /// Returns the Y-coordinate of the cell-center
    /// </summary>
    /// <param name="Row"></param>
    /// <returns></returns>
    public double GetYCenter(int Row)
    {
      return GetY(Row) + 0.5 * _gridSize;
    }



    public double Interpolate(double X, double Y, Matrix M, out int DeleteValues, out int BoundaryCells)
    {
      BoundaryCells = 0;
      DeleteValues = 0;


      int column = GetColumnIndex(X);
      int row = GetRowIndex(Y);

      if (M[row, column] == _deleteValue)
        return _deleteValue;

      int columnLL = column;
      int rowLL = row;

      //Finds the coordinate of the lower left of the four cells closest to the point
      if (X < GetXCenter(column))
      {
        columnLL -= 1;
      }
      if (X < GetYCenter(row))
      {
        rowLL -= 1;
      }

      double InterpolatedValue = 0;
      double dx1 = GetXCenter(columnLL) - X;
      double dy1 = GetYCenter(rowLL) - Y;
      double dx3 = GetXCenter(columnLL + 1) - X;
      double dy3 = GetYCenter(rowLL + 1) - Y;

      //Get the values of the four points
      double P1 = M[rowLL, columnLL];
      if (_pd.ModelDomainAndGrid.Data[rowLL, columnLL] == 2)
      {
        P1 = _deleteValue;
        BoundaryCells++;
      }

      double P2 = M[rowLL+1, columnLL];
      if (_pd.ModelDomainAndGrid.Data[rowLL+1, columnLL] == 2)
      {
        P2 = _deleteValue;
        BoundaryCells++;
      }

      double P3 = M[rowLL + 1, columnLL + 1 ];
      if (_pd.ModelDomainAndGrid.Data[rowLL + 1, columnLL+1] == 2)
      {
        P3 = _deleteValue;
        BoundaryCells++;
      }

      double P4 = M[rowLL, columnLL + 1];
      if (_pd.ModelDomainAndGrid.Data[rowLL, columnLL + 1] == 2)
      {
        P4 = _deleteValue;
        BoundaryCells++;
      }

      //Inverse distance
      if (P1 == _deleteValue | P2 == _deleteValue | P3 == _deleteValue | P4 == _deleteValue)
      {
        double distance = 0;
        if (P1 != _deleteValue)
        {
          double d1 = Math.Pow(Math.Pow(dx1, 2) + Math.Pow(dy1, 2), -1.5);
          distance += d1;
          InterpolatedValue += d1 * P1;
        }
        if (P2 != _deleteValue)
        {
          double d2 = Math.Pow(Math.Pow(dx1, 2) + Math.Pow(dy3, 2), -1.5);
          distance += d2;
          InterpolatedValue += d2 * P2;
        }
        if (P3 != _deleteValue)
        {
          double d3 = Math.Pow(Math.Pow(dx3, 2) + Math.Pow(dy3, 2), -1.5);
          distance += d3;
          InterpolatedValue += d3 * P3;
        }
        if (P4 != _deleteValue)
        {
          double d4 = Math.Pow(Math.Pow(dx3, 2) + Math.Pow(dy1, 2), -1.5);
          distance += d4;
          InterpolatedValue += d4 * P4;
        }
        InterpolatedValue = InterpolatedValue / distance;
      }
      else
      {
        //Four-point bilinear interpolation
        P1 = P1 * dx3 * dy3;
        P2 = P2 * dx3 * (-dy1);
        P3 = P3 * (-dx1) * (-dy1);
        P4 = P4 * (-dx1) * dy3;
        InterpolatedValue = (P1 + P2 + P3 + P4) / Math.Pow(_gridSize, 2);
      }

      return InterpolatedValue;


    }

  }
}
