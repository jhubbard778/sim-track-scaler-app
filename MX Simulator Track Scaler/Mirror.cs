using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MX_Simulator_Track_Scaler
{
    internal class Mirror
    {
        public static double Get_Mirrored_Angle(double angle)
        {
            bool locked_vertical_axis = angle < (-2 * Math.PI);

            angle *= -1;
            angle %= (2 * Math.PI);

            while (locked_vertical_axis && angle > (-2 * Math.PI))
            {
                angle -= (2 * Math.PI);
            }

            return angle;
        }

        public static decimal Get_Mirrored_XCoord(decimal old_x)
        {
            decimal scale = TrackScalerForm.ByFactorMethod ? TrackScalerForm.terrain_scale * TrackScalerForm.scalar_input : TrackScalerForm.scalar_input;
            decimal center_coord = ((TrackScalerForm.terrain_dimension - 1) * scale) / 2;

            decimal new_x = ((old_x - center_coord) * -1) + center_coord;
            return new_x;
        }

        public static void Mirror_Lighting(string dir_to_move)
        {
            if (File.Exists(TrackScalerForm.folderPath + "\\lighting"))
            {
                string tempfile = Path.GetTempFileName();
                StreamWriter temp = File.CreateText(tempfile);
                StreamReader lighting = File.OpenText(TrackScalerForm.folderPath + "\\lighting");

                string sun_vector_line = lighting.ReadLine();
                string[] line_pieces = sun_vector_line.Split('[');
                string[] vector = line_pieces[1].Trim().Split(' ');
                Decimal.TryParse(vector[0], out decimal x_vector);
                x_vector *= -1;
                vector[0] = x_vector.ToString();
                line_pieces[1] = "[ " + string.Join(" ", vector);
                sun_vector_line = string.Join("", line_pieces);

                temp.WriteLine(sun_vector_line);

                string other_lines = lighting.ReadToEnd();
                temp.Write(other_lines);

                temp.Close();
                lighting.Close();
                File.Move(TrackScalerForm.folderPath + "\\lighting", dir_to_move + "\\lighting");
                File.Move(tempfile, TrackScalerForm.folderPath + "\\lighting");
            }
        }

        public static int Mirror_Images(string dir_to_move)
        {
            // Mirror_Image("terrain.png", dir_to_move);
            Mirror_Image("map.png", dir_to_move);
            // Mirror_Image("shading.ppm", dir_to_move);
            // Mirror_Image("shadingx2.ppm", dir_to_move);
            // Mirror_Image("shadows.pgm", dir_to_move);

            Bitmap bm = new Bitmap(TrackScalerForm.folderPath + "\\terrain.png");
            int terrain_width = bm.Width;

            bm.Dispose();
            return terrain_width;
        }

        private static void Mirror_Image(string file, string dir)
        {
            string filename = TrackScalerForm.folderPath + '\\' + file;

            if (!File.Exists(filename)) return;

            ImageFormat imageFormat = Path.GetExtension(file) == ".png" ? ImageFormat.Png : ImageFormat.Bmp;
            string prevdir = Path.Combine(dir, file);

            File.Move(filename, prevdir);

            Bitmap new_image = new Bitmap(prevdir);

            /*if (file == "terrain.png")
            {
                Bitmap terrain = ImageFunctions.ConvertTo16bppGrayscale(new_image);
                terrain.RotateFlip(RotateFlipType.RotateNoneFlipX);
                ImageFunctions.SaveBmp(terrain, filename);
                terrain.Dispose();
                return;
            }*/

            // Mirror the image
            new_image.RotateFlip(RotateFlipType.RotateNoneFlipX);
            new_image.Save(filename, imageFormat);
            new_image.Dispose();

        }
    }
}
