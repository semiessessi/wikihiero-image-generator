using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHIG
{
    public static class ImageResize
    {
        public static bool ProcessRotateRight(string inPath, string outPath)
        {
            Image image = Image.FromFile(inPath);
            if (image == null)
            {
                return false;
            }

            Image newImage = new Bitmap(image);

            newImage.RotateFlip(RotateFlipType.Rotate90FlipNone);

            OutputBase64JS.RegisterData(Path.GetFileName(outPath), newImage, !Program.OptiPNG);

            if (Directory.Exists(Path.GetDirectoryName(outPath)) == false)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(outPath));
            }

            newImage.Save(outPath);
            newImage.Dispose();
            return true;
        }

        public static bool ProcessRotateLeft(string inPath, string outPath)
        {
            Image image = Image.FromFile(inPath);
            if (image == null)
            {
                return false;
            }

            Image newImage = new Bitmap(image);

            newImage.RotateFlip(RotateFlipType.Rotate270FlipNone);

            OutputBase64JS.RegisterData(Path.GetFileName(outPath), newImage, !Program.OptiPNG);

            if (Directory.Exists(Path.GetDirectoryName(outPath)) == false)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(outPath));
            }

            newImage.Save(outPath);
            newImage.Dispose();
            return true;
        }

        public static bool ProcessSmallAndTallStack(string[] inputSymbols, string outDirectory, string outPath)
        {
            List<Image> images = new List<Image>();
            foreach (string inPath in inputSymbols)
            {
                string inputPath = Path.Join(outDirectory, "hiero_" + inPath + ".png");
                Image image = Image.FromFile(inputPath);
                if (image == null)
                {
                    return false;
                }
                images.Add(image);
            }

            if (images.Count == 0)
            {
                return false;
            }

            if (images.Count > 2)
            {
                return false;
            }

            // use the target size to guide the image sizing
            // have a top-third and a bottom-two-thirds...
            int width = (int)(Program.TargetSizePixels * 0.33333f);
            int height = Program.TargetSizePixels;

            Image newImage = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(newImage);

            // this assumes the loaf, X1, where it is always flatter than square.
            int smallHeight = (int)((float)width * ((float)images[0].Height / (float)images[0].Width));
            g.DrawImage(images[0], new Rectangle(
                   new Point(0, (width - smallHeight) / 2),
                   new Size(width, smallHeight)));

            // this assumes a tall sign, always taller than the rectangle...
            int tallWidth = (int)((float)2 * width * ((float)images[1].Width / (float)images[1].Height));
            int tallHeight = 2 * width;
            if(2 * images[1].Width > images[1].Height)
            {
                // the prportions need fixing the other way...
                tallWidth = width;
                tallHeight = (int)((float)width * ((float)images[1].Height / (float)images[1].Width));
            }

            g.DrawImage(images[1], new Rectangle(
                   new Point((width - tallWidth) / 2, width + (2 * width - tallHeight) / 2),
                   new Size(tallWidth, tallHeight)));

            images[0].Dispose();
            images[1].Dispose();

            OutputBase64JS.RegisterData(Path.GetFileName(outPath), newImage, !Program.OptiPNG);

            if (Directory.Exists(Path.GetDirectoryName(outPath)) == false)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(outPath));
            }

            newImage.Save(outPath);
            newImage.Dispose();

            return true;
        }

        public static bool ProcessTallAndSmallStack(string[] inputSymbols, string outDirectory, string outPath)
        {
            List<Image> images = new List<Image>();
            foreach (string inPath in inputSymbols)
            {
                string inputPath = Path.Join(outDirectory, "hiero_" + inPath + ".png");
                Image image = Image.FromFile(inputPath);
                if (image == null)
                {
                    return false;
                }
                images.Add(image);
            }

            if (images.Count == 0)
            {
                return false;
            }

            if (images.Count > 2)
            {
                return false;
            }

            // use the target size to guide the image sizing
            // have a bottom-third and a top-two-thirds...
            int width = (int)(Program.TargetSizePixels * 0.33333f);
            int height = Program.TargetSizePixels;

            Image newImage = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(newImage);

            // this assumes the loaf, X1, where it is always flatter than square.
            int smallHeight = (int)((float)width * ((float)images[1].Height / (float)images[1].Width));
            g.DrawImage(images[1], new Rectangle(
                   new Point(0, 2 * width + (width - smallHeight) / 2),
                   new Size(width, smallHeight)));

            // this assumes a tall sign, always taller than the rectangle...
            int tallWidth = (int)((float)2 * width * ((float)images[0].Width / (float)images[0].Height));
            int tallHeight = 2 * width;
            if (2 * images[0].Width > images[0].Height)
            {
                // the prportions need fixing the other way...
                tallWidth = width;
                tallHeight = (int)((float)width * ((float)images[0].Height / (float)images[0].Width));
            }

            g.DrawImage(images[0], new Rectangle(
                   new Point((width - tallWidth) / 2, (2 * width - tallHeight) / 2),
                   new Size(tallWidth, tallHeight)));

            images[0].Dispose();
            images[1].Dispose();

            OutputBase64JS.RegisterData(Path.GetFileName(outPath), newImage, !Program.OptiPNG);

            if (Directory.Exists(Path.GetDirectoryName(outPath)) == false)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(outPath));
            }

            newImage.Save(outPath);
            newImage.Dispose();

            return true;
        }

        public static bool ProcessBottomLeftTopRight(string[] inputSymbols, string outDirectory, string outPath)
        {
            List<Image> images = new List<Image>();
            foreach (string inPath in inputSymbols)
            {
                string inputPath = Path.Join(outDirectory, "hiero_" + inPath + ".png");
                Image image = Image.FromFile(inputPath);
                if (image == null)
                {
                    return false;
                }
                images.Add(image);
            }

            if (images.Count == 0)
            {
                return false;
            }

            if (images.Count > 2)
            {
                return false;
            }

            int width = Program.TargetSizePixels;
            int height = Program.TargetSizePixels;

            Image newImage = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(newImage);

            // this assumes the loaf, X1, where it is always flatter than square.
            int smallWidth = Program.TargetSizePixels >> 1;
            int smallHeight = (int)((float)smallWidth * ((float)images[1].Height / (float)images[1].Width));
            g.DrawImage(images[1], new Rectangle(
                   new Point(width - smallWidth, 0),
                   new Size(smallWidth, smallHeight)));

            // this assumes a tall sign...
            int tallWidth = (int)((float)width * ((float)images[0].Width / (float)images[0].Height));
            int tallHeight = width;
            if (images[0].Width > images[0].Height)
            {
                // the proportions need fixing the other way...
                tallWidth = width;
                tallHeight = (int)((float)width * ((float)images[0].Height / (float)images[0].Width));
            }

            g.DrawImage(images[0], new Rectangle(
                   new Point(0, height - tallHeight),
                   new Size(tallWidth, tallHeight)));

            images[0].Dispose();
            images[1].Dispose();

            OutputBase64JS.RegisterData(Path.GetFileName(outPath), newImage, !Program.OptiPNG);

            if (Directory.Exists(Path.GetDirectoryName(outPath)) == false)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(outPath));
            }

            newImage.Save(outPath);
            newImage.Dispose();

            return true;
        }

        public static bool ProcessSimpleTopRightBig(string[] inputSymbols, string outDirectory, string outPath)
        {
            List<Image> images = new List<Image>();
            foreach (string inPath in inputSymbols)
            {
                string inputPath = Path.Join(outDirectory, "hiero_" + inPath + ".png");
                Image image = Image.FromFile(inputPath);
                if (image == null)
                {
                    return false;
                }
                images.Add(image);
            }

            if (images.Count == 0)
            {
                return false;
            }

            if (images.Count > 2)
            {
                return false;
            }

            int width = Program.TargetSizePixels;
            int height = Program.TargetSizePixels;

            Image newImage = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(newImage);

            // this assumes a flat sign...
            int smallWidth = (width * 3) / 4;
            if ((inputSymbols[1] == "X1")
                 || (inputSymbols[1] == "N5"))
            {
                smallWidth = width >> 1;
            }
            
            int smallHeight = (int)((float)smallWidth * ((float)images[1].Height / (float)images[1].Width));
            if (images[1].Width < images[1].Height)
            {
                // the proportions need fixing the other way...
                smallHeight = smallWidth;
                smallWidth = (int)((float)smallHeight * ((float)images[1].Height / (float)images[1].Width));
            }

            g.DrawImage(images[1], new Rectangle(
                new Point(0, height - smallHeight),
                new Size(smallWidth, smallHeight)));

            // this assumes a tall sign...
            int bigWidth = (int)((float)width * ((float)images[0].Width / (float)images[0].Height));
            int bigHeight = width;
            if (images[0].Width > images[0].Height)
            {
                // the proportions need fixing the other way...
                bigWidth = width;
                bigHeight = (int)((float)width * ((float)images[0].Height / (float)images[0].Width));
            }

            g.DrawImage(images[0], new Rectangle(
                   new Point(width - bigWidth, 0),
                   new Size(bigWidth, bigHeight)));

            images[0].Dispose();
            images[1].Dispose();

            OutputBase64JS.RegisterData(Path.GetFileName(outPath), newImage, !Program.OptiPNG);

            if (Directory.Exists(Path.GetDirectoryName(outPath)) == false)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(outPath));
            }

            newImage.Save(outPath);
            newImage.Dispose();

            return true;
        }

        public static bool ProcessStack(string[] inputSymbols, string outDirectory, string outPath)
        {
            List<Image> images = new List<Image>();
            int z91count = 0;
            foreach(string inPath in inputSymbols)
            {
                if(inPath == "Z91")
                {
                    ++z91count;
                }
                string inputPath = Path.Join(outDirectory, "hiero_" + inPath + ".png");
                Image image = Image.FromFile(inputPath);
                if (image == null)
                {
                    return false;
                }
                images.Add(image);
            }

            if(images.Count == 0)
            {
                return false;
            }

            int heightCount = 0;
            foreach(string symbol in inputSymbols)
            {
                if(OutputImages.LargeInStack.Contains(symbol))
                {
                    ++heightCount;
                }
                ++heightCount;
            }

            // use the target size to guide the image sizing
            int width = Program.TargetSizePixels;
            // make stacks of z91 proportioned correctly..
            switch(z91count)
            {
                case 3:
                {
                    width = (int)(width * 0.667f);
                    break;
                }
                case 4:
                {
                    width = (int)(width * 0.5f);
                    break;
                }
                case 5:
                {
                    width = (int)(width * 0.333f);
                    break;
                }
                default:
                    break;
            }
            int height = Program.TargetSizePixels;
            int individualHeight = height / heightCount;

            Image newImage = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(newImage);
            // for each image, fit it into the rectangle as appropriate.
            int y = 0;
            int index = 0;
            foreach (Image image in images)
            {
                int targetHeight = individualHeight;
                int targetWidth = (int)((float)targetHeight * (float)image.Width / (float)image.Height);
                if ((inputSymbols[index] == "X1")
                    || (inputSymbols[index] == "Q3"))
                {
                    float ratio = (float)image.Height / (float)image.Width;
                    targetWidth = width >> 1;
                    targetHeight = (int)(targetWidth * ratio);
                }
                else if (inputSymbols[index] == "D36") // a needs a little scaling.
                {
                    float ratio = (float)image.Height / (float)image.Width;
                    targetWidth = (int)((float)width * 0.9f);
                    targetHeight = (int)(targetWidth * ratio);
                }
                else if (inputSymbols[index] == "E34") // a needs a little scaling.
                {
                    float ratio = (float)image.Height / (float)image.Width;
                    targetWidth = (int)((float)width * 0.45f);
                    targetHeight = (int)(targetWidth * ratio);
                }
                else if (inputSymbols[index] == "F29") // a needs a little scaling.
                {
                    float ratio = (float)image.Height / (float)image.Width;
                    targetWidth = (int)((float)width * 0.3f);
                    targetHeight = (int)(targetWidth * ratio);
                }
                else if (inputSymbols[index] == "G36") // a needs a little scaling.
                {
                    float ratio = (float)image.Height / (float)image.Width;
                    targetWidth = (int)((float)width * 0.35f);
                    targetHeight = (int)(targetWidth * ratio);
                }
                else if (inputSymbols[index] == "Aa1") // x needs a little scaling.
                {
                    float ratio = (float)image.Height / (float)image.Width;
                    targetWidth = (int)((float)width * 0.4f);
                    targetHeight = (int)(targetWidth * ratio);
                }
                else if (image.Width > image.Height)
                {
                    float ratio = (float)image.Height / (float)image.Width;
                    targetWidth = width;
                    targetHeight = (int)(targetWidth * ratio);
                }

                int adjust = (individualHeight - targetHeight) / 2;
                if (OutputImages.LargeInStack.Contains(inputSymbols[index]))
                {
                    targetWidth <<= 1;
                    targetHeight <<= 1;
                    adjust = (2 * individualHeight - targetHeight) / 2;
                }

                if((index > 0) && (inputSymbols[index - 1] == "D36"))
                {
                    // add a little space after a because it is a pain.
                    adjust += (int)(Program.TargetSizePixels / 40.0);
                }

                else if ((index > 0)
                    && (inputSymbols[index - 1] == "N35")
                    && (inputSymbols[index] == "Aa1"))
                {
                    // remove before final x here...
                    adjust -= (int)(2.0 * Program.TargetSizePixels / 40.0);
                }
                else if ((index > 0)
                    && (inputSymbols[index - 1] == "Aa1")
                    && (inputSymbols[index] == "D21"))
                {
                    // add space after x for r
                    adjust += (int)(2.0 * Program.TargetSizePixels / 40.0);
                }
                else if ((index > 0)
                    && (inputSymbols[index - 1] == "Q3")
                    && (inputSymbols[index] == "O34"))
                {
                    // add space after p for z
                    adjust += (int)(4.0 * Program.TargetSizePixels / 40.0);
                }

                g.DrawImage(image, new Rectangle(
                    new Point((width - targetWidth) / 2, y + adjust),
                    new Size(targetWidth, targetHeight)));
                y += individualHeight;
                if (OutputImages.LargeInStack.Contains(inputSymbols[index]))
                {
                    y += individualHeight;
                }
                image.Dispose();

                ++index;
            }

            OutputBase64JS.RegisterData(Path.GetFileName(outPath), newImage, !Program.OptiPNG);

            if (Directory.Exists(Path.GetDirectoryName(outPath)) == false)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(outPath));
            }

            newImage.Save(outPath);
            newImage.Dispose();

            return true;
            // 
        }

        public static bool ProcessImage(string inPath, string outPath)
        {
            Image image = Image.FromFile(inPath);
            if(image == null)
            {
                return false;
            }

            int width = image.Width;
            int height = image.Height;
            int targetHeight = Program.TargetSizePixels;
            int targetWidth = (int)((float)targetHeight * (float)width / (float)height);
            if (width > height)
            {
                float ratio = (float)height / (float)width;
                targetWidth = targetHeight;
                targetHeight = (int)(targetWidth * ratio);
            }

            Image newImage = new Bitmap(targetWidth, targetHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            if(newImage == null)
            {
                image.Dispose();
                return false;
            }

            Graphics g = Graphics.FromImage(newImage);
            g.DrawImage(image, new Rectangle(new Point(0, 0), new Size(targetWidth, targetHeight)));
            image.Dispose();

            OutputBase64JS.RegisterData(Path.GetFileName(outPath), newImage, !Program.OptiPNG);

            if (Directory.Exists(Path.GetDirectoryName(outPath)) == false)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(outPath));
            }

            newImage.Save(outPath);
            newImage.Dispose();
            

            return true;
        }
    }
}
