using Microsoft.Win32;
using QuantumConcepts.Common.Forms.UI.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ImageMagick;
using System.Net.NetworkInformation;
using System.Windows.Forms.VisualStyles;

namespace MX_Simulator_Track_Scaler
{
    internal class MirrorHelper
    {
        private static readonly string[] imagesToFlip = { "terrain.png", "map.png", "shading.ppm", "shadingx2.ppm", "shadows.pgm" };
        public static readonly string mirrorScriptPath = Directory.GetCurrentDirectory() + @"\py\imageflipper.py";
        private static readonly List<string> pathsChecked = new List<string>();
        private static readonly List<string> filesNotFound = new List<string>();

        private static readonly string[] possibleNormSpecEndings = 
            { "_tnorm", "_tnorm-repeat", "_tspec", "_tspec-repeat", "_norm", "_norm-repeat", "_spec", "_spec-repeat" };

        private static readonly string[] possibleImageTypes = { ".png", ".seq", ".ppm" };
        private static readonly string[] seqImageTypes = { "PNG", "PPM" };

        private static readonly List<string> filesToMirror = new List<string>();
        private static readonly Dictionary<string, string> fileToDestinationMap = new Dictionary<string, string>();

        /// <summary>
        /// Mirrors all the track image files listed in imagesToFlip by invoking a python script
        /// </summary>
        /// <param name="ScalerForm">The form instance</param>
        /// <returns>Boolean whether mirroring succeeded or failed</returns>
        public static async Task<bool> MirrorTrackImages(TrackScalerForm ScalerForm)
        {
            return await Task.Run(() =>
            {
                List<string> args = new List<string>();
                int numImagesToFlip = 0;
                foreach (string imagePath in imagesToFlip)
                {
                    // Path to the input image
                    string originalImagePath = DirectoryInfo.trackFolderPath + '\\' + imagePath;

                    if (!File.Exists(originalImagePath)) continue;

                    if (Helpers.IsASCIIPlainText(originalImagePath))
                    {
                        if (Helpers.IsFileSeqFormat(originalImagePath))
                        {
                            List<string> files = new List<string>();
                            ReadAndAddAllSourceImagesFromSeq(originalImagePath, files);
                            numImagesToFlip += files.Count;
                            filesToMirror.AddRange(files);
                        }
                        continue;
                    }

                    numImagesToFlip++;

                    string oldImageNewPath = Path.Combine(DirectoryInfo.directoryToMoveFilesTo, imagePath);
                    if (File.Exists(oldImageNewPath))
                    {
                        File.Delete(oldImageNewPath);
                    }

                    File.Move(originalImagePath, oldImageNewPath);
                    args.Add(oldImageNewPath);
                }
                args.Add(DirectoryInfo.trackFolderPath);

                bool result = PythonProcess.RunPythonProcess(mirrorScriptPath, args, ScalerForm);

                // Have this process take up 5% of progress bar
                int stepSize = (int)Math.Floor(ScalerForm.FormProgressBar.Maximum * 0.05);
                UIHelper.PerformProgressStep(ScalerForm, stepSize);

                return result;
            });
        }

        /// <summary>
        /// Mirrors every image file associated with a track file resource
        /// </summary>
        /// <param name="path">The track file resource path</param>
        public static void GetTrackReferenceImages(string path)
        {
            if (pathsChecked.Contains(path)) return;
            pathsChecked.Add(path);
            filesToMirror.AddRange(GetAllFilesAssociatedWithPath(path));
        }

        public async static Task MirrorTrackReferenceImages(TrackScalerForm scalerForm)
        {
            // Mirroring Track Reference Images will take up 45% of the progress bar
            int stepSize = (int) Math.Floor((scalerForm.FormProgressBar.Maximum * 0.45) / filesToMirror.Count);

            await Task.Run(() =>
            {
                foreach (string filePath in filesToMirror)
                {
                    string destinationPath = fileToDestinationMap[filePath];
                    if (File.Exists(destinationPath))
                    {
                        File.Delete(destinationPath);
                    }

                    // Create directory if doesn't exist and move files
                    string destinationDirectory = Path.GetDirectoryName(destinationPath);
                    if (!Directory.Exists(destinationPath))
                    {
                        Directory.CreateDirectory(destinationDirectory);
                    }

                    File.Move(filePath, destinationPath);

                    // Read the image, flip it, save
                    using (var image = new MagickImage(destinationPath))
                    {
                        image.Flop();
                        image.Write(filePath);
                    }

                    UIHelper.PerformProgressStep(scalerForm, stepSize);
                }

                filesToMirror.Clear();
                fileToDestinationMap.Clear();
            });
        }

