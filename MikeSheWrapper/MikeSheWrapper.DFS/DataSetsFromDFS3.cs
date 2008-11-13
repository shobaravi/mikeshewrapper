using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MikeSheWrapper.Interfaces;
using MikeSheWrapper.Tools;

namespace MikeSheWrapper.DFS
{
  public class DataSetsFromDFS3 : IXYZDataSet, IXYZTDataSet
  {
    private DFS3 _dataFile;
    private int _itemNumber;

    //Buffer to remember the data that has already been read
    private Dictionary<int, Matrix3d> _bufferedData = new Dictionary<int,Matrix3d>();

    public DataSetsFromDFS3(DFS3 dfsFile, int ItemNumber)
    {
      _dataFile = dfsFile;
      _itemNumber = ItemNumber;
    }

    #region IXYZDataSet Members

    public IMatrix3d Data
    {
      get
      {
        return TimeData(0);
      }
    }

    #endregion


    #region IXYZTDataSet Members

    public IMatrix3d TimeData(int TimeStep)
    {
      if (!_bufferedData.ContainsKey(TimeStep))
        _bufferedData.Add(TimeStep, _dataFile.GetData(TimeStep, _itemNumber));
      return _bufferedData[TimeStep];
    }

    public IMatrix3d TimeData(DateTime TimeStep)
    {
      throw new NotImplementedException();
    }

    #endregion
  }
}
