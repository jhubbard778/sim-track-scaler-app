namespace MX_Simulator_Track_Scaler
{
    partial class TrackScalerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrackScalerForm));
            this.OpenFolderButton = new System.Windows.Forms.Button();
            this.fileErrLabel = new System.Windows.Forms.Label();
            this.FilesGroupBox = new System.Windows.Forms.GroupBox();
            this.terrainCheckBox = new System.Windows.Forms.CheckBox();
            this.AllCheckBox = new System.Windows.Forms.CheckBox();
            this.TimingGateCheckBox = new System.Windows.Forms.CheckBox();
            this.FlaggersCheckBox = new System.Windows.Forms.CheckBox();
            this.DecalsCheckBox = new System.Windows.Forms.CheckBox();
            this.StatueCheckBox = new System.Windows.Forms.CheckBox();
            this.BillboardCheckBox = new System.Windows.Forms.CheckBox();
            this.MethodGroupBox = new System.Windows.Forms.GroupBox();
            this.ToFactorRadioButton = new System.Windows.Forms.RadioButton();
            this.ByFactorRadioButton = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.methodErrLabel = new System.Windows.Forms.Label();
            this.fileCheckErrLabel = new System.Windows.Forms.Label();
            this.ScaleButton = new System.Windows.Forms.Button();
            this.UserInputTextBox = new System.Windows.Forms.TextBox();
            this.progressLabel = new System.Windows.Forms.Label();
            this.userInputErrLabel = new System.Windows.Forms.Label();
            this.filenameLabel = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.edinfoCheckbox = new System.Windows.Forms.CheckBox();
            this.progressBar = new QuantumConcepts.Common.Forms.UI.Controls.NewProgressBar();
            this.FilesGroupBox.SuspendLayout();
            this.MethodGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // OpenFolderButton
            // 
            this.OpenFolderButton.BackColor = System.Drawing.SystemColors.Control;
            this.OpenFolderButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.OpenFolderButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.OpenFolderButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.OpenFolderButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.OpenFolderButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OpenFolderButton.ForeColor = System.Drawing.Color.Black;
            this.OpenFolderButton.Location = new System.Drawing.Point(238, 12);
            this.OpenFolderButton.Name = "OpenFolderButton";
            this.OpenFolderButton.Size = new System.Drawing.Size(106, 31);
            this.OpenFolderButton.TabIndex = 0;
            this.OpenFolderButton.Text = "Choose Folder";
            this.OpenFolderButton.UseVisualStyleBackColor = false;
            this.OpenFolderButton.Click += new System.EventHandler(this.OpenFolderButton_Click);
            // 
            // fileErrLabel
            // 
            this.fileErrLabel.AutoSize = true;
            this.fileErrLabel.BackColor = System.Drawing.Color.Transparent;
            this.fileErrLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileErrLabel.Location = new System.Drawing.Point(352, 20);
            this.fileErrLabel.Name = "fileErrLabel";
            this.fileErrLabel.Size = new System.Drawing.Size(45, 16);
            this.fileErrLabel.TabIndex = 1;
            this.fileErrLabel.Text = "label1";
            // 
            // FilesGroupBox
            // 
            this.FilesGroupBox.BackColor = System.Drawing.Color.Transparent;
            this.FilesGroupBox.Controls.Add(this.edinfoCheckbox);
            this.FilesGroupBox.Controls.Add(this.terrainCheckBox);
            this.FilesGroupBox.Controls.Add(this.AllCheckBox);
            this.FilesGroupBox.Controls.Add(this.TimingGateCheckBox);
            this.FilesGroupBox.Controls.Add(this.FlaggersCheckBox);
            this.FilesGroupBox.Controls.Add(this.DecalsCheckBox);
            this.FilesGroupBox.Controls.Add(this.StatueCheckBox);
            this.FilesGroupBox.Controls.Add(this.BillboardCheckBox);
            this.FilesGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FilesGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(193)))), ((int)(((byte)(198)))));
            this.FilesGroupBox.Location = new System.Drawing.Point(165, 70);
            this.FilesGroupBox.Name = "FilesGroupBox";
            this.FilesGroupBox.Size = new System.Drawing.Size(249, 112);
            this.FilesGroupBox.TabIndex = 3;
            this.FilesGroupBox.TabStop = false;
            this.FilesGroupBox.Text = "Files To Scale";
            // 
            // terrainCheckBox
            // 
            this.terrainCheckBox.AutoSize = true;
            this.terrainCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.terrainCheckBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.terrainCheckBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.terrainCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.terrainCheckBox.Location = new System.Drawing.Point(6, 30);
            this.terrainCheckBox.Name = "terrainCheckBox";
            this.terrainCheckBox.Size = new System.Drawing.Size(70, 20);
            this.terrainCheckBox.TabIndex = 23;
            this.terrainCheckBox.Text = "Terrain";
            this.terrainCheckBox.UseVisualStyleBackColor = false;
            // 
            // AllCheckBox
            // 
            this.AllCheckBox.AutoSize = true;
            this.AllCheckBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AllCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AllCheckBox.Location = new System.Drawing.Point(181, 56);
            this.AllCheckBox.Name = "AllCheckBox";
            this.AllCheckBox.Size = new System.Drawing.Size(42, 20);
            this.AllCheckBox.TabIndex = 9;
            this.AllCheckBox.Text = "All";
            this.AllCheckBox.UseVisualStyleBackColor = true;
            this.AllCheckBox.CheckedChanged += new System.EventHandler(this.AllCheckBox_CheckedChanged);
            // 
            // TimingGateCheckBox
            // 
            this.TimingGateCheckBox.AutoSize = true;
            this.TimingGateCheckBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.TimingGateCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimingGateCheckBox.Location = new System.Drawing.Point(91, 82);
            this.TimingGateCheckBox.Name = "TimingGateCheckBox";
            this.TimingGateCheckBox.Size = new System.Drawing.Size(107, 20);
            this.TimingGateCheckBox.TabIndex = 8;
            this.TimingGateCheckBox.Text = "Timing Gates";
            this.TimingGateCheckBox.UseVisualStyleBackColor = true;
            // 
            // FlaggersCheckBox
            // 
            this.FlaggersCheckBox.AutoSize = true;
            this.FlaggersCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.FlaggersCheckBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.FlaggersCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FlaggersCheckBox.Location = new System.Drawing.Point(91, 56);
            this.FlaggersCheckBox.Name = "FlaggersCheckBox";
            this.FlaggersCheckBox.Size = new System.Drawing.Size(81, 20);
            this.FlaggersCheckBox.TabIndex = 7;
            this.FlaggersCheckBox.Text = "Flaggers";
            this.FlaggersCheckBox.UseVisualStyleBackColor = false;
            // 
            // DecalsCheckBox
            // 
            this.DecalsCheckBox.AutoSize = true;
            this.DecalsCheckBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DecalsCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DecalsCheckBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(193)))), ((int)(((byte)(198)))));
            this.DecalsCheckBox.Location = new System.Drawing.Point(91, 30);
            this.DecalsCheckBox.Name = "DecalsCheckBox";
            this.DecalsCheckBox.Size = new System.Drawing.Size(70, 20);
            this.DecalsCheckBox.TabIndex = 6;
            this.DecalsCheckBox.Text = "Decals";
            this.DecalsCheckBox.UseVisualStyleBackColor = true;
            // 
            // StatueCheckBox
            // 
            this.StatueCheckBox.AutoSize = true;
            this.StatueCheckBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.StatueCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatueCheckBox.Location = new System.Drawing.Point(6, 82);
            this.StatueCheckBox.Name = "StatueCheckBox";
            this.StatueCheckBox.Size = new System.Drawing.Size(72, 20);
            this.StatueCheckBox.TabIndex = 5;
            this.StatueCheckBox.Text = "Statues";
            this.StatueCheckBox.UseVisualStyleBackColor = true;
            // 
            // BillboardCheckBox
            // 
            this.BillboardCheckBox.AutoSize = true;
            this.BillboardCheckBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BillboardCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BillboardCheckBox.Location = new System.Drawing.Point(6, 56);
            this.BillboardCheckBox.Name = "BillboardCheckBox";
            this.BillboardCheckBox.Size = new System.Drawing.Size(88, 20);
            this.BillboardCheckBox.TabIndex = 4;
            this.BillboardCheckBox.Text = "Billboards";
            this.BillboardCheckBox.UseVisualStyleBackColor = true;
            // 
            // MethodGroupBox
            // 
            this.MethodGroupBox.BackColor = System.Drawing.Color.Transparent;
            this.MethodGroupBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.MethodGroupBox.Controls.Add(this.ToFactorRadioButton);
            this.MethodGroupBox.Controls.Add(this.ByFactorRadioButton);
            this.MethodGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MethodGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MethodGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(193)))), ((int)(((byte)(198)))));
            this.MethodGroupBox.Location = new System.Drawing.Point(173, 279);
            this.MethodGroupBox.Name = "MethodGroupBox";
            this.MethodGroupBox.Size = new System.Drawing.Size(232, 100);
            this.MethodGroupBox.TabIndex = 13;
            this.MethodGroupBox.TabStop = false;
            this.MethodGroupBox.Text = "Scale Method";
            // 
            // ToFactorRadioButton
            // 
            this.ToFactorRadioButton.AutoSize = true;
            this.ToFactorRadioButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ToFactorRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ToFactorRadioButton.Location = new System.Drawing.Point(21, 64);
            this.ToFactorRadioButton.Name = "ToFactorRadioButton";
            this.ToFactorRadioButton.Size = new System.Drawing.Size(193, 28);
            this.ToFactorRadioButton.TabIndex = 15;
            this.ToFactorRadioButton.TabStop = true;
            this.ToFactorRadioButton.Text = "Scale to Input Scale";
            this.ToFactorRadioButton.UseVisualStyleBackColor = true;
            this.ToFactorRadioButton.CheckedChanged += new System.EventHandler(this.RadioButton_CheckChanged);
            // 
            // ByFactorRadioButton
            // 
            this.ByFactorRadioButton.AutoSize = true;
            this.ByFactorRadioButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ByFactorRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ByFactorRadioButton.Location = new System.Drawing.Point(21, 30);
            this.ByFactorRadioButton.Name = "ByFactorRadioButton";
            this.ByFactorRadioButton.Size = new System.Drawing.Size(204, 28);
            this.ByFactorRadioButton.TabIndex = 14;
            this.ByFactorRadioButton.TabStop = true;
            this.ByFactorRadioButton.Text = "Scale by Input Factor";
            this.ByFactorRadioButton.UseVisualStyleBackColor = true;
            this.ByFactorRadioButton.CheckedChanged += new System.EventHandler(this.RadioButton_CheckChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(193)))), ((int)(((byte)(198)))));
            this.label2.Location = new System.Drawing.Point(233, 207);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 24);
            this.label2.TabIndex = 22;
            this.label2.Text = "Scale/Factor";
            // 
            // methodErrLabel
            // 
            this.methodErrLabel.BackColor = System.Drawing.Color.Transparent;
            this.methodErrLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.methodErrLabel.Location = new System.Drawing.Point(173, 381);
            this.methodErrLabel.Name = "methodErrLabel";
            this.methodErrLabel.Size = new System.Drawing.Size(232, 24);
            this.methodErrLabel.TabIndex = 15;
            this.methodErrLabel.Text = "label5";
            this.methodErrLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fileCheckErrLabel
            // 
            this.fileCheckErrLabel.BackColor = System.Drawing.Color.Transparent;
            this.fileCheckErrLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileCheckErrLabel.Location = new System.Drawing.Point(15, 187);
            this.fileCheckErrLabel.Name = "fileCheckErrLabel";
            this.fileCheckErrLabel.Size = new System.Drawing.Size(553, 18);
            this.fileCheckErrLabel.TabIndex = 10;
            this.fileCheckErrLabel.Text = "label3";
            this.fileCheckErrLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ScaleButton
            // 
            this.ScaleButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(126)))), ((int)(((byte)(2)))));
            this.ScaleButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ScaleButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ScaleButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(54)))), ((int)(((byte)(2)))));
            this.ScaleButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(88)))), ((int)(((byte)(2)))));
            this.ScaleButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ScaleButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScaleButton.ForeColor = System.Drawing.Color.White;
            this.ScaleButton.Location = new System.Drawing.Point(211, 408);
            this.ScaleButton.Name = "ScaleButton";
            this.ScaleButton.Size = new System.Drawing.Size(158, 47);
            this.ScaleButton.TabIndex = 15;
            this.ScaleButton.Text = "Scale";
            this.ScaleButton.UseVisualStyleBackColor = false;
            this.ScaleButton.Click += new System.EventHandler(this.ScaleButton_Click);
            // 
            // UserInputTextBox
            // 
            this.UserInputTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.UserInputTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UserInputTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserInputTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(193)))), ((int)(((byte)(198)))));
            this.UserInputTextBox.Location = new System.Drawing.Point(238, 234);
            this.UserInputTextBox.MaxLength = 14;
            this.UserInputTextBox.Name = "UserInputTextBox";
            this.UserInputTextBox.Size = new System.Drawing.Size(106, 22);
            this.UserInputTextBox.TabIndex = 11;
            this.UserInputTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.UserInputTextBox.TextChanged += new System.EventHandler(this.UserInputTextBox_TextChanged);
            this.UserInputTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.UserInputTextBox_KeyPress);
            // 
            // progressLabel
            // 
            this.progressLabel.BackColor = System.Drawing.Color.Transparent;
            this.progressLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.progressLabel.Location = new System.Drawing.Point(16, 515);
            this.progressLabel.Name = "progressLabel";
            this.progressLabel.Size = new System.Drawing.Size(556, 46);
            this.progressLabel.TabIndex = 19;
            this.progressLabel.Text = "label7";
            this.progressLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // userInputErrLabel
            // 
            this.userInputErrLabel.BackColor = System.Drawing.Color.Transparent;
            this.userInputErrLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userInputErrLabel.Location = new System.Drawing.Point(175, 260);
            this.userInputErrLabel.Name = "userInputErrLabel";
            this.userInputErrLabel.Size = new System.Drawing.Size(232, 16);
            this.userInputErrLabel.TabIndex = 12;
            this.userInputErrLabel.Text = "label4";
            this.userInputErrLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // filenameLabel
            // 
            this.filenameLabel.BackColor = System.Drawing.Color.Transparent;
            this.filenameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filenameLabel.Location = new System.Drawing.Point(12, 46);
            this.filenameLabel.Name = "filenameLabel";
            this.filenameLabel.Size = new System.Drawing.Size(556, 25);
            this.filenameLabel.TabIndex = 2;
            this.filenameLabel.Text = "label2";
            this.filenameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::MX_Simulator_Track_Scaler.Properties.Resources.cube_icon_kjijxo;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(444, 76);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(113, 108);
            this.pictureBox1.TabIndex = 24;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = global::MX_Simulator_Track_Scaler.Properties.Resources.racing_flag_icon_race_checker_chequred_checkered_flag_tom_hill_transparent;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox2.Location = new System.Drawing.Point(28, 76);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(117, 108);
            this.pictureBox2.TabIndex = 25;
            this.pictureBox2.TabStop = false;
            // 
            // edinfoCheckbox
            // 
            this.edinfoCheckbox.AutoSize = true;
            this.edinfoCheckbox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.edinfoCheckbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.edinfoCheckbox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(193)))), ((int)(((byte)(198)))));
            this.edinfoCheckbox.Location = new System.Drawing.Point(181, 30);
            this.edinfoCheckbox.Name = "edinfoCheckbox";
            this.edinfoCheckbox.Size = new System.Drawing.Size(65, 20);
            this.edinfoCheckbox.TabIndex = 24;
            this.edinfoCheckbox.Text = "Edinfo";
            this.edinfoCheckbox.UseVisualStyleBackColor = true;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(132, 475);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(319, 34);
            this.progressBar.TabIndex = 26;
            this.progressBar.UseWaitCursor = true;
            this.progressBar.Visible = false;
            // 
            // TrackScalerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(583, 554);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.filenameLabel);
            this.Controls.Add(this.userInputErrLabel);
            this.Controls.Add(this.progressLabel);
            this.Controls.Add(this.ScaleButton);
            this.Controls.Add(this.fileCheckErrLabel);
            this.Controls.Add(this.methodErrLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.MethodGroupBox);
            this.Controls.Add(this.FilesGroupBox);
            this.Controls.Add(this.UserInputTextBox);
            this.Controls.Add(this.fileErrLabel);
            this.Controls.Add(this.OpenFolderButton);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TrackScalerForm";
            this.Text = "MX Simulator Track Scaler";
            this.Load += new System.EventHandler(this.TrackScalerForm_Load);
            this.FilesGroupBox.ResumeLayout(false);
            this.FilesGroupBox.PerformLayout();
            this.MethodGroupBox.ResumeLayout(false);
            this.MethodGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OpenFolderButton;
        private System.Windows.Forms.Label fileErrLabel;
        private System.Windows.Forms.GroupBox FilesGroupBox;
        private System.Windows.Forms.GroupBox MethodGroupBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox AllCheckBox;
        private System.Windows.Forms.CheckBox TimingGateCheckBox;
        private System.Windows.Forms.CheckBox FlaggersCheckBox;
        private System.Windows.Forms.CheckBox DecalsCheckBox;
        private System.Windows.Forms.CheckBox StatueCheckBox;
        private System.Windows.Forms.CheckBox BillboardCheckBox;
        private System.Windows.Forms.RadioButton ToFactorRadioButton;
        private System.Windows.Forms.RadioButton ByFactorRadioButton;
        private System.Windows.Forms.Label methodErrLabel;
        private System.Windows.Forms.Label fileCheckErrLabel;
        private System.Windows.Forms.Button ScaleButton;
        private System.Windows.Forms.TextBox UserInputTextBox;
        private System.Windows.Forms.Label progressLabel;
        private System.Windows.Forms.Label userInputErrLabel;
        private System.Windows.Forms.Label filenameLabel;
        private System.Windows.Forms.CheckBox terrainCheckBox;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private QuantumConcepts.Common.Forms.UI.Controls.NewProgressBar progressBar;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.CheckBox edinfoCheckbox;
    }
}

