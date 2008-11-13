using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.LinearAlgebra;
using MikeSheWrapper.Interfaces;

namespace MikeSheWrapper.DFS
{
  public class DataSetsFromDFS2:IXYDataSet,IXYTDataSet
  {
    private DFS2 _dataFile;
    private int _itemNumber;

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
      if (!_bufferedData.ContainsKey(TimeStep))
        _bufferedData.Add(TimeStep, _dataFile.GetData(TimeStep, _itemNumber));
      return _bufferedData[TimeStep];
    }

    public Matrix TimeData(DateTime TimeStep)
    {
      throw new NotImplementedException();
    }

    #endregion
  }
}
