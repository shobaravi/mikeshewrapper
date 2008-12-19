using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DHI.Generic.MikeZero;


namespace WriteRouting
{
  class Program
  {
    static void Main(string[] args)
    {
      PFSClass M11 = new PFSClass(@"F:\Jacob\suhua\river network\mike 11\river_TotalMcRouting.nwk11");

      PFSTarget Top = M11.GetTarget("MIKE_11_Network_editor", 1);
      PFSSection CompSetup = Top.GetSection("COMPUTATIONAL_SETUP", 1);

      PFSSection Routing = Top.GetSection("MIKE11_ROUTING", 1);
      Routing.Clear();

      int NumberOfBranches = CompSetup.GetSectionsNo("branch");
      string BranchName;
      PFSSection branch;
      PFSKeyword point;
      int NumberOfRoutingPoints = 0;


      for (int i = 1; i <= NumberOfBranches; i++)
      {
        branch = CompSetup.GetSection("branch", i);
        BranchName = branch.GetKeyword("name", 1).GetParameter(1).ToString();
        int NumberOfPoints = branch.GetSection("points",1).GetKeywordsNo("point");

        for (int j = 1; j <= NumberOfPoints; j++)
        {
          point = branch.GetSection("points",1).GetKeyword("point", j);
          if (point.GetParameter(3).ToInt() == 1)
          {
            Routing.AddSection("Routing_Data");
            NumberOfRoutingPoints++;
            PFSSection NewRouting = Routing.GetSection("Routing_Data", NumberOfRoutingPoints);
            
            PFSKeyword NewLocation = new PFSKeyword("Location");
            NewLocation.AddParameter(PFSParameterType.String, BranchName);
            NewLocation.AddParameter(PFSParameterType.Double, point.GetParameter(1).ToDouble());
            NewLocation.AddParameter(PFSParameterType.String, "Novana-model");
            NewRouting.AddKeyword(NewLocation);

            PFSKeyword Attributes = new PFSKeyword("Attributes");
            Attributes.AddParameter(PFSParameterType.Integer, -1);
            Attributes.AddParameter(PFSParameterType.Integer, 1);
            NewRouting.AddKeyword(Attributes);

            PFSSection ELPar = new PFSSection("Elevation_Parameters");
            ELPar.AddSection("QH_Relations");
            NewRouting.AddSection(ELPar);

            PFSSection DisPar = new PFSSection("Discharge_Parameters");
            PFSKeyword Musk = new PFSKeyword("Muskingum");
            Musk.AddParameter(PFSParameterType.Integer, 0);
            Musk.AddParameter(PFSParameterType.Integer, 0);
            DisPar.AddKeyword(Musk);
            NewRouting.AddSection(DisPar);

          }
        }
        
      }

      M11.DumpToPfsFile(@"F:\Jacob\suhua\river network\mike 11\river_TotalMcRouting1.nwk11");

    }
  }
}
