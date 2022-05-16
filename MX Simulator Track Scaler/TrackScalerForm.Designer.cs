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
            this.OpenTerrainButton = new System.Windows.Forms.Button();
            this.fileErrLabel = new System.Windows.Forms.Label();
            this.FilesGroupBox = new System.Windows.Forms.GroupBox();
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
            this.scaleErrLabel = new System.Windows.Forms.Label();
            this.methodErrLabel = new System.Windows.Forms.Label();
            this.fileCheckErrLabel = new System.Windows.Forms.Label();
            this.ScaleButton = new System.Windows.Forms.Button();
            this.terrainFileOpenDialog = new System.Windows.Forms.OpenFileDialog();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.UserInputTextBox = new System.Windows.Forms.TextBox();
            this.progressLabel = new System.Windows.Forms.Label();
            this.userInputErrLabel = new System.Windows.Forms.Label();
            this.filenameLabel = new System.Windows.Forms.Label();
            this.terrainCheckBox = new System.Windows.Forms.CheckBox();
            this.FilesGroupBox.SuspendLayout();
            this.MethodGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // OpenTerrainButton
            // 
            this.OpenTerrainButton.Location = new System.Drawing.Point(236, 21);
            this.OpenTerrainButton.Name = "OpenTerrainButton";
            this.OpenTerrainButton.Size = new System.Drawing.Size(106, 31);
            this.OpenTerrainButton.TabIndex = 0;
            this.OpenTerrainButton.Text = "Choose terrain.hf";
            this.OpenTerrainButton.UseVisualStyleBackColor = true;
            this.OpenTerrainButton.Click += new System.EventHandler(this.OpenTerrainButton_Click);
            // 
            // fileErrLabel
            // 
            this.fileErrLabel.AutoSize = true;
            this.fileErrLabel.BackColor = System.Drawing.Color.Transparent;
            this.fileErrLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileErrLabel.Location = new System.Drawing.Point(348, 28);
            this.fileErrLabel.Name = "fileErrLabel";
            this.fileErrLabel.Size = new System.Drawing.Size(45, 16);
            this.fileErrLabel.TabIndex = 1;
            this.fileErrLabel.Text = "label1";
            // 
            // FilesGroupBox
            // 
            this.FilesGroupBox.Controls.Add(this.terrainCheckBox);
            this.FilesGroupBox.Controls.Add(this.AllCheckBox);
            this.FilesGroupBox.Controls.Add(this.TimingGateCheckBox);
            this.FilesGroupBox.Controls.Add(this.FlaggersCheckBox);
            this.FilesGroupBox.Controls.Add(this.DecalsCheckBox);
            this.FilesGroupBox.Controls.Add(this.StatueCheckBox);
            this.FilesGroupBox.Controls.Add(this.BillboardCheckBox);
            this.FilesGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FilesGroupBox.Location = new System.Drawing.Point(178, 90);
            this.FilesGroupBox.Name = "FilesGroupBox";
            this.FilesGroupBox.Size = new System.Drawing.Size(249, 112);
            this.FilesGroupBox.TabIndex = 3;
            this.FilesGroupBox.TabStop = false;
            this.FilesGroupBox.Text = "Files To Scale";
            // 
            // AllCheckBox
            // 
            this.AllCheckBox.AutoSize = true;
            this.AllCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AllCheckBox.Location = new System.Drawing.Point(201, 56);
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
            this.TimingGateCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimingGateCheckBox.Location = new System.Drawing.Point(91, 56);
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
            this.FlaggersCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FlaggersCheckBox.Location = new System.Drawing.Point(91, 82);
            this.FlaggersCheckBox.Name = "FlaggersCheckBox";
            this.FlaggersCheckBox.Size = new System.Drawing.Size(81, 20);
            this.FlaggersCheckBox.TabIndex = 7;
            this.FlaggersCheckBox.Text = "Flaggers";
            this.FlaggersCheckBox.UseVisualStyleBackColor = false;
            // 
            // DecalsCheckBox
            // 
            this.DecalsCheckBox.AutoSize = true;
            this.DecalsCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.MethodGroupBox.Controls.Add(this.ToFactorRadioButton);
            this.MethodGroupBox.Controls.Add(this.ByFactorRadioButton);
            this.MethodGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MethodGroupBox.Location = new System.Drawing.Point(170, 283);
            this.MethodGroupBox.Name = "MethodGroupBox";
            this.MethodGroupBox.Size = new System.Drawing.Size(232, 100);
            this.MethodGroupBox.TabIndex = 13;
            this.MethodGroupBox.TabStop = false;
            this.MethodGroupBox.Text = "Scale Method";
            // 
            // ToFactorRadioButton
            // 
            this.ToFactorRadioButton.AutoSize = true;
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
            this.label2.Location = new System.Drawing.Point(228, 225);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 24);
            this.label2.TabIndex = 22;
            this.label2.Text = "Scale/Factor";
            // 
            // scaleErrLabel
            // 
            this.scaleErrLabel.AutoSize = true;
            this.scaleErrLabel.BackColor = System.Drawing.Color.Transparent;
            this.scaleErrLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scaleErrLabel.Location = new System.Drawing.Point(382, 419);
            this.scaleErrLabel.Name = "scaleErrLabel";
            this.scaleErrLabel.Size = new System.Drawing.Size(45, 16);
            this.scaleErrLabel.TabIndex = 17;
            this.scaleErrLabel.Text = "label6";
            // 
            // methodErrLabel
            // 
            this.methodErrLabel.AutoSize = true;
            this.methodErrLabel.BackColor = System.Drawing.Color.Transparent;
            this.methodErrLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.methodErrLabel.Location = new System.Drawing.Point(408, 321);
            this.methodErrLabel.Name = "methodErrLabel";
            this.methodErrLabel.Size = new System.Drawing.Size(45, 16);
            this.methodErrLabel.TabIndex = 15;
            this.methodErrLabel.Text = "label5";
            // 
            // fileCheckErrLabel
            // 
            this.fileCheckErrLabel.BackColor = System.Drawing.Color.Transparent;
            this.fileCheckErrLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileCheckErrLabel.Location = new System.Drawing.Point(15, 205);
            this.fileCheckErrLabel.Name = "fileCheckErrLabel";
            this.fileCheckErrLabel.Size = new System.Drawing.Size(553, 18);
            this.fileCheckErrLabel.TabIndex = 10;
            this.fileCheckErrLabel.Text = "label3";
            this.fileCheckErrLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ScaleButton
            // 
            this.ScaleButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScaleButton.Location = new System.Drawing.Point(210, 402);
            this.ScaleButton.Name = "ScaleButton";
            this.ScaleButton.Size = new System.Drawing.Size(158, 47);
            this.ScaleButton.TabIndex = 15;
            this.ScaleButton.Text = "Scale";
            this.ScaleButton.UseVisualStyleBackColor = true;
            this.ScaleButton.Click += new System.EventHandler(this.ScaleButton_Click);
            // 
            // terrainFileOpenDialog
            // 
            this.terrainFileOpenDialog.FileName = "terrain.hf";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(111, 465);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(361, 29);
            this.progressBar.TabIndex = 18;
            this.progressBar.Visible = false;
            // 
            // UserInputTextBox
            // 
            this.UserInputTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserInputTextBox.Location = new System.Drawing.Point(233, 252);
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
            this.progressLabel.Location = new System.Drawing.Point(12, 502);
            this.progressLabel.Name = "progressLabel";
            this.progressLabel.Size = new System.Drawing.Size(556, 48);
            this.progressLabel.TabIndex = 19;
            this.progressLabel.Text = "label7";
            this.progressLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // userInputErrLabel
            // 
            this.userInputErrLabel.AutoSize = true;
            this.userInputErrLabel.BackColor = System.Drawing.Color.Transparent;
            this.userInputErrLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userInputErrLabel.Location = new System.Drawing.Point(351, 255);
            this.userInputErrLabel.Name = "userInputErrLabel";
            this.userInputErrLabel.Size = new System.Drawing.Size(45, 16);
            this.userInputErrLabel.TabIndex = 12;
            this.userInputErrLabel.Text = "label4";
            // 
            // filenameLabel
            // 
            this.filenameLabel.BackColor = System.Drawing.Color.Transparent;
            this.filenameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filenameLabel.Location = new System.Drawing.Point(12, 60);
            this.filenameLabel.Name = "filenameLabel";
            this.filenameLabel.Size = new System.Drawing.Size(556, 25);
            this.filenameLabel.TabIndex = 2;
            this.filenameLabel.Text = "label2";
            this.filenameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // terrainCheckBox
            // 
            this.terrainCheckBox.AutoSize = true;
            this.terrainCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.terrainCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.terrainCheckBox.Location = new System.Drawing.Point(6, 30);
            this.terrainCheckBox.Name = "terrainCheckBox";
            this.terrainCheckBox.Size = new System.Drawing.Size(70, 20);
            this.terrainCheckBox.TabIndex = 23;
            this.terrainCheckBox.Text = "Terrain";
            this.terrainCheckBox.UseVisualStyleBackColor = false;
            // 
            // TrackScalerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(580, 559);
            this.Controls.Add(this.filenameLabel);
            this.Controls.Add(this.userInputErrLabel);
            this.Controls.Add(this.progressLabel);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.ScaleButton);
            this.Controls.Add(this.fileCheckErrLabel);
            this.Controls.Add(this.methodErrLabel);
            this.Controls.Add(this.scaleErrLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.MethodGroupBox);
            this.Controls.Add(this.FilesGroupBox);
            this.Controls.Add(this.UserInputTextBox);
            this.Controls.Add(this.fileErrLabel);
            this.Controls.Add(this.OpenTerrainButton);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TrackScalerForm";
            this.Text = "MX Simulator Track Scaler";
            this.Load += new System.EventHandler(this.TrackScalerForm_Load);
            this.FilesGroupBox.ResumeLayout(false);
            this.FilesGroupBox.PerformLayout();
            this.MethodGroupBox.ResumeLayout(false);
            this.MethodGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OpenTerrainButton;
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
        private System.Windows.Forms.Label scaleErrLabel;
        private System.Windows.Forms.Label methodErrLabel;
        private System.Windows.Forms.Label fileCheckErrLabel;
        private System.Windows.Forms.Button ScaleButton;
        private System.Windows.Forms.OpenFileDialog terrainFileOpenDialog;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.TextBox UserInputTextBox;
        private System.Windows.Forms.Label progressLabel;
        private System.Windows.Forms.Label userInputErrLabel;
        private System.Windows.Forms.Label filenameLabel;
        private System.Windows.Forms.CheckBox terrainCheckBox;
    }
}

