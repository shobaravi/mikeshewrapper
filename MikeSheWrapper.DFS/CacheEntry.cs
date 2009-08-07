using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MikeSheWrapper.Tools;
using MathNet.Numerics.LinearAlgebra;


namespace MikeSheWrapper.DFS
{
  #region Small class holding info about data and the data
  internal class CacheEntry : IEquatable<CacheEntry>
  {
    internal CacheEntry(string FileName, int Item, int TimeStep)
    {
      this.FileName = FileName;
      this.Item = Item;
      this.TimeStep = TimeStep;
    }

    internal CacheEntry(string FileName, int Item, int TimeStep, Matrix Data):this(FileName, Item, TimeStep)
    {
      this.Data = Data;
    }

    internal CacheEntry(string FileName, int Item, int TimeStep, Matrix3d Data)
      : this(FileName, Item, TimeStep)
    {
      this.Data3d = Data;
    }

    internal string FileName;
    internal int Item;
    internal int TimeStep;
    internal Matrix Data;
    internal Matrix3d Data3d;

    #region IEquatable<CacheId> Members

    public bool Equals(CacheEntry other)
    {
      return FileName.Equals(other.FileName) & Item.Equals(other.Item) & TimeStep.Equals(other.TimeStep);
    }

    #endregion
  }
  #endregion
}
