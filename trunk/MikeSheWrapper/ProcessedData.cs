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
    private DFS3 _PreProcessed_3DSZ;
    private DFS2 _prePro2D;
    private DataSetsFromDFS3 _initialHeads;
    private DataSetsFromDFS3 _boundaryConditionsForTheSaturatedZone;
    private DataSetsFromDFS3 _lowerLevelOfComputationalLayers;
    private DataSetsFromDFS3 _thicknessOfComputationalLayers;
    private DataSetsFromDFS3 _horizontalConductivity;
    private DataSetsFromDFS3 _verticalConductivity;
    private DataSetsFromDFS3 _transmissivity;
    private DataSetsFromDFS3 _specificYield;
    private DataSetsFromDFS3 _specificStorage;

    private DataSetsFromDFS2 _modelDomainAndGrid;
    private DataSetsFromDFS2 _surfaceTopography;
    private DataSetsFromDFS2 _netRainFallFraction;
    private DataSetsFromDFS2 _infiltrationFraction;

    private TopOfCell _upperLevelOfComputationalLayers;

    #region Constructors

    public ProcessedData(string SheFileName)
    {
      Initialize(new FileNames(SheFileName));
    }

    internal ProcessedData(FileNames files)
    {
      Initialize(files);
    }

    #endregion

    /// <summary>
    /// Returns the indeces for a set of coordinates.
    /// Necessary to sent the output as par
    /// Returns true if the grid point is within the active domain.
    /// Note that Column and Row may have positive values and still the point is outside of the active domain
    /// </summary>
    /// <param name="X"></param>
    /// <param name="Y"></param>
    /// <param name="Column"></param>
    /// <param name="Row"></param>
    public bool GetIndex(double X, double Y, out int Column, out int Row)
    {
      Column = ((DFS2)_modelDomainAndGrid).GetColumnIndex(X);
      Row = ((DFS2)_modelDomainAndGrid).GetColumnIndex(X);
      return 1 == _modelDomainAndGrid.Data[Row, Column];
    }

    /// <summary>
    /// Returns the layer number. Lower layer is 0. 
    /// If -1 is returned Z is above the surface and if -2 is returned Z is below the bottom.
    /// </summary>
    /// <param name="Column"></param>
    /// <param name="Row"></param>
    /// <param name="Z"></param>
    /// <returns></returns>
    public int GetLayer(int Column, int Row, double Z)
    {
      if (Z > _surfaceTopography.Data[Row, Column])
        return -1;
      else if (Z < _lowerLevelOfComputationalLayers.Data[Row, Column, 0])
        return -2;
      else
      {
        int i = 0;
        while (Z < _upperLevelOfComputationalLayers[Row, Column, i])
          i++;
        return i - 1;
      }
    }
    
    
    private void Initialize(FileNames files)
    {
      //Open File with 3D data
      _PreProcessed_3DSZ = new DFS3(files.PreProcessedSZ3D);

      //Generate 3D properties
      for (int i = 0; i < _PreProcessed_3DSZ.DynamicItemInfos.Length; i++)
      {
        switch (_PreProcessed_3DSZ.DynamicItemInfos[i].Name)
        {
          case "Boundary conditions for the saturated zone":
            _boundaryConditionsForTheSaturatedZone = new DataSetsFromDFS3(_PreProcessed_3DSZ, i+1);
            break;
          case "Lower level of computational layers in the saturated zone":
            _lowerLevelOfComputationalLayers = new DataSetsFromDFS3(_PreProcessed_3DSZ, i+1);
            break;
          case "Thickness of computational layers in the saturated zone":
            _thicknessOfComputationalLayers = new DataSetsFromDFS3(_PreProcessed_3DSZ, i+1);
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
      _prePro2D = new DFS2(files.PreProcessed2D);

      //Generate 2D properties by looping the items
      for (int i = 0; i < _prePro2D.DynamicItemInfos.Length; i++)
      {
        switch (_prePro2D.DynamicItemInfos[i].Name)
        {
          case "Model domain and grid":
            _modelDomainAndGrid = new DataSetsFromDFS2(_prePro2D, i +1);
            break;
          case "Surface topography":
            _surfaceTopography = new DataSetsFromDFS2(_prePro2D, i +1);
            break;
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
    }

    public IXYDataSet ModelDomainAndGrid
    {
      get { return _modelDomainAndGrid; }
    }

    public IXYDataSet SurfaceTopography
    {
      get { return _surfaceTopography; }
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

    public IXYZDataSet LowerLevelOfComputationalLayers
    {
      get { return _lowerLevelOfComputationalLayers; }
    }

    public IXYZDataSet UpperLevelOfComputationalLayers
    {
      get
      {
        if (_upperLevelOfComputationalLayers == null)
          _upperLevelOfComputationalLayers = new TopOfCell(LowerLevelOfComputationalLayers, SurfaceTopography);
        return _upperLevelOfComputationalLayers;
      }
    }

    public IXYZDataSet BoundaryConditionsForTheSaturatedZone
    {
      get { return _boundaryConditionsForTheSaturatedZone; }
    }

    public IXYZDataSet ThicknessOfComputationalLayers
    {
      get { return _thicknessOfComputationalLayers; }
    }

  }
}
