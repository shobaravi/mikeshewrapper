using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.LinearAlgebra;


namespace MikeSheWrapper.Tools
{
  public class Matrix3d : MikeSheWrapper.Tools.IMatrix3d
  {
    private Matrix[] _data;

    public Matrix3d(int NumberOfRows, int NumberOfColumns, int NumberOfLayers)
    {
      _data = new Matrix[NumberOfLayers];

      for (int i = 0; i < NumberOfLayers; i++)
        _data[i] = new Matrix(NumberOfRows, NumberOfColumns);
    }

    /// <summary>
    /// Values in DFS style! 
    /// Do not use this constructor unless you are absolutely sure that values are ordering as in DFS
    /// </summary>
    /// <param name="NumberOfRows"></param>
    /// <param name="NumberOfColumns"></param>
    /// <param name="NumberOfLayers"></param>
    /// <param name="values"></param>
    public Matrix3d(int NumberOfRows, int NumberOfColumns, int NumberOfLayers, float[] values)
    {
      _data = new Matrix[NumberOfLayers];

      int m = 0;
      for (int k = 0; k < NumberOfLayers; k++)
      {
        double[][] _jagged = new double[NumberOfRows][];
        for (int i = 0; i < NumberOfRows; i++)
        {
          _jagged[i] = new double[NumberOfColumns];
        }

        for (int i = 0; i < NumberOfRows; i++)
          for (int j = 0; j < NumberOfColumns; j++)
          {
            _jagged[i][j] = values[m];
            m++;
          }

        _data[k] = new Matrix(_jagged);
      }
    }

    /// <summary>
    /// Gets the number of layers
    /// </summary>
    public int LayerCount
    {
      get { return _data.Length; }
    }

    /// <summary>
    /// Gets and sets a value using indeces.
    /// </summary>
    /// <param name="Row"></param>
    /// <param name="Column"></param>
    /// <param name="Layer"></param>
    /// <returns></returns>
    public double this[int Row, int Column, int Layer]
    {
      get
      {
        return _data[Layer][Row, Column];
      }
      set
      {
        _data[Layer][Row, Column] = value;
      }
    }
    
    /// <summary>
    /// Gets and sets data in a column using indeces 
    /// </summary>
    /// <param name="Row"></param>
    /// <param name="Column"></param>
    /// <returns></returns>
    public Vector this[int Row, int Column]
    {
      get
      {
        Vector V = new Vector(_data.Length);

        for (int i = 0; i < _data.Length; i++)
          V[i] = this[Row, Column, i];
        return V;
      }
      set
      {
        if (value.Length != _data.Length)
          throw new Exception("Number of elements in Vector is not equal to the number of layers in the 3D object");
        for (int i = 0; i < _data.Length; i++)
          this[Row, Column, i]=value[i];
      }
    }


    /// <summary>
    /// Gets and sets a matrix using index 
    /// </summary>
    /// <param name="Layer"></param>
    /// <returns></returns>
    public Matrix this[int Layer]
    {
      get
      {
        return _data[Layer];
      }
      set
      {
        _data[Layer] = value;
      }
    }
  }
}
