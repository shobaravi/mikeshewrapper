using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MikeSheWrapper.Interfaces;
using MikeSheWrapper.DFS;

namespace MikeSheWrapper
{
  public class Results
  {

    private DataSetsFromDFS3 _heads;
    private DataSetsFromDFS3 _xflow;
    private DataSetsFromDFS3 _yflow;
    private DataSetsFromDFS3 _zflow;
    private DataSetsFromDFS3 _groundWaterExtraction;
    private DataSetsFromDFS3 _sZExchangeFlowWithRiver;
    private DataSetsFromDFS3 _sZDrainageFlow;
    private PhreaticPotential _phreaticHead;


    private ProcessedData _processed;


    public Results(FileNames fileNames, ProcessedData Processed)
    {
      _processed = Processed;
      Initialize(fileNames);
    }

    public Results(string SheFileName)
    {
      FileNames fn = new FileNames(SheFileName);
      _processed = new ProcessedData(fn);
      Initialize(fn);
    }

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
    private void Initialize(FileNames fileNames)
    {
      DFS3 SZ3D = new DFS3(fileNames.Get3DSZFileName);
      for (int i = 0; i < SZ3D.DynamicItemInfos.Length; i++)
      {
        switch (SZ3D.DynamicItemInfos[i].Name)
        {
          case "head elevation in saturated zone":
            _heads = new DataSetsFromDFS3(SZ3D, i + 1);
            //Also create the phreatic heads;
            _phreaticHead = new PhreaticPotential(_heads, _processed.LowerLevelOfComputationalLayers, _processed.ThicknessOfComputationalLayers);
            break;
          default:
            break;
        }
      }

      DFS3 SZ3DFlow = new DFS3(fileNames.get3DSZFlowFileName);
      for (int i = 0; i < SZ3DFlow.DynamicItemInfos.Length; i++)
      {
        switch (SZ3DFlow.DynamicItemInfos[i].Name)
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
  }
}
