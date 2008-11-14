using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MikeSheWrapper.Interfaces;
using MikeSheWrapper.Tools;

namespace MikeSheWrapper
{
  internal class PhreaticPotential:IXYZTDataSet
  {
    IXYZTDataSet _potential;
    IXYZDataSet _bottomOfCell;
    IXYZDataSet _thicknessOfCell;
    private double _phreaticFactor = 0.5;
    private double _deleteValue = 1e-35;

    //Buffer on the timesteps
    Dictionary<int, PhreaticPotentialData> _bufferedData = new Dictionary<int, PhreaticPotentialData>();
    private static object _lock = new object();

    internal PhreaticPotential(IXYZTDataSet Potential, MikeSheGridInfo Grid)
    {
      _potential = Potential;
      _bottomOfCell = Grid.LowerLevelOfComputationalLayers;
      _thicknessOfCell = Grid.ThicknessOfComputationalLayers;
    }


    #region IXYZTDataSet Members

    /// <summary>
    /// Returns the phreatic potential.
    /// Note that it returns a reference to a matrix
    /// </summary>
    /// <param name="TimeStep"></param>
    /// <returns></returns>
    public IMatrix3d TimeData(int TimeStep)
    {
      PhreaticPotentialData PC;
      lock (_lock)
      {
        if (!_bufferedData.TryGetValue(TimeStep, out PC))
        {
          PC = new PhreaticPotentialData(_potential.TimeData(TimeStep), _bottomOfCell.Data, _thicknessOfCell.Data, _phreaticFactor, _deleteValue);
          _bufferedData.Add(TimeStep, PC);
        }
      }
      return PC;
    }

    public IMatrix3d TimeData(DateTime TimeStep)
    {
      return TimeData(10);
    }

    #endregion
  }
}
