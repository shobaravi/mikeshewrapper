using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MikeSheWrapper.InputDataPreparation
{
    [Serializable]
    public class ShapeReaderConfiguration
    {
      public string PlantIDHeader { get; set; }
        public string WellIDHeader { get; set; }
        public string IntakeNumber { get; set; }
        public string XHeader { get; set; }
        public string YHeader { get; set; }
        public string TOPHeader { get; set; }
        public string BOTTOMHeader { get; set; }
        public string TerrainHeader { get; set; }
        public string FraAArHeader { get; set; }
        public string TilAArHeader { get; set; }
    }
}
