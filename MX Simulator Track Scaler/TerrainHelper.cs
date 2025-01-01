using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MX_Simulator_Track_Scaler
{
    internal class TerrainHelper : Helpers
    {
        public static bool IsTerrainValid(TrackScalerForm ScalerForm)
        {
            if (!File.Exists(DirectoryInfo.trackFolderPath + "\\terrain.hf"))
            {
                UIHelper.ChangeLabel(ScalerForm.FormFileErrorLabel, Color.Red, "Error: terrain.hf file does not exist in this folder!");
                return false;
            }

            // read data into variables
            StreamReader terrainFile = File.OpenText(DirectoryInfo.trackFolderPath + "\\terrain.hf");
            string line = terrainFile.ReadLine();
            terrainFile.Close();
            string[] args = line.Trim().Split(' ');

            if (args.Length != 4)
            {
                UIHelper.ParseError(ScalerForm, "terrain.hf");
                return false;
            }

            var parsers = new Func<string, bool>[]
            {
                input => int.TryParse(input, out ScaleInfo.terrainScaleNumber),
                input => decimal.TryParse(input, out ScaleInfo.terrainScale),
                input => decimal.TryParse(input, out ScaleInfo.minHeight),
                input => decimal.TryParse(input, out ScaleInfo.maxHeight)
            };

            // Read terrain.hf values into variables
            for (int i = 0; i < parsers.Length; i++)
            {
                if (parsers[i](args[i])) continue;
                
                // If parsing failed notify user
                UIHelper.ParseError(ScalerForm, "terrain.hf");
                return false;
            }

            return true;
        }

        public static bool ScaleTerrain(TrackScalerForm ScalerForm)
        {

            UIHelper.ChangeLabel(ScalerForm.FormProgressBarLabel, Color.White, "Scaling Terrain...");

            decimal scalarOutput = ScaleInfo.isByFactorMethod ? ScaleInfo.scalarInput * ScaleInfo.terrainScale : ScaleInfo.scalarInput;
            decimal min = ScaleInfo.minHeight * ScaleInfo.multiplier;
            decimal max = ScaleInfo.maxHeight * ScaleInfo.multiplier;

            Helpers.MoveUnscaledFileToNewDirectory("terrain.hf");

            // Open the temp file
            string tempPath = Path.GetTempFileName();
            StreamWriter tempFile = File.CreateText(tempPath);

            // write to the file and close as we're done writing
            tempFile.WriteLine(ScaleInfo.terrainScaleNumber.ToString() + ' ' +
                scalarOutput.ToString() + ' ' + min.ToString() + ' ' + max.ToString());
            tempFile.Close();

            // Rename the file to terrain.hf
            File.Move(tempPath, DirectoryInfo.trackFolderPath + "\\terrain.hf");

            // Step the progress bar and return
            ScalerForm.FormProgressBar.PerformStep();
            return true;
        }
    }
}
