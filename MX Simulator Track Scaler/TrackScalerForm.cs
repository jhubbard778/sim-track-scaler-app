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
        readonly string temp_file_dir = Environment.CurrentDirectory + "\\replace.tmp";
        readonly string old_folder_dir = Environment.CurrentDirectory + "\\old files";

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

                // Reset progress label error if the user selects new terrain.hf
                progressLabel.ResetText();
                progressBar.Visible = false;

                terrain_filepath = terrainFileOpenDialog.FileName;

                // We need to check to make sure it's a terrain.hf file
                terrain_file = File.OpenText(terrain_filepath);
                
                if (terrain_file.EndOfStream) {
                    ChangeLabel(fileErrLabel, Color.Red, "Error: File Incompatible");
                    return;
                }

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

                ChangeLabel(filenameLabel, Color.FromArgb(189, 193, 198), terrain_filepath);
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
            fileCheckErrLabel.ResetText();

            /*
            ##########################
            User Input Error Handling
            ##########################
             */
            if (!terrain_opened) {
                ChangeLabel(fileErrLabel, Color.Red, "Error: Input a terrain.hf");
                return;
            }

            if (!BillboardCheckBox.Checked && !StatueCheckBox.Checked && !DecalsCheckBox.Checked
                && !FlaggersCheckBox.Checked && !TimingGateCheckBox.Checked && !terrainCheckBox.Checked) {
                ChangeLabel(fileCheckErrLabel, Color.Red, "No Files to Scale!");
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

            // Check to make sure files weren't deleted in the process
            if (!Check_Terrain()) return;
            if (!CheckBox_ErrHandling(BillboardCheckBox, "billboards")) return;
            if (!CheckBox_ErrHandling(StatueCheckBox, "statues")) return;
            if (!CheckBox_ErrHandling(DecalsCheckBox, "decals")) return;
            if (!CheckBox_ErrHandling(FlaggersCheckBox, "flaggers")) return;
            if (!CheckBox_ErrHandling(TimingGateCheckBox, "timing_gates")) return;

            /*
            ###################################
            Get Total Lines & Set Streamreaders
            ###################################
            */

            // Total lines is initialized to 1 for the 1 line in terrain.hf
            int total_lines = 0;

            if (terrainCheckBox.Checked) total_lines++;

            // Open the files, get the line count, close the files
            if (BillboardCheckBox.Checked) {
                billboards_file = File.OpenText(Environment.CurrentDirectory + "\\billboards");
                total_lines += File.ReadLines(Environment.CurrentDirectory + "\\billboards").Count();
                billboards_file.Close();
            }
            if (StatueCheckBox.Checked) { 
                statues_file = File.OpenText(Environment.CurrentDirectory + "\\statues");
                total_lines += File.ReadLines(Environment.CurrentDirectory + "\\statues").Count();
                statues_file.Close();
            }
            if (DecalsCheckBox.Checked) { 
                decals_file = File.OpenText(Environment.CurrentDirectory + "\\decals");
                total_lines += File.ReadLines(Environment.CurrentDirectory + "\\decals").Count();
                decals_file.Close();
            }
            if (FlaggersCheckBox.Checked){ 
                flaggers_file = File.OpenText(Environment.CurrentDirectory + "\\flaggers");
                total_lines += File.ReadLines(Environment.CurrentDirectory + "\\flaggers").Count();
                flaggers_file.Close();
            }
            if (TimingGateCheckBox.Checked) { 
                timing_gates_file = File.OpenText(Environment.CurrentDirectory + "\\timing_gates");
                total_lines += File.ReadLines(Environment.CurrentDirectory + "\\timing_gates").Count();
                timing_gates_file.Close();
            }


            // Set up Progress Bar
            progressBar.Minimum = 0;
            progressBar.Maximum = total_lines;
            progressBar.ForeColor = Color.FromArgb(186, 79, 2);
            progressBar.BackColor = Color.FromArgb(227, 177, 118);
            progressBar.Visible = true;
            progressBar.Value = 0;
            progressBar.Step = 1;
            
            // Parse the user input text box and output to scalar_input variable
            decimal.TryParse(UserInputTextBox.Text, out scalar_input);

            // Reget terrain info
            terrain_file = File.OpenText(terrain_filepath);
            string line = terrain_file.ReadLine();
            terrain_file.Close();
            string[] args = line.Split(' ');

            // Re-read into variables
            decimal.TryParse(args[1], out terrain_scale);
            decimal.TryParse(args[2], out min_height);
            decimal.TryParse(args[3], out max_height);


            multiplier = scalar_input;
            if (ToFactorRadioButton.Checked) {
                multiplier = scalar_input / terrain_scale;
            }

            // Create Folder to hold old files
            if (!System.IO.Directory.Exists(old_folder_dir)) {
                System.IO.Directory.CreateDirectory(old_folder_dir);
            }

            if (terrainCheckBox.Checked) {
                if (!Do_terrain_work()) return;
            }
            else {
                ChangeLabel(fileCheckErrLabel, Color.FromArgb(189, 193, 198), "Warning: multiple scalings without changing terrain can cause incorrect outputs");
            }


            if (BillboardCheckBox.Checked) {
                if (!Do_billboard_work()) return;
            }
            if (StatueCheckBox.Checked) {
                if (!Do_statue_work()) return;
            }
            if (DecalsCheckBox.Checked) {
                if (!Do_decal_work()) return;
            }
            if (FlaggersCheckBox.Checked) {
                if (!Do_flagger_work()) return;
            }
            if (TimingGateCheckBox.Checked) {
                if (!Do_timing_gate_work()) return;
            }

            progressLabel.Location = new Point(15, 515);
            ChangeLabel(progressLabel, Color.FromArgb(189, 193, 198), "Success!");

        }

        private bool CheckBox_ErrHandling(CheckBox box, string filename) {
            bool pass = true;
            if (box.Checked && !File.Exists(Environment.CurrentDirectory + '\\' + filename)) {
                ChangeLabel(fileCheckErrLabel, Color.Red, "Error: " + filename + " does not exist in application directory!");
                pass = false;
            }
            return pass;
        }

        private bool Check_Terrain() {
            bool pass = true;
            if (!File.Exists(terrain_filepath)) {
                ChangeLabel(filenameLabel, Color.Red, "Error: terrain file does not exist anymore!");
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

            ChangeLabel(progressLabel, Color.FromArgb(189, 193, 198), "Scaling Terrain...");

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
                    progressBar.Visible = false;
                    // Change the location
                    progressLabel.Location = new Point(15, 474);
                    return false;
                }
                // If a file exists in the old folder directory, delete it
                if (File.Exists(old_folder_dir + "\\terrain.hf")) {
                    File.Delete(old_folder_dir + "\\terrain.hf");
                }
                // Move the terrain.hf to the old folder directory
                File.Move(Environment.CurrentDirectory + "\\terrain.hf", old_folder_dir + "\\terrain.hf");
            }
            // Open the temp file
            temp_file = File.CreateText(temp_file_dir);

            // write to the file and close as we're done writing
            temp_file.WriteLine(terrain_scale_num.ToString() + ' ' + 
                scalar_output.ToString() + ' ' + min.ToString() + ' ' + max.ToString());
            temp_file.Close();

            // Rename the file to terrain.hf
            File.Move(temp_file_dir, Environment.CurrentDirectory + "\\terrain.hf");
            
            // Step the progress bar and return
            progressBar.PerformStep();
            return true;
        }

        private bool Do_billboard_work() {

            ChangeLabel(progressLabel, Color.FromArgb(189, 193, 198), "Scaling Billboards...");

            // re-open the file
            billboards_file = File.OpenText(Environment.CurrentDirectory + "\\billboards");
            temp_file = File.CreateText(temp_file_dir);
            while (billboards_file.EndOfStream == false) {
                // Read the line, split it into an array
                string line = billboards_file.ReadLine();
                if (line == string.Empty) {
                    progressBar.PerformStep();
                    continue;
                }
                string[] args = line.Split(' ');

                // if the format is not in billboards format throw error
                if ((line[0] != '[' && line[0] != '\n') || !args[2].Contains("]") || args.Length != 6) {
                    Parse_Error("Billboards");
                    return false;
                }

                // replace brackets
                args[0] = args[0].Replace("[", string.Empty);
                args[2] = args[2].Replace("]", string.Empty);

                // parse coords and sizes
                if (!decimal.TryParse(args[0], out decimal x)) {
                    Parse_Error("Billboards");
                    return false;
                }
                if (!decimal.TryParse(args[1], out decimal y)) {
                    Parse_Error("Billboards");
                    return false;
                }
                if (!decimal.TryParse(args[2], out decimal z)) {
                    Parse_Error("Billboards");
                    return false;
                }
                if (!decimal.TryParse(args[3], out decimal size)) {
                    Parse_Error("Billboards");
                    return false;
                }

                string aspect = args[4];
                string png = args[5];

                // calculate new coords / size
                decimal[] coords = Get_Coords(x, y ,z);
                decimal new_size = size * multiplier;

                // write to file
                temp_file.WriteLine('[' + coords[0].ToString() + ' ' + coords[1].ToString() + ' ' + coords[2] + "] " +
                    new_size.ToString() + ' ' + aspect + ' ' + png);

                // step the progress bar
                progressBar.PerformStep();

            }

            // move the current file to an old file, the move the temp file to the new file
            Move_Temp_File("billboards", billboards_file);

            return true;
        }

        private bool Do_statue_work() {
            ChangeLabel(progressLabel, Color.FromArgb(189, 193, 198), "Scaling Statues...");

            statues_file = File.OpenText(Environment.CurrentDirectory + "\\statues");
            temp_file = File.CreateText(temp_file_dir);
            while (statues_file.EndOfStream == false) {
                // Read the line, split it into an array
                string line = statues_file.ReadLine();
                if (line == string.Empty) {
                    progressBar.PerformStep();
                    continue;
                }

                string[] args = line.Split(' ');

                // if the format is not in statues format throw error
                if ((line[0] != '[' && line[0] != '\n') || !args[2].Contains("]") || args.Length != 7) {
                    Parse_Error("Statues");
                    return false;
                }

                // replace brackets
                args[0] = args[0].Replace("[", string.Empty);
                args[2] = args[2].Replace("]", string.Empty);

                // parse coords and sizes
                if (!decimal.TryParse(args[0], out decimal x)) {
                    Parse_Error("Statues");
                    return false;
                }
                if (!decimal.TryParse(args[1], out decimal y)) {
                    Parse_Error("Statues");
                    return false;
                }
                if (!decimal.TryParse(args[2], out decimal z)) {
                    Parse_Error("Statues");
                    return false;
                }

                string angle = args[3];
                string jm = args[4];
                string png = args[5];
                string shp = args[6];

                // calculate new coords
                decimal[] coords = Get_Coords(x, y, z);

                // write to file
                temp_file.WriteLine('[' + coords[0].ToString() + ' ' + coords[1].ToString() + ' ' + coords[2].ToString() + "] " +
                    angle + ' ' + jm + ' ' + png + ' ' + shp);

                // step the progress bar
                progressBar.PerformStep();

            }

            Move_Temp_File("statues", statues_file);

            return true;
        }

        private bool Do_decal_work() {
            ChangeLabel(progressLabel, Color.FromArgb(189, 193, 198), "Scaling Decals...");

            decals_file = File.OpenText(Environment.CurrentDirectory + "\\decals");
            temp_file = File.CreateText(temp_file_dir);
            while (decals_file.EndOfStream == false) {

                // Read the line, split it into an array
                string line = decals_file.ReadLine();
                if (line == string.Empty) {
                    progressBar.PerformStep();
                    continue;
                }
                string[] args = line.Split(' ');

                // if the format is not in decals throw error
                if ((line[0] != '[' && line[0] != '\n') || !args[1].Contains("]") || args.Length != 6) {
                    Parse_Error("Decals");
                    return false;
                }
                // replace brackets
                args[0] = args[0].Replace("[", string.Empty);
                args[1] = args[1].Replace("]", string.Empty);

                if (!decimal.TryParse(args[0], out decimal x)) {
                    Parse_Error("Decals");
                    return false;
                }

                if (!decimal.TryParse(args[1], out decimal z)) {
                    Parse_Error("Decals");
                    return false;
                }

                string angle = args[2];
                if (!decimal.TryParse(args[3], out decimal size)) {
                    Parse_Error("Decals");
                    return false;
                }
                string aspect = args[4];
                string png = args[5];

                // new coords / sizes
                decimal[] coords = Get_Coords(x, 0, z);
                decimal new_size = size * multiplier;

                temp_file.WriteLine('[' + coords[0].ToString() + ' ' + coords[2].ToString() + "] " + angle +  ' ' +
                    new_size.ToString() + ' ' + aspect + ' ' + png);

                // step the progress bar
                progressBar.PerformStep();

            }

            Move_Temp_File("decals", decals_file);
            return true;
        }

        private bool Do_flagger_work() {
            ChangeLabel(progressLabel, Color.FromArgb(189, 193, 198), "Scaling Flaggers...");

            flaggers_file = File.OpenText(Environment.CurrentDirectory + "\\flaggers");
            temp_file = File.CreateText(temp_file_dir);
            while (flaggers_file.EndOfStream == false) {
                string line = flaggers_file.ReadLine();
                if (line == string.Empty || line[0] != '[') {
                    temp_file.WriteLine(line);
                    progressBar.PerformStep();
                    continue;
                }
                string[] args = line.Split(' ');

                if (args.Length != 3 || !args[0].Contains("[") || !args[2].Contains("]")) {
                    Parse_Error("Flaggers");
                    return false;
                }

                // replace brackets
                args[0] = args[0].Replace("[", string.Empty);
                args[2] = args[2].Replace("]", string.Empty);

                // Parse coords
                if (!decimal.TryParse(args[0], out decimal x)) {
                    Parse_Error("Flaggers");
                    return false;
                }
                if (!decimal.TryParse(args[1], out decimal y)) {
                    Parse_Error("Flaggers");
                    return false;
                }
                if (!decimal.TryParse(args[2], out decimal z)) {
                    Parse_Error("Flaggers");
                    return false;
                }

                // calculate new coords
                decimal[] coords = Get_Coords(x, y, z);

                // write to file
                temp_file.WriteLine('[' + coords[0].ToString() + ' ' + coords[1].ToString() + ' ' + coords[2].ToString() + ']');

                // step the progress bar
                progressBar.PerformStep();
            }

            Move_Temp_File("flaggers", flaggers_file);
            return true;
        }

        private bool Do_timing_gate_work() {
            ChangeLabel(progressLabel, Color.FromArgb(189, 193, 198), "Scaling Timing Gates...");

            timing_gates_file = File.OpenText(Environment.CurrentDirectory + "\\timing_gates");
            temp_file = File.CreateText(temp_file_dir);
            int timing_gate_num = 0;
            while (timing_gates_file.EndOfStream == false) {
                string line = timing_gates_file.ReadLine();

                if (line == "startinggate:") {
                    temp_file.WriteLine(line);
                    progressBar.PerformStep();
                    continue;
                }
                if (line == "checkpoints:") {
                    temp_file.WriteLine(line);
                    timing_gate_num = 1;
                    progressBar.PerformStep();
                    continue;
                }

                if (line == "firstlap:") timing_gate_num = 2;

                string[] args = line.Split(' ');

                // Do First Segment
                if (timing_gate_num == 0) {
                    // Check lines to make sure they're valid
                    if (args.Length < 4 || (line[0] != '[' && line[0] != '\n') || !args[2].Contains("]")) {
                        Parse_Error("Timing Gates");
                        return false;
                    }

                    args[0] = args[0].Replace("[", string.Empty);
                    args[2] = args[2].Replace("]", string.Empty);
                    
                    // parse coords and get angle
                    if (!decimal.TryParse(args[0], out decimal x)) {
                        Parse_Error("Timing Gates");
                        return false;
                    }
                    if (!decimal.TryParse(args[1], out decimal y)) {
                        Parse_Error("Timing Gates");
                        return false;
                    }
                    if (!decimal.TryParse(args[2], out decimal z)) {
                        Parse_Error("Timing Gates");
                        return false;
                    }
                    
                    string angle = args[3];

                    // calculate new coords
                    decimal[] coords = Get_Coords(x, y, z);

                    // set default outline
                    string out_line = '[' + coords[0].ToString() + ' ' + coords[1].ToString() + ' ' 
                        + coords[2].ToString() + "] " + angle;

                    // if they have jm, png, and shp add it
                    if (args.Length == 7) {
                        string jm = args[4];
                        string png = args[5];
                        string shp = args[6];
                        out_line += ' ' + jm + ' ' + png + ' ' + shp;
                    }

                    temp_file.WriteLine(out_line);

                }
                // Do Second Segment
                else if (timing_gate_num == 1) {

                    // Check lines to make sure they're valid
                    if (args.Length != 7 || !args[1].Contains("[") || !args[3].Contains("]")
                        || !args[4].Contains("[") || !args[6].Contains("]")) {
                        Parse_Error("Timing Gates");
                        return false;
                    }

                    // Replace Brackets
                    args[1] = args[1].Replace("[", string.Empty);
                    args[3] = args[3].Replace("]", string.Empty);
                    args[4] = args[4].Replace("[", string.Empty);
                    args[6] = args[6].Replace("]", string.Empty);

                    // Parse size and coords into variables
                    if (!decimal.TryParse(args[0], out decimal size)) {
                        Parse_Error("Timing Gates");
                        return false;
                    }
                    if (!decimal.TryParse(args[1], out decimal x1)) {
                        Parse_Error("Timing Gates");
                        return false;
                    }
                    if (!decimal.TryParse(args[2], out decimal y1)) {
                        Parse_Error("Timing Gates");
                        return false;
                    }
                    if (!decimal.TryParse(args[3], out decimal z1)) {
                        Parse_Error("Timing Gates");
                        return false;
                    }
                    if (!decimal.TryParse(args[4], out decimal x2)) {
                        Parse_Error("Timing Gates");
                        return false;
                    }
                    if (!decimal.TryParse(args[5], out decimal y2)) {
                        Parse_Error("Timing Gates");
                        return false;
                    }
                    if (!decimal.TryParse(args[6], out decimal z2)) {
                        Parse_Error("Timing Gates");
                        return false;
                    }

                    // Calculate new size & coords
                    decimal new_size = size * multiplier;
                    decimal[] coords1 = Get_Coords(x1, y1, z1);
                    decimal[] coords2 = Get_Coords(x2, y2, z2);

                    // Write to file
                    temp_file.WriteLine(new_size.ToString() + " [" + coords1[0].ToString() + ' ' + coords1[1].ToString()
                        + ' ' + coords1[2].ToString() + "] [" + coords2[0].ToString() + ' ' +
                        coords2[1].ToString() + ' ' + coords2[2].ToString() + ']');


                }
                // Do Final Segment
                else temp_file.WriteLine(line);

                // step the progress bar
                progressBar.PerformStep();
            }

            Move_Temp_File("timing_gates", timing_gates_file);
            return true;
        }

        private void Parse_Error (string filename) {
            ChangeLabel(fileCheckErrLabel, Color.Red, "Error: Incompatible " + filename +  " File");
            temp_file.Close();
            progressBar.Visible = false;
            ChangeLabel(progressLabel, Color.Red, "Failed!");
        }

        private decimal[] Get_Coords (decimal x, decimal y, decimal z) {
            decimal[] coords = new decimal[3];
            coords[0] = x * multiplier;
            coords[1] = y * multiplier;
            coords[2] = z * multiplier;
            return coords;
        }

        private void Move_Temp_File(string filename, StreamReader file_to_close) {
            // move the current file to an old file, the move the temp file to the new file
            if (File.Exists(old_folder_dir + '\\' + filename)) {
                File.Delete(old_folder_dir + '\\' + filename);
            }

            // Close the files before moving
            file_to_close.Close();
            temp_file.Close();

            File.Move(Environment.CurrentDirectory + '\\' + filename, old_folder_dir + '\\' + filename);
            File.Move(temp_file_dir, Environment.CurrentDirectory + '\\' + filename);
        }
    }
}
