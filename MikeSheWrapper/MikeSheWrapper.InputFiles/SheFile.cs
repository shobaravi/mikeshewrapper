using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DHI.Generic.MikeZero;

namespace MikeSheWrapper.InputFiles
{
  public class SheFile
  {
    private MIKESHE_FLOWMODEL _mshe;
    private PFSClass she1;
    private string _fileName;
 
    
    public SheFile(string SheFileName)
    {
      _fileName = SheFileName;
      she1 = new PFSClass(Path.GetFullPath(SheFileName));
      _mshe = new MIKESHE_FLOWMODEL( she1.GetTarget(1) );      
    }

    public void SaveAs(string SheFileName)
    {
      she1.DumpToPfsFile(SheFileName);
    }

    public void Save()
    {
      SaveAs(_fileName);
    }

    public MIKESHE_FLOWMODEL MIKESHE_FLOWMODEL
    {
      get { return _mshe; }
    }

    public string FileName
    {
      get { return _fileName; }
    }

  }
}
