using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MikeSheWrapper.Tools
{
  public class Intake:IComparable<Intake>,IIntake, IEquatable<IIntake>, IEqualityComparer<IIntake>
  {
    private List<double> _screenTop = new List<double>();
    private List<double> _screenBottom = new List<double>();
    private List<double> _screenTopAsKote = new List<double>();
    private List<double> _screenBottomAsKote = new List<double>();
    private List<ObservationEntry> _observations = new List<ObservationEntry>();
    private List<Screen> _screens = new List<Screen>();

    public IWell well { get; protected set; }
    public int IDNumber { get; set; }
    public int? Layer { get; set; }


    public List<Screen> Screens
    {
      get { return _screens; }
    }



    protected Intake()
    { }

    /// <summary>
    /// Constructs a new intake in the well and adds it to the list of Intakes.
    /// </summary>
    /// <param name="Well"></param>
    /// <param name="IDNumber"></param>
    internal Intake(IWell Well, int IDNumber)
    {
      this.well = Well;
      this.IDNumber = IDNumber;
    }


    #region Statistics

    /// <summary>
    /// Returns the Root mean square error for the observations
    /// </summary>
    public double? RMS
    {
      get
      {
        if (_observations.Count == 0)
          return null;

        return Math.Pow(_observations.Average(new Func<ObservationEntry, double>(num => num.RMSE)), 0.5);
      }
    }

    public double? ME
    {
      get
      {
        if (_observations.Count == 0)
          return null;
        return _observations.Average(new Func<ObservationEntry, double>(num => num.ME));
      }
    }

    public double? MAE
    {
      get
      {
        if (_observations.Count == 0)
          return null;
        return _observations.Average(new Func<ObservationEntry, double>(num => Math.Abs(num.ME)));
      }
    }


    public double? RMST
    {
      get
      {
        if (_observations.Count == 0)
          return null;
        double simmean = _observations.Average(new Func<ObservationEntry, double>(num => num.SimulatedValue));
        double obsmean = _observations.Average(new Func<ObservationEntry, double>(num => num.Value));

        double val = _observations.Sum(new Func<ObservationEntry, double>(num => Math.Pow(num.Value - obsmean - (num.SimulatedValue - simmean), 2)));
        return Math.Pow(val / _observations.Count, 0.5);
      }
    }


    #endregion

    /// <summary>
    /// Gets the observations. Also used to add data
    /// </summary>
    public List<ObservationEntry> Observations
    {
      get { return _observations; }
    }

    /// <summary>
    /// Returns the well ID without spaces and the intake nummer added to the end with an underscore
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
      return well.ID.Replace(" ","") + "_" +IDNumber;
    }

    #region IComparable<Intake> Members

    /// <summary>
    /// Compares using the ID-number 
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public int CompareTo(Intake other)
    {
      return IDNumber.CompareTo(other.IDNumber);
    }

    #endregion

    #region IEquatable<IIntake> Members

    public bool Equals(IIntake other)
    {
      return IDNumber.Equals(other.IDNumber) & well.Equals(other.well);
    }

    #endregion

    #region IEqualityComparer<IIntake> Members

    public bool Equals(IIntake x, IIntake y)
    {
      return x.Equals(y);
    }

    public int GetHashCode(IIntake obj)
    {
      return obj.well.GetHashCode() + obj.IDNumber.GetHashCode();
    }

    #endregion
  }
}
