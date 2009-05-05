using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathNet.Numerics.LinearAlgebra;
using DHI.Generic.MikeZero.DFS;

namespace MikeSheWrapper.DFS
{


  public abstract class DFSBase 
  {

    const string UFSDll = "ufs.dll";  // Name of debug dll

    /// <summary>
    /// Call directly into ufs.dll because the wrapped call does not work on vista due to something with string.
    /// </summary>
    /// <param name="ItemPointer"></param>
    /// <param name="ItemType"></param>
    /// <param name="Name"></param>
    /// <param name="Unit"></param>
    /// <param name="DataType"></param>
    /// <returns></returns>
    [DllImport(UFSDll, CharSet = CharSet.None, CallingConvention = CallingConvention.StdCall)]
    private extern static int dfsGetItemInfo_(IntPtr ItemPointer, ref int ItemType, ref IntPtr Name, ref IntPtr Unit, ref int DataType);

    /// <summary>
    /// Call directly into ufs.dll because the wrapped call does not work on vista due to something with strings.
    /// </summary>
    /// <param name="HeaderPointer"></param>
    /// <param name="Projection"></param>
    /// <param name="longitude"></param>
    /// <param name="Latitude"></param>
    /// <param name="Orientation"></param>
    /// <returns></returns>
    [DllImport(UFSDll, CharSet = CharSet.None, CallingConvention = CallingConvention.StdCall)]
    private extern static int dfsGetGeoInfoUTMProj(IntPtr HeaderPointer, ref IntPtr Projection, ref double longitude, ref double Latitude, ref double Orientation);


    private int _currentTimeStep = -1;
    private int _currentItem = -1;

    protected float[] dfsdata; //Buffer used to fill data into
    protected int _numberOfLayers =1;
    protected int _numberOfColumns =1;
    protected int _numberOfRows = 1;

    protected double _xOrigin;
    protected double _yOrigin;
    protected double _gridSize;

    private DateTime _firstTimeStep;
    private TimeSpan _timeStep = TimeSpan.Zero;
    public DateTime[] TimeSteps {get; private set;}

    private IntPtr _fileWriter = IntPtr.Zero;
    private IntPtr _headerWriter = IntPtr.Zero;
    private bool _initializedForWriting = false;
    
    private string _filename;


