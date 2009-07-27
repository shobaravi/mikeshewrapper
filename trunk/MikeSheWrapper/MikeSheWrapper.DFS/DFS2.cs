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
  /// Provides read and write access to a .dfs2-file.
  /// Buffers all data being read. 
  /// </summary>
  public class DFS2 : DFS2DBase
  {
    //DataBuffer. First on Item, then on timeStep. 
    private Dictionary<int, Dictionary<int, Matrix>> _bufferData = new Dictionary<int, Dictionary<int, Matrix>>();

        
    public DFS2(string DFSFileName)
      : base(DFSFileName)
    {
      
    }

    /// <summary>
    /// Sets the data for the timestep and item
    /// </summary>
    /// <param name="TimeStep"></param>
    /// <param name="Item"></param>
    /// <param name="Data"></param>
    public void SetData(int TimeStep, int Item, Matrix Data)
    {
      float[] fdata = new float[Data.ColumnCount * Data.RowCount]; 
      int m = 0;
      for (int i = 0; i < Data.RowCount; i++)
        for (int j = 0; j < Data.ColumnCount; j++)
        {
          fdata[m] = (float) Data[i, j];
          m++;
        }
      WriteItemTimeStep(TimeStep, Item, fdata);
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
      Matrix _data;
      Dictionary<int, Matrix> _timeValues;
     
      if (!_bufferData.TryGetValue(Item, out _timeValues))
      {
        _timeValues = new Dictionary<int, Matrix>();
        _bufferData.Add(Item, _timeValues);
      }
      if (!_timeValues.TryGetValue(TimeStep, out _data))
      {
        ReadItemTimeStep(TimeStep, Item);
        _data = new Matrix(_numberOfRows, _numberOfColumns);
        int m = 0;
        for (int i = 0; i < _numberOfRows; i++)
          for (int j = 0; j < _numberOfColumns; j++)
          {
            _data[i, j] = dfsdata[m];
            m++;
          }
        _timeValues.Add(TimeStep, _data);
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

    

  }

}

