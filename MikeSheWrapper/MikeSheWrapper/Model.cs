using System;
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
      get { return _gridInfo; }
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
      _gridInfo = new MikeSheGridInfo(_processed, Processed._PreProcessed_3DSZ);
      //_results = new Results(_files);
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
