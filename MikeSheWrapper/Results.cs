using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MikeSheWrapper.Interfaces;
using MikeSheWrapper.DFS;

namespace MikeSheWrapper
{
  public class Results:IDisposable 
  {
    private DFS3 SZ3D;

    /// <summary>
    /// The name of the item containing head elevation data.  
    /// </summary>
    public string HeadElevationString = "head elevation in saturated zone";

    private DataSetsFromDFS3 _heads;
    private DataSetsFromDFS3 _xflow;
    private DataSetsFromDFS3 _yflow;
    private DataSetsFromDFS3 _zflow;
    private DataSetsFromDFS3 _groundWaterExtraction;
    private DataSetsFromDFS3 _sZExchangeFlowWithRiver;
    private DataSetsFromDFS3 _sZDrainageFlow;
    private PhreaticPotential _phreaticHead;

    private MikeSheGridInfo _grid;

    #region Constructors

    /// <summary>
    /// Constructs results from .dfs3 file with head data and a GridInfo object. 
    /// This constructor is only to be used by LayerStatistics.
    /// </summary>
    /// <param name="SZ3DFileName"></param>
    /// <param name="Grid"></param>
    public Results(string SZ3DFileName, MikeSheGridInfo Grid)
    {
      _grid = Grid;
      Initialize3DSZ(SZ3DFileName);
    }

    /// <summary>
    /// Constructs results from .dfs3 file with head data and a GridInfo object. 
    /// This constructor is only to be used by LayerStatistics.
    /// </summary>
    /// <param name="SZ3DFileName"></param>
    /// <param name="Grid"></param>
    public Results(string SZ3DFileName, MikeSheGridInfo Grid, string HeadElevationString)
    {
      this.HeadElevationString = HeadElevationString;
      _grid = Grid;
      Initialize3DSZ(SZ3DFileName);
    }


    /// <summary>
    /// Constructs the Results from a MikeShe setup file (.she)
    /// </summary>
    /// <param name="SheFileName"></param>
    public Results(string SheFileName)
    {
      FileNames fn = new FileNames(SheFileName);
      _grid = new MikeSheGridInfo(fn.PreProcessedSZ3D, fn.PreProcessed2D);
      Initialize3DSZ(fn.Get3DSZFileName);
      Initialize3DSZFlow(fn.get3DSZFlowFileName);
    }

    /// <summary>
    /// Use this when the filenames object and the GridInfo object have already been constructed
    /// </summary>
    /// <param name="fileNames"></param>
    /// <param name="Grid"></param>
    internal Results(FileNames fileNames, MikeSheGridInfo Grid)
    {
      _grid = Grid;
      Initialize3DSZ(fileNames.Get3DSZFileName);
      Initialize3DSZFlow(fileNames.get3DSZFlowFileName);
    }

    #endregion

    /// <summary>
    /// Gets the delete value
    /// </summary>
    public double DeleteValue { get; private set; }


    public IXYZTDataSet Heads
    {
      get { return _heads; }
    }

    public IXYZTDataSet PhreaticHead
    {
      get { return _phreaticHead; }
    }

    public IXYZTDataSet Xflow
    {
      get { return _xflow; }
    }
    public IXYZTDataSet Yflow
    {
      get { return _yflow; }
    }
    public IXYZTDataSet ZFlow
    {
      get { return _zflow; }
    }
    public IXYZTDataSet GroundWaterExtraction
    {
      get { return _groundWaterExtraction; }
    }
    public IXYZTDataSet SZExchangeFlowWithRiver
    {
      get { return _sZExchangeFlowWithRiver; }
    }
    public IXYZTDataSet SZDrainageFlow
    {
      get { return _sZDrainageFlow; }
    }


    /// <summary>
    /// Opens the necessary dfs-files and sets up the references to the properties
    /// </summary>
    /// <param name="fileNames"></param>
    private void Initialize3DSZ(string sz3dFile)
    {
      SZ3D = new DFS3(sz3dFile);
      DeleteValue = SZ3D.DeleteValue;
      for (int i = 0; i < SZ3D.ItemNames.Length; i++)
      {
        if (SZ3D.ItemNames[i].Equals(HeadElevationString, StringComparison.OrdinalIgnoreCase))
        {
            _heads = new DataSetsFromDFS3(SZ3D, i + 1);
            //Also create the phreatic heads;
            _phreaticHead = new PhreaticPotential(_heads, _grid, SZ3D.DeleteValue);
        }
      }
    }

    private void Initialize3DSZFlow(string sz3dFlowFile)
    {
      DFS3 SZ3DFlow = new DFS3(sz3dFlowFile);
      for (int i = 0; i < SZ3DFlow.ItemNames.Length; i++)
      {
        switch (SZ3DFlow.ItemNames[i])
        {
          case "groundwater flow in x-direction":
            _xflow = new DataSetsFromDFS3(SZ3DFlow, i + 1);
            break;
          case "groundwater flow in y-direction":
            _yflow = new DataSetsFromDFS3(SZ3DFlow, i + 1);
            break;
          case "groundwater flow in z-direction":
            _zflow = new DataSetsFromDFS3(SZ3DFlow, i + 1);
            break;
          case "groundwater extraction":
            _groundWaterExtraction = new DataSetsFromDFS3(SZ3DFlow, i + 1);
            break;
          case "SZ exchange flow with river":
            _sZExchangeFlowWithRiver = new DataSetsFromDFS3(SZ3DFlow, i + 1);
            break;
          case "SZ drainage flow from point":
            _sZDrainageFlow = new DataSetsFromDFS3(SZ3DFlow, i + 1);
            break;
          default:
            break;
        }
      }
    }

    #region IDisposable Members

    public void Dispose()
    {
      if (SZ3D != null)
        SZ3D.Dispose();
    }

    #endregion
  }
}