    public DFSBase(string DFSFileName)
    {
      _filename = DFSFileName;

      DFSWrapper.dfsFileRead(DFSFileName, ref _headerWriter, ref _fileWriter);


      int nitems = DFSWrapper.dfsGetNoOfItems(_headerWriter);
      IntPtr[] IPointers = new IntPtr[nitems];
      ItemNames = new string[nitems];

      //Gets the pointers to the items
      for (int i = 1; i <= nitems; i++)
        IPointers[i - 1] = (DFSWrapper.dfsItemD(_headerWriter, i));

      int item_type = 0;
      int data_type = 0;
      IntPtr name = new IntPtr();
      IntPtr Eum = new IntPtr();
      string eum_unit = "";
      int unit = 0;

      List<string> eumunits = new List<string>();

      float x = 0;
      float y = 0;
      float z = 0;

      float dx = 0;
      float dy = 0;
      float dz = 0;

      bool firstItem = true;

      int ii = 0;
      //Loop the items
      foreach (IntPtr IP in IPointers)
      {
        dfsGetItemInfo_(IP, ref item_type, ref name, ref Eum, ref data_type);
        ItemNames[ii] = (Marshal.PtrToStringAnsi(name));
        eumunits.Add(Marshal.PtrToStringAnsi(Eum));
        DFSWrapper.dfsGetItemRefCoords(IP, ref x, ref y, ref z);
        ii++;

        //Read in xyz axis-info
        if (firstItem)
        {
          firstItem = false;
          int axistype = DFSWrapper.dfsGetItemAxisType(IP);

          if (axistype == 3)
          {
            IntPtr coords = new IntPtr();
            DFSWrapper.dfsGetItemAxisNeqD1(IP, ref unit, ref eum_unit, ref data_type, ref coords);

            DFSWrapper.dfsGetItemRefCoords(coords, ref x, ref y, ref z);
          }

          //DFS2 from MikeShe
          else if (axistype == 5)
          {
            DFSWrapper.dfsGetItemAxisEqD2(IP, ref item_type, ref eum_unit, ref _numberOfColumns, ref _numberOfRows, ref x, ref y, ref dx, ref dy);
            _xOrigin = x;
            _yOrigin = y;
            _gridSize = dx;
          }
          //DFS3 from MikeShe
          else if (axistype == 8)
          {
            DFSWrapper.dfsGetItemAxisEqD3(IP, ref item_type, ref eum_unit, ref _numberOfColumns, ref _numberOfRows, ref _numberOfLayers, ref x, ref y, ref z, ref dx, ref dy, ref dz);
            _gridSize = dx;

            double lon = 0;
            double lat = 0;
            double or = 0;

            dfsGetGeoInfoUTMProj(_headerWriter, ref name, ref lon, ref lat, ref or);
            _xOrigin = lon;
            _yOrigin = lat;
          }
        }
      }
      //Prepares an array of floats to recieve the data
      dfsdata = new float[_numberOfColumns * _numberOfRows * _numberOfLayers];

      //Now look at time axis
      int timeAxisType = DFSWrapper.dfsGetTimeAxisType(_headerWriter);
      string startdate = "";
      string starttime = "";
      double tstart = 0;
      double tstep = 0;
      int nt = 0;
      int tindex = 0;

      if (timeAxisType != 4)
      {
        DFSWrapper.dfsGetEqCalendarAxis(_headerWriter, ref startdate, ref starttime, ref unit, ref eum_unit, ref tstart, ref tstep, ref nt, ref tindex);

        if (unit == 1400)
          _timeStep = new TimeSpan(0, 0, (int)tstep);

      }
      else if (timeAxisType == 4)
      {
        DFSWrapper.dfsGetNeqCalendarAxis(_headerWriter, ref startdate, ref starttime, ref unit, ref eum_unit, ref tstart, ref tstep, ref nt, ref tindex);
      }

      _firstTimeStep = DateTime.Parse(startdate).Add(TimeSpan.Parse(starttime));
      NumberOfTimeSteps = nt;
      TimeSteps = new DateTime[NumberOfTimeSteps];
      TimeSteps[0] = _firstTimeStep;

      for (int i = 1; i < nt; i++)
      {
        if (timeAxisType == 4)
        {
          if (unit == 1400)
            TimeSteps[i] = _firstTimeStep.AddSeconds(ReadItemTimeStep(i, 1));
          else if (unit == 1402)
            TimeSteps[i] = _firstTimeStep.AddHours(ReadItemTimeStep(i, 1));
        }
        else
          TimeSteps[i] = TimeSteps[i - 1].Add(_timeStep);
      }
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
      DFSWrapper.dfsFileClose(_headerWriter, ref _fileWriter);
   }

    /// <summary>
    /// Destructor called when the object is garbage collected.
    /// </summary>
   ~DFSBase()
   {
     // Simply call Dispose(false).
     Dispose(false);
   }

    /// <summary>
    /// Opens the file for writing. The file is now open twice!
    /// </summary>
   private void InitializeForWriting()
   {
     Dispose(false);
     int ok = DFSWrapper.dfsFileEdit(_filename, ref _headerWriter, ref _fileWriter);
     if (ok != 0)
       throw new Exception("Error in initializing file : " + _filename + " for writing");
     _initializedForWriting = true;
   }

   /// <summary>
   /// Writes timestep and starttime
   /// Because it is called twice
   /// </summary>
   private void WriteTime()
   {
     if (!_initializedForWriting)
       InitializeForWriting();
     int ok = DFSWrapper.dfsSetEqCalendarAxis(_headerWriter, _firstTimeStep.ToString("yyyy-MM-dd"), _firstTimeStep.ToString("hh:mm:ss"), 1400, 0, _timeStep.TotalSeconds, 0);
   }
 

