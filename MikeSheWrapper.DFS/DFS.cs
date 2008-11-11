﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.LinearAlgebra;
using DHI.Generic.MikeZero.DFS;

namespace MikeSheWrapper.DFS
{
  public abstract class DFS : DfsFileInfo
  {
    private int _currentTimeStep = -1;
    private int _currentItem = -1;
    protected float[] dfsdata; //Buffer used to fill data into
    private int _numberOfTimeSteps;
    protected int _numberOfLayers;
    protected int _numberOfColumns;
    protected int _numberOfRows;

    protected double _xOrigin;
    protected double _yOrigin;

    public DFS(string DFSFileName)
      : base()
    {
      base.Read(DFSFileName);

      //Read in dimensionality
      if (DynamicItemInfos[0].XCoords != null)
      {
        _numberOfColumns = DynamicItemInfos[0].XCoords.Length;
        _xOrigin = DynamicItemInfos[0].XCoords[0] - DynamicItemInfos[0].DX / 2;
      }
      else
      {
        _numberOfColumns = 1; //Dfs0-file
        _xOrigin = double.NaN;
      }

      if (DynamicItemInfos[0].YCoords != null)
      {
        _numberOfRows = DynamicItemInfos[0].YCoords.Length;
        _yOrigin = DynamicItemInfos[0].YCoords[0] - DynamicItemInfos[0].DY / 2;
      }
      else
      {
        _numberOfRows = 1; //Dfs1-file   
        _yOrigin = double.NaN;
      }

      if (DynamicItemInfos[0].ZCoords != null)
        _numberOfLayers = DynamicItemInfos[0].ZCoords.Length;
      else
        _numberOfLayers = 1; //Dfs2-file

      _numberOfTimeSteps = TimeSteps.Length;

      //Prepares an array of floats to recieve the data
      dfsdata = new float[base.DynamicItemInfos[0].GetTotalNumberOfPoints()];
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
      double ColumnD = Math.Max(-1, Math.Floor((UTMX - _xOrigin) / DynamicItemInfos[0].DX));

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
      double RowD = Math.Max(-1, Math.Floor((UTMY - _yOrigin) / DynamicItemInfos[0].DY));

      if (RowD > _numberOfRows)
        return -2;
      return (int)RowD;
    }

    /// <summary>
    /// Reads data for the TimeStep and Item if necessary and fills them into the buffer
    /// </summary>
    /// <param name="TimeStep"></param>
    /// <param name="Item"></param>
    protected void readNextItemTimeStep(int TimeStep, int Item)
    {
      if (TimeStep != _currentTimeStep || Item != _currentItem)
      {
        _currentTimeStep = TimeStep;
        _currentItem = Item;
        //Spools to the correct Item and TimeStep
        int ok = DFSWrapper.dfsFindItemDynamic(base.HeaderPtr, base.FilePtr, TimeStep, Item);
        if (ok != 0)
          throw new Exception("Could not find TimeStep number: " + TimeStep +" and Item number: " + Item);

        double time = 0;

        //Reads the data
        ok = DFSWrapper.dfsReadItemTimeStep(base.HeaderPtr, base.FilePtr, ref time, dfsdata);
        if (ok != 0)
          throw new Exception("Error in file: " + this.FileName + " reading timestep number: " + this._currentTimeStep);
      }
    }

    /// <summary>
    /// Gets the DeleteValue from the DFS-file
    /// </summary>
    public double DeleteValue
    {
      get
      {
        return DFSWrapper.dfsGetDeleteValFloat(HeaderPtr);
      }
    }

    /// <summary>
    /// Gets the number of timesteps
    /// </summary>
    public int NumberOfTimeSteps
    {
      get
      {
        return _numberOfTimeSteps;
      }
    }






  }
}