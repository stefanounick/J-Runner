namespace JRunner.Panels
{
    partial class NandTools
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnCPUDB = new System.Windows.Forms.Button();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.txtLPTPort = new System.Windows.Forms.TextBox();
            this.lblLPTPort = new System.Windows.Forms.Label();
            this.rbtnLPT = new System.Windows.Forms.RadioButton();
            this.rbtnUSB = new System.Windows.Forms.RadioButton();
            this.btnProgramCR = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnCreateECC = new System.Windows.Forms.Button();
            this.lblNReads = new System.Windows.Forms.Label();
            this.btnXeBuild = new System.Windows.Forms.Button();
            this.btnWrite = new System.Windows.Forms.Button();
            this.numericIterations = new System.Windows.Forms.NumericUpDown();
            this.btnRead = new System.Windows.Forms.Button();
            this.btnWriteECC = new System.Windows.Forms.Button();
            this.pBoxDevice = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox9.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericIterations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBoxDevice)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCPUDB
            // 
            this.btnCPUDB.Location = new System.Drawing.Point(197, 101);
            this.btnCPUDB.Name = "btnCPUDB";
            this.btnCPUDB.Size = new System.Drawing.Size(66, 51);
            this.btnCPUDB.TabIndex = 80;
            this.btnCPUDB.TabStop = false;
            this.btnCPUDB.Text = "CPU Key Database";
            this.toolTip1.SetToolTip(this.btnCPUDB, "This opens the database with all your previous CPU Keys");
            this.btnCPUDB.UseVisualStyleBackColor = true;
            this.btnCPUDB.Click += new System.EventHandler(this.btnCPUDB_Click);
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.txtLPTPort);
            this.groupBox9.Controls.Add(this.lblLPTPort);
            this.groupBox9.Controls.Add(this.rbtnLPT);
            this.groupBox9.Controls.Add(this.rbtnUSB);
            this.groupBox9.Controls.Add(this.btnProgramCR);
            this.groupBox9.Location = new System.Drawing.Point(3, 94);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(188, 62);
            this.groupBox9.TabIndex = 82;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "CoolRunner Programming";
            // 
            // txtLPTPort
            // 
            this.txtLPTPort.Location = new System.Drawing.Point(135, 36);
            this.txtLPTPort.MaxLength = 4;
            this.txtLPTPort.Name = "txtLPTPort";
            this.txtLPTPort.Size = new System.Drawing.Size(46, 20);
            this.txtLPTPort.TabIndex = 3;
            this.txtLPTPort.TabStop = false;
            this.txtLPTPort.Text = "378";
            this.txtLPTPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtLPTPort.Visible = false;
            this.txtLPTPort.TextChanged += new System.EventHandler(this.txtLPTPort_TextChanged);
            // 
            // lblLPTPort
            // 
            this.lblLPTPort.AutoSize = true;
            this.lblLPTPort.Location = new System.Drawing.Point(136, 20);
            this.lblLPTPort.Name = "lblLPTPort";
            this.lblLPTPort.Size = new System.Drawing.Size(26, 13);
            this.lblLPTPort.TabIndex = 2;
            this.lblLPTPort.Text = "Port";
            this.lblLPTPort.Visible = false;
            // 
            // rbtnLPT
            // 
            this.rbtnLPT.AutoSize = true;
            this.rbtnLPT.Location = new System.Drawing.Point(83, 39);
            this.rbtnLPT.Name = "rbtnLPT";
            this.rbtnLPT.Size = new System.Drawing.Size(45, 17);
            this.rbtnLPT.TabIndex = 1;
            this.rbtnLPT.TabStop = true;
            this.rbtnLPT.Text = "LPT";
            this.rbtnLPT.UseVisualStyleBackColor = true;
            this.rbtnLPT.CheckedChanged += new System.EventHandler(this.rbtn_CheckedChanged);
            // 
            // rbtnUSB
            // 
            this.rbtnUSB.AutoSize = true;
            this.rbtnUSB.Checked = true;
            this.rbtnUSB.Location = new System.Drawing.Point(83, 20);
            this.rbtnUSB.Name = "rbtnUSB";
            this.rbtnUSB.Size = new System.Drawing.Size(47, 17);
            this.rbtnUSB.TabIndex = 10;
            this.rbtnUSB.TabStop = true;
            this.rbtnUSB.Text = "USB";
            this.rbtnUSB.UseVisualStyleBackColor = true;
            this.rbtnUSB.CheckedChanged += new System.EventHandler(this.rbtn_CheckedChanged);
            // 
            // btnProgramCR
            // 
            this.btnProgramCR.Location = new System.Drawing.Point(6, 19);
            this.btnProgramCR.Name = "btnProgramCR";
            this.btnProgramCR.Size = new System.Drawing.Size(71, 39);
            this.btnProgramCR.TabIndex = 6;
            this.btnProgramCR.TabStop = false;
            this.btnProgramCR.Text = "Program CoolRunner";
            this.toolTip1.SetToolTip(this.btnProgramCR, "Press this to select XSVF to write to your CR Chip");
            this.btnProgramCR.UseVisualStyleBackColor = true;
            this.btnProgramCR.Click += new System.EventHandler(this.btnProgramCR_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.groupBox3.Controls.Add(this.btnCreateECC);
            this.groupBox3.Controls.Add(this.lblNReads);
            this.groupBox3.Controls.Add(this.btnXeBuild);
            this.groupBox3.Controls.Add(this.btnWrite);
            this.groupBox3.Controls.Add(this.numericIterations);
            this.groupBox3.Controls.Add(this.btnRead);
            this.groupBox3.Controls.Add(this.btnWriteECC);
            this.groupBox3.Location = new System.Drawing.Point(3, 1);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(406, 87);
            this.groupBox3.TabIndex = 81;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Nand";
            // 
            // btnCreateECC
            // 
            this.btnCreateECC.Location = new System.Drawing.Point(120, 17);
            this.btnCreateECC.Name = "btnCreateECC";
            this.btnCreateECC.Size = new System.Drawing.Size(65, 51);
            this.btnCreateECC.TabIndex = 64;
            this.btnCreateECC.TabStop = false;
            this.btnCreateECC.Text = "Create ECC";
            this.toolTip1.SetToolTip(this.btnCreateECC, "Used to create ECC or Xell file. This created file should be\r\nloaded into source " +
        "ready to be written");
            this.btnCreateECC.UseVisualStyleBackColor = true;
            this.btnCreateECC.Click += new System.EventHandler(this.btnCreateECC_Click);
            // 
            // lblNReads
            // 
            this.lblNReads.AutoSize = true;
            this.lblNReads.Location = new System.Drawing.Point(3, 35);
            this.lblNReads.Name = "lblNReads";
            this.lblNReads.Size = new System.Drawing.Size(38, 13);
            this.lblNReads.TabIndex = 63;
            this.lblNReads.Text = "Reads";
            // 
            // btnXeBuild
            // 
            this.btnXeBuild.Location = new System.Drawing.Point(263, 17);
            this.btnXeBuild.Name = "btnXeBuild";
            this.btnXeBuild.Size = new System.Drawing.Size(62, 51);
            this.btnXeBuild.TabIndex = 5;
            this.btnXeBuild.TabStop = false;
            this.btnXeBuild.Text = "Create XeBuild Image";
            this.toolTip1.SetToolTip(this.btnXeBuild, "This uses the file in source (with a valid cpu key) to create a\r\nfreeboot image r" +
        "eady to be written to nand.");
            this.btnXeBuild.UseVisualStyleBackColor = true;
            this.btnXeBuild.Click += new System.EventHandler(this.btnXeBuild_Click);
            // 
            // btnWrite
            // 
            this.btnWrite.Location = new System.Drawing.Point(331, 17);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(66, 51);
            this.btnWrite.TabIndex = 56;
            this.btnWrite.TabStop = false;
            this.btnWrite.Text = "Write Nand";
            this.toolTip1.SetToolTip(this.btnWrite, "Does what it says on the tin!");
            this.btnWrite.UseVisualStyleBackColor = true;
            this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
            // 
            // numericIterations
            // 
            this.numericIterations.BackColor = System.Drawing.Color.Gainsboro;
            this.numericIterations.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.numericIterations.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.numericIterations.ForeColor = System.Drawing.Color.Black;
            this.numericIterations.Location = new System.Drawing.Point(6, 51);
            this.numericIterations.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericIterations.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericIterations.Name = "numericIterations";
            this.numericIterations.Size = new System.Drawing.Size(37, 17);
            this.numericIterations.TabIndex = 20;
            this.numericIterations.TabStop = false;
            this.numericIterations.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTip1.SetToolTip(this.numericIterations, "This is the number of reads that will be performed when you select read nand");
            this.numericIterations.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericIterations.ValueChanged += new System.EventHandler(this.numericIterations_ValueChanged);
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(49, 17);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(65, 51);
            this.btnRead.TabIndex = 8;
            this.btnRead.TabStop = false;
            this.btnRead.Text = "Read Nand";
            this.toolTip1.SetToolTip(this.btnRead, "This button starts the nand read process. A Nand-x, JRP or \r\nDemon is required to" +
        " be connected to use this. The read \r\nNand will be automatically loaded into sou" +
        "rce box");
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // btnWriteECC
            // 
            this.btnWriteECC.Location = new System.Drawing.Point(191, 17);
            this.btnWriteECC.Name = "btnWriteECC";
            this.btnWriteECC.Size = new System.Drawing.Size(66, 51);
            this.btnWriteECC.TabIndex = 9;
            this.btnWriteECC.TabStop = false;
            this.btnWriteECC.Text = "Write ECC";
            this.toolTip1.SetToolTip(this.btnWriteECC, "Write ECC or Xell-Reloaded. Again a Nand \r\nread/write device is required to be co" +
        "nnected.\r\nThe file in source will be written to nand.");
            this.btnWriteECC.UseVisualStyleBackColor = true;
            this.btnWriteECC.Click += new System.EventHandler(this.btnWriteECC_Click);
            // 
            // pBoxDevice
            // 
            this.pBoxDevice.ErrorImage = null;
            this.pBoxDevice.InitialImage = null;
            this.pBoxDevice.Location = new System.Drawing.Point(295, 106);
            this.pBoxDevice.Name = "pBoxDevice";
            this.pBoxDevice.Size = new System.Drawing.Size(53, 43);
            this.pBoxDevice.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pBoxDevice.TabIndex = 83;
            this.pBoxDevice.TabStop = false;
            // 
            // NandTools
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pBoxDevice);
            this.Controls.Add(this.btnCPUDB);
            this.Controls.Add(this.groupBox9);
            this.Controls.Add(this.groupBox3);
            this.Name = "NandTools";
            this.Size = new System.Drawing.Size(416, 175);
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericIterations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBoxDevice)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pBoxDevice;
        private System.Windows.Forms.Button btnCPUDB;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.TextBox txtLPTPort;
        private System.Windows.Forms.Label lblLPTPort;
        private System.Windows.Forms.RadioButton rbtnLPT;
        private System.Windows.Forms.RadioButton rbtnUSB;
        private System.Windows.Forms.Button btnProgramCR;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnCreateECC;
        private System.Windows.Forms.Label lblNReads;
        private System.Windows.Forms.Button btnXeBuild;
        private System.Windows.Forms.Button btnWrite;
        private System.Windows.Forms.NumericUpDown numericIterations;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Button btnWriteECC;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
