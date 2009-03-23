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

    public JupiterWell(IWell Well):base(Well.ID,Well.X,Well.Y)
    {
      this.Description = Well.Description;
      this.Terrain = Well.Terrain;

      foreach (IIntake I in Well.Intakes)
      {
         JupiterIntake J = new JupiterIntake(this, I);
      }
    }

    #endregion


  }
}
