using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MX_Simulator_Track_Scaler
{
    internal class Helpers
    {
        private static readonly string[] validSeqAnimationNames = { "ANIM1" };
        public static int totalFileLines = 0;

        public static decimal[] GetNewCoords(decimal x, decimal y, decimal z)
        {
            decimal[] coords = new decimal[3];
            coords[0] = Math.Round(x * ScaleInfo.multiplier, 6);
            coords[1] = y;
            if (ScaleInfo.scaleVerticalValues)
            {
                coords[1] = Math.Round(y * ScaleInfo.multiplier, 6);
            }
            coords[2] = Math.Round(z * ScaleInfo.multiplier, 6);
            return coords;
        }

        public static void CleanupFilesAndMoveTempFile(string filename, StreamReader fileToClose, StreamWriter tempFile, string tempFilePath)
        {
            // Close the files before moving
            fileToClose.Close();
            tempFile.Close();

            MoveUnscaledFileToNewDirectory(filename);
            File.Move(tempFilePath, DirectoryInfo.trackFolderPath + '\\' + filename);
        }

        public static void MoveUnscaledFileToNewDirectory(string filename, string path = null)
        {
            string directoryToMoveTo = path ?? DirectoryInfo.directoryToMoveFilesTo;
            if (directoryToMoveTo == DirectoryInfo.previousFilesDirectory && File.Exists(directoryToMoveTo + '\\' + filename))
            {
                File.Delete(directoryToMoveTo + '\\' + filename);
            }

            File.Move(DirectoryInfo.trackFolderPath + '\\' + filename, directoryToMoveTo + '\\' + filename);
        }

        public static bool IsCheckboxSelectionValid(CheckBox box, string filename, Label errorLabel, string errorText = null)
        {
            string errorLabelText = errorText ?? $"Error: cannot find {filename} file!";
            if (box.Checked && !File.Exists(DirectoryInfo.trackFolderPath + '\\' + filename))
            {
                UIHelper.ChangeLabel(errorLabel, Color.Red, errorLabelText);
                return false;
            }
            return true;
        }

        public static int GetFileLineCount(string filename)
        {
            string filePath = DirectoryInfo.trackFolderPath + '\\' + filename;
            
            if (!File.Exists(filePath)) return 0;
            return File.ReadLines(filePath).Count();
        }

        public static int GetProgressBarMaximum(TrackScalerForm scalerForm)
        {
            // Total lines is initialized to 1 for the 1 line in terrain.hf
            int totalLines = scalerForm.IsTerrainChecked ? 1 : 0;

            foreach (KeyValuePair<string, CheckBox> pair in scalerForm.ScalerFormCheckboxes)
            {
                string filename = pair.Key;
                CheckBox checkbox = pair.Value;

                string filePath = DirectoryInfo.trackFolderPath + '\\' + filename;
                if (!checkbox.Checked || !File.Exists(filePath)) continue;

                totalLines += GetFileLineCount(filename);
            }

            totalFileLines = totalLines;

            // If mirroring is enabled, 50% of the progress bar goes to file scaling, other 50 goes to scaling track files / references
            if (scalerForm.IsMirrorEnabled)
            {
                totalLines *= 2;
            }

            return totalLines;
        }

        public static byte[,] ReadBinaryFile(string filePath, int rows, int? cols = null)
        {
            int columns = cols ?? rows;

            // Initialize the 2D array
            byte[,] result = new byte[rows, columns];

            // Read the file into a 1D byte array
            byte[] rawData = File.ReadAllBytes(filePath);

            // Validate the file size
            if (rawData.Length != rows * columns)
            {
                throw new Exception("File size does not match expected dimensions.");
            }

            // Populate the 2D array
            int index = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    result[i, j] = rawData[index++];
                }
            }

            return result;
        }

        public static void FlipByteArrayColumns(byte[,] array, int rows, int? cols = null)
        {
            int columns = cols ?? rows;

            // Flip columns across the center
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns / 2; j++)
                {
                    (array[i, columns - j - 1], array[i, j]) = (array[i, j], array[i, columns - j - 1]);
                }
            }
        }

        public static void WriteBinaryFile(string filePath, byte[,] array, int rows, int? cols = null)
        {
            int columns = cols ?? rows;

            using (BinaryWriter writer = new BinaryWriter(File.Open(filePath, FileMode.Create)))
            {
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {
                        writer.Write(array[i, j]);
                    }
                }
            }
        }

        public static bool IsASCIIPlainText(string filePath)
        {
            // Read the file contents
            byte[] fileBytes = File.ReadAllBytes(filePath);

            // Check if all bytes are either printable ASCII characters or common line endings
            foreach (byte b in fileBytes)
            {
                if ((b < 32 && b != 9 && b != 10 && b != 13) || b > 127) // Exclude tab (9), line feed (\n) (10), and carriage return (13)
                {
                    return false; // Non-printable byte found
                }
            }
            return true;
        }

        public static bool IsFileSeqFormat(string filePath)
        {
            // Check if file is seq file type, if so, attempt to add all files to files to mirror
            StreamReader reader = new StreamReader(filePath);
            string line = reader.ReadLine(); // read first line
            string[] lineArgs = line.Trim().Split(' ');

            if (lineArgs.Length != 5) return false;
            if (!validSeqAnimationNames.Contains(lineArgs[0])) return false;

            for (int i = 1; i < lineArgs.Length; i++)
            {
                if (!int.TryParse(lineArgs[i], out _))
                {
                    return false;
                }
            }

            return true;
        }

        
    }
}
