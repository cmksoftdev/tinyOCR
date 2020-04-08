using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tinyOCREngine;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var iso = new Isolator();
            var image = Image.FromFile(@"D:\faubolog\tifs\00-30_4.tif");
            var result = iso.Isolate(image);

            var text = "";

            foreach (var img in result)
            {
                img.Image.Save($"{img.Sequence}.png", ImageFormat.Png);
                if (Alphabet.isA((Bitmap)img.Image))
                    text += "a";
                else if(Alphabet.isB((Bitmap)img.Image))
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
                else if (Alphabet.isK((Bitmap)img.Image))
                    text += "k";
                else if (Alphabet.isN((Bitmap)img.Image))
                    text += "n";
                else if (Alphabet.isP((Bitmap)img.Image))
                    text += "p";
                else if (Alphabet.isQ((Bitmap)img.Image))
                    text += "q";
                else if (Alphabet.isO((Bitmap)img.Image))
                    text += "o";
                else
                    text += "-";
            }
        }
    }
}
