using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DHI.Generic.MikeZero;

namespace MikeSheWrapper.InputFiles
{
  public class PFSMapper
  {
    internal PFSSection _pfsHandle;
    protected List<string> _unMappedSections = new List<string>();

    protected List<string> UnMappedSections
    {
      get { return _unMappedSections; }
    }

    /// <summary>
    /// Returns a deep clone of the section. Does not clone any parents;
    /// </summary>
    /// <param name="SectionToClone"></param>
    /// <returns></returns>
    public static PFSSection DeepClone(PFSSection SectionToClone)
    {
      PFSSection ps = new PFSSection(SectionToClone.Name);

      int NumberOfKeywords = SectionToClone.GetKeywordsNo();
      for (int i = 1; i <= NumberOfKeywords; i++)
        ps.AddKeyword(PFSMapper.DeepClone(SectionToClone.GetKeyword(i)));

      int NumberOfSections = SectionToClone.GetSectionsNo();
      for (int i = 1; i <= NumberOfSections; i++)
        ps.AddSection(PFSMapper.DeepClone(SectionToClone.GetSection(i)));

      return ps;
    }

    /// <summary>
    /// Returns a deep clone of this Keyword. Does not clone parents.
    /// </summary>
    /// <param name="KeywordToClone"></param>
    /// <returns></returns>
    public static PFSKeyword DeepClone(PFSKeyword KeywordToClone)
    {
      PFSKeyword pk = new PFSKeyword(KeywordToClone.Name);
      int NumberOfParameters = KeywordToClone.GetParametersNo();

      for (int i = 1; i <= NumberOfParameters; i++)
      {
        pk.AddParameter(new PFSParameter(KeywordToClone.GetParameter(i).Type, KeywordToClone.GetParameter(i).Value));
      }
      return pk;
    }

  }
}
