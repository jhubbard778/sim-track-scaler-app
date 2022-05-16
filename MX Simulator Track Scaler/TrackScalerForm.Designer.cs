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
            this.button1 = new System.Windows.Forms.Button();
            this.fileErrLabel = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.FilesGroupBox = new System.Windows.Forms.GroupBox();
            this.AllCheckBox = new System.Windows.Forms.CheckBox();
            this.TimingGateCheckBox = new System.Windows.Forms.CheckBox();
            this.FlaggersCheckBox = new System.Windows.Forms.CheckBox();
            this.DecalsCheckBox = new System.Windows.Forms.CheckBox();
            this.StatueCheckBox = new System.Windows.Forms.CheckBox();
            this.BillboardCheckBox = new System.Windows.Forms.CheckBox();
            this.MethodGroupBox = new System.Windows.Forms.GroupBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.scaleErrLabel = new System.Windows.Forms.Label();
            this.methodErrLabel = new System.Windows.Forms.Label();
            this.numErrLabel = new System.Windows.Forms.Label();
            this.fileCheckErrLabel = new System.Windows.Forms.Label();
            this.ScaleButton = new System.Windows.Forms.Button();
            this.FilesGroupBox.SuspendLayout();
            this.MethodGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(213, 43);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(106, 31);
            this.button1.TabIndex = 0;
            this.button1.Text = "Choose terrain.hf";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // fileErrLabel
            // 
            this.fileErrLabel.AutoSize = true;
            this.fileErrLabel.BackColor = System.Drawing.Color.Transparent;
            this.fileErrLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileErrLabel.Location = new System.Drawing.Point(328, 52);
            this.fileErrLabel.Name = "fileErrLabel";
            this.fileErrLabel.Size = new System.Drawing.Size(45, 16);
            this.fileErrLabel.TabIndex = 3;
            this.fileErrLabel.Text = "label1";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(211, 241);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(106, 20);
            this.textBox1.TabIndex = 4;
            // 
            // FilesGroupBox
            // 
            this.FilesGroupBox.Controls.Add(this.AllCheckBox);
            this.FilesGroupBox.Controls.Add(this.TimingGateCheckBox);
            this.FilesGroupBox.Controls.Add(this.FlaggersCheckBox);
            this.FilesGroupBox.Controls.Add(this.DecalsCheckBox);
            this.FilesGroupBox.Controls.Add(this.StatueCheckBox);
            this.FilesGroupBox.Controls.Add(this.BillboardCheckBox);
            this.FilesGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FilesGroupBox.Location = new System.Drawing.Point(156, 90);
            this.FilesGroupBox.Name = "FilesGroupBox";
            this.FilesGroupBox.Size = new System.Drawing.Size(218, 112);
            this.FilesGroupBox.TabIndex = 5;
            this.FilesGroupBox.TabStop = false;
            this.FilesGroupBox.Text = "Files To Scale";
            // 
            // AllCheckBox
            // 
            this.AllCheckBox.AutoSize = true;
            this.AllCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AllCheckBox.Location = new System.Drawing.Point(105, 82);
            this.AllCheckBox.Name = "AllCheckBox";
            this.AllCheckBox.Size = new System.Drawing.Size(42, 20);
            this.AllCheckBox.TabIndex = 13;
            this.AllCheckBox.Text = "All";
            this.AllCheckBox.UseVisualStyleBackColor = true;
            // 
            // TimingGateCheckBox
            // 
            this.TimingGateCheckBox.AutoSize = true;
            this.TimingGateCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimingGateCheckBox.Location = new System.Drawing.Point(105, 56);
            this.TimingGateCheckBox.Name = "TimingGateCheckBox";
            this.TimingGateCheckBox.Size = new System.Drawing.Size(107, 20);
            this.TimingGateCheckBox.TabIndex = 12;
            this.TimingGateCheckBox.Text = "Timing Gates";
            this.TimingGateCheckBox.UseVisualStyleBackColor = true;
            // 
            // FlaggersCheckBox
            // 
            this.FlaggersCheckBox.AutoSize = true;
            this.FlaggersCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FlaggersCheckBox.Location = new System.Drawing.Point(105, 30);
            this.FlaggersCheckBox.Name = "FlaggersCheckBox";
            this.FlaggersCheckBox.Size = new System.Drawing.Size(81, 20);
            this.FlaggersCheckBox.TabIndex = 11;
            this.FlaggersCheckBox.Text = "Flaggers";
            this.FlaggersCheckBox.UseVisualStyleBackColor = true;
            // 
            // DecalsCheckBox
            // 
            this.DecalsCheckBox.AutoSize = true;
            this.DecalsCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DecalsCheckBox.Location = new System.Drawing.Point(6, 82);
            this.DecalsCheckBox.Name = "DecalsCheckBox";
            this.DecalsCheckBox.Size = new System.Drawing.Size(70, 20);
            this.DecalsCheckBox.TabIndex = 10;
            this.DecalsCheckBox.Text = "Decals";
            this.DecalsCheckBox.UseVisualStyleBackColor = true;
            // 
            // StatueCheckBox
            // 
            this.StatueCheckBox.AutoSize = true;
            this.StatueCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StatueCheckBox.Location = new System.Drawing.Point(6, 56);
            this.StatueCheckBox.Name = "StatueCheckBox";
            this.StatueCheckBox.Size = new System.Drawing.Size(72, 20);
            this.StatueCheckBox.TabIndex = 9;
            this.StatueCheckBox.Text = "Statues";
            this.StatueCheckBox.UseVisualStyleBackColor = true;
            // 
            // BillboardCheckBox
            // 
            this.BillboardCheckBox.AutoSize = true;
            this.BillboardCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BillboardCheckBox.Location = new System.Drawing.Point(6, 30);
            this.BillboardCheckBox.Name = "BillboardCheckBox";
            this.BillboardCheckBox.Size = new System.Drawing.Size(88, 20);
            this.BillboardCheckBox.TabIndex = 8;
            this.BillboardCheckBox.Text = "Billboards";
            this.BillboardCheckBox.UseVisualStyleBackColor = true;
            // 
            // MethodGroupBox
            // 
            this.MethodGroupBox.BackColor = System.Drawing.Color.Transparent;
            this.MethodGroupBox.Controls.Add(this.radioButton2);
            this.MethodGroupBox.Controls.Add(this.radioButton1);
            this.MethodGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MethodGroupBox.Location = new System.Drawing.Point(163, 282);
            this.MethodGroupBox.Name = "MethodGroupBox";
            this.MethodGroupBox.Size = new System.Drawing.Size(200, 100);
            this.MethodGroupBox.TabIndex = 6;
            this.MethodGroupBox.TabStop = false;
            this.MethodGroupBox.Text = "Scale Method";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton2.Location = new System.Drawing.Point(21, 64);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(153, 28);
            this.radioButton2.TabIndex = 15;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Scale to Factor";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioButton1.Location = new System.Drawing.Point(21, 30);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(158, 28);
            this.radioButton1.TabIndex = 14;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Scale by Factor";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(206, 214);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 24);
            this.label2.TabIndex = 7;
            this.label2.Text = "Scale/Factor";
            // 
            // scaleErrLabel
            // 
            this.scaleErrLabel.AutoSize = true;
            this.scaleErrLabel.BackColor = System.Drawing.Color.Transparent;
            this.scaleErrLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scaleErrLabel.Location = new System.Drawing.Point(360, 417);
            this.scaleErrLabel.Name = "scaleErrLabel";
            this.scaleErrLabel.Size = new System.Drawing.Size(45, 16);
            this.scaleErrLabel.TabIndex = 8;
            this.scaleErrLabel.Text = "label5";
            // 
            // methodErrLabel
            // 
            this.methodErrLabel.AutoSize = true;
            this.methodErrLabel.BackColor = System.Drawing.Color.Transparent;
            this.methodErrLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.methodErrLabel.Location = new System.Drawing.Point(380, 332);
            this.methodErrLabel.Name = "methodErrLabel";
            this.methodErrLabel.Size = new System.Drawing.Size(45, 16);
            this.methodErrLabel.TabIndex = 9;
            this.methodErrLabel.Text = "label4";
            // 
            // numErrLabel
            // 
            this.numErrLabel.AutoSize = true;
            this.numErrLabel.BackColor = System.Drawing.Color.Transparent;
            this.numErrLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numErrLabel.Location = new System.Drawing.Point(333, 244);
            this.numErrLabel.Name = "numErrLabel";
            this.numErrLabel.Size = new System.Drawing.Size(45, 16);
            this.numErrLabel.TabIndex = 10;
            this.numErrLabel.Text = "label3";
            // 
            // fileCheckErrLabel
            // 
            this.fileCheckErrLabel.AutoSize = true;
            this.fileCheckErrLabel.BackColor = System.Drawing.Color.Transparent;
            this.fileCheckErrLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileCheckErrLabel.Location = new System.Drawing.Point(380, 146);
            this.fileCheckErrLabel.Name = "fileCheckErrLabel";
            this.fileCheckErrLabel.Size = new System.Drawing.Size(45, 16);
            this.fileCheckErrLabel.TabIndex = 11;
            this.fileCheckErrLabel.Text = "label2";
            // 
            // ScaleButton
            // 
            this.ScaleButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScaleButton.Location = new System.Drawing.Point(188, 400);
            this.ScaleButton.Name = "ScaleButton";
            this.ScaleButton.Size = new System.Drawing.Size(158, 47);
            this.ScaleButton.TabIndex = 12;
            this.ScaleButton.Text = "Scale";
            this.ScaleButton.UseVisualStyleBackColor = true;
            // 
            // TrackScalerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(580, 525);
            this.Controls.Add(this.ScaleButton);
            this.Controls.Add(this.fileCheckErrLabel);
            this.Controls.Add(this.numErrLabel);
            this.Controls.Add(this.methodErrLabel);
            this.Controls.Add(this.scaleErrLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.MethodGroupBox);
            this.Controls.Add(this.FilesGroupBox);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.fileErrLabel);
            this.Controls.Add(this.button1);
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

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label fileErrLabel;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox FilesGroupBox;
        private System.Windows.Forms.GroupBox MethodGroupBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox AllCheckBox;
        private System.Windows.Forms.CheckBox TimingGateCheckBox;
        private System.Windows.Forms.CheckBox FlaggersCheckBox;
        private System.Windows.Forms.CheckBox DecalsCheckBox;
        private System.Windows.Forms.CheckBox StatueCheckBox;
        private System.Windows.Forms.CheckBox BillboardCheckBox;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Label scaleErrLabel;
        private System.Windows.Forms.Label methodErrLabel;
        private System.Windows.Forms.Label numErrLabel;
        private System.Windows.Forms.Label fileCheckErrLabel;
        private System.Windows.Forms.Button ScaleButton;
    }
}

