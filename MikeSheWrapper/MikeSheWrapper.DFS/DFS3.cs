using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MikeSheWrapper.Tools;

namespace MikeSheWrapper.DFS
{
  public class DFS3:DFS2DBase
  {

    //DataBuffer. First on Item, then on timeStep. 
    private Dictionary<int, Dictionary<int, Matrix3d>> _bufferData = new Dictionary<int, Dictionary<int, Matrix3d>>();

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
      Matrix3d _data;
      Dictionary<int, Matrix3d> _timeValues;

      if (!_bufferData.TryGetValue(Item, out _timeValues))
      {
        _timeValues = new Dictionary<int, Matrix3d>();
        _bufferData.Add(Item, _timeValues);
      }
      if (!_timeValues.TryGetValue(TimeStep, out _data))
      {
        ReadItemTimeStep(TimeStep, Item);
        _data = new Matrix3d(_numberOfRows, _numberOfColumns, _numberOfLayers, dfsdata);
        _timeValues.Add(TimeStep, _data);
      }
      return _data;
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
