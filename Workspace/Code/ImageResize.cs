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
                targetWidth = (int)(targetHeight);
                targetHeight = (int)(targetHeight * ratio);
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
