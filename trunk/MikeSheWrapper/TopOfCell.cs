using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MikeSheWrapper.Interfaces;
using MikeSheWrapper.Tools;

namespace MikeSheWrapper
{
  /// <summary>
  /// Small internal class used to make the top of cells available from processed data
  /// </summary>
  internal class TopOfCell:IXYZDataSet
  {
    private Matrix3d _data;

    internal TopOfCell(IXYZDataSet CellBottom, IXYDataSet TopoGraphy)
    {
      _data = new Matrix3d(CellBottom.Data[0].RowCount, CellBottom.Data[0].ColumnCount, CellBottom.Data.LayerCount);

      for (int i = 0; i < _data.LayerCount - 1; i++)
      {
        _data[i] = CellBottom.Data[i + 1];
      }

      _data[_data.LayerCount - 1] = TopoGraphy.Data;
    }


    #region IXYZDataSet Members

    public Matrix3d Data
    {
      get { return _data; }
    }

    #endregion
  }
}
