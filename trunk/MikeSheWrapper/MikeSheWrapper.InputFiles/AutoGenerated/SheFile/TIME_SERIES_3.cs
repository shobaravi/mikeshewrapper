using System;
using System.Collections.Generic;
using DHI.Generic.MikeZero;

namespace MikeSheWrapper.InputFiles
{
  /// <summary>
  /// This is an autogenerated class. Do not edit. 
  /// If you want to add methods create a new partial class in another file
  /// </summary>
  public partial class TIME_SERIES_3: PFSMapper
  {

    private VEG_PROP_FILES3 _vEG_PROP_FILES;
    private List<DFS_2D_DATA_FILE> _tIME_SERIES_FILE_1s = new List<DFS_2D_DATA_FILE>();

    internal TIME_SERIES_3(PFSSection Section)
    {
      _pfsHandle = Section;

      for (int i = 1; i <= Section.GetSectionsNo(); i++)
      {
        PFSSection sub = Section.GetSection(i);
        switch (sub.Name)
        {
        case "VEG_PROP_FILES":
          _vEG_PROP_FILES = new VEG_PROP_FILES3(sub);
          break;
          default:
            if (sub.Name.Substring(0,6).Equals("TIME_S"))
            {
              _tIME_SERIES_FILE_1s.Add(new DFS_2D_DATA_FILE(sub));
              break;
            }
            _unMappedSections.Add(sub.Name);
          break;
        }
      }
    }

    public VEG_PROP_FILES3 VEG_PROP_FILES
    {
     get { return _vEG_PROP_FILES; }
    }

    public List<DFS_2D_DATA_FILE> TIME_SERIES_FILE_1s
   {
     get { return _tIME_SERIES_FILE_1s; }
    }

    public string NAME
    {
      get
      {
        return _pfsHandle.GetKeyword("NAME", 1).GetParameter(1).ToString();
      }
      set
      {
        _pfsHandle.GetKeyword("NAME", 1).GetParameter(1).Value = value;
      }
    }

    public string GRIDCODEID
    {
      get
      {
        return _pfsHandle.GetKeyword("GRIDCODEID", 1).GetParameter(1).ToString();
      }
      set
      {
        _pfsHandle.GetKeyword("GRIDCODEID", 1).GetParameter(1).Value = value;
      }
    }

    public int GRIDCODE
    {
      get
      {
        return _pfsHandle.GetKeyword("GRIDCODE", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("GRIDCODE", 1).GetParameter(1).Value = value;
      }
    }

    public int FIXED_VALUE_LAI
    {
      get
      {
        return _pfsHandle.GetKeyword("FIXED_VALUE_LAI", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("FIXED_VALUE_LAI", 1).GetParameter(1).Value = value;
      }
    }

    public int FIXED_VALUE_RD
    {
      get
      {
        return _pfsHandle.GetKeyword("FIXED_VALUE_RD", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("FIXED_VALUE_RD", 1).GetParameter(1).Value = value;
      }
    }

    public int TYPE
    {
      get
      {
        return _pfsHandle.GetKeyword("TYPE", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("TYPE", 1).GetParameter(1).Value = value;
      }
    }

  }
}
