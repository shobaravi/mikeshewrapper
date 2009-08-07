using System;
using System.Collections.Generic;
using DHI.Generic.MikeZero;

namespace MikeSheWrapper.InputFiles
{
  /// <summary>
  /// This is an autogenerated class. Do not edit. 
  /// If you want to add methods create a new partial class in another file
  /// </summary>
  public partial class StoringOfResults: PFSMapper
  {

    private DetailedTimeseriesOutput _detailedTimeseriesOutput;
    private DetailedM11TimeseriesOutput _detailedM11TimeseriesOutput;
    private GridSeriesOutput _gridSeriesOutput;

    internal StoringOfResults(PFSSection Section)
    {
      _pfsHandle = Section;

      for (int i = 1; i <= Section.GetSectionsNo(); i++)
      {
        PFSSection sub = Section.GetSection(i);
        switch (sub.Name)
        {
        case "DetailedTimeseriesOutput":
          _detailedTimeseriesOutput = new DetailedTimeseriesOutput(sub);
          break;
        case "DetailedM11TimeseriesOutput":
          _detailedM11TimeseriesOutput = new DetailedM11TimeseriesOutput(sub);
          break;
        case "GridSeriesOutput":
          _gridSeriesOutput = new GridSeriesOutput(sub);
          break;
          default:
            _unMappedSections.Add(sub.Name);
          break;
        }
      }
    }

    public DetailedTimeseriesOutput DetailedTimeseriesOutput
    {
     get { return _detailedTimeseriesOutput; }
    }

    public DetailedM11TimeseriesOutput DetailedM11TimeseriesOutput
    {
     get { return _detailedM11TimeseriesOutput; }
    }

    public GridSeriesOutput GridSeriesOutput
    {
     get { return _gridSeriesOutput; }
    }

    public int Touched
    {
      get
      {
        return _pfsHandle.GetKeyword("Touched", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("Touched", 1).GetParameter(1).Value = value;
      }
    }

    public int IsDataUsedInSetup
    {
      get
      {
        return _pfsHandle.GetKeyword("IsDataUsedInSetup", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("IsDataUsedInSetup", 1).GetParameter(1).Value = value;
      }
    }

    public int ADInputData
    {
      get
      {
        return _pfsHandle.GetKeyword("ADInputData", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("ADInputData", 1).GetParameter(1).Value = value;
      }
    }

    public int WaterBalance
    {
      get
      {
        return _pfsHandle.GetKeyword("WaterBalance", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("WaterBalance", 1).GetParameter(1).Value = value;
      }
    }

    public int AdRadio
    {
      get
      {
        return _pfsHandle.GetKeyword("AdRadio", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("AdRadio", 1).GetParameter(1).Value = value;
      }
    }

    public int HotStartData
    {
      get
      {
        return _pfsHandle.GetKeyword("HotStartData", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("HotStartData", 1).GetParameter(1).Value = value;
      }
    }

    public int OnlyStoreHotDataAtEndOfSimulation
    {
      get
      {
        return _pfsHandle.GetKeyword("OnlyStoreHotDataAtEndOfSimulation", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("OnlyStoreHotDataAtEndOfSimulation", 1).GetParameter(1).Value = value;
      }
    }

    public int HotstartStoringTimestep_Hrs
    {
      get
      {
        return _pfsHandle.GetKeyword("HotstartStoringTimestep_Hrs", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("HotstartStoringTimestep_Hrs", 1).GetParameter(1).Value = value;
      }
    }

    public int WM_OverlandFrequency
    {
      get
      {
        return _pfsHandle.GetKeyword("WM_OverlandFrequency", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("WM_OverlandFrequency", 1).GetParameter(1).Value = value;
      }
    }

    public int WM_PrecFrequency
    {
      get
      {
        return _pfsHandle.GetKeyword("WM_PrecFrequency", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("WM_PrecFrequency", 1).GetParameter(1).Value = value;
      }
    }

    public int WM_FHeadsFrequency
    {
      get
      {
        return _pfsHandle.GetKeyword("WM_FHeadsFrequency", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("WM_FHeadsFrequency", 1).GetParameter(1).Value = value;
      }
    }

    public int WM_FluxesFrequency
    {
      get
      {
        return _pfsHandle.GetKeyword("WM_FluxesFrequency", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("WM_FluxesFrequency", 1).GetParameter(1).Value = value;
      }
    }

    public int WQ_OverlandFrequency
    {
      get
      {
        return _pfsHandle.GetKeyword("WQ_OverlandFrequency", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("WQ_OverlandFrequency", 1).GetParameter(1).Value = value;
      }
    }

    public int WQ_UnsaturatedZoneFrequency
    {
      get
      {
        return _pfsHandle.GetKeyword("WQ_UnsaturatedZoneFrequency", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("WQ_UnsaturatedZoneFrequency", 1).GetParameter(1).Value = value;
      }
    }

    public int WQ_SaturatedZoneFrequency
    {
      get
      {
        return _pfsHandle.GetKeyword("WQ_SaturatedZoneFrequency", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("WQ_SaturatedZoneFrequency", 1).GetParameter(1).Value = value;
      }
    }

    public int WQ_TimeSeriesFrequency
    {
      get
      {
        return _pfsHandle.GetKeyword("WQ_TimeSeriesFrequency", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("WQ_TimeSeriesFrequency", 1).GetParameter(1).Value = value;
      }
    }

    public int WQ_SummaryFrequency
    {
      get
      {
        return _pfsHandle.GetKeyword("WQ_SummaryFrequency", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("WQ_SummaryFrequency", 1).GetParameter(1).Value = value;
      }
    }

  }
}
