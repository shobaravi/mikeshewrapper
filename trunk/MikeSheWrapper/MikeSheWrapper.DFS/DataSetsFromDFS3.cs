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

    public Matrix3d Data
    {
      get { return _dataFile.GetData(0, _itemNumber); }
    }

    #endregion


    #region IXYZTDataSet Members

    public Matrix3d TimeData(int TimeStep)
    {
      return _dataFile.GetData(TimeStep, _itemNumber); 
    }

    public Matrix3d TimeData(DateTime TimeStep)
    {
      throw new NotImplementedException();
    }

    #endregion
  }
}
