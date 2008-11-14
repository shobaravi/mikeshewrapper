using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MikeSheWrapper.Interfaces;
using MikeSheWrapper.Tools;
using MikeSheWrapper.DFS;


namespace MikeSheWrapper
{
  public class ProcessedData
  {
    private DataSetsFromDFS3 _initialHeads;
    private DataSetsFromDFS3 _boundaryConditionsForTheSaturatedZone;
    private DataSetsFromDFS3 _horizontalConductivity;
    private DataSetsFromDFS3 _verticalConductivity;
    private DataSetsFromDFS3 _transmissivity;
    private DataSetsFromDFS3 _specificYield;
    private DataSetsFromDFS3 _specificStorage;


    private DataSetsFromDFS2 _netRainFallFraction;
    private DataSetsFromDFS2 _infiltrationFraction;
    private MikeSheGridInfo _grid;

    public MikeSheGridInfo Grid
    {
      get { return _grid; }
    }


    #region Constructors

    public ProcessedData(string SheFileName)
    {
      FileNames files = new FileNames(SheFileName);
      Initialize(files.PreProcessedSZ3D, files.PreProcessed2D);
    }

    public ProcessedData(string PreProcessed3dSzFile, string PreProcessed2dSzFile)
    {
      Initialize(PreProcessed3dSzFile, PreProcessed2dSzFile);
    }

    internal ProcessedData(FileNames files)
    {
      Initialize(files.PreProcessedSZ3D, files.PreProcessed2D);
    }

    #endregion



    private void Initialize(string PreProcessed3dSzFile, string PreProcessed2dSzFile)
    {
      //Open File with 3D data
      DFS3 _PreProcessed_3DSZ = new DFS3(PreProcessed3dSzFile);

      //Generate 3D properties
      for (int i = 0; i < _PreProcessed_3DSZ.DynamicItemInfos.Length; i++)
      {
        switch (_PreProcessed_3DSZ.DynamicItemInfos[i].Name)
        {
          case "Boundary conditions for the saturated zone":
            _boundaryConditionsForTheSaturatedZone = new DataSetsFromDFS3(_PreProcessed_3DSZ, i+1);
            break;
          case "Horizontal conductivity in the saturated zone":
            _horizontalConductivity = new DataSetsFromDFS3(_PreProcessed_3DSZ, i+1);
            break;
          case "Vertical conductivity in the saturated zone":
            _verticalConductivity = new DataSetsFromDFS3(_PreProcessed_3DSZ, i+1);
            break;
          case "Transmissivity in the saturated zone":
            _transmissivity = new DataSetsFromDFS3(_PreProcessed_3DSZ, i+1);
            break;
          case "Specific yield in the saturated zone":
            _specificYield = new DataSetsFromDFS3(_PreProcessed_3DSZ, i+1);
            break;
          case "Specific storage in the saturated zone":
            _specificStorage = new DataSetsFromDFS3(_PreProcessed_3DSZ, i+1);
            break;
          case "Initial potential heads in the saturated zone":
            _initialHeads = new DataSetsFromDFS3(_PreProcessed_3DSZ, i+1);
            break;
          default: //Unknown item
            break;
        }
      }

      //Open File with 2D data
      DFS2 _prePro2D = new DFS2(PreProcessed2dSzFile);

      //Generate 2D properties by looping the items
      for (int i = 0; i < _prePro2D.DynamicItemInfos.Length; i++)
      {
        switch (_prePro2D.DynamicItemInfos[i].Name)
        {
          case "Net Rainfall Fraction":
            _netRainFallFraction = new DataSetsFromDFS2(_prePro2D, i +1);
            break;
          case "Infiltration Fraction":
            _infiltrationFraction = new DataSetsFromDFS2(_prePro2D, i +1);
            break;
          default: //Unknown item
            break;
        }
      }

      //Now construct the grid from the open files
      _grid = new MikeSheGridInfo(_PreProcessed_3DSZ, _prePro2D);
    }


    public IXYDataSet NetRainFallFraction
    {
      get { return _netRainFallFraction; }
    }

    public IXYDataSet InfiltrationFraction
    {
      get { return _infiltrationFraction; }
    }

    public IXYZDataSet InitialHeads
    {
      get { return _initialHeads; }
    }

    public IXYZDataSet SpecificStorage
    {
      get { return _specificStorage; }
    }

    public IXYZDataSet SpecificYield
    {
      get { return _specificYield; }
    }

    public IXYZDataSet Transmissivity
    {
      get { return _transmissivity; }
    }


    public IXYZDataSet VerticalConductivity
    {
      get { return _verticalConductivity; }
    }

    public IXYZDataSet HorizontalConductivity
    {
      get { return _horizontalConductivity; }
    }


    public IXYZDataSet BoundaryConditionsForTheSaturatedZone
    {
      get { return _boundaryConditionsForTheSaturatedZone; }
    }


  }
}
