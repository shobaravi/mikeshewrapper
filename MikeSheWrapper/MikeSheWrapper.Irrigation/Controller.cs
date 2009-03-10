using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MikeSheWrapper;
using MikeSheWrapper.Tools;
using MikeSheWrapper.InputFiles;

namespace MikeSheWrapper.Irrigation
{
  public class Controller
  {
    private Model _she;
    private Configuration _config;
    private DataTable _wellData;
    private List<IrrigationWell> _wells = new List<IrrigationWell>();
    private List<CommandAreas> _originalAreas = new List<CommandAreas>();


    public Controller(Configuration Config)
    {
      _config = Config;
      _she = new Model(_config.SheFile);

    }

    public void Run()
    {
      ReadWellsFromShape();
      InsertIrrigationWells();
      SaveAs(_config.SheFile);
      MSheLauncher.PreprocessAndRun(_config.SheFile, true);
      if (_config.DeleteWellsAfterRun)
      {
        _she.Input.MIKESHE_FLOWMODEL.LandUse.CommandAreas.ClearCommandAreas();
        SaveAs(_config.SheFile);
      }
    }

    public bool IrrigationEnabled
    {
      get
      {
        return true;
      }

    }

    public void ReadWellsFromShape()
    {
      PointShapeReader SR = new PointShapeReader(_config.WellShapeFile);
      _wellData = SR.Data.Read();
      SR.Dispose();

      foreach (DataRow dr in _wellData.Rows)
      {
        IrrigationWell IW = new IrrigationWell();
        IW.X = (double) dr[_config.XHeader];
        IW.Y = (double) dr[_config.YHeader];
        IW.ID = dr[_config.IdHeader].ToString();
        //IW.MaxDepth = (double) dr[_config.MaxDepthHeader];
        //IW.MaxRate = (double) dr[_config.MaxRateHeader];
        IW.ScreenBottom.Add( (double)dr[_config.BottomHeader]);
        IW.ScreenTop.Add( (double) dr[_config.TopHeader]);
        _wells.Add(IW);
      }
    }

    public void InsertIrrigationWells()
    {
      _she.Input.MIKESHE_FLOWMODEL.LandUse.CommandAreas.ClearCommandAreas();
 

      //Note. It is assumed that the wells are ordered in the same way as the shapes
      for (int i = 0; i < _wells.Count; i++) 
      {
        _she.Input.MIKESHE_FLOWMODEL.LandUse.CommandAreas.AddNewCommandArea();

        int AreaCode;
        if (int.TryParse(_wells[i].ID, out AreaCode))
          _she.Input.MIKESHE_FLOWMODEL.LandUse.CommandAreas.CommandAreas1[i].AreaCode = AreaCode;
        else
          _she.Input.MIKESHE_FLOWMODEL.LandUse.CommandAreas.CommandAreas1[i].AreaCode = 0;

        _she.Input.MIKESHE_FLOWMODEL.LandUse.CommandAreas.CommandAreas1[i ].AreaCodeID = _wells[i].ID;
        _she.Input.MIKESHE_FLOWMODEL.LandUse.CommandAreas.CommandAreas1[i ].AreaName  = _wells[i].ID;
        _she.Input.MIKESHE_FLOWMODEL.LandUse.CommandAreas.CommandAreas1[i ].Sources.Source1.WellXposSIWS = _wells[i].X;
        _she.Input.MIKESHE_FLOWMODEL.LandUse.CommandAreas.CommandAreas1[i ].Sources.Source1.WellYposSIWS = _wells[i].Y;

        double Topo = _she.GridInfo.SurfaceTopography.GetData(_wells[i].X, _wells[i].Y);

        _she.Input.MIKESHE_FLOWMODEL.LandUse.CommandAreas.CommandAreas1[i ].Sources.Source1.ScreenTopDepthSIWS  = Math.Max(2,Topo -_wells[i].ScreenTop[0]);
        _she.Input.MIKESHE_FLOWMODEL.LandUse.CommandAreas.CommandAreas1[i].Sources.Source1.ScreenBottomDepthSIWS = Math.Max(3, Topo - _wells[i].ScreenBottom[0]);
        _she.Input.MIKESHE_FLOWMODEL.LandUse.CommandAreas.CommandAreas1[i].Sources.Source1.ThresholdDepthSIWS = Math.Max(3, Topo - _wells[i].ScreenBottom[0]);

      }
      _she.Input.MIKESHE_FLOWMODEL.LandUse.CommandAreas.Type = 2;
      _she.Input.MIKESHE_FLOWMODEL.LandUse.CommandAreas.SHAPE_FILE.FILE_NAME = _config.WellShapeFile;
      _she.Input.MIKESHE_FLOWMODEL.LandUse.CommandAreas.SHAPE_FILE.ITEM_NUMBERS = _config.IdHeader;

      _she.Input.MIKESHE_FLOWMODEL.LandUse.CommandAreas.RemoveCommandArea(0);


    }

    public void SaveAs(string FileName)
    {
      _she.Input.SaveAs(FileName);
    }

  }
}
