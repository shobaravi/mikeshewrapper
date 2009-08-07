using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DHI.Generic.MikeZero;

namespace MikeSheWrapper.InputFiles
{
  public partial class CommandAreas
  {
    /// <summary>
    /// Adds a new command area by cloning the first command area in the list
    /// </summary>
    public void AddNewCommandArea()
    {
      if (_commandAreas.Count!=0)
      {
        CommandArea CA = new CommandArea(PFSMapper.DeepClone(_commandAreas[0]._pfsHandle));
        _commandAreas.Add(CA);
        _pfsHandle.AddSection(CA._pfsHandle);
        this.NO_AREAS++;
      }     
    }

    /// <summary>
    /// Adds a new command area that only contains the necessary sections and keywords for a single well.
    /// </summary>
    public void AddNewSingleWellCommandArea()
    {
      PFSSection Nc = new PFSSection("CommandArea");
      Nc.AddKeyword(new PFSKeyword("AreaName", PFSParameterType.String,""));
      Nc.AddKeyword(new PFSKeyword("AreaCodeID", PFSParameterType.String, ""));
      Nc.AddKeyword(new PFSKeyword("AreaCode", PFSParameterType.Integer, 0));
      PFSSection Sources = new PFSSection("Sources");
      Sources.AddKeyword(new PFSKeyword("NumberOfSources", PFSParameterType.Integer, 1));

      PFSSection Source1 = new PFSSection("Source1");
      Source1.AddKeyword(new PFSKeyword("SourceTypeCode", PFSParameterType.Integer, 2));
      Source1.AddKeyword(new PFSKeyword("WaterApplication", PFSParameterType.Integer, 1));
      Source1.AddKeyword(new PFSKeyword("DirectApplication", PFSParameterType.Integer, 1));
      Source1.AddKeyword(new PFSKeyword("WellXposSIWS", PFSParameterType.Double, 1));
      Source1.AddKeyword(new PFSKeyword("WellYposSIWS", PFSParameterType.Double, 1));
      Source1.AddKeyword(new PFSKeyword("ScreenTopDepthSIWS", PFSParameterType.Double, 1));
      Source1.AddKeyword(new PFSKeyword("CapacitySIWS", PFSParameterType.Double, 100));
      Source1.AddKeyword(new PFSKeyword("ThresholdDepthSIWS", PFSParameterType.Double, 1));
      Source1.AddKeyword(new PFSKeyword("ScreenBottomDepthSIWS", PFSParameterType.Double, 1));
      Source1.AddKeyword(new PFSKeyword("IrrigationLicenseIncluded", PFSParameterType.Integer, 0));
      Source1.AddKeyword(new PFSKeyword("TYPE", PFSParameterType.Integer, 2));


      Sources.AddSection(Source1);
      Nc.AddSection(Sources);

      CommandArea CA = new CommandArea(Nc);
      _commandAreas.Add(CA);
      _pfsHandle.AddSection(Nc);

      
      NO_AREAS++;

    }
      

    /// <summary>
    /// Removes the command area at index
    /// </summary>
    /// <param name="index"></param>
    public void RemoveCommandArea(int index)
    {
      _pfsHandle.DeleteSection(_commandAreas[index]._pfsHandle);
      _commandAreas.RemoveAt(index);
      NO_AREAS--;
    }

    /// <summary>
    /// Clear all command areas.
    /// </summary>
    public void ClearCommandAreas()
    {
      for (int i = _commandAreas.Count-1; i>=0;i--)
      {
        _pfsHandle.DeleteSection(_commandAreas[i]._pfsHandle);
        _commandAreas.RemoveAt(i);
      }
      NO_AREAS = 0;

      //PFSMapper.SafeDeleteSection(_commandAreas[0]._pfsHandle, "INDIRECT_APPLICATION_AREA");
      //PFSMapper.SafeDeleteSection(_commandAreas[0].Sources.Source1._pfsHandle, "TIME_SERIES_FILE");
      //PFSMapper.SafeDeleteKeyword(_commandAreas[0].Sources.Source1._pfsHandle, "RiverNameRS");
      //PFSMapper.SafeDeleteKeyword(_commandAreas[0].Sources.Source1._pfsHandle, "UpstreamChainageRS");
      //PFSMapper.SafeDeleteKeyword(_commandAreas[0].Sources.Source1._pfsHandle, "DownstreamChainageRS");
      //PFSMapper.SafeDeleteKeyword(_commandAreas[0].Sources.Source1._pfsHandle, "CapacityRS");
      //PFSMapper.SafeDeleteKeyword(_commandAreas[0].Sources.Source1._pfsHandle, "UseThresholdDischargeRateRS");
      //PFSMapper.SafeDeleteKeyword(_commandAreas[0].Sources.Source1._pfsHandle, "ThresholdDischargeRateStopRS");
      //PFSMapper.SafeDeleteKeyword(_commandAreas[0].Sources.Source1._pfsHandle, "ThresholdDischargeRateRestartRS");
      //PFSMapper.SafeDeleteKeyword(_commandAreas[0].Sources.Source1._pfsHandle, "UseThresholdStageRS");
      //PFSMapper.SafeDeleteKeyword(_commandAreas[0].Sources.Source1._pfsHandle, "ThresholdStageStopRS");
      //PFSMapper.SafeDeleteKeyword(_commandAreas[0].Sources.Source1._pfsHandle, "ThresholdStageRestartRS");
      //PFSMapper.SafeDeleteKeyword(_commandAreas[0].Sources.Source1._pfsHandle, "ScreenTopDepthSWS");
      //PFSMapper.SafeDeleteKeyword(_commandAreas[0].Sources.Source1._pfsHandle, "ThresholdDepthSWS");
      //PFSMapper.SafeDeleteKeyword(_commandAreas[0].Sources.Source1._pfsHandle, "CapacitySWS");
      //PFSMapper.SafeDeleteKeyword(_commandAreas[0].Sources.Source1._pfsHandle, "ScreenBottomDepthSWS");

    }
  }
}
