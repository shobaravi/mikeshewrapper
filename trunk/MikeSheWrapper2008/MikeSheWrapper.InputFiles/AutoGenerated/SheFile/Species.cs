using System;
using System.Collections.Generic;
using DHI.Generic.MikeZero;

namespace MikeSheWrapper.InputFiles
{
  /// <summary>
  /// This is an autogenerated class. Do not edit. 
  /// If you want to add methods create a new partial class in another file
  /// </summary>
  public partial class Species: PFSMapper
  {


    internal Species(PFSSection Section)
    {
      _pfsHandle = Section;

      for (int i = 1; i <= Section.GetSectionsNo(); i++)
      {
        PFSSection sub = Section.GetSection(i);
        switch (sub.Name)
        {
          default:
            _unMappedSections.Add(sub.Name);
          break;
        }
      }
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

    public int MzSEPfsListItemCount
    {
      get
      {
        return _pfsHandle.GetKeyword("MzSEPfsListItemCount", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("MzSEPfsListItemCount", 1).GetParameter(1).Value = value;
      }
    }

    public int NumberOfSpecies
    {
      get
      {
        return _pfsHandle.GetKeyword("NumberOfSpecies", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("NumberOfSpecies", 1).GetParameter(1).Value = value;
      }
    }

  }
}
