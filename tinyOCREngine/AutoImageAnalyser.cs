using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tinyOCREngine;

namespace tinyOCREngine
{
    public class AutoImageAnalyser
    {
        static List<OcrWhitePlaces> analyseTopDown(Image image)
        {
            int xMiddle = image.Width / 2;
            int y = 0;

            List<OcrWhitePlaces> whitePoints = new List<OcrWhitePlaces>();

            bool isCalled;
            bool isEnded;
            //y = Alphabet.goDownWhite_s((Bitmap)image, xMiddle, y, out isCalled, out isEnded);
            do
            {
                y = Alphabet.goDownBlack_s((Bitmap)image, xMiddle, ++y, out isCalled, out isEnded);
                y = Alphabet.goDownWhite_s((Bitmap)image, xMiddle, ++y, out isCalled, out isEnded);
                if (isCalled && !isEnded)
                    whitePoints.Add(new OcrWhitePlaces
                    {
                        X = xMiddle,
                        Y = y - 1
                    });
            }
            while (!isEnded);

            foreach (var whitePlace in whitePoints)
            {
                whitePlace.OpenToSite = Isolator.OpenToSite(image, whitePlace.X, whitePlace.Y);
            }

            return whitePoints;
        }

        static void analyseLines(Image image, OcrAutoAnalysedData data)
        {
            int[] x = new int[2];
            int[] y = new int[3];

            x[0] = image.Width / 3;
            x[1] = x[0] * 2;

            y[0] = image.Height / 4;
            y[1] = y[0] * 2;
            y[2] = y[0] * 3;

            int i = 0;

            bool isCalled, isEnded;

            data.X_Line_Low = new List<int>();
            data.X_Line_Mid = new List<int>();
            data.X_Line_Up = new List<int>();

            do
            {
                i = Alphabet.goRightWhite_s((Bitmap)image, i++, y[0], out isCalled, out isEnded);
                i = Alphabet.goRightBlack_s((Bitmap)image, i++, y[0], out isCalled, out isEnded);
                if (isCalled && !isEnded)
                {
                    int position = i < x[0] ? 0 :
                         i < x[1] ? 1 : 2;
                    data.X_Line_Up.Add(position);
                }
            }
            while (!isEnded);

            i = 0;
            do
            {
                i = Alphabet.goRightWhite_s((Bitmap)image, i++, y[1], out isCalled, out isEnded);
                i = Alphabet.goRightBlack_s((Bitmap)image, i++, y[1], out isCalled, out isEnded);
                if (isCalled && !isEnded)
                {
                    int position = i < x[0] ? 0 :
                         i < x[1] ? 1 : 2;
                    data.X_Line_Mid.Add(position);
                }
            }
            while (!isEnded);

            i = 0;
            do
            {
                i = Alphabet.goRightWhite_s((Bitmap)image, i++, y[2], out isCalled, out isEnded);
                i = Alphabet.goRightBlack_s((Bitmap)image, i++, y[2], out isCalled, out isEnded);
                if (isCalled && !isEnded)
                {
                    int position = i < x[0] ? 0 :
                         i < x[1] ? 1 : 2;
                    data.X_Line_Low.Add(position);
                }
            }
            while (!isEnded);
        }

        static OcrAutoAnalysedData analyseImage(string dataPath)
        {
            var def = new OcrAutoAnalysedData();
            def.Text = "" + Path.GetFileNameWithoutExtension(dataPath)[0];

            using (var image = Image.FromFile(dataPath))
                def.TopDown_WhitePlaces = analyseTopDown(image);

            return def;
        }

        static OcrAutoAnalysedData analyseImage(Image image)
        {
            var def = new OcrAutoAnalysedData();

            def.TopDown_WhitePlaces = analyseTopDown(image);

            return def;
        }

        public static OcrAutoAnalysedDatas AnalyseData(string directory)
        {
            var data = new OcrAutoAnalysedDatas
            {
                Datas = new List<OcrAutoAnalysedData>()
            };
            DirectoryInfo d = new DirectoryInfo(directory);
            var iso = new Isolator();
            foreach (var file in d.GetFiles("*.png"))
            {
                var dat = analyseImage(file.FullName);
                bool b = false;
                using (var image = Image.FromFile(file.FullName))
                {
                    var result = iso.Isolate(image);
                    if (result.Count() == 1)
                    {
                        b = true;
                        analyseLines(result.First().Image, dat);
                    }
                }
                if(b)
                data.Datas.Add(dat);
            }
            return data;
        }

        public static string GetString(Image image, OcrAutoAnalysedDatas datas)
        {
            OcrAutoAnalysedData data = analyseImage(image);
            analyseLines(image, data);
            var sorted = datas.Datas.Where(x => 
                x.TopDown_WhitePlaces.Count() == data.TopDown_WhitePlaces.Count() &&
                x.X_Line_Low.Count() == data.X_Line_Low.Count() &&
                x.X_Line_Mid.Count() == data.X_Line_Mid.Count() &&
                x.X_Line_Up.Count() == data.X_Line_Up.Count());
            foreach (var curData in sorted)
            {
                var ziped = data.TopDown_WhitePlaces.Zip(curData.TopDown_WhitePlaces,
                    (a, b) => new { A = a, B = b });
                bool matched = true;
                foreach (var combined in ziped)
                {
                    if(combined.A.OpenToSite != combined.B.OpenToSite)
                    {
                        matched = false;
                        break;
                    }
                }
                if (matched)
                    for (int i = 0; i < curData.X_Line_Low.Count(); ++i)
                    {
                        if(curData.X_Line_Low[i] != data.X_Line_Low[i])
                        {
                            matched = false;
                            break;
                        }
                    }
                //if (matched)
                //    for (int i = 0; i < curData.X_Line_Mid.Count(); ++i)
                //    {
                //        if (curData.X_Line_Mid[i] != data.X_Line_Mid[i])
                //        {
                //            matched = false;
                //            break;
                //        }
                //    }
                if (matched)
                    for (int i = 0; i < curData.X_Line_Up.Count(); ++i)
                    {
                        if (curData.X_Line_Up[i] != data.X_Line_Up[i])
                        {
                            matched = false;
                            break;
                        }
                    }
                if (matched)
                    return curData.Text;
            }
            return "";
        }
    }
}
