using System;
using System.Data;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MikeSheWrapper.Tools
{
  /// <summary>
  /// The observation well is a small class that holds the timeseries measurements for an observation. 
  /// An observation well can only have a single timeseries.  
  /// </summary>
  public class ObservationWell:Well 
  {

    public int _column;
    public int _row;
    private int _layer =-3;

    /// <summary>
    /// Gets and sets the depth of the observation in meters below surface
    /// </summary>
    public double Depth {get; set;}
    
    private double _z;
   

    #region Constructors
    public ObservationWell(string ID)
      : base(ID)
    {
    }

    public ObservationWell(string ID, double UTMX, double UTMY):base(ID, UTMX, UTMY)
    {
    }

    #endregion




    public override string ToString()
    {
      return base.ToString();
    }


    #region Properties


    public int Column
    {
      get { return _column; }
      set { _column = value; }
    }

    public int Row
    {
      get { return _row; }
      set { _row = value; }
    }

    public int Layer
    {
      get { return _layer; }
      set { _layer = value; }
    }

    /// <summary>
    /// Gets and sets the z-coordinate for the observation in meters above mean sea level
    /// </summary>
    public double Z
    {
      get { return _z; }
      set { _z = value; }
    }



    #endregion
  }
}
