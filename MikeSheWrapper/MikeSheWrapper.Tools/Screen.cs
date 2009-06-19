using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MikeSheWrapper.Tools
{
  public class Screen
  {
    private double _depthToBottom;
    private double _depthToTop;
    private IIntake _intake;
    public int Number { get; set; }

    public Screen(IIntake Intake)
    {
      _intake = Intake;
      _intake.Screens.Add(this);

    }

    /// <summary>
    /// Gets and sets the depth to the top in meters below surface
    /// </summary>
    public double DepthToTop
    {
      get { return _depthToTop; }
      set { _depthToTop = value; }
    }

    /// <summary>
    /// Gets and sets the depth to the bottom in meters below surface
    /// </summary>
    public double DepthToBottom
    {
      get { return _depthToBottom; }
      set { _depthToBottom = value; }
    }

    /// <summary>
    /// Gets and sets the top in meters above sea level
    /// This property requires that the terrain level of the well is set.
    /// </summary>
    public double TopAsKote
    {
      get { return _intake.well.Terrain - _depthToTop; }
      set
      {
        _depthToTop = _intake.well.Terrain - value;
      }
    }

    /// <summary>
    /// Gets and sets the bottom in meters above sea level.
    /// This property requires that the terrain level of the well is set.
    /// </summary>
    public double BottomAsKote
    {
      get { return _intake.well.Terrain - _depthToBottom; }
      set
      {
        _depthToBottom = _intake.well.Terrain - value;
      }
    }

  }
}