        /// <summary>
        /// Gets a list of every single full resource path associated with the track file resource passes into the function
        /// </summary>
        /// <param name="path">The track file resource path</param>
        /// <returns>A list of full paths to every file associated with this track file resource path</returns>
        private static List<string> GetAllFilesAssociatedWithPath(string path)
        {
            List<string> filesAssociatedWithPath = new List<string>();

            string imageExtension = Path.GetExtension(path).Trim();
            string strippedPath = path.Substring(0, path.IndexOf(imageExtension));
            
            string imagePath = GetSourceImageIfExists(path);
            if (imagePath == null) return filesAssociatedWithPath;

            if (imageExtension == ".seq")
            {
                ReadAndAddAllSourceImagesFromSeq(imagePath, filesAssociatedWithPath);
                return filesAssociatedWithPath;
            }

            FindAndAddAllNormsAndSpecsFromImage(strippedPath, filesAssociatedWithPath);
            filesAssociatedWithPath.Add(imagePath);

            return filesAssociatedWithPath;
        }

        /// <summary>
        /// Takes a track file seq resource path and gets all full paths of each image if they exist,
        /// and adds it to the list of files passed into the function
        /// </summary>
        /// <param name="path">The track file resource path</param>
        /// <param name="files">The list of files to add to if the file exists</param>
        private static void ReadAndAddAllSourceImagesFromSeq(string path, List<string> files)
        {
            if (!File.Exists(path)) return;

            StreamReader reader = new StreamReader(path);
            while (reader.EndOfStream == false)
            {
                string line = reader.ReadLine();
                string[] splitLine = line.Split(' ');

                if (splitLine.Length != 2 || !seqImageTypes.Contains(splitLine[0])) continue;

                string localPath = splitLine[1];
                string imageExtension = Path.GetExtension(localPath).Trim();
                string strippedPath = localPath.Substring(0, localPath.IndexOf(imageExtension));

                string imagePath = GetSourceImageIfExists(localPath);

                if (imagePath != null)
                {
                    files.Add(imagePath);
                    FindAndAddAllNormsAndSpecsFromImage(strippedPath, files, false);
                }
            }

            reader.Close();
        }

        /// <summary>
        /// Finds any norms and spec files associated with a track file resource, and adds the full path to the resource 
        /// to a list of files if the file exists.
        /// </summary>
        /// <param name="trimmedPathWithoutExtension">The track file resource with no extension</param>
        /// <param name="files">String list of filepaths to add the full path of the resource to if it exists</param>
        /// <param name="includeSeqChecks">Optional flag to skip checking seq files</param>
        private static void FindAndAddAllNormsAndSpecsFromImage(string trimmedPathWithoutExtension, List<string> files, bool includeSeqChecks = true)
        {
            // Search for norms and specs
            foreach (string possibleImageExtension in possibleImageTypes)
            {
                if (possibleImageExtension == ".seq" && !includeSeqChecks) continue;
                
                foreach (string possibleEnding in possibleNormSpecEndings)
                {
                    string imagePath = GetSourceImageIfExists(trimmedPathWithoutExtension + possibleEnding + possibleImageExtension, false);
                    if (imagePath == null) continue;
                    if (possibleImageExtension == ".seq")
                    {
                        ReadAndAddAllSourceImagesFromSeq(imagePath, files);
                        continue;
                    }

                    files.Add(imagePath);
                }
            }
        }