    /// <summary>
    /// Returns the zero-based index of the TimeStep closest to the TimeStamp. If the timestamp falls exactly between two timestep the smallest is returned.
    /// If the TimeStamp is before the first timestep 0 is returned.
    /// If the TimeStamp is after the last timestep the index of the last timestep is returned
    /// </summary>
    /// <param name="TimeStamp"></param>
    /// <returns></returns>
    public int GetTimeStep(DateTime TimeStamp)
    {
      if (TimeStamp < _firstTimeStep || NumberOfTimeSteps==1)
        return 0;
      int TimeStep;
      //fixed timestep
      if (_timeStep != TimeSpan.Zero)
        TimeStep = (int)Math.Round(TimeStamp.Subtract(_firstTimeStep).TotalSeconds / _timeStep.TotalSeconds, 0);
      //Variabale timestep
      else
      {
        //Last timestep is known
        if (TimeStamp >= TimeSteps[TimeSteps.Length - 1])
          return TimeSteps.Length-1;
        
        int i = 1;
        //Loop the timesteps
        while (TimeStamp > TimeSteps[i])
        {
          i++;
        }
        //Check if last one was actually close
        if (TimeSteps[i].Subtract(TimeStamp) < TimeStamp.Subtract(TimeSteps[i - 1]))
          return i;
        else
          return i - 1;
        }
      return Math.Min(NumberOfTimeSteps, TimeStep);
    }

    /// <summary>
    /// Reads data for the TimeStep and Item if necessary and fills them into the buffer.
    /// Time steps counts from 0 and Item from 1.
    /// In case of nonequidistant time (only dfs0) it returns the timestep as double
    /// </summary>
    /// <param name="TimeStep"></param>
    /// <param name="Item"></param>
    protected double ReadItemTimeStep(int TimeStep, int Item)
    {
      double time = 0;

      if (TimeStep != _currentTimeStep || Item != _currentItem)
      {
        _currentTimeStep = TimeStep;
        _currentItem = Item;
        //Spools to the correct Item and TimeStep
        int ok = DFSWrapper.dfsFindItemDynamic(_headerWriter, _fileWriter, TimeStep, Item);
        if (ok != 0)
          throw new Exception("Could not find TimeStep number: " + TimeStep +" and Item number: " + Item);


        //Reads the data
        ok = DFSWrapper.dfsReadItemTimeStep(_headerWriter, _fileWriter, ref time, dfsdata);
        if (ok != 0)
          throw new Exception("Error in file: " + _filename + " reading timestep number: " + this._currentTimeStep);
      }
      return time;
    }

    /// <summary>
    /// Writes data for the TimeStep and Item
    /// </summary>
    /// <param name="TimeStep"></param>
    /// <param name="Item"></param>
    protected void WriteItemTimeStep(int TimeStep, int Item, float[] data)
    {
      if (!_initializedForWriting)
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

    #region Properties

    /// <summary>
    /// Gets and sets the date and time of the first time step.
    /// </summary>
    public DateTime TimeOfFirstTimestep
    {
      get
      {
        return _firstTimeStep;
      }
      set
      {
        _firstTimeStep = value;
        WriteTime();
      }
    }

    /// <summary>
    /// Gets and sets the size of a time step
    /// </summary>
    public TimeSpan TimeStep
    {
      get
      {
        return _timeStep;
      }
      set
      {
        if (_timeStep == TimeSpan.Zero)
          throw new Exception("Cannot set the time step when the dfs-file is non-equidistant"); 
        _timeStep = value;
        WriteTime();
      }
    }


    /// <summary>
    /// Gets the DeleteValue from the DFS-file
    /// </summary>
    public double DeleteValue
    {
      get
      {
        return DFSWrapper.dfsGetDeleteValFloat(_headerWriter);
      }
    }

    /// <summary>
    /// Gets an string array with the names of the items
    /// </summary>
    public string[] ItemNames { get; private set; }

    /// <summary>
    /// Gets the number of timesteps
    /// </summary>
    public int NumberOfTimeSteps{get; protected set;}


    #endregion
  }
}
