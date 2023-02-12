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
        public static bool ProcessStack(string[] inputSymbols, string outDirectory, string outPath)
        {
            List<Image> images = new List<Image>();
            foreach(string inPath in inputSymbols)
            {
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

            OutputBase64JS.RegisterData(Path.GetFileName(outPath), newImage);

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

            OutputBase64JS.RegisterData(Path.GetFileName(outPath), newImage);

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
