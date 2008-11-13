using System;
using System.Text;

/**
 * @(#) Observation.cs
 */

namespace MikeSheWrapper.LayerStatistics

{

	public class Observation
	{
	
    #region Private members

		private double _simulatedValueInterpolated, _simulatedValueCell;
	
		private int _dryCells, _boundaryCells;
	
		private int _row, _column, _layer, _timeStep;
	
		private double _rMSE, _mE;
		private Well WR;
		private MSHE MSHEObject;

    #endregion
	
    # region Constructors
		public Observation( Well WellReference, MSHE MSHEObject)
		{
			this.WR = WellReference;
			this.MSHEObject = MSHEObject;
			int[] temp = MSHEObject.ToGridCoordinates( WR.UTMX, WR.UTMY, WR.getFilter(0).PointInFilter );
			_column = temp[0];
			_row = temp[1];
			_layer = temp[2];
      this._timeStep = MSHEObject.getTimeStep( WR.getFilter(0).ObsTime, MSHE.gridkeys.Potential );
		}

		public Observation( Well WellReference, MSHE MSHEObject,int Layer ):this(WellReference, MSHEObject )
		{
			_layer=Layer;
		}

    #endregion

    #region Public methods
		
    /// <summary>
    /// Gets the simulated value for the cell and calculates the ME and RMSE
    /// </summary>
    public void calculate()
		{

			if(Math.Min(Math.Min(_row,_column),Layer) < 0) //Hvis boringen ikke er inden for modelområdet
			{
				_simulatedValueCell         = -9999;
				_simulatedValueInterpolated = -9999;
				_mE                        = -9999;
				_rMSE                      = -9999;
			}
			else
			{
				_simulatedValueCell = MSHEObject.GetScalar(_column,_row, Layer,_timeStep,MSHE.gridkeys.PhreaticPotential );
				_simulatedValueInterpolated = MSHEObject.GetScalar(WR.UTMX, WR.UTMY, Layer, _timeStep, MSHE.gridkeys.PhreaticPotential,out _dryCells,out _boundaryCells);
        if (_simulatedValueInterpolated != MSHEObject.getDeleteValue(MSHE.gridkeys.Potential))
        {
          _mE = WR.getFilter(0).Potential - _simulatedValueInterpolated;
          _rMSE = Math.Pow( _mE, 2.0 );
        }
			}
		}

    /// <summary>
    /// Creates the string to be used as output
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
      StringBuilder ObsString=new StringBuilder();
      ObsString.Append(WR.WellID  +"\t");
      ObsString.Append(WR.UTMX   +"\t");
      ObsString.Append(WR.UTMY   +"\t");
      ObsString.Append(WR.getFilter(0).PointInFilter +"\t");
      ObsString.Append(this.Layer  +"\t");
      ObsString.Append(WR.getFilter(0).Potential   +"\t");
      ObsString.Append(WR.getFilter(0).ObsTime.ToShortDateString() +"\t");
      ObsString.Append(this._simulatedValueInterpolated  +"\t");
      ObsString.Append(this._simulatedValueCell  +"\t");
      ObsString.Append(this.ME +"\t");
      ObsString.Append(this.RMSE +"\t");
      ObsString.Append(this._dryCells  +"\t");		
      ObsString.Append(this._boundaryCells  +"\t");
      ObsString.Append(this._column  +"\t");
      ObsString.Append(this._row  +"\t");

      return ObsString.ToString();
    }

    #endregion

    #region Properties
    public double ME
    {
      get
      {
        return _mE;
      }
    }

    public double RMSE
    {
      get
      {
        return _rMSE;
      }
    }
    public int Layer
    {
      get
      {
        return _layer;
      }
    }
    public double SimValueCell
    {
      get
      {
        return this._simulatedValueCell;
      }
    }

    #endregion

	
	}
}
