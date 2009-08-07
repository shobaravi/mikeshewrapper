using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MikeSheWrapper.Tools;

namespace MikeSheWrapper.DFS
{
  public class DFS3:DFS2DBase
  {
    //Static list holding all Matrices read from dfs2-files
    private static Dictionary<string, Dictionary<int, Dictionary<int, CacheEntry>>> SuperCache = new Dictionary<string, Dictionary<int, Dictionary<int, CacheEntry>>>();
    private static LinkedList<CacheEntry> AccessList = new LinkedList<CacheEntry>();
    public static int MaxEntriesInBuffer = 25;

    //DataBuffer. First on Item, then on timeStep. 
    private Dictionary<int, Dictionary<int, CacheEntry>> _bufferData;

    /// <summary>
    /// Provides read access to a .DFS3 file.
    /// </summary>
    /// <param name="DFSFileName"></param>
    public DFS3(string DFSFileName):base(DFSFileName)
    {
      if (!SuperCache.TryGetValue(AbsoluteFileName, out _bufferData))
      {
        _bufferData = new Dictionary<int, Dictionary<int, CacheEntry>>();
        SuperCache.Add(AbsoluteFileName, _bufferData);
      }
    }

    /// <summary>
    /// Returns a Matrix3D with the data for the TimeStep, Item
    /// TimeStep counts from 0, Item from 1.
    /// 
    /// </summary>
    /// <param name="TimeStep"></param>
    /// <param name="Item"></param>
    /// <returns></returns>
    public Matrix3d GetData(int TimeStep, int Item)
    {

      Dictionary<int, CacheEntry> _timeValues;
      CacheEntry cen;

      if (!_bufferData.TryGetValue(Item, out _timeValues))
      {
        _timeValues = new Dictionary<int, CacheEntry>();
        _bufferData.Add(Item, _timeValues);
      }
      if (!_timeValues.TryGetValue(TimeStep, out cen))
      {
        ReadItemTimeStep(TimeStep, Item);
        Matrix3d _data = new Matrix3d(_numberOfRows, _numberOfColumns, _numberOfLayers, dfsdata);
        cen = new CacheEntry(AbsoluteFileName, Item, TimeStep, _data);
        _timeValues.Add(TimeStep, cen);
        CheckBuffer();
      }
      else
        AccessList.Remove(cen);

      AccessList.AddLast(cen);
      return cen.Data3d;
    }

    /// <summary>
    /// Removes the oldest Matrix from the dictionary if the Accesslist contains more than MaxNumberOfEntries
    /// </summary>
    private void CheckBuffer()
    {
      if (AccessList.Count > MaxEntriesInBuffer)
      {
        CacheEntry ToRemove = AccessList.First.Value;
        SuperCache[ToRemove.FileName][ToRemove.Item].Remove(ToRemove.TimeStep);
        AccessList.RemoveFirst();
      }
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
