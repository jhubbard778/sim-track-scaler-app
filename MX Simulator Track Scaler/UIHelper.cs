using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WK.Libraries.BetterFolderBrowserNS;

namespace MX_Simulator_Track_Scaler
{
    internal class UIHelper : Helpers
    {
        public static void CalculateAndDisplayNewScale(TrackScalerForm ScalerForm)
        {
            TextBox textbox = ScalerForm.FormUserInputScaleTextBox;
            if (!decimal.TryParse(textbox.Text, out decimal scale) || textbox.TextLength == 0)
            {
                ScalerForm.FormNewScaleLabel.ResetText();
                return;
            }

            decimal newScale = scale;
            // Calculate the new factor if not scaling by factor
            if (!ScalerForm.IsByFactorScale)
            {
                newScale = Math.Round(scale / ScaleInfo.terrainScale, 6);
            }

            ChangeLabel(ScalerForm.FormNewScaleLabel, Color.White, $"New Track Scale - {newScale}:1");
        }

        public static void ChangeLabel(Label label, Color color, string msg)
        {
            label.ForeColor = color;
            label.Text = msg;
        }

        public static void ParseError(TrackScalerForm ScalerForm, string filename, StreamReader fileReading = null, StreamWriter tempFile = null)
        {
            ChangeLabel(ScalerForm.FormFileErrorLabel, Color.Red, $"Error: Incompatible {filename} file!");
            fileReading?.Close();
            tempFile?.Close();

            ScalerForm.FormProgressBar.Visible = false;
            ChangeLabel(ScalerForm.FormProgressBarLabel, Color.Red, "Failed!");
        }

        public static void SelectTrackFolder(TrackScalerForm ScalerForm)
        {
            var betterFolderBrowser = new BetterFolderBrowser
            {
                Title = "Select Track Folder...",
                RootFolder = Environment.CurrentDirectory,
                Multiselect = false
            };

            if (betterFolderBrowser.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(betterFolderBrowser.SelectedPath))
            {

                string[] files = Directory.GetFiles(betterFolderBrowser.SelectedPath);
                if (files.Length == 0)
                {
                    ChangeLabel(ScalerForm.FormTrackDirectoryLabel, Color.Red, "Error: Empty Folder");
                    return;
                }

                // Reset progress label error if the user selects new folder
                ScalerForm.FormProgressBarLabel.ResetText();
                ScalerForm.FormProgressBar.Visible = false;

                DirectoryInfo.SetFileDirectories(betterFolderBrowser.SelectedPath);

                if (!TerrainHelper.IsTerrainValid(ScalerForm))
                {
                    ChangeLabel(ScalerForm.FormTrackDirectoryLabel, Color.Red, "Error: Missing/Incompatible terrain.hf file");
                    return;
                }

                ChangeLabel(ScalerForm.FormTrackDirectoryLabel, Color.White, DirectoryInfo.trackFolderPath);
            }
        }

        public static void PerformProgressStep(TrackScalerForm scalerForm, int stepSize = 1)
        {
            if (scalerForm.InvokeRequired)
            {
                scalerForm.Invoke(new Action(() => {
                    scalerForm.FormProgressBar.Step = stepSize;
                    scalerForm.FormProgressBar.PerformStep();
                    scalerForm.FormProgressBar.Step = 1;
                }));
                return;
            }

            scalerForm.FormProgressBar.Step = stepSize;
            scalerForm.FormProgressBar.PerformStep();
            scalerForm.FormProgressBar.Step = 1;
        }
    }
}
