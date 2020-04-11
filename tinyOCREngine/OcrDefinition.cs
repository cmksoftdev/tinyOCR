using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tinyOCREngine
{
    [Serializable]
    public class OcrDefinition
    {
        public string Text { get; set; }        
        public List<OcrAnalyseStep> Steps { get; set; }
    }
}
