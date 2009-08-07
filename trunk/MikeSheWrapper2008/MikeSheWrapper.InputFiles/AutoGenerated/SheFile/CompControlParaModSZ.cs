using System;
using System.Collections.Generic;
using DHI.Generic.MikeZero;

namespace MikeSheWrapper.InputFiles
{
  /// <summary>
  /// This is an autogenerated class. Do not edit. 
  /// If you want to add methods create a new partial class in another file
  /// </summary>
  public partial class CompControlParaModSZ: PFSMapper
  {

    private CompControlParaModLMG _compControlParaModLMG;
    private CompControlParaModPCG2 _compControlParaModPCG2;
    private CompControlParaModPCG4 _compControlParaModPCG4;
    private CompControlParaModSIP _compControlParaModSIP;
    private CompControlParaModSOR _compControlParaModSOR;

    internal CompControlParaModSZ(PFSSection Section)
    {
      _pfsHandle = Section;

      for (int i = 1; i <= Section.GetSectionsNo(); i++)
      {
        PFSSection sub = Section.GetSection(i);
        switch (sub.Name)
        {
        case "CompControlParaModLMG":
          _compControlParaModLMG = new CompControlParaModLMG(sub);
          break;
        case "CompControlParaModPCG2":
          _compControlParaModPCG2 = new CompControlParaModPCG2(sub);
          break;
        case "CompControlParaModPCG4":
          _compControlParaModPCG4 = new CompControlParaModPCG4(sub);
          break;
        case "CompControlParaModSIP":
          _compControlParaModSIP = new CompControlParaModSIP(sub);
          break;
        case "CompControlParaModSOR":
          _compControlParaModSOR = new CompControlParaModSOR(sub);
          break;
          default:
            _unMappedSections.Add(sub.Name);
          break;
        }
      }
    }

    public CompControlParaModLMG CompControlParaModLMG
    {
     get { return _compControlParaModLMG; }
    }

    public CompControlParaModPCG2 CompControlParaModPCG2
    {
     get { return _compControlParaModPCG2; }
    }

    public CompControlParaModPCG4 CompControlParaModPCG4
    {
     get { return _compControlParaModPCG4; }
    }

    public CompControlParaModSIP CompControlParaModSIP
    {
     get { return _compControlParaModSIP; }
    }

    public CompControlParaModSOR CompControlParaModSOR
    {
     get { return _compControlParaModSOR; }
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

    public int SolverOption
    {
      get
      {
        return _pfsHandle.GetKeyword("SolverOption", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("SolverOption", 1).GetParameter(1).Value = value;
      }
    }

  }
}
