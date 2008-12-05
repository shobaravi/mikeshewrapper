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

    private string _shefilename;


    public Model(string SheFileName)
    {
      _shefilename = SheFileName;
    }



    public FileNames Files
    {
      get { 
        if (_files == null)
          _files = new FileNames(Input);

        return _files; }
    }

    public MikeSheGridInfo GridInfo
    {
      get { return Processed.Grid; }
    }

    public SheFile Input
    {
      get {
        if (_input == null)
          _input = new SheFile(_shefilename);
        return _input;
      }
    }

    public Results Results
    {
      get { 
        if (_results == null)
          if (File.Exists(Files.Get3DSZFileName))
            _results = new Results(Files, GridInfo);

        return _results; }
    }

    public ProcessedData Processed
    {
      get
      {
        _processed = new ProcessedData(Files);

        return _processed;
      }
    }

  }
}
