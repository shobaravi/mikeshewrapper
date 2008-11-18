using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MikeSheWrapper.InputFiles;

namespace MikeSheWrapper
{
  public class Model
  {
    private ProcessedData _processed;
    private Results _results;
    private FileNames _files;
    private SheFile _input;
    private MikeSheGridInfo _gridInfo;

    public MikeSheGridInfo GridInfo
    {
      get { return _processed.Grid; }
    }

    public SheFile Input
    {
      get { return _input; }
    }

    public Model(string SheFileName)
    {
      _input = new SheFile(SheFileName);
      _files = new FileNames(_input);
      _processed = new ProcessedData(_files);
      if(File.Exists(_files.Get3DSZFileName))
        _results = new Results(_files, GridInfo);
    }

    public Results Results
    {
      get { return _results; }
    }

    public ProcessedData Processed
    {
      get
      {
        return _processed;
      }
    }

  }
}
