using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MikeSheWrapper.Interfaces;

namespace MikeSheWrapper
{
  public class GridInfo
  {
    private IXYDataSet _topoGraphy;
    private IXYZDataSet _bottomOfCells;
    private IXYDataSet _modelDomain;
    private IXYZDataSet _sZBoundaries;

    //public int GetLayerIndex(double UTMX, double UTMY, double Z)
    //{
    //  return GetLayerIndex(GetColumnIndex(UTMX), GetRowIndex(UTMY), Z);
    //}

    //public int GetLayerIndex(int Column, int Row, double Z)
    //{
    //  int Layer = -1;

    //  return Layer;

    //}

  }
}
