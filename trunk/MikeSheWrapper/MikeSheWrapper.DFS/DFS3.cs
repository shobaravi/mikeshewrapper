using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MikeSheWrapper.Tools;

namespace MikeSheWrapper.DFS
{
  public class DFS3:DFS2DBase
  {
    /// <summary>
    /// Provides read access to a .DFS3 file.
    /// </summary>
    /// <param name="DFSFileName"></param>
    public DFS3(string DFSFileName):base(DFSFileName)
    {
    }

    /// <summary>
    /// Returns a Matrix3D with the data for the TimeStep, Item
    /// TimeStep counts from 0, Item from 1.
    /// </summary>
    /// <param name="TimeStep"></param>
    /// <param name="Item"></param>
    /// <returns></returns>
    public Matrix3d GetData(int TimeStep, int Item)
    {
      ReadItemTimeStep(TimeStep, Item);

      return new Matrix3d(_numberOfRows, _numberOfColumns, _numberOfLayers, dfsdata);
    }

    /// <summary>
    /// Gets the number of Layers
    /// </summary>
    public int NumberOfLayers
    {
      get
      {
        return _numberOfLayers;
      }
    }

  }
}
