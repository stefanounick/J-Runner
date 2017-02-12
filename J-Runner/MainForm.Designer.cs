using System.Windows.Forms;


namespace JRunner
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnExit = new System.Windows.Forms.Button();
            this.comparebutton = new System.Windows.Forms.Button();
            this.btnLoadFile2 = new System.Windows.Forms.Button();
            this.btnLoadFile1 = new System.Windows.Forms.Button();
            this.txtFilePath2 = new System.Windows.Forms.TextBox();
            this.txtFilePath1 = new System.Windows.Forms.TextBox();
            this.txtConsole = new System.Windows.Forms.TextBox();
            this.txtCPUKey = new System.Windows.Forms.TextBox();
            this.lblCpuKey = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.btnCOM = new System.Windows.Forms.Button();
            this.btnScanner = new System.Windows.Forms.Button();
            this.labelIP = new System.Windows.Forms.Label();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.btnIPGetCPU = new System.Windows.Forms.Button();
            this.btnInit = new System.Windows.Forms.Button();
            this.btnWorkingFolder = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.extractFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.sMCConfigViewerToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.xValueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkSecdataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logPostToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripHexEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.advancedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customNandProCommandToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CustomXeBuildMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.corona4GBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.writeFusionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
            this.changeLDVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createAnImageWithoutNanddumpbinToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.patchNandToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.pirsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cBFuseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getLatestSystemUpdateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jRPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.powerOnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shutdownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bootloaderModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.demoNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleNANDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.powerOnToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.powerOffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateFwToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getInvalidBlocksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.devToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemVNand = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xBOXOneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hDDToolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.XeBuildOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusBlank = new System.Windows.Forms.ToolStripStatusLabel();
            this.xeBuildStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.xebuildVersion = new System.Windows.Forms.ToolStripStatusLabel();
            this.BlankSpace = new System.Windows.Forms.ToolStripStatusLabel();
            this.DashLaunchStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.DashLaunchVersion = new System.Windows.Forms.ToolStripStatusLabel();
            this.ModeStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.ModeVersion = new System.Windows.Forms.ToolStripStatusLabel();
            this.FWStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.FWVersion = new System.Windows.Forms.ToolStripStatusLabel();
            this.FlashStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.FlashVersion = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.btnCheckBadBlocks = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.pnlInfo = new System.Windows.Forms.Panel();
            this.txtBlocks = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlTools = new System.Windows.Forms.Panel();
            this.pnlExtra = new System.Windows.Forms.Panel();
            this.groupBox8.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnExit.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnExit.Location = new System.Drawing.Point(473, 548);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(83, 26);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // comparebutton
            // 
            this.comparebutton.Location = new System.Drawing.Point(333, 15);
            this.comparebutton.Name = "comparebutton";
            this.comparebutton.Size = new System.Drawing.Size(64, 49);
            this.comparebutton.TabIndex = 19;
            this.comparebutton.TabStop = false;
            this.comparebutton.Text = "Nand Compare";
            this.toolTip1.SetToolTip(this.comparebutton, "Manually Compares the file loaded into \"Source File\"\r\nwith the one in \"Additional" +
        " File\"");
            this.comparebutton.UseVisualStyleBackColor = true;
            this.comparebutton.Click += new System.EventHandler(this.comparebutton_Click);
            // 
            // btnLoadFile2
            // 
            this.btnLoadFile2.Location = new System.Drawing.Point(6, 44);
            this.btnLoadFile2.Name = "btnLoadFile2";
            this.btnLoadFile2.Size = new System.Drawing.Size(76, 20);
            this.btnLoadFile2.TabIndex = 16;
            this.btnLoadFile2.TabStop = false;
            this.btnLoadFile2.Text = "Load Extra";
            this.btnLoadFile2.UseVisualStyleBackColor = true;
            this.btnLoadFile2.Click += new System.EventHandler(this.btnLoadFile2_Click);
            // 
            // btnLoadFile1
            // 
            this.btnLoadFile1.Location = new System.Drawing.Point(6, 15);
            this.btnLoadFile1.Name = "btnLoadFile1";
            this.btnLoadFile1.Size = new System.Drawing.Size(76, 20);
            this.btnLoadFile1.TabIndex = 15;
            this.btnLoadFile1.TabStop = false;
            this.btnLoadFile1.Text = "Load Source";
            this.btnLoadFile1.UseVisualStyleBackColor = true;
            this.btnLoadFile1.Click += new System.EventHandler(this.btnLoadFile1_Click);
            // 
            // txtFilePath2
            // 
            this.txtFilePath2.AllowDrop = true;
            this.txtFilePath2.BackColor = System.Drawing.Color.White;
            this.txtFilePath2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFilePath2.Location = new System.Drawing.Point(88, 44);
            this.txtFilePath2.Name = "txtFilePath2";
            this.txtFilePath2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtFilePath2.Size = new System.Drawing.Size(239, 20);
            this.txtFilePath2.TabIndex = 2;
            this.toolTip1.SetToolTip(this.txtFilePath2, "The file listed in this box is used to compare against file loaded in \"Source fil" +
        "e\" box\r\n");
            this.txtFilePath2.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtFilePath2_DragDrop);
            this.txtFilePath2.DragEnter += new System.Windows.Forms.DragEventHandler(this.txtFilePath2_DragEnter);
            // 
            // txtFilePath1
            // 
            this.txtFilePath1.AllowDrop = true;
            this.txtFilePath1.BackColor = System.Drawing.SystemColors.Window;
            this.txtFilePath1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFilePath1.Location = new System.Drawing.Point(88, 15);
            this.txtFilePath1.Name = "txtFilePath1";
            this.txtFilePath1.Size = new System.Drawing.Size(239, 20);
            this.txtFilePath1.TabIndex = 1;
            this.toolTip1.SetToolTip(this.txtFilePath1, "The file in this box is used for all read/write/create operations.\r\nif you use th" +
        "e functions above out of order, ensure your required\r\nfile is loaded into this b" +
        "ox first.");
            this.txtFilePath1.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtFilePath1_DragDrop);
            this.txtFilePath1.DragEnter += new System.Windows.Forms.DragEventHandler(this.txtFilePath1_DragEnter);
            // 
            // txtConsole
            // 
            this.txtConsole.BackColor = System.Drawing.Color.Black;
            this.txtConsole.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.txtConsole.ForeColor = System.Drawing.Color.White;
            this.txtConsole.Location = new System.Drawing.Point(12, 321);
            this.txtConsole.Multiline = true;
            this.txtConsole.Name = "txtConsole";
            this.txtConsole.ReadOnly = true;
            this.txtConsole.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtConsole.Size = new System.Drawing.Size(404, 253);
            this.txtConsole.TabIndex = 11;
            this.txtConsole.TabStop = false;
            this.toolTip1.SetToolTip(this.txtConsole, "The log window: This displays everything that is going on in JRunner!\r\nDouble cli" +
        "ck to save the log file to text file.");
            this.txtConsole.DoubleClick += new System.EventHandler(this.txtConsole_DoubleClick);
            // 
            // txtCPUKey
            // 
            this.txtCPUKey.AllowDrop = true;
            this.txtCPUKey.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCPUKey.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCPUKey.Location = new System.Drawing.Point(88, 73);
            this.txtCPUKey.MaxLength = 32;
            this.txtCPUKey.Name = "txtCPUKey";
            this.txtCPUKey.Size = new System.Drawing.Size(239, 20);
            this.txtCPUKey.TabIndex = 3;
            this.toolTip1.SetToolTip(this.txtCPUKey, "This is where your CPU key should be entered. You can drag and drop previously sa" +
        "ved cpukey.txt or paste in your CPU Key details.");
            this.txtCPUKey.TextChanged += new System.EventHandler(this.txtCPUKey_TextChanged);
            this.txtCPUKey.DragDrop += new System.Windows.Forms.DragEventHandler(this.txtCPUKey_DragDrop);
            this.txtCPUKey.DragEnter += new System.Windows.Forms.DragEventHandler(this.txtCPUKey_DragEnter);
            // 
            // lblCpuKey
            // 
            this.lblCpuKey.AutoSize = true;
            this.lblCpuKey.BackColor = System.Drawing.Color.Transparent;
            this.lblCpuKey.ForeColor = System.Drawing.Color.Black;
            this.lblCpuKey.Location = new System.Drawing.Point(18, 75);
            this.lblCpuKey.Name = "lblCpuKey";
            this.lblCpuKey.Size = new System.Drawing.Size(50, 13);
            this.lblCpuKey.TabIndex = 27;
            this.lblCpuKey.Text = "CPU Key";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(61, 295);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(304, 20);
            this.progressBar.Step = 1;
            this.progressBar.TabIndex = 59;
            // 
            // groupBox8
            // 
            this.groupBox8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.groupBox8.Controls.Add(this.btnCOM);
            this.groupBox8.Controls.Add(this.btnScanner);
            this.groupBox8.Controls.Add(this.labelIP);
            this.groupBox8.Controls.Add(this.txtIP);
            this.groupBox8.Controls.Add(this.btnIPGetCPU);
            this.groupBox8.Location = new System.Drawing.Point(599, 477);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(165, 97);
            this.groupBox8.TabIndex = 73;
            this.groupBox8.TabStop = false;
            this.toolTip1.SetToolTip(this.groupBox8, "If you connect your XBox360 to your PC using a network cable, \r\nOnce booted with " +
        "Xellous or Xell-reloaded place the displayed IP address in\r\nthe box and retrieve" +
        " your CPU Key by pressing the button.");
            // 
            // btnCOM
            // 
            this.btnCOM.Location = new System.Drawing.Point(80, 71);
            this.btnCOM.Name = "btnCOM";
            this.btnCOM.Size = new System.Drawing.Size(79, 26);
            this.btnCOM.TabIndex = 7;
            this.btnCOM.TabStop = false;
            this.btnCOM.Text = "Monitor COM";
            this.toolTip1.SetToolTip(this.btnCOM, "Displays Xell information during boot utilising COM Port - Additional HW and Wiri" +
        "ng required!");
            this.btnCOM.UseVisualStyleBackColor = true;
            this.btnCOM.Click += new System.EventHandler(this.btnCOM_Click);
            // 
            // btnScanner
            // 
            this.btnScanner.Location = new System.Drawing.Point(6, 71);
            this.btnScanner.Name = "btnScanner";
            this.btnScanner.Size = new System.Drawing.Size(68, 26);
            this.btnScanner.TabIndex = 6;
            this.btnScanner.TabStop = false;
            this.btnScanner.Text = "Scan IP";
            this.toolTip1.SetToolTip(this.btnScanner, "Scans IP range in settings page looking for Xell to retrieve CPU Key");
            this.btnScanner.UseVisualStyleBackColor = true;
            this.btnScanner.Click += new System.EventHandler(this.btnScanner_Click);
            // 
            // labelIP
            // 
            this.labelIP.AutoSize = true;
            this.labelIP.Location = new System.Drawing.Point(8, 17);
            this.labelIP.Name = "labelIP";
            this.labelIP.Size = new System.Drawing.Size(23, 13);
            this.labelIP.TabIndex = 5;
            this.labelIP.Text = "IP :";
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(37, 14);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(114, 20);
            this.txtIP.TabIndex = 5;
            this.txtIP.Text = "192.168.1.";
            this.txtIP.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtIP_KeyUp);
            // 
            // btnIPGetCPU
            // 
            this.btnIPGetCPU.Location = new System.Drawing.Point(6, 39);
            this.btnIPGetCPU.Name = "btnIPGetCPU";
            this.btnIPGetCPU.Size = new System.Drawing.Size(153, 26);
            this.btnIPGetCPU.TabIndex = 3;
            this.btnIPGetCPU.TabStop = false;
            this.btnIPGetCPU.Text = "Get CPU Key";
            this.btnIPGetCPU.UseVisualStyleBackColor = true;
            this.btnIPGetCPU.Click += new System.EventHandler(this.btnIPGetCPU_Click);
            // 
            // btnInit
            // 
            this.btnInit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInit.Location = new System.Drawing.Point(333, 70);
            this.btnInit.Name = "btnInit";
            this.btnInit.Size = new System.Drawing.Size(64, 23);
            this.btnInit.TabIndex = 61;
            this.btnInit.TabStop = false;
            this.btnInit.Text = "Re-Init";
            this.toolTip1.SetToolTip(this.btnInit, "Reloads and initializes the nand in source file box");
            this.btnInit.UseVisualStyleBackColor = true;
            this.btnInit.Click += new System.EventHandler(this.btnInit_Click);
            // 
            // btnWorkingFolder
            // 
            this.btnWorkingFolder.Location = new System.Drawing.Point(424, 516);
            this.btnWorkingFolder.Name = "btnWorkingFolder";
            this.btnWorkingFolder.Size = new System.Drawing.Size(169, 26);
            this.btnWorkingFolder.TabIndex = 62;
            this.btnWorkingFolder.TabStop = false;
            this.btnWorkingFolder.Text = "Show Working Folder";
            this.toolTip1.SetToolTip(this.btnWorkingFolder, "Opens the folder from which J-Runner is currently working");
            this.btnWorkingFolder.UseVisualStyleBackColor = true;
            this.btnWorkingFolder.Click += new System.EventHandler(this.btnWorkingFolder_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.Location = new System.Drawing.Point(424, 485);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(169, 26);
            this.btnSettings.TabIndex = 80;
            this.btnSettings.TabStop = false;
            this.btnSettings.Text = "J-Runner Settings";
            this.toolTip1.SetToolTip(this.btnSettings, "Allows you to alter many of the operations and settings of J-Runner");
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolsToolStripMenuItem,
            this.advancedToolStripMenuItem,
            this.jRPToolStripMenuItem,
            this.demoNToolStripMenuItem,
            this.devToolStripMenuItem,
            this.xBOXOneToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
            this.menuStrip1.TabIndex = 64;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.restartToolStripMenuItem,
            this.toolStripSeparator1,
            this.extractFilesToolStripMenuItem,
            this.toolStripSeparator11,
            this.sMCConfigViewerToolStripMenuItem1,
            this.xValueToolStripMenuItem,
            this.checkSecdataToolStripMenuItem,
            this.logPostToolStripMenuItem,
            this.updateToolStripMenuItem,
            this.toolStripHexEditor});
            this.toolsToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // restartToolStripMenuItem
            // 
            this.restartToolStripMenuItem.Name = "restartToolStripMenuItem";
            this.restartToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.restartToolStripMenuItem.Text = "Restart";
            this.restartToolStripMenuItem.Click += new System.EventHandler(this.restartToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(169, 6);
            // 
            // extractFilesToolStripMenuItem
            // 
            this.extractFilesToolStripMenuItem.Name = "extractFilesToolStripMenuItem";
            this.extractFilesToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.extractFilesToolStripMenuItem.Text = "Extract Files";
            this.extractFilesToolStripMenuItem.Click += new System.EventHandler(this.extractFilesToolStripMenuItem_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(169, 6);
            // 
            // sMCConfigViewerToolStripMenuItem1
            // 
            this.sMCConfigViewerToolStripMenuItem1.Name = "sMCConfigViewerToolStripMenuItem1";
            this.sMCConfigViewerToolStripMenuItem1.Size = new System.Drawing.Size(172, 22);
            this.sMCConfigViewerToolStripMenuItem1.Text = "SMC Config Editor";
            this.sMCConfigViewerToolStripMenuItem1.Click += new System.EventHandler(this.sMCConfigViewerToolStripMenuItem1_Click);
            // 
            // xValueToolStripMenuItem
            // 
            this.xValueToolStripMenuItem.Name = "xValueToolStripMenuItem";
            this.xValueToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.xValueToolStripMenuItem.Text = "XValue";
            this.xValueToolStripMenuItem.Click += new System.EventHandler(this.xValToolStripMenuItem_Click);
            // 
            // checkSecdataToolStripMenuItem
            // 
            this.checkSecdataToolStripMenuItem.Name = "checkSecdataToolStripMenuItem";
            this.checkSecdataToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.checkSecdataToolStripMenuItem.Text = "Check Secdata";
            this.checkSecdataToolStripMenuItem.Click += new System.EventHandler(this.checkSecdataToolStripMenuItem_Click_1);
            // 
            // logPostToolStripMenuItem
            // 
            this.logPostToolStripMenuItem.Name = "logPostToolStripMenuItem";
            this.logPostToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.logPostToolStripMenuItem.Text = "Monitor POST";
            this.logPostToolStripMenuItem.Click += new System.EventHandler(this.logPostToolStripMenuItem_Click);
            // 
            // updateToolStripMenuItem
            // 
            this.updateToolStripMenuItem.Name = "updateToolStripMenuItem";
            this.updateToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.updateToolStripMenuItem.Text = "Update JR-P fw";
            this.updateToolStripMenuItem.Click += new System.EventHandler(this.updateToolStripMenuItem_Click);
            // 
            // toolStripHexEditor
            // 
            this.toolStripHexEditor.Name = "toolStripHexEditor";
            this.toolStripHexEditor.Size = new System.Drawing.Size(172, 22);
            this.toolStripHexEditor.Text = "Hex Editor";
            this.toolStripHexEditor.Click += new System.EventHandler(this.toolStripHexEditor_Click);
            // 
            // advancedToolStripMenuItem
            // 
            this.advancedToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.customNandProCommandToolStripMenuItem,
            this.CustomXeBuildMenuItem,
            this.corona4GBToolStripMenuItem,
            this.writeFusionToolStripMenuItem,
            this.toolStripSeparator15,
            this.changeLDVToolStripMenuItem,
            this.createAnImageWithoutNanddumpbinToolStripMenuItem,
            this.patchNandToolStripMenuItem,
            this.toolStripSeparator2,
            this.pirsToolStripMenuItem,
            this.cBFuseToolStripMenuItem,
            this.getLatestSystemUpdateToolStripMenuItem});
            this.advancedToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.advancedToolStripMenuItem.Name = "advancedToolStripMenuItem";
            this.advancedToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            this.advancedToolStripMenuItem.Text = "Advanced";
            // 
            // customNandProCommandToolStripMenuItem
            // 
            this.customNandProCommandToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.customNandProCommandToolStripMenuItem.Name = "customNandProCommandToolStripMenuItem";
            this.customNandProCommandToolStripMenuItem.Size = new System.Drawing.Size(296, 22);
            this.customNandProCommandToolStripMenuItem.Text = "Custom Nand/CR Functions";
            this.customNandProCommandToolStripMenuItem.Click += new System.EventHandler(this.customNandProCommandToolStripMenuItem_Click);
            // 
            // CustomXeBuildMenuItem
            // 
            this.CustomXeBuildMenuItem.Name = "CustomXeBuildMenuItem";
            this.CustomXeBuildMenuItem.Size = new System.Drawing.Size(296, 22);
            this.CustomXeBuildMenuItem.Text = "Custom xeBuild Command";
            this.CustomXeBuildMenuItem.Click += new System.EventHandler(this.CustomXeBuildMenuItem_Click);
            // 
            // corona4GBToolStripMenuItem
            // 
            this.corona4GBToolStripMenuItem.Name = "corona4GBToolStripMenuItem";
            this.corona4GBToolStripMenuItem.Size = new System.Drawing.Size(296, 22);
            this.corona4GBToolStripMenuItem.Text = "Corona 4GB Read/Write";
            this.corona4GBToolStripMenuItem.Click += new System.EventHandler(this.corona4GBToolStripMenuItem_Click);
            // 
            // writeFusionToolStripMenuItem
            // 
            this.writeFusionToolStripMenuItem.Name = "writeFusionToolStripMenuItem";
            this.writeFusionToolStripMenuItem.Size = new System.Drawing.Size(296, 22);
            this.writeFusionToolStripMenuItem.Text = "DemoN/Fusion Write (remaps bad blocks)";
            this.writeFusionToolStripMenuItem.Click += new System.EventHandler(this.writeFusionToolStripMenuItem_Click);
            // 
            // toolStripSeparator15
            // 
            this.toolStripSeparator15.Name = "toolStripSeparator15";
            this.toolStripSeparator15.Size = new System.Drawing.Size(293, 6);
            // 
            // changeLDVToolStripMenuItem
            // 
            this.changeLDVToolStripMenuItem.Name = "changeLDVToolStripMenuItem";
            this.changeLDVToolStripMenuItem.Size = new System.Drawing.Size(296, 22);
            this.changeLDVToolStripMenuItem.Text = "Change Lock Down Value";
            this.changeLDVToolStripMenuItem.Click += new System.EventHandler(this.changeLDVToolStripMenuItem_Click);
            // 
            // createAnImageWithoutNanddumpbinToolStripMenuItem
            // 
            this.createAnImageWithoutNanddumpbinToolStripMenuItem.Name = "createAnImageWithoutNanddumpbinToolStripMenuItem";
            this.createAnImageWithoutNanddumpbinToolStripMenuItem.Size = new System.Drawing.Size(296, 22);
            this.createAnImageWithoutNanddumpbinToolStripMenuItem.Text = "Create an image without nanddump.bin";
            this.createAnImageWithoutNanddumpbinToolStripMenuItem.Click += new System.EventHandler(this.createAnImageWithoutNanddumpbinToolStripMenuItem_Click);
            // 
            // patchNandToolStripMenuItem
            // 
            this.patchNandToolStripMenuItem.Name = "patchNandToolStripMenuItem";
            this.patchNandToolStripMenuItem.Size = new System.Drawing.Size(296, 22);
            this.patchNandToolStripMenuItem.Text = "Patch Nand";
            this.patchNandToolStripMenuItem.Click += new System.EventHandler(this.patchNandToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(293, 6);
            // 
            // pirsToolStripMenuItem
            // 
            this.pirsToolStripMenuItem.Name = "pirsToolStripMenuItem";
            this.pirsToolStripMenuItem.Size = new System.Drawing.Size(296, 22);
            this.pirsToolStripMenuItem.Text = "Pirs";
            this.pirsToolStripMenuItem.Click += new System.EventHandler(this.pirsToolStripMenuItem_Click);
            // 
            // cBFuseToolStripMenuItem
            // 
            this.cBFuseToolStripMenuItem.Name = "cBFuseToolStripMenuItem";
            this.cBFuseToolStripMenuItem.Size = new System.Drawing.Size(296, 22);
            this.cBFuseToolStripMenuItem.Text = "CB Fuse";
            this.cBFuseToolStripMenuItem.Click += new System.EventHandler(this.cBFuseToolStripMenuItem_Click);
            // 
            // getLatestSystemUpdateToolStripMenuItem
            // 
            this.getLatestSystemUpdateToolStripMenuItem.Name = "getLatestSystemUpdateToolStripMenuItem";
            this.getLatestSystemUpdateToolStripMenuItem.Size = new System.Drawing.Size(296, 22);
            this.getLatestSystemUpdateToolStripMenuItem.Text = "Get Latest SystemUpdate";
            this.getLatestSystemUpdateToolStripMenuItem.Click += new System.EventHandler(this.getLatestSystemUpdateToolStripMenuItem_Click);
            // 
            // jRPToolStripMenuItem
            // 
            this.jRPToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.powerOnToolStripMenuItem,
            this.shutdownToolStripMenuItem,
            this.bootloaderModeToolStripMenuItem});
            this.jRPToolStripMenuItem.Name = "jRPToolStripMenuItem";
            this.jRPToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
            this.jRPToolStripMenuItem.Text = "JR-P";
            this.jRPToolStripMenuItem.Visible = false;
            // 
            // powerOnToolStripMenuItem
            // 
            this.powerOnToolStripMenuItem.Name = "powerOnToolStripMenuItem";
            this.powerOnToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.powerOnToolStripMenuItem.Text = "Power on";
            this.powerOnToolStripMenuItem.Click += new System.EventHandler(this.powerOnToolStripMenuItem_Click);
            // 
            // shutdownToolStripMenuItem
            // 
            this.shutdownToolStripMenuItem.Name = "shutdownToolStripMenuItem";
            this.shutdownToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.shutdownToolStripMenuItem.Text = "Shutdown";
            this.shutdownToolStripMenuItem.Click += new System.EventHandler(this.shutdownToolStripMenuItem_Click);
            // 
            // bootloaderModeToolStripMenuItem
            // 
            this.bootloaderModeToolStripMenuItem.Name = "bootloaderModeToolStripMenuItem";
            this.bootloaderModeToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.bootloaderModeToolStripMenuItem.Text = "Bootloader Mode";
            this.bootloaderModeToolStripMenuItem.Click += new System.EventHandler(this.bootloaderModeToolStripMenuItem_Click);
            // 
            // demoNToolStripMenuItem
            // 
            this.demoNToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toggleNANDToolStripMenuItem,
            this.powerOnToolStripMenuItem1,
            this.powerOffToolStripMenuItem,
            this.updateFwToolStripMenuItem,
            this.getInvalidBlocksToolStripMenuItem});
            this.demoNToolStripMenuItem.Name = "demoNToolStripMenuItem";
            this.demoNToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.demoNToolStripMenuItem.Text = "DemoN";
            this.demoNToolStripMenuItem.Visible = false;
            // 
            // toggleNANDToolStripMenuItem
            // 
            this.toggleNANDToolStripMenuItem.Name = "toggleNANDToolStripMenuItem";
            this.toggleNANDToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.toggleNANDToolStripMenuItem.Text = "Toggle NAND";
            this.toggleNANDToolStripMenuItem.Click += new System.EventHandler(this.toggleNANDToolStripMenuItem_Click);
            // 
            // powerOnToolStripMenuItem1
            // 
            this.powerOnToolStripMenuItem1.Name = "powerOnToolStripMenuItem1";
            this.powerOnToolStripMenuItem1.Size = new System.Drawing.Size(167, 22);
            this.powerOnToolStripMenuItem1.Text = "Power On";
            this.powerOnToolStripMenuItem1.Click += new System.EventHandler(this.powerOnToolStripMenuItem1_Click);
            // 
            // powerOffToolStripMenuItem
            // 
            this.powerOffToolStripMenuItem.Name = "powerOffToolStripMenuItem";
            this.powerOffToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.powerOffToolStripMenuItem.Text = "Power Off";
            this.powerOffToolStripMenuItem.Click += new System.EventHandler(this.powerOffToolStripMenuItem_Click);
            // 
            // updateFwToolStripMenuItem
            // 
            this.updateFwToolStripMenuItem.Name = "updateFwToolStripMenuItem";
            this.updateFwToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.updateFwToolStripMenuItem.Text = "Update DemoN";
            this.updateFwToolStripMenuItem.Click += new System.EventHandler(this.updateFwToolStripMenuItem_Click);
            // 
            // getInvalidBlocksToolStripMenuItem
            // 
            this.getInvalidBlocksToolStripMenuItem.Name = "getInvalidBlocksToolStripMenuItem";
            this.getInvalidBlocksToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.getInvalidBlocksToolStripMenuItem.Text = "Get Invalid Blocks";
            this.getInvalidBlocksToolStripMenuItem.Click += new System.EventHandler(this.getInvalidBlocksToolStripMenuItem_Click);
            // 
            // devToolStripMenuItem
            // 
            this.devToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemVNand,
            this.toolStripSeparator10,
            this.debugToolStripMenuItem});
            this.devToolStripMenuItem.Name = "devToolStripMenuItem";
            this.devToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.devToolStripMenuItem.Text = "Dev";
            this.devToolStripMenuItem.Visible = false;
            // 
            // toolStripMenuItemVNand
            // 
            this.toolStripMenuItemVNand.Name = "toolStripMenuItemVNand";
            this.toolStripMenuItemVNand.Size = new System.Drawing.Size(110, 22);
            this.toolStripMenuItemVNand.Text = "VNand";
            this.toolStripMenuItemVNand.Click += new System.EventHandler(this.toolStripMenuItemVNand_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(107, 6);
            // 
            // debugToolStripMenuItem
            // 
            this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            this.debugToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.debugToolStripMenuItem.Text = "Debug";
            this.debugToolStripMenuItem.Click += new System.EventHandler(this.debugToolStripMenuItem_Click);
            // 
            // xBOXOneToolStripMenuItem
            // 
            this.xBOXOneToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hDDToolToolStripMenuItem});
            this.xBOXOneToolStripMenuItem.Name = "xBOXOneToolStripMenuItem";
            this.xBOXOneToolStripMenuItem.Size = new System.Drawing.Size(74, 20);
            this.xBOXOneToolStripMenuItem.Text = "XBOX One";
            // 
            // hDDToolToolStripMenuItem
            // 
            this.hDDToolToolStripMenuItem.Name = "hDDToolToolStripMenuItem";
            this.hDDToolToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.hDDToolToolStripMenuItem.Text = "HDD Tool";
            this.hDDToolToolStripMenuItem.Click += new System.EventHandler(this.hDDToolToolStripMenuItem_Click);
            // 
            // XeBuildOptionsToolStripMenuItem
            // 
            this.XeBuildOptionsToolStripMenuItem.Name = "XeBuildOptionsToolStripMenuItem";
            this.XeBuildOptionsToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.XeBuildOptionsToolStripMenuItem.Text = "XeBuild Options";
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.groupBox4.Controls.Add(this.btnLoadFile2);
            this.groupBox4.Controls.Add(this.btnInit);
            this.groupBox4.Controls.Add(this.txtFilePath2);
            this.groupBox4.Controls.Add(this.txtFilePath1);
            this.groupBox4.Controls.Add(this.btnLoadFile1);
            this.groupBox4.Controls.Add(this.lblCpuKey);
            this.groupBox4.Controls.Add(this.comparebutton);
            this.groupBox4.Controls.Add(this.txtCPUKey);
            this.groupBox4.Location = new System.Drawing.Point(12, 192);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(404, 101);
            this.groupBox4.TabIndex = 67;
            this.groupBox4.TabStop = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusBlank,
            this.xeBuildStatus,
            this.xebuildVersion,
            this.BlankSpace,
            this.DashLaunchStatus,
            this.DashLaunchVersion,
            this.ModeStatus,
            this.ModeVersion,
            this.FWStatus,
            this.FWVersion,
            this.FlashStatus,
            this.FlashVersion});
            this.statusStrip1.Location = new System.Drawing.Point(0, 589);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(784, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 75;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusBlank
            // 
            this.StatusBlank.Name = "StatusBlank";
            this.StatusBlank.Size = new System.Drawing.Size(10, 17);
            this.StatusBlank.Text = " ";
            // 
            // xeBuildStatus
            // 
            this.xeBuildStatus.Name = "xeBuildStatus";
            this.xeBuildStatus.Size = new System.Drawing.Size(92, 17);
            this.xeBuildStatus.Text = "xeBuild Version: ";
            // 
            // xebuildVersion
            // 
            this.xebuildVersion.Name = "xebuildVersion";
            this.xebuildVersion.Size = new System.Drawing.Size(0, 17);
            // 
            // BlankSpace
            // 
            this.BlankSpace.Name = "BlankSpace";
            this.BlankSpace.Size = new System.Drawing.Size(127, 17);
            this.BlankSpace.Text = "                                        ";
            // 
            // DashLaunchStatus
            // 
            this.DashLaunchStatus.Name = "DashLaunchStatus";
            this.DashLaunchStatus.Size = new System.Drawing.Size(116, 17);
            this.DashLaunchStatus.Text = "DashLaunch Version:";
            // 
            // DashLaunchVersion
            // 
            this.DashLaunchVersion.Name = "DashLaunchVersion";
            this.DashLaunchVersion.Size = new System.Drawing.Size(0, 17);
            // 
            // ModeStatus
            // 
            this.ModeStatus.AutoSize = false;
            this.ModeStatus.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.ModeStatus.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.ModeStatus.Name = "ModeStatus";
            this.ModeStatus.Size = new System.Drawing.Size(198, 17);
            this.ModeStatus.Spring = true;
            this.ModeStatus.Text = "MODE: ";
            this.ModeStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ModeStatus.Visible = false;
            // 
            // ModeVersion
            // 
            this.ModeVersion.AutoSize = false;
            this.ModeVersion.Name = "ModeVersion";
            this.ModeVersion.Size = new System.Drawing.Size(70, 17);
            this.ModeVersion.Text = "NOMODE";
            this.ModeVersion.Visible = false;
            // 
            // FWStatus
            // 
            this.FWStatus.AutoSize = false;
            this.FWStatus.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.FWStatus.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.FWStatus.Name = "FWStatus";
            this.FWStatus.Size = new System.Drawing.Size(34, 17);
            this.FWStatus.Text = "FW: ";
            this.FWStatus.Visible = false;
            // 
            // FWVersion
            // 
            this.FWVersion.AutoSize = false;
            this.FWVersion.Name = "FWVersion";
            this.FWVersion.Size = new System.Drawing.Size(28, 17);
            this.FWVersion.Text = "0.99";
            this.FWVersion.Visible = false;
            // 
            // FlashStatus
            // 
            this.FlashStatus.AutoSize = false;
            this.FlashStatus.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.FlashStatus.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.FlashStatus.Name = "FlashStatus";
            this.FlashStatus.Size = new System.Drawing.Size(44, 17);
            this.FlashStatus.Text = "FLASH: ";
            this.FlashStatus.Visible = false;
            // 
            // FlashVersion
            // 
            this.FlashVersion.AutoSize = false;
            this.FlashVersion.Name = "FlashVersion";
            this.FlashVersion.Size = new System.Drawing.Size(50, 17);
            this.FlashVersion.Text = "NOFLASH";
            this.FlashVersion.Visible = false;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Enabled = false;
            this.splitter1.Location = new System.Drawing.Point(0, 586);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(784, 3);
            this.splitter1.TabIndex = 78;
            this.splitter1.TabStop = false;
            // 
            // btnCheckBadBlocks
            // 
            this.btnCheckBadBlocks.Location = new System.Drawing.Point(0, 0);
            this.btnCheckBadBlocks.Name = "btnCheckBadBlocks";
            this.btnCheckBadBlocks.Size = new System.Drawing.Size(75, 23);
            this.btnCheckBadBlocks.TabIndex = 0;
            // 
            // pnlInfo
            // 
            this.pnlInfo.Location = new System.Drawing.Point(424, 219);
            this.pnlInfo.Name = "pnlInfo";
            this.pnlInfo.Size = new System.Drawing.Size(342, 260);
            this.pnlInfo.TabIndex = 79;
            // 
            // txtBlocks
            // 
            this.txtBlocks.BackColor = System.Drawing.SystemColors.Control;
            this.txtBlocks.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBlocks.Location = new System.Drawing.Point(369, 299);
            this.txtBlocks.Name = "txtBlocks";
            this.txtBlocks.ReadOnly = true;
            this.txtBlocks.Size = new System.Drawing.Size(47, 13);
            this.txtBlocks.TabIndex = 60;
            this.txtBlocks.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 299);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 83;
            this.label1.Text = "Progress";
            // 
            // pnlTools
            // 
            this.pnlTools.Location = new System.Drawing.Point(9, 23);
            this.pnlTools.Name = "pnlTools";
            this.pnlTools.Size = new System.Drawing.Size(416, 175);
            this.pnlTools.TabIndex = 84;
            // 
            // pnlExtra
            // 
            this.pnlExtra.Location = new System.Drawing.Point(424, 23);
            this.pnlExtra.Name = "pnlExtra";
            this.pnlExtra.Size = new System.Drawing.Size(342, 189);
            this.pnlExtra.TabIndex = 85;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(784, 611);
            this.Controls.Add(this.pnlExtra);
            this.Controls.Add(this.pnlTools);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.btnWorkingFolder);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.txtBlocks);
            this.Controls.Add(this.pnlInfo);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.txtConsole);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(800, 650);
            this.MinimumSize = new System.Drawing.Size(800, 650);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "J-Runner";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }



        #endregion

        private Button btnExit;
        private Button comparebutton;
        private Button btnLoadFile2;
        private Button btnLoadFile1;
        private TextBox txtFilePath2;
        private TextBox txtFilePath1;
        private TextBox txtCPUKey;
        private Label lblCpuKey;
        private TextBox txtConsole;
        private ProgressBar progressBar;
        private ToolTip toolTip1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem toolsToolStripMenuItem;
        private ToolStripMenuItem advancedToolStripMenuItem;
        private ToolStripMenuItem customNandProCommandToolStripMenuItem;
        private GroupBox groupBox8;
        private Label labelIP;
        private TextBox txtIP;
        private Button btnIPGetCPU;
        private GroupBox groupBox4;
        private ToolStripMenuItem changeLDVToolStripMenuItem;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel xeBuildStatus;
        private ToolStripStatusLabel xebuildVersion;
        private ToolStripStatusLabel BlankSpace;
        private ToolStripStatusLabel DashLaunchStatus;
        private ToolStripStatusLabel DashLaunchVersion;
        private ToolStripStatusLabel StatusBlank;
        private Button btnInit;
        private ToolStripMenuItem XeBuildOptionsToolStripMenuItem;
        private ToolStripMenuItem patchNandToolStripMenuItem;
        private ToolStripMenuItem extractFilesToolStripMenuItem;
        private ToolStripMenuItem createAnImageWithoutNanddumpbinToolStripMenuItem;
        private Button btnWorkingFolder;
        private ToolStripMenuItem sMCConfigViewerToolStripMenuItem1;
        private ToolStripMenuItem devToolStripMenuItem;
        private ToolStripMenuItem debugToolStripMenuItem;
        private ToolStripMenuItem shutdownToolStripMenuItem;
        private ToolStripMenuItem updateToolStripMenuItem;
        private ToolStripMenuItem powerOnToolStripMenuItem;
        private ToolStripMenuItem logPostToolStripMenuItem;
        private ToolStripMenuItem demoNToolStripMenuItem;
        private ToolStripStatusLabel ModeStatus;
        private ToolStripStatusLabel ModeVersion;
        private ToolStripStatusLabel FWStatus;
        private ToolStripStatusLabel FWVersion;
        private ToolStripStatusLabel FlashStatus;
        private ToolStripStatusLabel FlashVersion;
        private ToolStripMenuItem bootloaderModeToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator10;
        private ToolStripMenuItem writeFusionToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator11;
        private ToolStripMenuItem toggleNANDToolStripMenuItem;
        private ToolStripMenuItem powerOnToolStripMenuItem1;
        private ToolStripMenuItem powerOffToolStripMenuItem;
        private ToolStripMenuItem updateFwToolStripMenuItem;
        private Splitter splitter1;
        private ToolStripMenuItem getInvalidBlocksToolStripMenuItem;
        private Button btnCOM;
        private Button btnScanner;
        private ToolStripMenuItem corona4GBToolStripMenuItem;
        private ToolStripMenuItem toolStripHexEditor;
        private Button btnCheckBadBlocks;
        private FolderBrowserDialog folderBrowserDialog1;
        private ToolStripMenuItem pirsToolStripMenuItem;
        private ToolStripMenuItem CustomXeBuildMenuItem;
        private Panel pnlInfo;
        private ToolStripSeparator toolStripSeparator15;
        private TextBox txtBlocks;
        private Button btnSettings;
        private Label label1;
        private Panel pnlTools;
        private Panel pnlExtra;
        private ToolStripMenuItem restartToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem xValueToolStripMenuItem;
        private ToolStripMenuItem checkSecdataToolStripMenuItem;
        private ToolStripMenuItem getLatestSystemUpdateToolStripMenuItem;
        private ToolStripMenuItem toolStripMenuItemVNand;
        private ToolStripMenuItem jRPToolStripMenuItem;
        private ToolStripMenuItem xBOXOneToolStripMenuItem;
        private ToolStripMenuItem hDDToolToolStripMenuItem;
        private ToolStripMenuItem cBFuseToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;

    }
}

