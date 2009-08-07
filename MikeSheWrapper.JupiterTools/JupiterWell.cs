using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MikeSheWrapper.Tools;

namespace MikeSheWrapper.JupiterTools
{
  public class JupiterWell:Well 
  {
    private List<Lithology> _lithSamples = new List<Lithology>();

    public override IIntake AddNewIntake(int IDNumber)
    {
      JupiterIntake JI = new JupiterIntake(this, IDNumber);

      _intakes.Add(JI);
      return JI;
    }

    private IIntake AddNewIntake(IIntake intake)
    {
      JupiterIntake Ji = new JupiterIntake(this, intake);
      _intakes.Add(Ji);
      return Ji;
    }

    public List<Lithology> LithSamples
    {
      get { return _lithSamples; }
      set { _lithSamples = value; }
    }
    private List<ChemistrySample> _chemSamples = new List<ChemistrySample>();

    public List<ChemistrySample> ChemSamples
    {
      get { return _chemSamples; }
      set { _chemSamples = value; }
    }

        #region Constructors
    public JupiterWell(string ID)
      : base(ID)
    {
    }

    public JupiterWell(string ID, double UTMX, double UTMY)
      : base(ID, UTMX, UTMY)
    {
    }

    /// <summary>
    /// Constructs a new JupiterWell based on the other Well. Also construct new JupiterIntakes;
    /// </summary>
    /// <param name="Well"></param>
    public JupiterWell(IWell Well):base(Well.ID,Well.X,Well.Y)
    {
      this.Description = Well.Description;
      this.Terrain = Well.Terrain;

      foreach (IIntake I in Well.Intakes)
      {
         this.AddNewIntake(I);
      }
    }

    #endregion


  }
}
