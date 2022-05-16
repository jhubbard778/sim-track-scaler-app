using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MX_Simulator_Track_Scaler
{
    public partial class TrackScalerForm : Form
    {
        public TrackScalerForm()
        {
            InitializeComponent();
        }

        /*
        ##############
        File Variables
        ##############
        */
        StreamReader terrain_file;
        StreamReader billboards_file;
        StreamReader statues_file;
        StreamReader decals_file;
        StreamReader flaggers_file;
        StreamReader timing_gates_file;
        StreamWriter temp_file;

        // Scalar and Multiplier
        decimal scalar_input;
        decimal multiplier;

        /*
        #################
        Terrain Variables
        #################
        */
        string terrain_filepath;
        int terrain_scale_num;
        decimal terrain_scale;
        decimal min_height;
        decimal max_height;
        decimal[] coords;

        /*
        #################
        Booleans
        #################
        */
        bool terrain_opened = false;


        private void TrackScalerForm_Load(object sender, EventArgs e) {
            fileErrLabel.ResetText();
            fileCheckErrLabel.ResetText();
            methodErrLabel.ResetText();
            scaleErrLabel.ResetText();
            progressLabel.ResetText();
            userInputErrLabel.ResetText();
            filenameLabel.ResetText();
        }

        private void AllCheckBox_CheckedChanged(object sender, EventArgs e) {
            bool box_checked = false;
            if (AllCheckBox.Checked) {
                box_checked = true;
            }
            BillboardCheckBox.Checked = box_checked;
            StatueCheckBox.Checked = box_checked;
            DecalsCheckBox.Checked = box_checked;
            FlaggersCheckBox.Checked = box_checked;
            TimingGateCheckBox.Checked = box_checked;
            terrainCheckBox.Checked = box_checked;
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

        private void OpenTerrainButton_Click(object sender, EventArgs e) {

            // Set the initial directory
            terrainFileOpenDialog.InitialDirectory = Environment.CurrentDirectory;

            // Filter by hf or all files
            terrainFileOpenDialog.Filter = "HF Files (*.hf)|*.hf|All Files|*.*";

            if (terrainFileOpenDialog.ShowDialog() == DialogResult.OK) {

                terrain_filepath = terrainFileOpenDialog.FileName;

                // We need to check to make sure it's a terrain.hf file
                terrain_file = File.OpenText(terrain_filepath);
                

                string line = terrain_file.ReadLine();
                string[] args = line.Split(' ');

                // if we don't have 4 arguments or the first argument isn't an int, then we have an incompatible file
                if (args.Length != 4 || !int.TryParse(args[0], out _)) {
                    ChangeLabel(fileErrLabel, Color.Red, "Error: File Incompatible");
                    return;
                }

                // if the rest of the args decimal numbers, then we have an incompatible file
                for (int i = 1; i < args.Length; i++) {
                    if (!decimal.TryParse(args[i], out _)) {
                        ChangeLabel(fileErrLabel, Color.Red, "Error: File Incompatible");
                        return;
                    }
                }

                // Set the global variables
                int.TryParse(args[0], out terrain_scale_num);
                decimal.TryParse(args[1], out terrain_scale);
                decimal.TryParse(args[2], out min_height);
                decimal.TryParse(args[3], out max_height);

                filenameLabel.Text = terrain_filepath;
                filenameLabel.TextAlign = ContentAlignment.MiddleCenter;
                terrain_opened = true;
                // Close the terrain file, reset any error messages
                terrain_file.Close();
                fileErrLabel.ResetText();
            }
        }

        private void ChangeLabel(Label label, Color color, string msg) {
            label.ForeColor = color;
            label.Text = msg;
        }

        private void RadioButton_CheckChanged(object sender, EventArgs e) {
            methodErrLabel.ResetText();
        }

        private void ScaleButton_Click(object sender, EventArgs e) {

            progressLabel.ResetText();

            /*
            ##########################
            User Input Error Handling
            ##########################
             */
            if (!terrain_opened) {
                ChangeLabel(fileErrLabel, Color.Red, "Error: Input a terrain.hf");
                return;
            }

            if (UserInputTextBox.Text == string.Empty) {
                ChangeLabel(userInputErrLabel, Color.Red, "Error: Enter a scale/factor");
                return;
            }

            if (!ByFactorRadioButton.Checked && !ToFactorRadioButton.Checked) {
                ChangeLabel(methodErrLabel, Color.Red, "Error: Select a Method");
                return;
            }

            if (!CheckBox_ErrHandling(BillboardCheckBox, "billboards")) return;
            if (!CheckBox_ErrHandling(StatueCheckBox, "statues")) return;
            if (!CheckBox_ErrHandling(DecalsCheckBox, "decals")) return;
            if (!CheckBox_ErrHandling(FlaggersCheckBox, "flaggers")) return;
            if (!CheckBox_ErrHandling(TimingGateCheckBox, "timing_gates")) return;


            if (!BillboardCheckBox.Checked && !StatueCheckBox.Checked && !DecalsCheckBox.Checked
                && !FlaggersCheckBox.Checked && !TimingGateCheckBox.Checked && !terrainCheckBox.Checked) {
                ChangeLabel(fileCheckErrLabel, Color.Red, "No Files to Scale!");
                return;
            }

            /*
            ###################################
            Get Total Lines & Set Streamreaders
            ###################################
            */

            // Total lines is initialized to 1 for the 1 line in terrain.hf
            int total_lines = 1;

            if (BillboardCheckBox.Checked) {
                billboards_file = File.OpenText(Environment.CurrentDirectory + "\\billboards");
                total_lines += File.ReadLines(Environment.CurrentDirectory + "\\billboards").Count();
            }
            if (StatueCheckBox.Checked) { 
                statues_file = File.OpenText(Environment.CurrentDirectory + "\\statues");
                total_lines += File.ReadLines(Environment.CurrentDirectory + "\\statues").Count();
            }
            if (DecalsCheckBox.Checked) { 
                decals_file = File.OpenText(Environment.CurrentDirectory + "\\decals");
                total_lines += File.ReadLines(Environment.CurrentDirectory + "\\decals").Count();
            }
            if (FlaggersCheckBox.Checked){ 
                flaggers_file = File.OpenText(Environment.CurrentDirectory + "\\flaggers");
                total_lines += File.ReadLines(Environment.CurrentDirectory + "\\flaggers").Count();
            }
            if (TimingGateCheckBox.Checked) { 
                timing_gates_file = File.OpenText(Environment.CurrentDirectory + "\\timing_gates");
                total_lines += File.ReadLines(Environment.CurrentDirectory + "\\timing_gates").Count();
            }


            // Set up Progress Bar
            progressBar.Minimum = 0;
            progressBar.Maximum = total_lines;
            progressBar.Visible = true;
            progressBar.Value = 0;
            progressBar.Step = 1;
            
            // Parse the user input text box and output to scalar_input variable
            decimal.TryParse(UserInputTextBox.Text, out scalar_input);

            multiplier = scalar_input;
            if (ToFactorRadioButton.Checked) {
                multiplier = scalar_input / terrain_scale;
            }

            if (terrainCheckBox.Checked) {
                if (!Do_terrain_work()) return;
            }
            

            if (BillboardCheckBox.Checked) Do_billboard_work();
            if (StatueCheckBox.Checked) Do_statue_work();
            if (DecalsCheckBox.Checked) Do_decal_work();
            if (FlaggersCheckBox.Checked) Do_flagger_work();
            if (TimingGateCheckBox.Checked) Do_timing_gate_work();

            fileCheckErrLabel.ResetText();

            ChangeLabel(progressLabel, Color.Green, "Success!");

        }

        private bool CheckBox_ErrHandling(CheckBox box, string filename) {
            bool pass = true;
            if (box.Checked && !File.Exists(Environment.CurrentDirectory + '\\' + filename)) {
                ChangeLabel(fileCheckErrLabel, Color.Red, "Error: " + filename + " does not exist in application directory!");
                pass = false;
            }
            return pass;
        }

        private void UserInputTextBox_TextChanged(object sender, EventArgs e) {
            if (UserInputTextBox.TextLength == 1) {
                userInputErrLabel.ResetText();
            }
        }

        private bool Do_terrain_work() {

            ChangeLabel(progressLabel, Color.Black, "Scaling Terrain...");

            decimal scalar_output = scalar_input;
            if (ByFactorRadioButton.Checked) {
                scalar_output = scalar_input * terrain_scale;
            }
            decimal min = min_height * multiplier;
            decimal max = max_height * multiplier;

            // If a terrain.hf file exists in the current directory and the user didn't send that file in,
            // throw an error.
            if (File.Exists(Environment.CurrentDirectory + "\\terrain.hf")) {
                if (Environment.CurrentDirectory + "\\terrain.hf" != terrain_filepath) {
                    ChangeLabel(progressLabel, Color.Red, "There is a file in the application directory and you didn't select it.  " +
                        "Either delete the file in the application directory or select the file in the application directory.");
                    return false;
                }
                File.Delete(Environment.CurrentDirectory + "\\terrain.hf");
            }
            // Open the temp file
            temp_file = File.CreateText(Environment.CurrentDirectory + "\\replace.tmp");
            // write to the file and close as we're done writing
            temp_file.WriteLine(terrain_scale_num.ToString() + ' ' + 
                scalar_output.ToString() + ' ' + min.ToString() + ' ' + max.ToString());
            temp_file.Close();

            // Rename the file to terrain.hf
            File.Move(Environment.CurrentDirectory + "\\replace.tmp", Environment.CurrentDirectory + "\\terrain.hf");
            
            // Step the progress bar and return
            progressBar.PerformStep();
            return true;
        }

        private void Do_billboard_work() {
            billboards_file.Close();
        }

        private void Do_statue_work() {
            statues_file.Close();
        }

        private void Do_decal_work() {
            decals_file.Close();
        }

        private void Do_flagger_work() {
            flaggers_file.Close();
        }

        private void Do_timing_gate_work() {
            timing_gates_file.Close();
        }
    }
}
