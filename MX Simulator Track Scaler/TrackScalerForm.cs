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
using WK.Libraries.BetterFolderBrowserNS;

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
        StreamReader edinfoFile;
        StreamWriter temp_file;
        string temp_file_dir;
        string original_files_dir;
        string prev_files_dir;

        // Scalar and Multiplier
        decimal scalar_input;
        decimal multiplier;

        /*
        #################
        Terrain Variables
        #################
        */
        string folderPath;
        int terrain_scale_num;
        decimal terrain_scale;
        decimal min_height;
        decimal max_height;

        /*
        #################
        Booleans
        #################
        */
        bool folderSelected = false;
        bool scaleYValues = true;
        bool originalFilesCreated = true;


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
            edinfoCheckbox.Checked = box_checked;
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

        private void OpenFolderButton_Click(object sender, EventArgs e) {
            
            var betterFolderBrowser = new BetterFolderBrowser();

            betterFolderBrowser.Title = "Select Track Folder...";
            betterFolderBrowser.RootFolder = "C:\\";

            // Only one folder allowed.
            betterFolderBrowser.Multiselect = false;

            if (betterFolderBrowser.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(betterFolderBrowser.SelectedPath)) {

                string[] files = Directory.GetFiles(betterFolderBrowser.SelectedPath);
                if (files.Length == 0)
                {
                    ChangeLabel(fileErrLabel, Color.Red, "Error: File Incompatible");
                    return;
                }
                // Reset progress label error if the user selects new folder
                progressLabel.ResetText();
                progressBar.Visible = false;

                // store folder path
                folderPath = betterFolderBrowser.SelectedPath;
                temp_file_dir = folderPath + "\\replace.tmp";
                original_files_dir = folderPath + "\\original files";
                prev_files_dir = folderPath + "\\previous files";

                ChangeLabel(filenameLabel, Color.FromArgb(189, 193, 198), folderPath);
                folderSelected = true;
                // Close the terrain file, reset any error messages
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
            if (!folderSelected) {
                ChangeLabel(fileErrLabel, Color.Red, "Error: Select a folder");
                return;
            }

            if (!BillboardCheckBox.Checked && !StatueCheckBox.Checked && !DecalsCheckBox.Checked
                && !FlaggersCheckBox.Checked && !TimingGateCheckBox.Checked && !terrainCheckBox.Checked && !edinfoCheckbox.Checked) {
                ChangeLabel(fileCheckErrLabel, Color.Red, "No Files to Scale!");
                return;
            }

            if (UserInputTextBox.Text == string.Empty) {
                ChangeLabel(userInputErrLabel, Color.Red, "Error: Enter a factor/scale");
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
            if (!CheckBox_ErrHandling(edinfoCheckbox, "edinfo")) return;

            scaleYValues = !disableScaleYCheckBox.Checked;

            /*
            ###################################
            Get Total Lines & Set Streamreaders
            ###################################
            */

            // Total lines is initialized to 1 for the 1 line in terrain.hf
            int total_lines = 0;

            if (terrainCheckBox.Checked) total_lines++;

            // Get the line counts of each file
            if (BillboardCheckBox.Checked) {
                total_lines += File.ReadLines(folderPath + "\\billboards").Count();
            }
            if (StatueCheckBox.Checked) { 
                total_lines += File.ReadLines(folderPath + "\\statues").Count();
            }
            if (DecalsCheckBox.Checked) { 
                total_lines += File.ReadLines(folderPath + "\\decals").Count();
            }
            if (FlaggersCheckBox.Checked){ 
                total_lines += File.ReadLines(folderPath + "\\flaggers").Count();
            }
            if (TimingGateCheckBox.Checked) { 
                total_lines += File.ReadLines(folderPath + "\\timing_gates").Count();
            }
            if (edinfoCheckbox.Checked) {
                total_lines += File.ReadLines(folderPath + "\\edinfo").Count();
            }


            // Set up Progress Bar
            progressBar.Minimum = 0;
            progressBar.Maximum = total_lines;
            progressBar.ForeColor = Color.FromArgb(238, 126, 2);
            progressBar.BackColor = Color.FromArgb(227, 177, 118);
            progressBar.Visible = true;
            progressBar.Value = 0;
            progressBar.Step = 1;
            
            // Parse the user input text box and output to scalar_input variable
            if (!decimal.TryParse(UserInputTextBox.Text, out scalar_input)) {
                ChangeLabel(userInputErrLabel, Color.Red, "Enter integer or decimal value.");
                return;
            }

            multiplier = scalar_input;
            if (ToFactorRadioButton.Checked) {
                multiplier = scalar_input / terrain_scale;
            }

            // Create Folder to hold old files
            if (!System.IO.Directory.Exists(original_files_dir)) 
            {
                System.IO.Directory.CreateDirectory(original_files_dir);
                originalFilesCreated = false;

            } else if (!System.IO.Directory.Exists(prev_files_dir)) 
            {
                System.IO.Directory.CreateDirectory(prev_files_dir);
            }
            
            // Do the main work
            if (terrainCheckBox.Checked && !Do_terrain_work()) return;
            if (BillboardCheckBox.Checked && !Do_billboard_work()) return;
            if (StatueCheckBox.Checked && !Do_statue_work()) return;
            if (DecalsCheckBox.Checked && !Do_decal_work()) return;
            if (FlaggersCheckBox.Checked && !Do_flagger_work()) return;
            if (TimingGateCheckBox.Checked && !Do_timing_gate_work()) return;
            if (edinfoCheckbox.Checked && !Do_edinfo_work()) return;

            progressLabel.Location = new Point(15, 440);
            ChangeLabel(progressLabel, Color.FromArgb(189, 193, 198), "Success!");

            if (!originalFilesCreated) originalFilesCreated = true;
        }

        private bool CheckBox_ErrHandling(CheckBox box, string filename) {
            bool pass = true;
            if (box.Checked && !File.Exists(folderPath + '\\' + filename)) {
                ChangeLabel(fileCheckErrLabel, Color.Red, "Error: " + filename + " does not exist in application directory!");
                pass = false;
            }
            return pass;
        }

        private bool Check_Terrain() {
            if (!File.Exists(folderPath + "\\terrain.hf")) {
                ChangeLabel(filenameLabel, Color.Red, "Error: terrain.hf file does not exist!");
                return false;
            }

            // read data into variables
            terrain_file = File.OpenText(folderPath + "\\terrain.hf");
            string line = terrain_file.ReadLine();
            terrain_file.Close();
            string[] args = line.Split(' ');

            if (args.Length != 4)
            {
                Parse_Error("terrain.hf");
                return false;
            }

            // Read terrain.hf values into variables
            if (!int.TryParse(args[0], out terrain_scale_num))
            {
                Parse_Error("terrain.hf");
                return false;
            }
            if (!decimal.TryParse(args[1], out terrain_scale))
            {
                Parse_Error("terrain.hf");
                return false;
            }
            if (!decimal.TryParse(args[2], out min_height))
            {
                Parse_Error("terrain.hf");
                return false;
            }
            if (!decimal.TryParse(args[3], out max_height))
            {
                Parse_Error("terrain.hf");
                return false;
            }

            return true;
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
            if (File.Exists(folderPath + "\\terrain.hf")) {
                if (originalFilesCreated)
                {
                    // If a file exists in the old folder directory, delete it
                    if (File.Exists(prev_files_dir + "\\terrain.hf")) {
                        File.Delete(prev_files_dir + "\\terrain.hf");
                    }
                    // Move the terrain.hf to the previous files directory
                    File.Move(folderPath + "\\terrain.hf", prev_files_dir + "\\terrain.hf");
                } 
                else
                {
                    // Move the terrain.hf to the original files directory
                    File.Move(folderPath + "\\terrain.hf", original_files_dir + "\\terrain.hf");
                }
                
            }
            // Open the temp file
            temp_file = File.CreateText(temp_file_dir);

            // write to the file and close as we're done writing
            temp_file.WriteLine(terrain_scale_num.ToString() + ' ' + 
                scalar_output.ToString() + ' ' + min.ToString() + ' ' + max.ToString());
            temp_file.Close();

            // Rename the file to terrain.hf
            File.Move(temp_file_dir, folderPath + "\\terrain.hf");
            
            // Step the progress bar and return
            progressBar.PerformStep();
            return true;
        }

        private bool Do_billboard_work() {

            ChangeLabel(progressLabel, Color.FromArgb(189, 193, 198), "Scaling Billboards...");

            // re-open the file
            billboards_file = File.OpenText(folderPath + "\\billboards");
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

            statues_file = File.OpenText(folderPath + "\\statues");
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

            decals_file = File.OpenText(folderPath + "\\decals");
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

            flaggers_file = File.OpenText(folderPath + "\\flaggers");
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

            timing_gates_file = File.OpenText(folderPath + "\\timing_gates");
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

        private bool Do_edinfo_work() {
            ChangeLabel(progressLabel, Color.FromArgb(189, 193, 198), "Scaling Gradients...");

            edinfoFile = File.OpenText(folderPath + "\\edinfo");
            temp_file = File.CreateText(temp_file_dir);

            while (edinfoFile.EndOfStream == false) {
                string line = edinfoFile.ReadLine();
                string[] args = line.Split(' ');

                if (args[0] == "add_gradient") {
                    // Process gradient x and z points
                    // add_gradient <start_x> <start_z> <end_x> <end_z>

                    if (args.Length != 5) {
                        Parse_Error("edinfo");
                    }

                    // Parse all information
                    if (!decimal.TryParse(args[1], out decimal start_x)) {
                        Parse_Error("edinfo");
                        return false;
                    }
                    if (!decimal.TryParse(args[2], out decimal start_z)) {
                        Parse_Error("edinfo");
                        return false;
                    }
                    if (!decimal.TryParse(args[3], out decimal end_x)) {
                        Parse_Error("edinfo");
                        return false;
                    }
                    if (!decimal.TryParse(args[4], out decimal end_z)) {
                        Parse_Error("edinfo");
                        return false;
                    }

                    start_x *= multiplier;
                    start_z *= multiplier;
                    end_x *= multiplier;
                    end_z *= multiplier;

                    temp_file.WriteLine("add_gradient " + start_x.ToString() + ' ' + start_z.ToString() + ' ' + end_x.ToString() + ' ' + end_z.ToString());
                }

                else if (args[0] == "add_point") {
                    // Process gradient points
                    // add_point <0 linear, 1 curved> <distance from origin> <height>
                    if (args.Length != 4) {
                        Parse_Error("edinfo");
                    }

                    // Parse all information
                    if (!int.TryParse(args[1], out int point_type)) {
                        Parse_Error("edinfo");
                        return false;
                    }
                    if (!decimal.TryParse(args[2], out decimal origin_distance)) {
                        Parse_Error("edinfo");
                        return false;
                    }
                    if (!decimal.TryParse(args[3], out decimal height)) {
                        Parse_Error("edinfo");
                        return false;
                    }

                    origin_distance *= multiplier;
                    height *= multiplier;

                    temp_file.WriteLine("add_point " + point_type.ToString() + ' ' + origin_distance.ToString() + ' ' + height.ToString());
                }

                progressBar.PerformStep();
            }


            Move_Temp_File("edinfo", edinfoFile);
            return true;
        }

        private void Parse_Error (string filename) {
            ChangeLabel(fileCheckErrLabel, Color.Red, "Error: Incompatible " + filename +  " File");
            temp_file.Close();
            progressBar.Visible = false;
            ChangeLabel(progressLabel, Color.Red, "Failed!");
        }

        private decimal[] Get_Coords(decimal x, decimal y, decimal z) {
            decimal[] coords = new decimal[3];
            coords[0] = x * multiplier;
            coords[1] = y;
            if (scaleYValues)
            {
                coords[1] = y * multiplier;
            }
            coords[2] = z * multiplier;
            return coords;
        }

        private void Move_Temp_File(string filename, StreamReader file_to_close) {
            // move the current file to an old file, the move the temp file to the new file
            string dir_to_move = prev_files_dir;
            
            if (!originalFilesCreated)
            {
                dir_to_move = original_files_dir;
            }
            
            if (File.Exists(prev_files_dir + '\\' + filename)) {
                File.Delete(prev_files_dir + '\\' + filename);
            }

            // Close the files before moving
            file_to_close.Close();
            temp_file.Close();

            File.Move(folderPath + '\\' + filename, dir_to_move + '\\' + filename);
            File.Move(temp_file_dir, folderPath + '\\' + filename);
        }
    }
}