        /// <summary>
        /// Takes a track file path resource that's relative to mx simulator directory and returns the full path to the resouce on the 
        /// system
        /// </summary>
        /// <param name="path">Path to track file resource from track file</param>
        /// <param name="addToUnfoundListIfNotFound">Optional boolean to add the file to files not found list if file isn't found</param>
        /// <returns>Filepath to the track resource if it exists, otherwise null</returns>
        private static string GetSourceImageIfExists(string path, bool addToUnfoundListIfNotFound = true)
        {
            string trackFolder = path.Split('/')[0].Substring(1);
            bool isLocalSource = DirectoryInfo.trackFolderPath.ToLower().EndsWith(trackFolder.ToLower());

            // First check if the file exists in the track directory we have selected
            if (isLocalSource)
            {
                // Strip the track folder path from the track file resource and add the relative path to the track folder path we selected
                string relativePath = path.Substring(path.IndexOf("/")).Replace("/", "\\");
                string potentialFilepath = DirectoryInfo.trackFolderPath + relativePath;

                if (File.Exists(potentialFilepath))
                {
                    fileToDestinationMap[potentialFilepath] = DirectoryInfo.directoryToMoveFilesTo + "\\" + relativePath;
                    return potentialFilepath;
                }
            }

            string mxPath = path.Substring(1).Replace("/", "\\");
            
            // Then check the install folder and localappdata folder to see if it exists
            foreach (string directory in new string[] { DirectoryInfo.mxInstallDirectory, DirectoryInfo.mxAppdataDirectory })
            {
                if (directory == null) continue;

                string potentialPath = directory + "\\" + mxPath;
                if (File.Exists(potentialPath))
                {
                    string destinationPath = DirectoryInfo.directoryToMoveFilesTo + "\\";
                    destinationPath += isLocalSource ? mxPath.Substring(mxPath.IndexOf("\\")) : mxPath;
                    fileToDestinationMap[potentialPath] = destinationPath;
                    return potentialPath;
                }
            }

            // If it doesn't exist, we will check later on if this file is within a saf somewhere, for now return false
            if (addToUnfoundListIfNotFound)
            {
                filesNotFound.Add(path);
            }
            return null;
        }

        /// <summary>
        /// Takes an angle and returns it's mirrored counterpart
        /// </summary>
        /// <param name="angle">The original angle</param>
        /// <returns>The mirrored angle across the vertical axis</returns>
        public static double GetMirroredAngle(double angle)
        {
            bool locked_vertical_axis = angle < (-2 * Math.PI) || angle > (2 * Math.PI);

            angle *= -1;
            angle %= (2 * Math.PI);

            while (locked_vertical_axis && angle > (-2 * Math.PI))
            {
                angle -= (2 * Math.PI);
            }

            return Math.Round(angle, 6);
        }

        /// <summary>
        /// Takes an X value and returns it's mirrored X value
        /// </summary>
        /// <param name="oldX">The original X value</param>
        /// <returns>The mirrored X value</returns>
        public static decimal GetMirroredHorizontalAxis(decimal oldX)
        {
            decimal scale = ScaleInfo.isByFactorMethod ? ScaleInfo.terrainScale * ScaleInfo.scalarInput : ScaleInfo.scalarInput;
            decimal centerCoord = ((ScaleInfo.GetTerrainImageWidth() - 1) * scale) / 2;

            decimal newX = ((oldX - centerCoord) * -1) + centerCoord;
            return Math.Round(newX, 6);
        }

        /// <summary>
        /// Mirrors a track's tilemap by reading the binary file into a byte array, flipping the columns of the byte array, and 
        /// rewriting the file back out into a binary tilemap file
        /// </summary>
        public static void MirrorTilemap()
        {
            string tilemapPath = DirectoryInfo.trackFolderPath + "\\tilemap";
            if (!File.Exists(tilemapPath)) return;

            int rowsColumnSize = ScaleInfo.GetTilemapRowColumnCount();
            byte[,] tilemapBytes = Helpers.ReadBinaryFile(tilemapPath, rowsColumnSize);

            Helpers.MoveUnscaledFileToNewDirectory("tilemap");
            Helpers.FlipByteArrayColumns(tilemapBytes, rowsColumnSize);
            Helpers.WriteBinaryFile(tilemapPath, tilemapBytes, rowsColumnSize);
        }

        /// <summary>
        /// Mirrors the X component of the lighting vector, moves the old file to backup, and writes the new file.
        /// </summary>
        public static void MirrorLighting()
        {
            if (!File.Exists(DirectoryInfo.trackFolderPath + "\\lighting")) return;

            string tempPath = Path.GetTempFileName();
            StreamWriter tempFile = File.CreateText(tempPath);
            StreamReader lightingFile = File.OpenText(DirectoryInfo.trackFolderPath + "\\lighting");

            string sunVectorLine = lightingFile.ReadLine();
            string[] vectorComponents = sunVectorLine.Split('[');
            string[] vector = vectorComponents[1].Trim().Split(' ');

            Decimal.TryParse(vector[0], out decimal XVectorComponent);
            XVectorComponent *= -1;
            vector[0] = XVectorComponent.ToString();
            
            vectorComponents[1] = "[ " + string.Join(" ", vector);
            sunVectorLine = string.Join("", vectorComponents);

            tempFile.WriteLine(sunVectorLine);

            string otherLines = lightingFile.ReadToEnd();
            tempFile.Write(otherLines);

            Helpers.CleanupFilesAndMoveTempFile("lighting", lightingFile, tempFile, tempPath);
        }
    }
}
