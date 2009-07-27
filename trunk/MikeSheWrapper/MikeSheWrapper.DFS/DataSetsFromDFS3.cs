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
      return _dataFile.GetData(TimeStep, _itemNumber);
    }

    public IMatrix3d TimeData(DateTime TimeStep)
    {
      return TimeData(_dataFile.GetTimeStep(TimeStep));
    }

    public int GetTimeStep(DateTime TimeStep)
    {
      return _dataFile.GetTimeStep(TimeStep);
    }

    #endregion
  }
}
