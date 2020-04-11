using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tinyOCREngine
{
    [Serializable]
    public class OctDefinitionConfig
    {
        public List<OcrDefinition> Definitions { get; set; }
    }
}
