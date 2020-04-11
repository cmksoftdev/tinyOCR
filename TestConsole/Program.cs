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
            var image = Image.FromFile(@"D:\faubolog\tifs\00-42_6.tif");
            //var result = iso.Isolate(image);

            var text = OCR.GetTextFromImage(image);
        }
    }
}
