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

    public DataSetsFromDFS2(DFS2 dfsFile, int ItemNumber)
    {
      _dataFile = dfsFile;
      _itemNumber = ItemNumber;
    }


    #region IXYDataSet Members

    public Matrix Data
    {
      get { return _dataFile.GetData(0, _itemNumber); }
    }

    public double GetData(double UTMX, double UTMY)
    {
      return _dataFile.GetData(UTMX, UTMY, 0, _itemNumber);
    }

    #endregion

    #region IXYTDataSet Members

    public Matrix TimeData(int TimeStep)
    {
      return _dataFile.GetData(TimeStep, _itemNumber); 
    }

    public Matrix TimeData(DateTime TimeStep)
    {
      throw new NotImplementedException();
    }

    #endregion
  }
}
