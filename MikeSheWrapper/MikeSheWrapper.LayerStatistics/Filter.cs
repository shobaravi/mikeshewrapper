using System;
/**
 * @(#) Filter.cs
 */

namespace MikeSheWrapper.LayerStatistics
{
  public class Filter
  {
    /**
     * Kote
     */
    private double _top, _bottom, _potential, _pointInFilter;
    private DateTime _obsTime;


    public Filter(double Z, double Potential, DateTime ObsTime)
    {
      _pointInFilter = Z;
      _potential = Potential;
      _obsTime = ObsTime;
    }

    #region Properties
    public double Top
    {
      get
      {
        return _top;
      }
      set
      {
        _top = value;
      }
    }
    public double Bottom
    {
      get
      {
        return _bottom;
      }
      set
      {
        _bottom = value;
      }
    }
    public double PointInFilter
    {
      get
      {
        return _pointInFilter;
      }
      set
      {
        //Indlæg top og check
        _pointInFilter = value;
      }
    }
    public double Potential
    {
      get
      {
        return _potential;
      }
      set
      {
        _potential = value;
      }
    }

    public DateTime ObsTime
    {
      get
      {
        return _obsTime;
      }
      set
      {
        _obsTime = value;
      }
    }



    #endregion


  }
}