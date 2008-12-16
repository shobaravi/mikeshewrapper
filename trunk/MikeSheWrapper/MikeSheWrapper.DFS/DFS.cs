﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.LinearAlgebra;
using DHI.Generic.MikeZero.DFS;

namespace MikeSheWrapper.DFS
{
  public abstract class DFS 
  {
    private int _currentTimeStep = -1;
    private int _currentItem = -1;

    protected float[] dfsdata; //Buffer used to fill data into
    protected int _numberOfLayers;
    protected int _numberOfColumns;
    protected int _numberOfRows;
    private DfsFileInfo _fileInfo;

    private IntPtr _fileWriter = IntPtr.Zero;
    private IntPtr _headerWriter = IntPtr.Zero;
    private string _filename;


    public DFS(string DFSFileName)
    {
      _filename = DFSFileName;
      _fileInfo = new DfsFileInfo();
      _fileInfo.Read(_filename);   
      
      //Read in dimensionality
      if (DynamicItemInfos[0].XCoords != null)
      {
        _numberOfColumns = DynamicItemInfos[0].XCoords.Length;
        XOrigin = DynamicItemInfos[0].XCoords[0] - DynamicItemInfos[0].DX / 2;
      }
      else
      {
        _numberOfColumns = 1; //Dfs0-file
        XOrigin = double.NaN;
      }

      if (DynamicItemInfos[0].YCoords != null)
      {
        _numberOfRows = DynamicItemInfos[0].YCoords.Length;
        YOrigin = DynamicItemInfos[0].YCoords[0] - DynamicItemInfos[0].DY / 2;

      }
      else
      {
        _numberOfRows = 1; //Dfs1-file   
        YOrigin = double.NaN;
      }

      // DFS3-file
      if (DynamicItemInfos[0].ZCoords != null)
      {
        _numberOfLayers = DynamicItemInfos[0].ZCoords.Length;
        //in DFS3 files it it is necessary to add the longitude and latitude.
        XOrigin += _fileInfo.Longitude;
        YOrigin += _fileInfo.Latitude;
      }
      else
      {
        _numberOfLayers = 1; //Dfs2-file
      }

      NumberOfTimeSteps = _fileInfo.TimeSteps.Length;

      //Prepares an array of floats to recieve the data
      dfsdata = new float[DynamicItemInfos[0].GetTotalNumberOfPoints()];
    }

   /// <summary>
   /// Override of the Dispose method in DFSFileInfo which probably does not account for finalization
   /// </summary>
   public void Dispose() 
   {
     Dispose(true);
      GC.SuppressFinalize(this); 
   }

   protected virtual void Dispose(bool disposing) 
   {
      if (disposing) 
      {
        dfsdata = null;
      }
      _fileInfo.Dispose();
     if (_fileWriter!= IntPtr.Zero)
       DFSWrapper.dfsFileClose(_headerWriter, ref _fileWriter);


   }

    /// <summary>
    /// Destructor called when the object is garbage collected.
    /// </summary>
   ~DFS()
   {
     // Simply call Dispose(false).
     Dispose(false);
   }

   private void InitializeForWriting()
   {
     int ok = DFSWrapper.dfsFileEdit(_filename, ref _headerWriter, ref _fileWriter);
     if (ok != 0)
       throw new Exception("Error in initializing file : " + _filename + " for writing");

   }




    /// <summary>
    /// Gets the Column index for this coordinate. Lower left is (0,0). 
    /// Returns -1 if UTMY is left of the grid and -2 if it is right.
    /// </summary>
    /// <param name="UTMY"></param>
    /// <returns></returns>
    public int GetColumnIndex(double UTMX)
    {
      //Calculate as a double to prevent overflow errors when casting 
      double ColumnD = Math.Max(-1, Math.Floor((UTMX - XOrigin) / DynamicItemInfos[0].DX));

      if (ColumnD > _numberOfColumns)
        return -2;
      return (int) ColumnD;
    }

    /// <summary>
    /// Gets the Row index for this coordinate. Lower left is (0,0). 
    /// Returns -1 if UTMY is below the grid and -2 if it is above.
    /// </summary>
    /// <param name="UTMY"></param>
    /// <returns></returns>
    public int GetRowIndex(double UTMY)
    {
      //Calculate as a double to prevent overflow errors when casting 
      double RowD = Math.Max(-1, Math.Floor((UTMY - YOrigin) / DynamicItemInfos[0].DY));

      if (RowD > _numberOfRows)
        return -2;
      return (int)RowD;
    }

