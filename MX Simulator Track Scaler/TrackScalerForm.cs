using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;

namespace MX_Simulator_Track_Scaler
{
    public partial class TrackScalerForm : Form
    {
        public TrackScalerForm()
        {
            InitializeComponent();
        }

        // Expose components to other functions
        public Label FormFileErrorLabel { get { return FileCheckErrLabel; } }
        public Label FormProgressBarLabel { get { return ProgressLabel; } }
        public Label FormTrackDirectoryLabel { get { return TrackDirectoryLabel; } }
        public Label FormNewScaleLabel { get { return NewTrackSizeLabel; } }
        public NewProgressBar FormProgressBar { get { return ScalingProgressBar; } }
        public TextBox FormUserInputScaleTextBox { get { return UserInputScaleTextBox; } }
        public bool IsMirrorEnabled { get { return MirroredCheckbox.Checked; } }
        public bool IsByFactorScale { get { return ByFactorRadioButton.Checked; } }
        public bool IsTerrainChecked { get { return TerrainCheckBox.Checked; } }
        public Dictionary<string, CheckBox> ScalerFormCheckboxes { get { return fileCheckBoxes; } }



        private Dictionary<string, CheckBox> fileCheckBoxes;

        private Point lastLocation;
        private bool mouseDown;

        private void TrackScalerForm_Load(object sender, EventArgs e) {

            NewTrackSizeLabel.ResetText();
            FileCheckErrLabel.ResetText();
            methodErrLabel.ResetText();
            ProgressLabel.ResetText();
            UserInputErrLabel.ResetText();
            TrackDirectoryLabel.ResetText();
            ExtraOptionsErrLabel.ResetText();

            fileCheckBoxes = new Dictionary<string, CheckBox>() {
                { "billboards", BillboardCheckBox },
                { "statues", StatueCheckBox },
                { "decals", DecalsCheckBox },
                { "flaggers", FlaggersCheckBox },
                { "timing_gates", TimingGateCheckBox },
                { "edinfo", GradientCheckbox }
            };

            // If there's no python interpreter path on the machine disable the checkbox
            if (!PythonProcess.pythonPathExists || !File.Exists(MirrorHelper.mirrorScriptPath))
            {
                string scriptPathFilename = Path.GetFileName(MirrorHelper.mirrorScriptPath);

                string title = !PythonProcess.pythonPathExists
                        ? "Python Not Found"
                        : $"{scriptPathFilename} Not Found";

                string tooltip = !PythonProcess.pythonPathExists
                        ? "Python interpreter not found. Please install python to use this feature."
                        : $"Could not find script {scriptPathFilename}. It is essential to mirror track images.";

                MirroredCheckbox.AutoCheck = false;
                MirroredCheckbox.FlatStyle = FlatStyle.Flat;
                MirroredCheckbox.ForeColor = Color.DimGray;
                MirroredCheckbox.Font = new Font(MirroredCheckbox.Font, MirroredCheckbox.Font.Style | FontStyle.Strikeout);
                MirrorTooltip.ToolTipTitle = title;
                MirrorTooltip.ToolTipIcon = ToolTipIcon.Error;
                MirrorTooltip.SetToolTip(MirroredCheckbox, tooltip);
            }
        }

        private void AllCheckBox_CheckedChanged(object sender, EventArgs e) {
            bool boxChecked = false;
            if (AllCheckBox.Checked) {
                boxChecked = true;
            }
            TerrainCheckBox.Checked = boxChecked;

            foreach (var pair in fileCheckBoxes)
            {

                CheckBox checkbox = pair.Value;
                checkbox.Checked = boxChecked;
            }
        }

