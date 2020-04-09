using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tinyOCREngine
{
    public class Alphabet
    {
        /*** Go Down ***/
        static int goDownWhite(Bitmap image, int x, int y)
        {
            var pixel = image.GetPixel(x, y);
            while (!Isolator.isDarkA(pixel))
                pixel = image.GetPixel(x, ++y);
            return y;
        }

        static int goDownBlack(Bitmap image, int x, int y)
        {
            var pixel = image.GetPixel(x, y);
            while (Isolator.isDarkA(pixel))
                pixel = image.GetPixel(x, ++y);
            return y;
        }

        static int goDownWhite(Bitmap image, int x, int y, out bool isCalled)
        {
            isCalled = false;
            var pixel = image.GetPixel(x, y);
            while (!Isolator.isDarkA(pixel))
            {
                isCalled = true;
                pixel = image.GetPixel(x, ++y);
            }
            return y;
        }

        static int goDownBlack(Bitmap image, int x, int y, out bool isCalled)
        {
            isCalled = false;
            var pixel = image.GetPixel(x, y);
            while (Isolator.isDarkA(pixel))
            {
                isCalled = true;
                pixel = image.GetPixel(x, ++y);
            }
            return y;
        }

        /*** Go Up ***/
        static int goUpWhite(Bitmap image, int x, int y)
        {
            var pixel = image.GetPixel(x, y);
            while (!Isolator.isDarkA(pixel))
                pixel = image.GetPixel(x, --y);
            return y;
        }

        static int goUpBlack(Bitmap image, int x, int y)
        {
            var pixel = image.GetPixel(x, y);
            while (Isolator.isDarkA(pixel))
                pixel = image.GetPixel(x, --y);
            return y;
        }

        static int goUpWhite(Bitmap image, int x, int y, out bool isCalled)
        {
            isCalled = false;
            var pixel = image.GetPixel(x, y);
            while (!Isolator.isDarkA(pixel))
            {
                isCalled = true;
                pixel = image.GetPixel(x, --y);
            }
            return y;
        }

        static int goUpBlack(Bitmap image, int x, int y, out bool isCalled)
        {
            isCalled = false;
            var pixel = image.GetPixel(x, y);
            while (Isolator.isDarkA(pixel))
            {
                isCalled = true;
                pixel = image.GetPixel(x, --y);
            }
            return y;
        }

        /*** Go Left ***/
        static int goLeftWhite(Bitmap image, int x, int y)
        {
            var pixel = image.GetPixel(x, y);
            while (!Isolator.isDarkA(pixel))
                pixel = image.GetPixel(--x, y);
            return x;
        }

        static int goLeftBlack(Bitmap image, int x, int y)
        {
            var pixel = image.GetPixel(x, y);
            while (Isolator.isDarkA(pixel))
                pixel = image.GetPixel(--x, y);
            return x;
        }

        static int goLeftWhite(Bitmap image, int x, int y, out bool isCalled)
        {
            isCalled = false;
            var pixel = image.GetPixel(x, y);
            while (!Isolator.isDarkA(pixel))
            {
                isCalled = true;
                pixel = image.GetPixel(--x, y);
            }
            return x;
        }

        static int goLeftBlack(Bitmap image, int x, int y, out bool isCalled)
        {
            isCalled = false;
            var pixel = image.GetPixel(x, y);
            while (Isolator.isDarkA(pixel))
            {
                isCalled = true;
                pixel = image.GetPixel(--x, y);
            }
            return x;
        }

        /*** Go Right ***/
        static int goRightWhite(Bitmap image, int x, int y)
        {
            var pixel = image.GetPixel(x, y);
            while (!Isolator.isDarkA(pixel))
                pixel = image.GetPixel(++x, y);
            return x;
        }

        static int goRightBlack(Bitmap image, int x, int y)
        {
            var pixel = image.GetPixel(x, y);
            while (Isolator.isDarkA(pixel))
                pixel = image.GetPixel(++x, y);
            return x;
        }

        static int goRightWhite(Bitmap image, int x, int y, out bool isCalled)
        {
            isCalled = false;
            var pixel = image.GetPixel(x, y);
            while (!Isolator.isDarkA(pixel))
            {
                isCalled = true;
                pixel = image.GetPixel(++x, y);
            }
            return x;
        }

        static int goRightBlack(Bitmap image, int x, int y, out bool isCalled)
        {
            isCalled = false;
            var pixel = image.GetPixel(x, y);
            while (Isolator.isDarkA(pixel))
            {
                isCalled = true;
                pixel = image.GetPixel(++x, y);
            }
            return x;
        }

        public static bool isA(Bitmap image)
        {
            try
            {
                var half = image.Width / 2;
                int i = image.Height - 1;

                i = goUpWhite(image, half, i);
                i = goUpBlack(image, half, --i);

                int post = i;

                i = goUpWhite(image, half, i);
                var j = post - i;

                int middle = i + (j / 2);

                i = goUpBlack(image, half, --i);
                i = goUpWhite(image, half, --i);

                bool b = false;
                var pixel = image.GetPixel(half, middle);
                b = Isolator.isDarkA(image, half, --i);

                pixel = image.GetPixel(half, middle);
                if (b && !Isolator.isDarkA(pixel))
                {
                    Isolator.scanUnsafe(image, half, middle);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }


        public static bool isB(Bitmap image)
        {
            try
            {
                var half = image.Width / 2;
                int i = image.Height - 1;

                i = goUpWhite(image, half, i);
                i = goUpBlack(image, half, --i);

                int post = i;

                i = goUpWhite(image, half, i);
                var j = post - i;

                int middle = i + (j / 2);

                i = goUpBlack(image, half, --i);

                var t = i / 2;

                bool b = true;
                i--;


                var pixel = image.GetPixel(half, middle);
                if (b && !Isolator.isDarkA(pixel))
                {
                    Isolator.scanUnsafe(image, half, middle);

                    j = image.Width - 1;
                    b = false;
                    j = goLeftWhite(image, j, t, out b);

                    if (j < half && b)
                        return true;

                    return false;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public static bool isC(Bitmap image)
        {
            try
            {
                var half = image.Width / 2;
                int i = image.Height - 1;

                i = goUpWhite(image, half, i);
                i = goUpBlack(image, half, --i);

                int post = i;

                i = goUpWhite(image, half, i);
                var j = post - i;

                int middle = i + (j / 2);

                bool b = false;
                try
                {
                    i = goUpBlack(image, half, --i);
                    i = goUpWhite(image, half, --i);
                }
                catch
                {
                    b = true;
                }

                if (!b)
                    return false;

                i = goDownWhite(image, half, 0);

                var t = i / 2;
                var tt = i - 1;

                b = true;
                i--;

                var pixel = image.GetPixel(half, middle);
                if (b && !Isolator.isDarkA(pixel))
                {
                    j = 1;
                    b = false;
                    j = goLeftWhite(image, half, middle, out b);

                    if (j < half && b)
                    {
                        try
                        {
                            j = goRightWhite(image, half, middle, out b);
                        }
                        catch
                        {
                            if (tt >= 1)
                            {
                                var a1 = goLeftWhite(image, image.Width - 1, tt);
                                var a2 = goRightWhite(image, 1, middle);
                                if (a1 < half && a2 > half)
                                    return true;
                                else
                                    return false;
                            }
                            return true;
                        }
                    }

                    return false;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public static bool isD(Bitmap image)
        {
            try
            {
                var half = image.Width / 2;
                int i = image.Height - 1;

                i = goUpWhite(image, half, i);
                i = goUpBlack(image, half, --i);

                int post = i;

                i = goUpWhite(image, half, i);
                var j = post - i;

                int middle = i + (j / 2);

                i = goUpBlack(image, half, --i);

                var t = i / 2;
                bool b = true;
                i--;

                var pixel = image.GetPixel(half, middle);
                if (b && !Isolator.isDarkA(pixel))
                {
                    Isolator.scanUnsafe(image, half, middle);

                    j = 1;
                    b = false;
                    j = goRightWhite(image, j, t, out b);

                    if (j > half && b)
                        return true;

                    return false;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public static bool isE(Bitmap image)
        {
            try
            {
                var half = image.Width / 2;
                int i = image.Height - 1;

                i = goUpWhite(image, half, i);
                i = goUpBlack(image, half, --i);

                int post = i;

                i = goUpWhite(image, half, i);
                var j = post - i;

                int middle1 = i + (j / 2);

                var open = Isolator.OpenToSite(image, half, middle1);
                if (open != 2)
                    return false;

                i = goUpBlack(image, half, --i);

                var middle = i;

                i = goUpWhite(image, half, --i);

                middle = middle - i / 2;

                bool b = false;
                var pixel = image.GetPixel(half, i);
                b = Isolator.isDarkA(pixel);

                pixel = image.GetPixel(half, middle);
                if (b && !Isolator.isDarkA(pixel))
                {
                    Isolator.scanUnsafe(image, half, middle);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public static bool isF(Bitmap image)
        { // TODO F
            return false;
        }

        public static bool isG(Bitmap image)
        {
            try
            {
                var half = image.Width / 2;
                int i = image.Height - 1;

                i = goUpWhite(image, half, i);
                i = goUpBlack(image, half, --i);

                int post = i;

                i = goUpWhite(image, half, i);
                var j = post - i;

                int middle1 = i + (j / 2);

                var open = Isolator.OpenToSite(image, half, middle1);
                if (open != 4)
                    return false;

                i = goUpBlack(image, half, --i);

                var middle = i;

                i = goUpWhite(image, half, --i);

                middle = middle - i / 2;

                bool b = false;
                goUpBlack(image, half, --i, out b);

                var pixel = image.GetPixel(half, middle);
                if (b && !Isolator.isDarkA(pixel))
                {
                    Isolator.scanUnsafe(image, half, middle);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public static bool isH(Bitmap image)
        {        
            var xMiddle = image.Width / 2;
            var yMiddle = image.Height / 2;

            try
            {
                int i = 1;
                bool b = false;
                i = goRightWhite(image, i, yMiddle);
                i = goRightBlack(image, ++i, yMiddle, out b);
                if (!b)
                    return false;
                i = goRightWhite(image, ++i, yMiddle, out b);
                if (!b)
                    return false;

                if (!Isolator.isDarkA(image, ++i, yMiddle))
                    return false;

                i = goDownWhite(image, xMiddle, 1);

                int j = image.Width - 1;
                j = goLeftWhite(image, j, i - 2);

                if (j > xMiddle)
                    return false;

                var open = Isolator.OpenToSite(image, xMiddle, yMiddle);
                if (open == 3)
                    return true;

                return false;
            }
            catch
            {
                return false;
            }
        }

        public static bool isI(Bitmap image)
        { // TODO I
            return false;
        }

        public static bool isJ(Bitmap image)
        { // TODO J
            return false;
        }

        public static bool isK(Bitmap image)
        {
            var xMiddle = image.Width / 2;
            var yMiddle = image.Height / 2;

            try
            {
                int i = 1;
                bool b = false;
                i = goRightWhite(image, i, yMiddle);
                i = goRightBlack(image, ++i, yMiddle, out b);
                if (!b)
                    return false;
                i = goRightWhite(image, ++i, yMiddle, out b);
                if (!b)
                    return false;

                if (!Isolator.isDarkA(image, ++i, yMiddle))
                    return false;

                i = goDownWhite(image, xMiddle, 1);

                int j = image.Width - 1;
                j = goLeftWhite(image, j, i - 2);
                j = goLeftBlack(image, j, i - 2);
                j = goLeftWhite(image, j, i - 2);

                if (j > xMiddle)
                    return false;

                var open = Isolator.OpenToSite(image, xMiddle, yMiddle);
                if (open == 3)
                    return true;

                return false;
            }
            catch
            {
                return false;
            }
        }

        public static bool isL(Bitmap image)
        { // TODO L
            return false;
        }

        public static bool isM(Bitmap image)
        { // TODO M
            return false;
        }

        public static bool isN(Bitmap image)
        {
            var xMiddle = image.Width / 2;
            var yMiddle = image.Height / 2;

            try
            {
                int i = 1;
                bool b = false;
                i = goRightWhite(image, i, yMiddle);
                i = goRightBlack(image, ++i, yMiddle, out b);
                if (!b)
                    return false;
                i = goRightWhite(image, ++i, yMiddle, out b);
                if (!b)
                    return false;
                //i = goRightBlack(image, ++i, yMiddle, out b);
                if (!Isolator.isDarkA(image, ++i, yMiddle))
                    return false;

                var open = Isolator.OpenToSite(image, xMiddle, yMiddle);
                if (open == 3)
                    return true;

                return false;
            }
            catch
            {
                return false;
            }
        }

        public static bool isO(Bitmap image)
        {
            var xMiddle = image.Width / 2;
            var yMiddle = image.Height / 2;

            try
            {
                int i = image.Height;
                bool b = false;

                i = goUpWhite(image, xMiddle, --i);
                i = goUpBlack(image, xMiddle, --i, out b);
                if (!b)
                    return false;
                i = goUpWhite(image, xMiddle, --i, out b);
                if (!b)
                    return false;
                i = goUpBlack(image, xMiddle, --i, out b);
                if (!b)
                    return false;

                Isolator.scanUnsafe(image, xMiddle, yMiddle);
                return true;
            }
            catch
            {
                return false;
            }

            return false;
        }

        public static bool isP(Bitmap image)
        {
            try
            {
                var half = image.Width / 2;
                int i = image.Height - 1;

                i = goUpWhite(image, half, i);

                int post = i / 2;

                i = goUpBlack(image, half, --i);

                int middle = i - 1;

                i = goUpWhite(image, half, i);
                var j = post - i;


                i = goUpBlack(image, half, --i);

                var t = i / 2;

                bool b = true;
                i--;

                var pixel = image.GetPixel(half, middle);
                if (b && !Isolator.isDarkA(pixel))
                {
                    Isolator.scanUnsafe(image, half, middle);

                    j = image.Width - 1;
                    b = false;
                    j = goLeftWhite(image, j, t, out b);

                    if (j < half && b)
                        return true;

                    return false;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public static bool isQ(Bitmap image)
        {
            try
            {
                var half = image.Width / 2;
                int i = image.Height - 1;

                i = goUpWhite(image, half, i);

                int post = i / 2;

                i = goUpBlack(image, half, --i);

                int middle = i - 1;

                i = goUpWhite(image, half, i);
                var j = post - i;


                i = goUpBlack(image, half, --i);

                var t = i / 2;

                bool b = true;
                i--;

                var pixel = image.GetPixel(half, middle);
                if (b && !Isolator.isDarkA(pixel))
                {
                    Isolator.scanUnsafe(image, half, middle);

                    j = image.Width - 1;
                    b = false;
                    j = goLeftWhite(image, j, t, out b);

                    if (j > half && b)
                        return true;

                    return false;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public static bool isR(Bitmap image)
        { // TODO R
            return false;
        }

        public static bool isS(Bitmap image)
        { // TODO S
            return false;
        }

        public static bool isT(Bitmap image)
        { // TODO T
            return false;
        }

        public static bool isU(Bitmap image)
        {
            var xMiddle = image.Width / 2;
            var yMiddle = image.Height / 2;

            try
            {
                int i = 1;
                bool b = false;
                i = goRightWhite(image, i, yMiddle);
                i = goRightBlack(image, ++i, yMiddle, out b);
                if (!b)
                    return false;
                i = goRightWhite(image, ++i, yMiddle, out b);
                if (!b)
                    return false;
                //i = goRightBlack(image, ++i, yMiddle, out b);
                if (!Isolator.isDarkA(image, ++i, yMiddle))
                    return false;

                var open = Isolator.OpenToSite(image, xMiddle, yMiddle);
                if (open == 1)
                    return true;

                return false;
            }
            catch
            {
                return false;
            }
        }

        public static bool isV(Bitmap image)
        { // TODO V
            return false;
        }

        public static bool isW(Bitmap image)
        { // TODO W
            return false;
        }

        public static bool isX(Bitmap image)
        { // TODO X
            return false;
        }

        public static bool isY(Bitmap image)
        { // TODO Y
            return false;
        }

        public static bool isZ(Bitmap image)
        { // TODO Z
            return false;
        }
    }
}
