using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MikeSheWrapper.InputFiles;

namespace MikeSheWrapper
{
  /// <summary>
  /// This class provides access to setup data, processed data and results.
  /// Access to processed data and results requires that the model is preprocessed and run, respectively. 
  /// </summary>
  public class Model:IDisposable
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


    /// <summary>
    /// Gets the file names
    /// </summary>
    public FileNames Files
    {
      get { 
        if (_files == null)
          _files = new FileNames(Input);

        return _files; }
    }

    /// <summary>
    /// Gets the grid info object
    /// Returns null if the model has not been preprocessed.
    /// </summary>
    public MikeSheGridInfo GridInfo
    {
      get 
      {
        if (Processed !=null)
          return Processed.Grid;
        return null;
      }
    }

    /// <summary>
    /// Gets read and write access to the input in the .she-file.
    /// Remember to save changes.
    /// </summary>
    public SheFile Input
    {
      get {
        if (_input == null)
          _input = new SheFile(_shefilename);
        return _input;
      }
    }

    /// <summary>
    /// Gets read access to the results
    /// Returns null if there are no results
    /// </summary>
    public Results Results
    {
      get { 
        if (_results == null)
          if (File.Exists(Files.Get3DSZFileName))
            _results = new Results(Files, GridInfo);

        return _results; }
    }

    /// <summary>
    /// Gets read access to the processed data
    /// Returns null if the model has not been preprocessed.
    /// </summary>
    public ProcessedData Processed
    {
      get
      {
        if (_processed == null)
          if (File.Exists(Files.PreProcessed2D)) 
            _processed = new ProcessedData(Files);

        return _processed;
      }
    }


    #region IDisposable Members

    public void Dispose()
    {
      if (_processed != null)
        _processed.Dispose();
      if (_results != null)
        _results.Dispose();
    }

    #endregion
  }
}