        private void UserInputTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
            (e.KeyChar != '.')) {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1)) {
                e.Handled = true;
            }
        }

        private void OpenFolderButton_Click(object sender, EventArgs e)
        {
            UIHelper.SelectTrackFolder(this);
        }

        private void RadioButton_CheckChanged(object sender, EventArgs e) {
            methodErrLabel.ResetText();
            if (DirectoryInfo.isTrackFolderSelected && (ByFactorRadioButton.Checked || ToTerrainScaleRadioButton.Checked))
            {
                UIHelper.CalculateAndDisplayNewScale(this);
            }
        }

        private async void ScaleButton_Click(object sender, EventArgs e) {

            ProgressLabel.ResetText();
            FileCheckErrLabel.ResetText();
            ExtraOptionsErrLabel.ResetText();

            var progress = new Progress<string>(message =>
            {
                UIHelper.ChangeLabel(ProgressLabel, Color.White, message);
            });

            ScaleInfo.isByFactorMethod = IsByFactorScale;
            ScaleInfo.scaleVerticalValues = !DisableScaleYCheckBox.Checked;

            /*
            ##########################
            User Input Error Handling
            ##########################
             */

            if (!DirectoryInfo.isTrackFolderSelected)
            {
                UIHelper.ChangeLabel(TrackDirectoryLabel, Color.Red, "Error: Select a folder");
                return;
            }

            if (!Directory.Exists(DirectoryInfo.trackFolderPath))
            {
                UIHelper.ChangeLabel(TrackDirectoryLabel, Color.Red, "Folder no longer exists. Was it deleted after selection?");
                return;
            }

            // If none of the checkboxes are checked to scale
            if (!fileCheckBoxes.Any(kvp => kvp.Value.Checked == true))
            {
                UIHelper.ChangeLabel(FileCheckErrLabel, Color.Red, "No Files to Scale!");
                return;
            }

            if (!ByFactorRadioButton.Checked && !ToTerrainScaleRadioButton.Checked)
            {
                UIHelper.ChangeLabel(methodErrLabel, Color.Red, "Error: Select a Method");
                return;
            }

            if (UserInputScaleTextBox.Text == string.Empty)
            {
                UIHelper.ChangeLabel(UserInputErrLabel, Color.Red, "Error: Enter a factor/scale");
                return;
            }

            if (IsMirrorEnabled && !PythonProcess.pythonPathExists)
            {
                UIHelper.ChangeLabel(UserInputErrLabel, Color.Red, "This shouldn't even be possible. How did you manage to delete python between checking the box and submitting to scale the track????");
                return;
            }

            // Check to make sure files weren't deleted in the process
            if (!TerrainHelper.IsTerrainValid(this)) return;

            
            // Make sure all checkboxes selected have the associated files with them
            foreach (var pair in fileCheckBoxes)
            {
                string filename = pair.Key;
                CheckBox checkbox = pair.Value;
                if (!Helpers.IsCheckboxSelectionValid(checkbox, filename, FileCheckErrLabel)) return;
            }

            // Check to make sure the terrain.png exists for mirrored selection
            if (!Helpers.IsCheckboxSelectionValid(MirroredCheckbox, "terrain.png", ExtraOptionsErrLabel, "Need terrain.png for mirror option!")) return;

            // Parse the user input text box and output to scalar_input variable
            if (!decimal.TryParse(UserInputScaleTextBox.Text, out ScaleInfo.scalarInput))
            {
                UIHelper.ChangeLabel(UserInputErrLabel, Color.Red, "Enter integer or decimal value.");
                return;
            }

            ExitButton.Enabled = false;
            ExitButton2.Enabled = false;

            // Set up Progress Bar
            ScalingProgressBar.SetForeColor(Color.FromArgb(238, 126, 2));
            ScalingProgressBar.SetBackColor(Color.FromArgb(227, 177, 118));
            ScalingProgressBar.SetupProgressBar(0, Helpers.GetProgressBarMaximum(this));
            ScalingProgressBar.Visible = true;

            // The number that we will multiply all coords / values
            ScaleInfo.multiplier = ToTerrainScaleRadioButton.Checked ? ScaleInfo.scalarInput / ScaleInfo.terrainScale : ScaleInfo.scalarInput;
            ScaleInfo.isUnchangingScale = (!IsMirrorEnabled && ((ByFactorRadioButton.Checked && ScaleInfo.scalarInput == 1) || (ToTerrainScaleRadioButton.Checked && ScaleInfo.scalarInput == ScaleInfo.terrainScale)));

            // Create Folder to hold old files
            DirectoryInfo.SetupFileDirectories();
            DirectoryInfo.SetDirectoryToMoveFilesTo();

            if (!DirectoryInfo.originalFilesCreated) DirectoryInfo.originalFilesCreated = true;

            // Scale the terrain
            if (TerrainCheckBox.Checked && !TerrainHelper.ScaleTerrain(this)) return;
            
            foreach (var pair in fileCheckBoxes)
            {
                string filename = pair.Key;
                CheckBox checkbox = pair.Value;

                UIHelper.ChangeLabel(ProgressLabel, Color.White, $"Scaling {filename}...");

                if (checkbox.Checked && !await FileParser.ScaleTrackFile(this, filename)) return;
            }

            // Do Mirror work
            if (IsMirrorEnabled)
            {
                if (TerrainCheckBox.Checked)
                {
                    UIHelper.ChangeLabel(ProgressLabel, Color.White, "Mirroring Track Files...");
                    if (!await MirrorHelper.MirrorTrackImages(this)) return;
                    MirrorHelper.MirrorTilemap();
                    MirrorHelper.MirrorLighting();
                }

                UIHelper.ChangeLabel(ProgressLabel, Color.White, "Mirroring Track File References...");
                await MirrorHelper.MirrorTrackReferenceImages(this);
            }

            ScalingProgressBar.Value = ScalingProgressBar.Maximum;

            ExitButton.Enabled = true;
            ExitButton2.Enabled = true;
            UIHelper.ChangeLabel(ProgressLabel, Color.White, "Success!");
        }
        private void UserInputTextBox_TextChanged(object sender, EventArgs e) {
            
            if (UserInputScaleTextBox.TextLength == 1)
            {
                UserInputErrLabel.ResetText();
            }

            if (DirectoryInfo.isTrackFolderSelected && (ByFactorRadioButton.Checked || ToTerrainScaleRadioButton.Checked))
            {
                UIHelper.CalculateAndDisplayNewScale(this);
            }
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void TrackScalerForm_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void TrackScalerForm_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void TrackScalerForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (!mouseDown) return;

            this.Location = new Point(
                (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

            this.Update();
        }

        private void MinimizeButton_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void ExitButton_EnabledChanged(object sender, EventArgs e)
        {
            ExitButton.ForeColor = ExitButton.Enabled ? Color.White : Color.DarkGray;
        }

        private void ExitButton_Paint(object sender, PaintEventArgs e)
        {
            Button btn = (Button)sender;
            // Make sure Text is not also written on button.
            btn.Text = string.Empty;
            // Set flags to center text on the button.
            TextFormatFlags flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak;   // center the text
            // Render the text onto the button.
            TextRenderer.DrawText(e.Graphics, "Exit", btn.Font, e.ClipRectangle, btn.ForeColor, flags);
        }

        private void ExitButton2_EnabledChanged(object sender, EventArgs e)
        {
            ExitButton2.BackgroundImage = ExitButton2.Enabled ? Properties.Resources.exiticon : Properties.Resources.exiticongreyed;
        }
    }
}
