using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tinyOCREngine
{
    public enum OcrAnalyseSteps
    {
        GoDownWhileWhite,
        GoDownWhileBlack,
        GoUpWhileWhite,
        GoUpWhileBlack,
        GoLeftWhileWhite,
        GoLeftWhileBlack,
        GoRightWhileWhite,
        GoRightWhileBlack,
        ScanForClosed,
        ScanForOpenUp,
        ScanForOpenRight,
        ScanForOpenDown,
        ScanForOpenLeft,
        IsPixelWhite,
        IsPixelBlack,
        IsBiggerThan,
        IsSmallerThan,
        SetResultToData,
        SetDataToX,
        SetXToData,
        SetYToData,
        SetDataToY,
        IfResultFalseReturnTrue,
        IfResultTrueReturnTrue,
        GetImageWidth,
        GetImageHeight
    }
}
