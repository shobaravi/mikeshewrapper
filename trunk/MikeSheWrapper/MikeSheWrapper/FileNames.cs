using System;
using System.IO;
using MikeSheWrapper.InputFiles;

namespace MikeSheWrapper
{
	/// <summary>
	/// This class provides different filenames from the .she-file
	/// </summary>
	public class FileNames
	{
		private string _fileNameWithPath;
    private string _fileNameWithoutPath;
    private int _version;
    private string _resultsPath;
    private SheFile _input;


    internal FileNames(SheFile Input)
    {
      _input = Input;
      _fileNameWithPath = Input.FileName;
      Initialize();
    }


    private void Initialize()
    {
      //Gets the filename without path and extension
      _fileNameWithoutPath = Path.GetFileNameWithoutExtension(_fileNameWithPath);
      _version = _input.MIKESHE_FLOWMODEL.FlowModelDocVersion.Version;

      //Todo read from SheFile when necessary
      _resultsPath = Path.Combine(Path.GetDirectoryName(_fileNameWithPath), _fileNameWithoutPath + ".she - Result Files");

    }
    
    public FileNames(string MSheFileName)
		{
			CheckFiles(MSheFileName);
			_fileNameWithPath = MSheFileName;
      _input = new SheFile(MSheFileName);
      Initialize();
		}

    public FileNames()
    {

    }

		/// <summary>
    /// Gets the Fif-filename
		/// </summary>
		/// <returns></returns>
		public string FifFileName
		{
      get
      {
        string FName = Path.Combine(_resultsPath , Path.ChangeExtension (_fileNameWithoutPath, ".fif"));
        CheckFiles(FName);
        return FName;
      }
		}

    /// <summary>
    /// Gets the .wel Filename
    /// </summary>
    public string WelFileName
    {
      get
      {
        return _input.MIKESHE_FLOWMODEL.SaturatedZone.Well.Filename; 
        //TODO reference to .shefile
//        return new PFSClass(this._fileNameWithPath).GetTarget("MIKESHE_FLOWMODEL", 1).GetSection("SaturatedZone", 1).GetSection("Well", 1).GetKeyword("Filename", 1).GetParameter(1).Value.ToString(); 
      }
    }

		//Returns the .dfs0 file with MikeShe to Mike11 flow.
		public string MShe2RiverFileName(int Code)
		{
			string extension="";
			if (Code==1)
				extension="_Dr2River.dfs0";
			else if (Code==2)
				extension="_OL2River.dfs0";
			else if (Code==3)
				extension="_SZ2River.dfs0";
			else if (Code==4)
				extension="_Total2River.dfs0";
      return getFile(extension);
		}
		
		/// <summary>
		/// Returns the Mike11-Additional HD output-file
		/// </summary>
		/// <returns></returns>
		public string addHDFileName
		{
      get
      {
        string addHDFileName= this.res11FileName.Insert(res11FileName.LastIndexOf("."),"HDAdd");
        CheckFiles(addHDFileName);
        return addHDFileName;
      }
		}

    /// <summary>
    /// Returns the Mike11 HD output-file
    /// </summary>
    /// <returns></returns>
    public string res11FileName
    {
      get
      {
        //Todo .shefile
        //string Sim11FileName=new PFSClass(this._fileNameWithPath).GetTarget("MIKESHE_FLOWMODEL",1).GetSection("River",1).GetKeyword("Filename",1).GetParameter(1).Value.ToString(); 
        //string Res11FileName=new PFSClass(Sim11FileName).GetTarget("Run11",1).GetSection("Results",1).GetKeyword("hd",1).GetParameter(1).Value.ToString(); 
        //Res11FileName=Path.Combine(Path.GetDirectoryName(Sim11FileName),Res11FileName);
        //CheckFiles(Res11FileName);
        //return Res11FileName;
        return "";
      }
    }

    ///// <summary>
    ///// Returns the Mike11-Networkfile
    ///// </summary>
    ///// <returns></returns>
    //public string NwkFileName
    //{
    //  get
    //  {
    //    string Sim11FileName=new PFSClass(this._fileNameWithPath).GetTarget("MIKESHE_FLOWMODEL",1).GetSection("River",1).GetKeyword("Filename",1).GetParameter(1).Value.ToString(); 
    //    string NwkFileName=new PFSClass(Sim11FileName).GetTarget("Run11",1).GetSection("Input",1).GetKeyword("nwk",1).GetParameter(1).Value.ToString(); 
    //    NwkFileName=Path.Combine(Path.GetDirectoryName(Sim11FileName),NwkFileName);
    //    CheckFiles(NwkFileName);
    //    return NwkFileName;
    //  }
    //}

		public string Get3DSZFileName
		{
      get
      {
        return getFile("_3DSZ.dfs3");
      }
		}
/// <summary>
/// Returns the FileName for the flow-file;
/// </summary>
/// <returns></returns>
    public string get3DSZFlowFileName
    {
      get
      {
        return getFile("_3DSZflow.dfs3");
      }
    }

    public string PreProcessed2D
    {
      get
      {
        return getFile("_PreProcessed.DFS2");
      }
    }

    public string PreProcessedSZ3D
    {
      get
      {
        return getFile("_PreProcessed_3DSZ.dfs3");
      }
    }

    public string DetailedTimeSeriesSZ
    {
      get
      {
        return getFile("DetailedTS_SZ.dfs0");
      }
    }

    /// <summary>
    /// Gets and sets a string with the name and path of the .she-file
    /// </summary>
    public string SheFile
    {
      get
      {
        return this._fileNameWithPath;
      }
      set
      {
        this.CheckFiles(value);
        this._fileNameWithPath = value;
      }      
    }

    /// <summary>
    /// Gets the ouput directory
    /// </summary>
    public string ResultsDirectory
    {
      get
      {
        return _resultsPath;
      }
    }

		//Throws an exception if the file does not exist
		public void CheckFiles(params string[] FileNames)
		{
			foreach (string file in FileNames)
			{
				if (!File.Exists(file))
					throw new FileNotFoundException (file + " ikke fundet!");
			}
		}

    private string getFile(string extension)
    {
      string FileName = Path.Combine(_resultsPath, _fileNameWithoutPath + extension);
      CheckFiles(FileName);
      return FileName;
    }

	}
}
