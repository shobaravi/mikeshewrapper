using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MikeSheWrapper.InputDataPreparation
{
    [Serializable]
    public class ShapeReaderConfiguration
    {
        public string WellIDHeader { get; set; }
        public string XHeader { get; set; }
        public string YHeader { get; set; }
        public string TOPHeader { get; set; }
        public string BOTTOMHeader { get; set; }
    }
}
