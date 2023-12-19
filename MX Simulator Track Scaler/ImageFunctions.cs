using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MX_Simulator_Track_Scaler
{
    internal class ImageFunctions
    {
        public static Bitmap ReadBitmapFromPPM(string file)
        {
            var reader = new BinaryReader(new FileStream(file, FileMode.Open));

            // Magic Identifier Line
            if (reader.ReadChar() != 'P' || reader.ReadChar() != '6' || reader.ReadChar() != '5')
                return null;
            
            reader.ReadChar(); //Eat newline
            string widths = "", heights = "";
            
            // Width Line
            char temp;
            while ((temp = reader.ReadChar()) != ' ')
            {
                widths += temp;
            }
            
            // Height Line
            while ((temp = reader.ReadChar()) >= '0' && temp <= '9')
            {
                heights += temp;
            }
            
            // 255
            if (reader.ReadChar() != '2' || reader.ReadChar() != '5')
            {
                return null;
            }
                
            reader.ReadChar(); //Eat the last newline
            int width = int.Parse(widths);
            int height = int.Parse(heights);

            Bitmap bitmap = new Bitmap(width, height);
            //Read in the pixels
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // TODO: CHANGE FOR PGM
                    // pgm files only store grayscale information, that is one value per pixel
                    // instead of 3 (r,g,b) for ppm
                    bitmap.SetPixel(x, y, System.Drawing.Color.FromArgb(reader.ReadByte(), reader.ReadByte(), reader.ReadByte()));
                }
            }
            return bitmap;
        }

        public static void WriteBitmapToPPM(string file, Bitmap bitmap)
        {
            string magicIdentifier = Path.GetExtension(file) == ".pgm" ? "P5" : "P6";

            //Use a streamwriter to write the text part of the encoding
            var writer = new StreamWriter(file);
            writer.WriteLine(magicIdentifier);
            writer.WriteLine($"{bitmap.Width}  {bitmap.Height}");
            writer.WriteLine("255");
            writer.Close();
            //Switch to a binary writer to write the data
            var writerB = new BinaryWriter(new FileStream(file, FileMode.Append));
            for (int x = 0; x < bitmap.Height; x++)
            {
                for (int y = 0; y < bitmap.Width; y++)
                {
                    System.Drawing.Color color = bitmap.GetPixel(y, x);
                    writerB.Write(color.R);
                    writerB.Write(color.G);
                    writerB.Write(color.B);
                }
            }

            writerB.Close();
        }

        public static Bitmap ConvertTo16bppGrayscale(Bitmap originalBitmap)
        {
            // Create a new bitmap with the same size and PixelFormat of 16bppGrayscale
            Bitmap newBitmap = new Bitmap(originalBitmap.Width, originalBitmap.Height, System.Drawing.Imaging.PixelFormat.Format16bppGrayScale);

            // Lock the bits of the original and new bitmaps
            BitmapData originalData = originalBitmap.LockBits(new Rectangle(0, 0, originalBitmap.Width, originalBitmap.Height), ImageLockMode.ReadOnly, originalBitmap.PixelFormat);
            BitmapData newData = newBitmap.LockBits(new Rectangle(0, 0, newBitmap.Width, newBitmap.Height), ImageLockMode.WriteOnly, newBitmap.PixelFormat);

            // Copy pixel data from ARGB32 to 16bpp grayscale
            CopyArgb32To16bppGrayScale(originalData.Scan0, originalData.Stride, newData.Scan0, newData.Stride, originalData.Width, originalData.Height);

            originalBitmap.UnlockBits(originalData);
            newBitmap.UnlockBits(newData);

            return newBitmap;
        }

        private static void CopyArgb32To16bppGrayScale(IntPtr src, int srcStride, IntPtr dest, int destStride, int width, int height)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // Get the ARGB32 pixel
                    int argb32Pixel = System.Runtime.InteropServices.Marshal.ReadInt32(src + y * srcStride + x * 4);

                    // Extract the grayscale value (assuming R, G, and B are the same in ARGB32)
                    int grayscaleValue = (int)((argb32Pixel & 0xFF) * 0.3 + ((argb32Pixel >> 8) & 0xFF) * 0.59 + ((argb32Pixel >> 16) & 0xFF) * 0.11);

                    // Scale the grayscale value to fit into 16 bits
                    short scaledGrayscaleValue = (short)(grayscaleValue * (65535.0 / 255.0));

                    // Write the scaled grayscale value to the 16bpp grayscale bitmap
                    System.Runtime.InteropServices.Marshal.WriteInt16(dest + y * destStride + x * 2, scaledGrayscaleValue);
                }
            }
        }

        public static void SaveBmp(Bitmap bmp, string path)
        {
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);

            BitmapData bitmapData = bmp.LockBits(rect, ImageLockMode.ReadOnly, bmp.PixelFormat);

            var pixelFormats = ConvertBmpPixelFormat(bmp.PixelFormat);

            BitmapSource source = BitmapSource.Create(bmp.Width,
                                                      bmp.Height,
                                                      bmp.HorizontalResolution,
                                                      bmp.VerticalResolution,
                                                      pixelFormats,
                                                      null,
                                                      bitmapData.Scan0,
                                                      bitmapData.Stride * bmp.Height,
                                                      bitmapData.Stride);

            bmp.UnlockBits(bitmapData);

            FileStream stream = new FileStream(path, FileMode.Create);

            PngBitmapEncoder encoder = new PngBitmapEncoder();

            encoder.Frames.Add(BitmapFrame.Create(source));
            encoder.Save(stream);

            stream.Close();
        }

        private static System.Windows.Media.PixelFormat ConvertBmpPixelFormat(System.Drawing.Imaging.PixelFormat pixelformat)
        {
            System.Windows.Media.PixelFormat pixelFormats = System.Windows.Media.PixelFormats.Default;

            switch (pixelformat)
            {
                case System.Drawing.Imaging.PixelFormat.Format32bppArgb:
                    pixelFormats = PixelFormats.Bgr32;
                    break;

                case System.Drawing.Imaging.PixelFormat.Format8bppIndexed:
                    pixelFormats = PixelFormats.Gray8;
                    break;

                case System.Drawing.Imaging.PixelFormat.Format16bppGrayScale:
                    pixelFormats = PixelFormats.Gray16;
                    break;
            }

            return pixelFormats;
        }

    }
}
