using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace tinyOCREngine
{
    [Serializable]
    public class Ray
    {
        public bool isVertical { get; set; }
        public int Position { get; set; }
        public int Changes { get; set; }

        public bool isSame(object obj)
        {
            if (obj == null)
                return false;
            return ((Ray)obj).Changes == Changes &&
                ((Ray)obj).isVertical == isVertical;
        }
    }

    public class OCR
    {
        Bitmap image;
        int accuracy = 1;

        OCRData data;

        static OcrAutoAnalysedDatas analysedDatas = null;

        public static string GetTextFromImage(string filePath)
        {
            using (var image = Image.FromFile(filePath))
            {
                var iso = new Isolator();
                var result = iso.Isolate(image);
                var text = "";

                if(analysedDatas == null)
                    analysedDatas = AutoImageAnalyser.AnalyseData(@"..\..\..\data");

                foreach (var img in result)
                {
                    text += AutoImageAnalyser.GetString(img.Image, analysedDatas);
                }

                return text;
            }
        }

        //public static string GetTextFromImage(string filePath)
        //{
        //    using (var image = Image.FromFile(filePath))
        //        return GetTextFromImage(image);
        //}

        public static string GetTextFromImage(Image image)
        {
            var iso = new Isolator();
            var result = iso.Isolate(image);
            var text = "";

            foreach (var img in result)
            {
                if (Alphabet.isA((Bitmap)img.Image))
                    text += "a";
                else if (Alphabet.isB((Bitmap)img.Image))
                    text += "b";
                else if (Alphabet.isC((Bitmap)img.Image))
                    text += "c";
                else if (Alphabet.isD((Bitmap)img.Image))
                    text += "d";
                else if (Alphabet.isE((Bitmap)img.Image))
                    text += "e";
                else if (Alphabet.isF((Bitmap)img.Image))
                    text += "f";
                else if (Alphabet.isG((Bitmap)img.Image))
                    text += "g";
                else if (Alphabet.isH((Bitmap)img.Image))
                    text += "h";
                else if (Alphabet.isI((Bitmap)img.Image))
                    text += "i";
                else if (Alphabet.isJ((Bitmap)img.Image))
                    text += "j";
                else if (Alphabet.isK((Bitmap)img.Image))
                    text += "k";
                else if (Alphabet.isL((Bitmap)img.Image))
                    text += "l";
                else if (Alphabet.isM((Bitmap)img.Image))
                    text += "m";
                else if (Alphabet.isN((Bitmap)img.Image))
                    text += "n";
                else if (Alphabet.isP((Bitmap)img.Image))
                    text += "p";
                else if (Alphabet.isQ((Bitmap)img.Image))
                    text += "q";
                else if (Alphabet.isO((Bitmap)img.Image))
                    text += "o";
                else if (Alphabet.isR((Bitmap)img.Image))
                    text += "r";
                else if (Alphabet.isS((Bitmap)img.Image))
                    text += "s";
                else if (Alphabet.isT((Bitmap)img.Image))
                    text += "t";
                else if (Alphabet.isU((Bitmap)img.Image))
                    text += "u";
                else if (Alphabet.isV((Bitmap)img.Image))
                    text += "v";
                else if (Alphabet.isW((Bitmap)img.Image))
                    text += "w";
                else if (Alphabet.isX((Bitmap)img.Image))
                    text += "x";
                else if (Alphabet.isY((Bitmap)img.Image))
                    text += "y";
                else if (Alphabet.isZ((Bitmap)img.Image))
                    text += "z";
                else
                    text += "-";
            }
            return text;
        }

        public static void CreateDataFromFolder(string folder, string dataName)
        {
            OCRData data = new OCRData
            {
                OCRPairs = new List<OCRPair>()
            };

            DirectoryInfo d = new DirectoryInfo(folder);
            var ocr = new OCR();

            foreach (var file in d.GetFiles("*.png"))
            {
                string ch = Path.GetFileNameWithoutExtension(file.Name);
                if(ch.Contains("_"))
                {
                    var index = ch.IndexOf('_');
                    ch = ch.Substring(0, index);
                }
                using (var image = Image.FromFile(file.FullName))
                    data.OCRPairs.Add(new OCRPair
                    {
                        Text = ch,
                        Rays = ocr.GetRays(image)
                    });
            }
            Serializer.SerializeBinary(dataName, data);
            Serializer.SerializeXml(dataName + ".xml", data);
        }

        public OCR()
        {
        }

        public OCR(string file)
        {
            data = Serializer.DeserializeBinary<OCRData>(file);
            accuracy = data.Accuancy;
        }

        string Pair(List<Ray> rays)
        {
            foreach (var r in data.OCRPairs)
            {
                int i = 0;
                foreach (var r2 in rays)
                {
                    if (i >= r.Rays.Count())
                        break;
                    if (r2.Changes != r.Rays[i].Changes)
                        break;
                    if (i == rays.Count() - 1 && i == r.Rays.Count() - 1)
                        return r.Text;
                    ++i;
                }
            }
            return "-";
        }

        public string RecognizeText(Image image)
        {
            this.image = (Bitmap)image;

            var ray = GetRays(image);

            return Pair(ray);
        }

        public List<Ray> GetRays(Image image)
        {
            var result = new List<Ray>();

            this.image = (Bitmap)image;
            Ray last = null;
            for (int x = 1; x < image.Width; x++)
            {
                var newRay = getVerticalRay(x);
                if (!newRay.isSame(last))
                    result.Add(newRay);
                last = newRay;
            }
            last = null;
            for (int y = 1; y < image.Height; y++)
            {
                var newRay = getHorizontalRay(y);
                if (!newRay.isSame(last))
                    result.Add(newRay);
                last = newRay;
            }

            return result.Where(x => x.Changes != 0).ToList();
        }

        Ray getVerticalRay(int posi)
        {
            var result = new Ray 
            {
                Position = posi,
                isVertical = true
            };
            var pixel = image.GetPixel(posi, 1);
            var last = Isolator.isDarkA(pixel);
            var accu = 0;
            for (int i = 2; i < image.Height; ++i)
            {
                pixel = image.GetPixel(posi, i);
                var changed = Isolator.isDarkA(pixel);
                if(changed != last)
                {
                    if(accu <= accuracy)
                    {
                        accu++;
                    }
                    else
                    {
                        accu = 0;
                        last = changed;
                        result.Changes++;
                    }
                }
            }
            return result;
        }

        bool Debug = false;
        int debugI = 0;

        Ray getHorizontalRay(int posi)
        {
            var result = new Ray
            {
                Position = posi,
                isVertical = false
            };
            var pixel = image.GetPixel(1, posi);
            var last = Isolator.isDarkA(pixel);
            var accu = 0;
            for (int i = 2; i < image.Width; ++i)
            {
                pixel = image.GetPixel(i, posi);
                var changed = Isolator.isDark(pixel);
                if (changed != last)
                {

                    if (accu <= accuracy)
                    {
                        accu++;
                    }
                    else
                    {
                        if (Debug)
                        {
                            image.SetPixel(i, posi, Color.Red);
                        }
                        accu = 0;
                        last = changed;
                        result.Changes++;
                    }
                }
            }
            if (Debug)
                image.Save($"debug/{debugI++}_debug.png", ImageFormat.Png);
            return result;
        }
    }
}
