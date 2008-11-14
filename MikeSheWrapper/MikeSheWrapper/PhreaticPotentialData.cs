using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MathNet.Numerics.LinearAlgebra;

using MikeSheWrapper.Tools;
using MikeSheWrapper.Interfaces;

namespace MikeSheWrapper
{
  internal class PhreaticPotentialData:IMatrix3d 
  {
    //Buffer on the layers
    Dictionary<int, Matrix> _bufferedData = new Dictionary<int, Matrix>();
    IMatrix3d _potential;
    IMatrix3d _bottomOfCell;
    IMatrix3d _thicknessOfCell;

    private double _phreaticFactor;
    private double _deletevalue;
    private static object _lock = new object();


    internal PhreaticPotentialData(IMatrix3d Potential, IMatrix3d BottomOfCell, IMatrix3d ThicknessOfCell, double PhreaticFactor, double DeleteValue)
    {
      _potential = Potential;
      _bottomOfCell = BottomOfCell;
      _thicknessOfCell = ThicknessOfCell;
      _deletevalue = DeleteValue;
      _phreaticFactor = PhreaticFactor;
    }



    #region IMatrix3d Members

    public int LayerCount
    {
      get { return _bottomOfCell.LayerCount; }
    }

    public Matrix this[int Layer]
    {
      get
      {
        Matrix M;
        lock (_lock)
        {
          if (!_bufferedData.TryGetValue(Layer, out M))
          {
            M = new Matrix(_bottomOfCell[0].RowCount,_bottomOfCell[0].ColumnCount);
            _bufferedData.Add(Layer, M);
            SetMatrix(Layer, M);
          }
        }
        return M;
      }
      set
      {
        throw new NotImplementedException();
      }
    }

    public Vector this[int Row, int Column]
    {
      get
      {
        Vector V = new Vector(LayerCount);

        for (int i = 0; i < LayerCount; i++)
          V[i] = this[Row, Column, i];
        return V;
      }
      set
      {
        throw new NotImplementedException();
      }
    }

    public double this[int Row, int Column, int Layer]
    {
      get
      {
        return this[Layer][Row,Column];
      }
      set
      {
        throw new NotImplementedException();
      }
    }

    /// <summary>
    /// Sets the potential in a point
    /// </summary>
    /// <param name="Row"></param>
    /// <param name="Column"></param>
    /// <param name="Layer"></param>
    private void SetMatrix(int Layer, Matrix M)
    {
      for (int Row = 0; Row < _bottomOfCell[0].RowCount; Row++)
      {
        for (int Column = 0; Column < _bottomOfCell[0].ColumnCount; Column++)
        {
          //In the bottom layer the phreatic potential is always equal to the potential
          if (Layer != 0)
          {
            double HeadAboveBottom = _potential[Row, Column, Layer] - _bottomOfCell[Row, Column, Layer];
            //Criteria 1: The distance from the bottom of the cell to the potential shall be lower 
            //than the distance from the bottom of the cell to the potential in the cell below 
            if (HeadAboveBottom < _bottomOfCell[Row, Column, Layer] - _potential[Row, Column, Layer - 1])
              // Criteria 2: The potential shall be below the middle of the cell. _phreaticFactor = 0.5
              if (HeadAboveBottom < _thicknessOfCell[Row, Column, Layer] * _phreaticFactor)
              {
                M[Row, Column] = _deletevalue;
                break;
              }
          }
          //The phreatic potential is equal to the potential 
          M[Row, Column] = _potential[Row, Column, Layer];
        }
      }
    }
    

    #endregion
  }
}
