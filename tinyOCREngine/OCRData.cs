using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tinyOCREngine
{
    [Serializable]
    public class OCRPair
    {
        public string Text { get; set; }
        public List<Ray> Rays { get; set; }
    }

    [Serializable]
    public class OCRData
    {
        public List<OCRPair> OCRPairs { get; set; }
        public int Accuancy { get; set; } = 1;
    }
}
