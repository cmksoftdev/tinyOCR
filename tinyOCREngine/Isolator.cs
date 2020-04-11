using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tinyOCREngine
{
    public class ImageOutput
    {
        public int Sequence { get; set; }
        public string Text { get; set; }
        public Image Image { get; set; }
        public Position Position { get; set; }
    }

    public class Position
    {
        public int X;
        public int Y;

        public bool IsScanned = false;

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public class Isolator
    {
        Bitmap image;

        public IEnumerable<ImageOutput> Isolate(Image image)
        {
            this.image = (Bitmap)image;
            var output = new List<ImageOutput>();

            ImageOutput result;
            do
            {
                result = getCharImage();
                if (result != null)
                    output.Add(result);
            }
            while (result != null);
            output = sort(output);
            return output;
        }

        public ImageOutput getCharImage()
        {
            for (int y = 0; y < image.Height; ++y)
            {
                for (int x = 0; x < image.Width; ++x)
                {
                    var pixel = image.GetPixel(x, y);
                    if(isDarkA(pixel))
                    {
                        return cutImage(x, y);
                    }
                }
            }
            return null;
        }

        bool isInImage(int x, int y)
        {
            return x >= 0 && x < image.Width &&
                y >= 0 && y < image.Height;
        }

        public static bool isInImage(Image image, int x, int y)
        {
            return x >= 0 && x < image.Width &&
                y >= 0 && y < image.Height;
        }

        public static bool isInImage(Image image, Position posi)
        {
            return isInImage(image, posi.X, posi.Y);
        }

        bool isInImage(Position posi)
        {
            return posi.X >= 0 && posi.X < image.Width &&
                posi.Y >= 0 && posi.Y < image.Height;
        }

        public static void scanUnsafe(Image image, int x, int y)
        {
            bool reference = isDarkA((Bitmap)image, x, y);

            var list = new List<Position>();

            list.Add(new Position(x, y));
            List<Position> notScanned = null;
            do
            {
                notScanned = list.Where(item => !item.IsScanned).ToList();
                foreach (var posi in notScanned)
                {
                    var all = new Position[]
                    {
                        //new Position(posi.X-1, posi.Y-1),
                        new Position(posi.X, posi.Y-1),
                        //new Position(posi.X+1, posi.Y-1),
                        new Position(posi.X-1, posi.Y),
                        new Position(posi.X+1, posi.Y),
                        //new Position(posi.X-1, posi.Y+1),
                        new Position(posi.X, posi.Y+1),
                        //new Position(posi.X+1, posi.Y+1)
                    };
                    foreach (var newPosi in all)
                    {
                        if (list.Any(item => item.X == newPosi.X && item.Y == newPosi.Y))
                            continue;
                            if (isDarkA((Bitmap)image, newPosi) == reference)
                            {
                                list.Add(newPosi);
                            }
                    }
                    posi.IsScanned = true;
                }
            }
            while (notScanned.Count > 0);
        }

        public static int OpenToSite(Image image, int x, int y)
        {
            bool reference = isDarkA((Bitmap)image, x, y);

            var list = new List<Position>();

            list.Add(new Position(x, y));
            List<Position> notScanned = null;
            do
            {
                notScanned = list.Where(item => !item.IsScanned).ToList();
                foreach (var posi in notScanned)
                {
                    var all = new Position[]
                    {
                        //new Position(posi.X-1, posi.Y-1),
                        new Position(posi.X, posi.Y-1),
                        //new Position(posi.X+1, posi.Y-1),
                        new Position(posi.X-1, posi.Y),
                        new Position(posi.X+1, posi.Y),
                        //new Position(posi.X-1, posi.Y+1),
                        new Position(posi.X, posi.Y+1)
                        //new Position(posi.X+1, posi.Y+1)
                    };
                    foreach (var newPosi in all)
                    {
                        if (list.Any(item => item.X == newPosi.X && item.Y == newPosi.Y))
                            continue;
                        try
                        {
                            if (isDarkA((Bitmap)image, newPosi) == reference)
                            {
                                list.Add(newPosi);
                            }
                        }
                        catch
                        {
                            if (newPosi.Y < 0)
                                return 1;
                            else if (newPosi.X >= image.Width)
                                return 2;
                            else if (newPosi.Y >= image.Height)
                                return 3;
                            else if (newPosi.X < 0)
                                return 4;
                            else return 0;
                        }
                    }
                    posi.IsScanned = true;
                }
            }
            while (notScanned.Count > 0);
            return 0;
        }

        public static bool isConnected(Image image, int x1, int y1, int x2, int y2)
        {
            var list = new List<Position>();

            list.Add(new Position(x1, y1));
            List<Position> notScanned = null;
            do
            {
                notScanned = list.Where(item => !item.IsScanned).ToList();
                foreach (var posi in notScanned)
                {
                    if (isInImage(image, posi))
                    {
                        var all = new Position[]
                        {
                        //new Position(posi.X-1, posi.Y-1),
                        new Position(posi.X, posi.Y-1),
                        //new Position(posi.X+1, posi.Y-1),
                        new Position(posi.X-1, posi.Y),
                        new Position(posi.X+1, posi.Y),
                        //new Position(posi.X-1, posi.Y+1),
                        new Position(posi.X, posi.Y+1),
                        //new Position(posi.X+1, posi.Y+1)
                        };
                        foreach (var newPosi in all)
                        {
                            if (newPosi.X == x2 && newPosi.Y == y2)
                                return true;
                            if (!isInImage(image, newPosi))
                                continue;
                            if (list.Any(item => item.X == newPosi.X && item.Y == newPosi.Y))
                                continue;
                            if (isDarkA((Bitmap)image, newPosi))
                            {
                                list.Add(newPosi);
                            }
                        }
                        posi.IsScanned = true;
                    }
                }
            }
            while (notScanned.Count > 0);
            return false;
        }

        ImageOutput cutImage(int x, int y)
        {
            var list = new List<Position>();

            list.Add(new Position(x, y));
            List<Position> notScanned = null;
            do
            {
                notScanned = list.Where(item => !item.IsScanned).ToList();
                foreach (var posi in notScanned)
                {
                    if (isInImage(posi))
                    {
                        var all = new Position[]
                        {
                        //new Position(posi.X-1, posi.Y-1),
                        new Position(posi.X, posi.Y-1),
                        //new Position(posi.X+1, posi.Y-1),
                        new Position(posi.X-1, posi.Y),
                        new Position(posi.X+1, posi.Y),
                        //new Position(posi.X-1, posi.Y+1),
                        new Position(posi.X, posi.Y+1),
                        //new Position(posi.X+1, posi.Y+1)
                        };
                        foreach (var newPosi in all)
                        {
                            if (!isInImage(newPosi))
                                continue;
                            if (list.Any(item => item.X == newPosi.X && item.Y == newPosi.Y))
                                continue;
                            if (isDarkA(image, newPosi))
                            {
                                list.Add(newPosi);
                            }
                        }
                        posi.IsScanned = true;
                    }
                }
            }
            while (notScanned.Count > 0);

            var width = list.Max(item => item.X) - list.Min(item => item.X) + 1;
            var height = list.Max(item => item.Y) - list.Min(item => item.Y) + 1;
            var wOffset = list.Min(item => item.X);
            var hOffset = list.Min(item => item.Y);

            var bitmap = new Bitmap(width, height);
            foreach (var posi in list)
            {
                //if (isInImage(posi.X - wOffset, posi.Y - hOffset))
                    bitmap.SetPixel(posi.X - wOffset, posi.Y - hOffset, Color.Black);
                image.SetPixel(posi.X, posi.Y, Color.White);
            }
            return new ImageOutput
            {
                Image = bitmap,
                Sequence = 0,
                Text = "",
                Position = new Position(wOffset, hOffset)
            };
        }

        public static bool isDark(Color pixel)
        {
            return pixel.R < 200 ||
                pixel.G < 200 ||
                pixel.B < 200;
        }

        public static bool isDarkA(Bitmap image, Position posi)
        {
            Color pixel = image.GetPixel(posi.X, posi.Y);
            return (pixel.R < 200 ||
                pixel.G < 200 ||
                pixel.B < 200) &&
                pixel.A > 5;
        }
        public static bool isDarkA(Bitmap image, int x, int y)
        {
            Color pixel = image.GetPixel(x, y);
            return (pixel.R < 200 ||
                pixel.G < 200 ||
                pixel.B < 200) &&
                pixel.A > 5;
        }

        public static bool isDarkA(Color pixel)
        {
            return (pixel.R < 200 ||
                pixel.G < 200 ||
                pixel.B < 200) &&
                pixel.A > 5;
        }

        public bool isDark(Position posi)
        {
            Color pixel = image.GetPixel(posi.X, posi.Y);
            return pixel.R < 200 ||
                pixel.G <200 ||
                pixel.B < 200;
        }

        Tuple<ImageOutput, ImageOutput> cutImages(ImageOutput image, bool vertical)
        {
            Rectangle rectangle1 = vertical ?
                new Rectangle(0, 0, image.Image.Width, image.Image.Height / 2) :
                new Rectangle(0, 0, image.Image.Width / 2, image.Image.Height);

            Rectangle rectangle2 = vertical ?
                new Rectangle(0, image.Image.Height / 2, image.Image.Width, image.Image.Height / 2) :
                new Rectangle(image.Image.Width / 2, 0, image.Image.Width / 2, image.Image.Height);

            Bitmap croppedImage1 = ((Bitmap)image.Image).Clone(rectangle1, image.Image.PixelFormat);
            Bitmap croppedImage2 = ((Bitmap)image.Image).Clone(rectangle2, image.Image.PixelFormat);

            return new Tuple<ImageOutput, ImageOutput>
                (new ImageOutput
                {
                    Image = croppedImage1,
                    Position = new Position(
                        image.Position.X + rectangle1.X,
                        image.Position.Y + rectangle1.Y)
                }, 
                new ImageOutput
                {
                    Image = croppedImage2,
                    Position = new Position(
                        image.Position.X + rectangle2.X,
                        image.Position.Y + rectangle2.Y)
                }
                );
        }

        List<ImageOutput> sort(List<ImageOutput> input)
        {
            var sorted = new List<ImageOutput>();

            int width = 0;
            int height = 0;

            foreach (var img in input)
            {
                width += img.Image.Width;
                height += img.Image.Height;
            }
            width = width / input.Count;
            height = height / input.Count;

            input = input.Where(item => item.Image.Height >= height / 2).ToList();

            var input2 = new List<ImageOutput>();
            foreach (var img in input)
            {
                if (img.Image.Width > width * 2)
                {
                    var split = cutImages(img, false);
                    input2.Add(split.Item1);
                    input2.Add(split.Item2);
                }
                else if (img.Image.Height > height * 2)
                {
                    var split = cutImages(img, true);
                    input2.Add(split.Item1);
                    input2.Add(split.Item2);
                }
                else
                    input2.Add(img);
            }

            sorted = input2.OrderBy(item => item.Position.X).ToList();
            int i = 0;

            var upper = new List<ImageOutput>();
            var lower = new List<ImageOutput>();

            var min = sorted.Max(x => x.Position.Y);
            var first = sorted.First(x => x.Position.Y == min);

            foreach (var img in sorted)
            {
                if (img.Position.Y < first.Position.Y - first.Image.Height)
                    upper.Add(img);
                else
                    lower.Add(img);
            }
            sorted = upper.Concat(lower).ToList();
            foreach (var img in sorted)
            {
                img.Sequence = i++;
            }

            return sorted;
        }
    }
}
