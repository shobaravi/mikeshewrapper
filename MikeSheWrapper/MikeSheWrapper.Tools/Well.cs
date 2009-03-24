using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MikeSheWrapper.Tools
{
  /// <summary>
  /// A small class holding typical data to describe a well
  /// </summary>
  public class Well : IWell
  {
    protected string _id;
    protected string _description;
    protected double _x;
    protected double _y;
    protected double _terrain;

    public IEnumerable<IIntake> Intakes
    {
      get
      {
        return _intakes;
        
      }
    }
    protected List<IIntake> _intakes = new List<IIntake>();

    #region Constructors


    public Well(string ID)
    {
      _id = ID;
    }

    public Well(string ID, double X, double Y):this(ID)
    {
      _x = X;
      _y = Y;
    }
    #endregion

    /// <summary>
    /// Adds a new intake to the well
    /// </summary>
    /// <param name="IDNumber"></param>
    /// <returns></returns>
    public virtual IIntake AddNewIntake(int IDNumber)
    {
      Intake I = new Intake(this, IDNumber);
      _intakes.Add(I);
      return I;
    }

    public override string ToString()
    {
      return _id;
    }

    #region Properties

    /// <summary>
    /// Gets and sets the x-coodinate
    /// </summary>
    public double X
    {
      get { return _x; }
      set { _x = value; }
    }

    /// <summary>
    /// Gets and sets the y-coodinate
    /// </summary>
    public double Y
    {
      get { return _y; }
      set { _y = value; }
    }

    /// <summary>
    /// Gets and sets the ID of the well
    /// </summary>
    public string ID
    {
      get { return _id; }
      set { _id = value; }
    }

    /// <summary>
    /// Gets and sets a description
    /// </summary>
    public string Description
    {
      get { return _description; }
      set { _description = value; }
    }

    /// <summary>
    /// Gets and sets the terrain in meters above mean sea level
    /// </summary>
    public double Terrain
    {
      get { return _terrain; }
      set { _terrain = value; }
    }

    #endregion
  }
}
