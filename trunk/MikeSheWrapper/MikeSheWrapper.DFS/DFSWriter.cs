using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DHI.Generic.MikeZero.DFS;

namespace MikeSheWrapper.DFS
{
  public class DFSWriter:IDisposable
  {
    private IntPtr _filePointer = IntPtr.Zero;
    private IntPtr _headerPointer = IntPtr.Zero;
    private int _currentTimeStep = -1;
    private int _currentItem = -1;



    public DFSWriter(string Filename)
    {
      int ok = DFSWrapper.dfsFileEdit(Filename, ref _headerPointer, ref _filePointer);
    }

    /// <summary>
    /// Reads data for the TimeStep and Item if necessary and fills them into the buffer
    /// </summary>
    /// <param name="TimeStep"></param>
    /// <param name="Item"></param>
    public void WriteItemTimeStep(int TimeStep, int Item, float[] data)
    {
      if (TimeStep != _currentTimeStep || Item != _currentItem)
      {
        _currentTimeStep = TimeStep;
        _currentItem = Item;
        //Spools to the correct Item and TimeStep
        int ok = DFSWrapper.dfsFindItemDynamic(_headerPointer, _filePointer, TimeStep, Item);
        if (ok != 0)
          throw new Exception("Could not find TimeStep number: " + TimeStep + " and Item number: " + Item);

        double time = 0;

        //Reads the data
        ok = DFSWrapper.dfsWriteItemTimeStep(_headerPointer, _filePointer, time, data);
        if (ok != 0)
          throw new Exception("Error writing timestep number: " + _currentTimeStep);
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
     DFSWrapper.dfsFileClose( _headerPointer, ref _filePointer);

   }

    /// <summary>
    /// Destructor called when the object is garbage collected.
    /// </summary>
   ~DFSWriter()
   {
     // Simply call Dispose(false).
     Dispose(false);
   }


  }
}
