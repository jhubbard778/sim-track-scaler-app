using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MX_Simulator_Track_Scaler
{
    internal class FileParser
    {
        private static TrackScalerForm fileParserTrackForm;
        private static StreamReader trackFileToRead;
        private static StreamWriter tempFile;
        private static string tempPath;

        public async static Task<bool> ScaleTrackFile(TrackScalerForm ScalerForm, string file)
        {
            return await Task.Run(() =>
            {
                if (ScaleInfo.isUnchangingScale)
                {
                    UIHelper.PerformProgressStep(ScalerForm, Helpers.GetFileLineCount(file));
                    return true;
                }

                fileParserTrackForm = ScalerForm;

                string trackFilePath = DirectoryInfo.trackFolderPath + $"\\{file}";
                trackFileToRead = File.OpenText(trackFilePath);

                tempPath = Path.GetTempFileName();
                tempFile = File.CreateText(tempPath);
                if (tempFile.BaseStream == null)
                {
                    return false;
                }

                switch (file)
                {
                    // ugly - fix
                    case "timing_gates":
                        if (!ScaleTimingGates()) return false;
                        break;
                    case "edinfo":
                        if (!ScaleGradients()) return false;
                        break;
                    case "flaggers":
                        if (!ScaleFlaggers()) return false;
                        break;
                    case "decals":
                        if (!ScaleDecals()) return false;
                        break;
                    case "statues":
                        if (!ScaleStatues()) return false;
                        break;
                    case "billboards":
                        if (!ScaleBillboards()) return false;
                        break;
                    default:
                        break;
                }

                Helpers.CleanupFilesAndMoveTempFile(file, trackFileToRead, tempFile, tempPath);
                return true;
            });
        }

        /*private static bool ScaleGenericFile()
        {
            return true;
        }*/

        private static bool ScaleTimingGates()
        {
            int timingGateParserPart = 0;
            while (trackFileToRead.EndOfStream == false)
            {
                string line = trackFileToRead.ReadLine();

                // Do not add newlines if we're not at the last part
                if (line[0] == '\n' && timingGateParserPart < 2) continue;

                if (line == "startinggate:")
                {
                    tempFile.WriteLine(line);
                    UIHelper.PerformProgressStep(fileParserTrackForm);
                    continue;
                }

                if (line == "checkpoints:")
                {
                    tempFile.WriteLine(line);
                    timingGateParserPart = 1;
                    UIHelper.PerformProgressStep(fileParserTrackForm);
                    continue;
                }

                if (line == "firstlap:") timingGateParserPart = 2;

                // Do Final Segment
                if (timingGateParserPart == 2)
                {
                    tempFile.WriteLine(line);
                    UIHelper.PerformProgressStep(fileParserTrackForm);
                    continue;
                }

                string[] args = line.Split(' ');

                // Do First Segment
                if (timingGateParserPart == 0)
                {
                    // Check line to make sure it's valid
                    bool lineHasCorrectFormatting = line[0] == '[' && args[2].EndsWith("]");
                    if (!HasAtLeastArgumentLength(args, 4, "Timing Gates", lineHasCorrectFormatting)) return false;

                    args[0] = args[0].Replace("[", string.Empty);
                    args[2] = args[2].Replace("]", string.Empty);

                    decimal x = 0, y = 0, z = 0;
                    double angle = 0;

                    var parsers = new Func<string, bool>[]
                    {
                        input => decimal.TryParse(input, out x),
                        input => decimal.TryParse(input, out y),
                        input => decimal.TryParse(input, out z),
                        input => Double.TryParse(input, out angle)
                    };

                    // parse coords and get angle
                    for (int i = 0; i < parsers.Length; i++)
                    {
                        if (parsers[i](args[i])) continue;

                        UIHelper.ParseError(fileParserTrackForm, "Timing Gates", trackFileToRead, tempFile);
                        return false;
                    }
                    
                    // calculate new coords
                    decimal[] coords = Helpers.GetNewCoords(x, y, z);

                    if (fileParserTrackForm.IsMirrorEnabled)
                    {
                        angle = MirrorHelper.GetMirroredAngle(angle);
                        coords[0] = MirrorHelper.GetMirroredHorizontalAxis(coords[0]);
                    }

                    // set default outline
                    string outLine = $"[{coords[0]} {coords[1]} {coords[2]}] {angle}";

                    // if they have jm, png, and shp add it
                    if (args.Length == 7)
                    {
                        string jm = args[4];
                        string png = args[5];
                        string shp = args[6];
                        outLine += $" {jm} {png} {shp}";
                    }

                    tempFile.WriteLine(outLine);

                }
                // Do Second Segment
                else if (timingGateParserPart == 1)
                {
                    // Check lines to make sure they're valid
                    bool lineHasCorrectFormatting = args[1].StartsWith("[") && args[3].EndsWith("]") && args[4].StartsWith("[") && args[6].EndsWith("]");
                    if (!HasArgumentLength(args, 7, "Timing Gates", lineHasCorrectFormatting)) return false;

                    // Replace Brackets
                    args[1] = args[1].Replace("[", string.Empty);
                    args[3] = args[3].Replace("]", string.Empty);
                    args[4] = args[4].Replace("[", string.Empty);
                    args[6] = args[6].Replace("]", string.Empty);

                    decimal size = 0, x1 = 0, y1 = 0, z1 = 0, x2 = 0, y2 = 0, z2 = 0;
                    var parsers = new Func<string, bool>[]
                    {
                        input => decimal.TryParse(input, out size),
                        input => decimal.TryParse(input, out x1),
                        input => decimal.TryParse(input, out y1),
                        input => decimal.TryParse(input, out z1),
                        input => decimal.TryParse(input, out x2),
                        input => decimal.TryParse(input, out y2),
                        input => decimal.TryParse(input, out z2),
                    };

                    // Parse size and coords into variables
                    for (int i = 0; i < parsers.Length; i++)
                    {
                        if (parsers[i](args[i])) continue;

                        UIHelper.ParseError(fileParserTrackForm, "Timing Gates", trackFileToRead, tempFile);
                        return false;
                    }

                    // Calculate new size & coords
                    decimal newSize = size * ScaleInfo.multiplier;
                    decimal[] coords1 = Helpers.GetNewCoords(x1, y1, z1);
                    decimal[] coords2 = Helpers.GetNewCoords(x2, y2, z2);

                    if (fileParserTrackForm.IsMirrorEnabled)
                    {
                        coords1[0] = MirrorHelper.GetMirroredHorizontalAxis(coords1[0]);
                        coords2[0] *= -1;
                    }

                    // Write to file
                    tempFile.WriteLine($"{newSize} [{coords1[0]} {coords1[1]} {coords1[2]}] [{coords2[0]} {coords2[1]} {coords2[2]}]");
                }

                // step the progress bar
                UIHelper.PerformProgressStep(fileParserTrackForm);
            }

            return true;
        }

        private static bool ScaleFlaggers()
        {
            while (trackFileToRead.EndOfStream == false)
            {
                string line = trackFileToRead.ReadLine();
                if (line == string.Empty || line[0] != '[')
                {
                    if (line != string.Empty && line[0] == '@' && fileParserTrackForm.IsMirrorEnabled)
                    {
                        MirrorHelper.GetTrackReferenceImages(line);
                    }
                        
                    tempFile.WriteLine(line);
                    UIHelper.PerformProgressStep(fileParserTrackForm);
                    continue;
                }

                string[] args = line.Split(' ');

                bool lineHasCorrectFormatting = args[0].StartsWith("[") && args[2].EndsWith("]");
                if (!HasArgumentLength(args, 3, "Flaggers", lineHasCorrectFormatting)) return false;

                // replace brackets
                args[0] = args[0].Replace("[", string.Empty);
                args[2] = args[2].Replace("]", string.Empty);

                decimal x = 0, y = 0, z = 0;
                var parsers = new Func<string, bool>[]
                {
                    input => decimal.TryParse(input, out x),
                    input => decimal.TryParse(input, out y),
                    input => decimal.TryParse(input, out z),
                };

                // Parse coords
                for (int i = 0; i < parsers.Length; i++)
                {
                    if (parsers[i](args[i])) continue;
                    UIHelper.ParseError(fileParserTrackForm, "Flaggers", trackFileToRead, tempFile);
                    return false;
                }

                // calculate new coords
                decimal[] coords = Helpers.GetNewCoords(x, y, z);

                if (fileParserTrackForm.IsMirrorEnabled)
                {
                    coords[0] = MirrorHelper.GetMirroredHorizontalAxis(coords[0]);
                }

                // write to file
                tempFile.WriteLine($"[{coords[0]} {coords[1]} {coords[2]}]");

                // step the progress bar
                UIHelper.PerformProgressStep(fileParserTrackForm);
            }

            return true;
        }

        private static bool ScaleGradients()
        {
            while (trackFileToRead.EndOfStream == false)
            {
                string line = trackFileToRead.ReadLine();
                string[] args = line.Split(' ');

                if (args[0] == "add_gradient")
                {
                    // Process gradient x and z points
                    // add_gradient <start_x> <start_z> <end_x> <end_z>
                    if (!HasArgumentLength(args, 5, "edinfo")) return false;

                    decimal startX = 0, startZ = 0, endX = 0, endZ = 0;
                    var parsers = new Func<string, bool>[]
                    {
                        input => decimal.TryParse(input, out startX),
                        input => decimal.TryParse(input, out startZ),
                        input => decimal.TryParse(input, out endX),
                        input => decimal.TryParse(input, out endZ),
                    };

                    for (int i = 0; i < parsers.Length; i++)
                    {
                        if (parsers[i](args[i + 1])) continue;

                        UIHelper.ParseError(fileParserTrackForm, "edinfo", trackFileToRead, tempFile);
                        return false;
                    }

                    startX *= ScaleInfo.multiplier;
                    startZ *= ScaleInfo.multiplier;
                    endX *= ScaleInfo.multiplier;
                    endZ *= ScaleInfo.multiplier;

                    if (fileParserTrackForm.IsMirrorEnabled)
                    {
                        startX = MirrorHelper.GetMirroredHorizontalAxis(startX);
                        endX = MirrorHelper.GetMirroredHorizontalAxis(endX);
                    }

                    tempFile.WriteLine($"add_gradient {startX} {startZ} {endX} {endZ}");
                }

                else if (args[0] == "add_point")
                {
                    // Process gradient points
                    // add_point <0 linear, 1 curved> <distance from origin> <height>
                    if (!HasArgumentLength(args, 4, "edinfo")) return false;

                    int pointType = 0;
                    decimal originDistance = 0, height = 0;

                    var parsers = new Func<string, bool>[]
                    {
                        input => int.TryParse(input, out pointType),
                        input => decimal.TryParse(input, out originDistance),
                        input => decimal.TryParse(input, out height),
                    };

                    // Parse all information
                    for (int i = 0; i < parsers.Length; i++)
                    {
                        if (parsers[i](args[i + 1])) continue;

                        UIHelper.ParseError(fileParserTrackForm, "edinfo", trackFileToRead, tempFile);
                        return false;
                    }

                    originDistance *= ScaleInfo.multiplier;
                    height *= ScaleInfo.multiplier;

                    tempFile.WriteLine($"add_point {pointType} {originDistance} {height}");
                }

                UIHelper.PerformProgressStep(fileParserTrackForm);
            }
            return true;
        }

        private static bool ScaleDecals()
        {
            while (trackFileToRead.EndOfStream == false)
            {

                // Read the line, split it into an array
                string line = trackFileToRead.ReadLine();
                if (line == string.Empty || line[0] == '\n')
                {
                    UIHelper.PerformProgressStep(fileParserTrackForm);
                    continue;
                }
                string[] args = line.Split(' ');

                // if the format is not in decals throw error
                bool lineHasCorrectFormatting = line[0] == '[' && args[1].EndsWith("]");
                if (!HasArgumentLength(args, 6, "Decals", lineHasCorrectFormatting)) return false;
                
                // replace brackets
                args[0] = args[0].Replace("[", string.Empty);
                args[1] = args[1].Replace("]", string.Empty);

                decimal x = 0, z = 0, size = 0;
                double angle = 0;
                var parsers = new Func<string, bool>[]
                {
                    input => decimal.TryParse(input, out x),
                    input => decimal.TryParse(input, out z),
                    input => Double.TryParse(input, out angle),
                    input => decimal.TryParse(input, out size),
                };

                for (int i = 0; i < parsers.Length; i++)
                {
                    if (parsers[i](args[i])) continue;

                    UIHelper.ParseError(fileParserTrackForm, "Decals", trackFileToRead, tempFile);
                    return false;
                }

                string aspect = args[4];
                string png = args[5];

                // new coords / sizes
                decimal[] coords = Helpers.GetNewCoords(x, 0, z);
                decimal newSize = size * ScaleInfo.multiplier;

                if (fileParserTrackForm.IsMirrorEnabled)
                {
                    angle = MirrorHelper.GetMirroredAngle(angle);
                    coords[0] = MirrorHelper.GetMirroredHorizontalAxis(coords[0]);
                    MirrorHelper.GetTrackReferenceImages(png);
                }

                tempFile.WriteLine($"[{coords[0]} {coords[2]}] {angle} {newSize} {aspect} {png}");

                // step the progress bar
                UIHelper.PerformProgressStep(fileParserTrackForm);

            }

            return true;
        }

        private static bool ScaleStatues()
        {
            while (trackFileToRead.EndOfStream == false)
            {
                // Read the line, split it into an array
                string line = trackFileToRead.ReadLine();
                if (line == string.Empty || line[0] == '\n')
                {
                    UIHelper.PerformProgressStep(fileParserTrackForm);
                    continue;
                }

                string[] args = line.Split(' ');

                // if the format is not in statues format throw error
                bool lineHasCorrectFormatting = line[0] == '[' && args[2].EndsWith("]");
                if (!HasArgumentLength(args, 7, "Statues", lineHasCorrectFormatting)) return false;

                // replace brackets
                args[0] = args[0].Replace("[", string.Empty);
                args[2] = args[2].Replace("]", string.Empty);

                decimal x = 0, y = 0, z = 0;
                double angle = 0;
                var parsers = new Func<string, bool>[]
                { 
                    input => decimal.TryParse(input, out x),
                    input => decimal.TryParse(input, out y),
                    input => decimal.TryParse(input, out z),
                    input => Double.TryParse(input, out angle),
                };

                // parse coords and sizes
                for (int i = 0; i < parsers.Length; i++)
                {
                    if (parsers[i](args[i])) continue;

                    UIHelper.ParseError(fileParserTrackForm, "Statues", trackFileToRead, tempFile);
                    return false;
                }

                string jm = args[4];
                string png = args[5];
                string shp = args[6];

                // calculate new coords
                decimal[] coords = Helpers.GetNewCoords(x, y, z);

                if (fileParserTrackForm.IsMirrorEnabled)
                {
                    angle = MirrorHelper.GetMirroredAngle(angle);
                    coords[0] = MirrorHelper.GetMirroredHorizontalAxis(coords[0]);
                }

                // write to file
                tempFile.WriteLine($"[{coords[0]} {coords[1]} {coords[2]}] {angle} {jm} {png} {shp}");

                UIHelper.PerformProgressStep(fileParserTrackForm);

            }

            return true;
        }

        private static bool ScaleBillboards()
        {
            while (trackFileToRead.EndOfStream == false)
            {
                // Read the line, split it into an array
                string line = trackFileToRead.ReadLine();
                if (line == string.Empty || line[0] == '\n')
                {
                    UIHelper.PerformProgressStep(fileParserTrackForm);
                    continue;
                }

                string[] args = line.Split(' ');

                // if the format is not in billboards format throw error
                bool lineHasCorrectFormatting = line[0] == '[' && args[2].EndsWith("]");
                if (!HasArgumentLength(args, 6, "billboards", lineHasCorrectFormatting)) return false;

                // replace brackets
                args[0] = args[0].Replace("[", string.Empty);
                args[2] = args[2].Replace("]", string.Empty);

                // parse coords and size
                decimal x = 0, y = 0, z = 0, size = 0;
                var parsers = new Func<string, bool>[]
                {
                    input => decimal.TryParse(input, out x),
                    input => decimal.TryParse(input, out y),
                    input => decimal.TryParse(input, out z),
                    input => decimal.TryParse(input, out size),
                };

                for (int i = 0; i < parsers.Length; i++)
                {
                    if (parsers[i](args[i])) continue;

                    UIHelper.ParseError(fileParserTrackForm, "billboards", trackFileToRead, tempFile);
                    return false;
                }

                string aspect = args[4];
                string png = args[5];
                
                // calculate new coords / size
                decimal[] coords = Helpers.GetNewCoords(x, y, z);
                decimal newSize = size * ScaleInfo.multiplier;

                if (fileParserTrackForm.IsMirrorEnabled)
                {
                    MirrorHelper.GetTrackReferenceImages(png);
                    coords[0] = MirrorHelper.GetMirroredHorizontalAxis(coords[0]);
                }

                // write to file
                tempFile.WriteLine($"[{coords[0]} {coords[1]} {coords[2]}] {newSize} {aspect} {png}");

                // step the progress bar
                UIHelper.PerformProgressStep(fileParserTrackForm);
            }

            return true;
        }

        private static bool HasArgumentLength(string[] args, int length, string filename, bool optionalBool = true)
        {
            if (args.Length == length && optionalBool) return true;

            UIHelper.ParseError(fileParserTrackForm, filename, trackFileToRead, tempFile);
            return false;
        }

        private static bool HasAtLeastArgumentLength(string[] args, int length, string filename, bool optionalBool = true)
        {
            if (args.Length >= length && optionalBool) return true;

            UIHelper.ParseError(fileParserTrackForm, filename, trackFileToRead, tempFile);
            return false;
        }
    }
}
