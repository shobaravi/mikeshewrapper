using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DHI.Generic.MikeZero;

namespace MikeSheWrapper.InputFiles
{
  public partial class SimulationPeriod
  {
    private DateTime? _start;
    private DateTime? _end;

    public DateTime GetDate(string Parameter)
    {
      PFSKeyword pk = _pfsHandle.GetKeyword(Parameter, 1);
      return new DateTime(pk.GetParameter(1).ToInt(), pk.GetParameter(2).ToInt(), pk.GetParameter(3).ToInt(), pk.GetParameter(4).ToInt(), pk.GetParameter(5).ToInt(), 0);
    }

    private void SetDate(string Paramter, DateTime date)
    {
      PFSKeyword pk = _pfsHandle.GetKeyword(Paramter, 1);
      pk.GetParameter(1).Value = date.Year;
      pk.GetParameter(2).Value = date.Month;
      pk.GetParameter(3).Value = date.Day;
      pk.GetParameter(4).Value = date.Hour;
      pk.GetParameter(5).Value = date.Minute;

    }


    public DateTime StartTime
    {
      get
      {
        if (!_start.HasValue)
        {
          _start = GetDate("SIMSTART");
        }
        return _start.Value;
      }
      set
      {
        if (_start.HasValue && _start.Value != value)
        { }
        else
        {
          _start = value;
          SetDate("SIMSTART", value);
        }

      }
    }

    public DateTime EndTime
    {
      get
      {
        if (!_end.HasValue)
        {
          _end = GetDate("SIMEND");
        }
        return _end.Value;
      }
      set
      {
        if (_end.HasValue && _end.Value != value)
        {}
        else
        {
          _end = value;
          SetDate("SIMEND", value);
        }
      }
    }

  }
}