    /// <summary>
    /// Returns the TimeStep closest to the TimeStamp. If the timestamp falls exactly between two timestep the smallest is returned
    /// </summary>
    /// <param name="TimeStamp"></param>
    /// <returns></returns>
    public int GetTimeStep(DateTime TimeStamp)
    {
      int TimeStep = 0;
      if (_fileInfo.TimeSteps.Length == 0)
        throw new Exception("No timesteps read from: " + _fileInfo.FileName);

      //If the TimeStamp is later than the simulated period the last timestep is returned
      if (TimeStamp >= _fileInfo.TimeSteps[_fileInfo.TimeSteps.Length - 1])
        TimeStep = _fileInfo.TimeSteps.Length - 1;
      else
      {
        //Note in case of equidistant times this could be made more efficient
        //Steps until a bigger timestep is found
        while (TimeStamp > _fileInfo.TimeSteps[TimeStep])
          TimeStep++;
      }

      if (TimeStep > 0)
      {
        //Checks if the timestep just before is actually closer.
        if (_fileInfo.TimeSteps[TimeStep].Subtract(TimeStamp) >= TimeStamp.Subtract(_fileInfo.TimeSteps[TimeStep - 1]))
          TimeStep--;
      }

      return TimeStep;
    }

    /// <summary>
    /// Reads data for the TimeStep and Item if necessary and fills them into the buffer
    /// </summary>
    /// <param name="TimeStep"></param>
    /// <param name="Item"></param>
    protected void ReadItemTimeStep(int TimeStep, int Item)
    {
      if (TimeStep != _currentTimeStep || Item != _currentItem)
      {
        _currentTimeStep = TimeStep;
        _currentItem = Item;
        //Spools to the correct Item and TimeStep
        int ok = DFSWrapper.dfsFindItemDynamic(_fileInfo.HeaderPtr, _fileInfo.FilePtr, TimeStep, Item);
        if (ok != 0)
          throw new Exception("Could not find TimeStep number: " + TimeStep +" and Item number: " + Item);

        double time = 0;

        //Reads the data
        ok = DFSWrapper.dfsReadItemTimeStep(_fileInfo.HeaderPtr, _fileInfo.FilePtr, ref time, dfsdata);
        if (ok != 0)
          throw new Exception("Error in file: " + _fileInfo.FileName + " reading timestep number: " + this._currentTimeStep);
      }
    }

    /// <summary>
    /// Writes data for the TimeStep and Item
    /// </summary>
    /// <param name="TimeStep"></param>
    /// <param name="Item"></param>
    protected void WriteItemTimeStep(int TimeStep, int Item, float[] data)
    {
      if (_headerWriter == IntPtr.Zero)
        InitializeForWriting();

      //Spools to the correct Item and TimeStep
      int ok = DFSWrapper.dfsFindItemDynamic(_headerWriter, _fileWriter, TimeStep, Item);
      //      if (ok != 0)
      //          throw new Exception("Could not find TimeStep number: " + TimeStep + " and Item number: " + Item);

      double time = 50;

      //Writes the data
      ok = DFSWrapper.dfsWriteItemTimeStep(_headerWriter, _fileWriter, time, data);
      if (ok != 0)
        throw new Exception("Error writing timestep number: " + _currentTimeStep);

    }


    /// <summary>
    /// Gets the DeleteValue from the DFS-file
    /// </summary>
    public double DeleteValue
    {
      get
      {
        return DFSWrapper.dfsGetDeleteValFloat(_fileInfo.HeaderPtr);
      }
    }

    public DfsFileItemInfo[] DynamicItemInfos
    {
      get { return _fileInfo.DynamicItemInfos; }
    }

    /// <summary>
    /// Gets the number of timesteps
    /// </summary>
    public int NumberOfTimeSteps{get; protected set;}

    /// <summary>
    /// Gets the x-coordinate of the grid the center of the lower left
    /// Only valid for DFS2 and DFS3
    /// Remember that MikeShe does not use the center
    /// </summary>
    public double XOrigin { get; private set; }

    /// <summary>
    /// Gets the Y-coordinate of the grid the center of the lower left
    /// Only valid for DFS2 and DFS3
    /// Remember that MikeShe does not use the center
    /// </summary>
    public double YOrigin { get; private set; }

  }
}
