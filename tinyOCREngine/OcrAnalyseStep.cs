using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tinyOCREngine
{
    [Serializable]
    public class OcrAnalyseStep
    {
        public int Sequence { get; set; }
        public string Parameters { get; set; }
        public OcrAnalyseSteps Step { get; set; }
    }

    //public class OcrAnalyseStepE : OcrAnalyseStep
    //{
    //    public bool Done { get; set; }
    //}
}
