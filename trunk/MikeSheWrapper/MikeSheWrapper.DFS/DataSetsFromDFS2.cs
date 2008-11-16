using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;

using MathNet.Numerics.LinearAlgebra;
using MikeSheWrapper.Interfaces;

namespace MikeSheWrapper.DFS
{
  public class DataSetsFromDFS2:IXYDataSet,IXYTDataSet
  {
    private DFS2 _dataFile;
    private int _itemNumber;
    private static object _lock = new object();

    private Dictionary<int, Matrix> _bufferedData = new Dictionary<int, Matrix>(); 

    public DataSetsFromDFS2(DFS2 dfsFile, int ItemNumber)
    {
      _dataFile = dfsFile;
      _itemNumber = ItemNumber;
    }


    #region IXYDataSet Members

    public Matrix Data
    {
      get { return TimeData(0); }
    }

    public double GetData(double UTMX, double UTMY)
    {
      return TimeData(0)[_dataFile.GetRowIndex(UTMY), _dataFile.GetColumnIndex(UTMX)];
    }

    #endregion

    #region IXYTDataSet Members

    public Matrix TimeData(int TimeStep)
    {
      lock (_lock)
      {
        if (!_bufferedData.ContainsKey(TimeStep))
          _bufferedData.Add(TimeStep, _dataFile.GetData(TimeStep, _itemNumber));
      }
      return _bufferedData[TimeStep];
    }

    public Matrix TimeData(DateTime TimeStep)
    {
      return TimeData(_dataFile.GetTimeStep(TimeStep));
    }

    #endregion
  }
}
