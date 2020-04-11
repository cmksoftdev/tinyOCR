using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tinyOCREngine
{
    [Serializable]
    public class OcrAutoAnalysedData
    {
        public string Text { get; set; }
        public List<OcrWhitePlaces> TopDown_WhitePlaces { get; set; }

        public List<int> X_Line_Up { get; set; }
        public List<int> X_Line_Mid { get; set; }
        public List<int> X_Line_Low { get; set; }
    }


    [Serializable]
    public class OcrAutoAnalysedDatas
    {
        public List<OcrAutoAnalysedData> Datas { get; set; }
    }
}
