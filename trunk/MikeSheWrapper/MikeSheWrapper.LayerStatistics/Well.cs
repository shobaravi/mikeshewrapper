using System.Collections;
using System;
/**
 * @(#) Well.cs
 */

namespace MikeSheWrapper.LayerStatistics
{
  public class Well
  {
    private string _wellID;

    private double _uTMX, _uTMY, _terrain;

    private ArrayList _filters = new ArrayList();


    //Constructor for GEUS-application
    public Well(string WellID, double UTMX, double UTMY, double Z, double Potential, DateTime ObsTime)
    {
      _uTMX = UTMX;
      _uTMY = UTMY;
      _wellID = WellID;
      _filters.Add(new Filter(Z, Potential, ObsTime));
    }

    #region Properties
    public double UTMX
    {
      get
      {
        return _uTMX;
      }
      set
      {
        _uTMX = value;
      }
    }
    public double UTMY
    {
      get
      {
        return _uTMY;
      }
      set
      {
        _uTMY = value;
      }
    }
    public double Terrain
    {
      get
      {
        return _terrain;
      }
      set
      {
        _terrain = value;
      }
    }
    public string WellID
    {
      get
      {
        return _wellID;
      }
      set
      {
        _wellID = value;
      }
    }

    public ArrayList Filters
    {
      get
      {
        return _filters;
      }
    }

    public Filter getFilter(int FilterNumber)
    {
      return (Filter)_filters[FilterNumber];
    }

    #endregion

  }
}
