using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading;

namespace JRunner.Panels
{
    public partial class XeBuildPanel : UserControl
    {
        List<String> patches = new List<string>(new string[8]);
        // -a nofcrt
        // -a noSShdd
        // -a nointmu
        // -a nohdd
        // -a nohdmiwait
        // -a nolan
        // -r WB/WB4G/13182

        public XeBuildPanel()
        {
            InitializeComponent();
        }

        #region delegates
        public delegate void ClickedGetMB();
        public event ClickedGetMB getmb;
        public delegate void ChangedHack();
        public event ChangedHack HackChanged;
        public delegate void DashAdded();
        public event DashAdded AddedDash;
        public delegate void DashDeleted();
        public event DashDeleted DeletedDash;
        public delegate void CallMotherboards();
        public event CallMotherboards CallMB;
        public delegate void loadFile(ref string filename, bool erase = false);
        public event loadFile loadFil;
        public delegate void updateProgress(int progress);
        public event updateProgress UpdateProgres;
        public delegate void updateSource(string filename);
        public event updateSource updateSourc;
        public delegate void ModeDrive();
        public event ModeDrive DriveMode;
        #endregion

        #region getters/setters
        public DataSet1 getDataSet()
        {
            return dataSet1;
        }
        public BindingSource getDatatable()
        {
            return dataTable2BindingSource;
        }
        public ComboBox getComboDash()
        {
            return comboDash;
        }
        public void setDashIndex(int index)
        {
            comboDash.SelectedIndex = index;
        }
        public bool getRbtnGlitchChecked()
        {
            return rbtnGlitch.Checked;
        }
        public void setRbtnGlitchChecked(bool check)
        {
            rbtnGlitch.Checked = check;
        }
        public void setRbtnGlitchEnabled(bool enabled)
        {
            rbtnGlitch.Enabled = enabled;
        }
        public bool getRbtnRetailChecked()
        {
            return rbtnRetail.Checked;
        }
        public void setRbtnRetailChecked(bool check)
        {
            rbtnRetail.Checked = check;
        }
        public void setRbtnRetailEnabled(bool enabled)
        {
            rbtnRetail.Enabled = enabled;
        }
        public bool getRbtnRGH2Checked()
        {
            return rbtnGlitch2.Checked;
        }
        public bool getRbtnGlitch2mChecked()
        {
            return rbtnGlitch2m.Checked;
        }
        public void setRbtnRGH2Checked(bool check)
        {
            rbtnGlitch2.Checked = check;
        }
        public bool getRbtnJtagChecked()
        {
            return rbtnJtag.Checked;
        }
        public void setRbtnJtagChecked(bool check)
        {
            rbtnJtag.Checked = check;
        }
        public void setRbtnJtagEnabled(bool enabled)
        {
            rbtnJtag.Enabled = enabled;
        }
        public bool getAudClampChecked()
        {
            return checkAUDclamp.Checked;
        }
        public bool getRJtagChecked()
        {
            return chkRJtag.Checked;
        }
        public bool getCR4Checked()
        {
            return chkCR4.Checked;
        }

        public void setRJtagVisible(bool enabled)
        {
            chkRJtag.Visible = enabled;
        }
        public void setMBname(string txt)
        {
            txtMBname.Text = txt;
            if (txt.Contains("Corona") || txt.Contains("Trinity")) rbtnGlitch2m.Enabled = true;
            else rbtnGlitch2m.Enabled = false;
        }
        public CheckedListBox getPatches()
        {
            return chkListBoxPatches;
        }
        #endregion

        #region UI

        private void btnRater_Click(object sender, EventArgs e)
        {
            if (!Application.OpenForms.OfType<POST>().Any())
            {
                POST ps = new POST();
                ps.Show();
            }
        }

        private void btnSonus_Click(object sender, EventArgs e)
        {
            if (!Application.OpenForms.OfType<Forms.SoundEditor>().Any())
            {
                Forms.SoundEditor se = new Forms.SoundEditor();
                se.ShowDialog();
            }
        }

        private void rbtn_CheckedChanged(object sender, EventArgs e)
        {
            chkRJtag.Visible = rbtnJtag.Checked;
            chkCR4.Visible = rbtnGlitch2.Checked;
            checkAUDclamp.Visible = rbtnJtag.Checked;
            label3.Visible = comboCB.Visible = rbtnRetail.Checked;
            groupBox1.Enabled = !rbtnRetail.Checked;
            //chkGlitch2m.Visible = rbtnGlitch2.Checked;
            //if (!chkGlitch2m.Visible) chkGlitch2m.Checked = false;
            if (!chkRJtag.Visible) chkRJtag.Checked = false;
            if (!checkAUDclamp.Visible) checkAUDclamp.Checked = false;

            if (rbtnRetail.Checked)
            {
                if (checkDLPatches.Checked) variables.DashLaunchE = checkDLPatches.Checked;
                checkDLPatches.Enabled = chkLaunch.Visible = false;
                if (sender.Equals(rbtnRetail)) Console.WriteLine("Retail Selected");
            }
            else if (rbtnJtag.Checked)
            {
                checkDLPatches.Enabled = true;
                checkDLPatches.Checked = chkLaunch.Visible = variables.DashLaunchE;
                if (sender.Equals(rbtnJtag)) Console.WriteLine("Jtag Selected");

            }
            else if (rbtnGlitch.Checked || rbtnGlitch2.Checked || rbtnGlitch2m.Checked)
            {
                checkDLPatches.Enabled = true;
                checkDLPatches.Checked = chkLaunch.Visible = variables.DashLaunchE;
                if (rbtnGlitch.Checked && sender.Equals(rbtnGlitch)) Console.WriteLine("Glitch Selected");
                else if (rbtnGlitch2.Checked && sender.Equals(rbtnGlitch2)) Console.WriteLine("Glitch2 Selected");
                else if (rbtnGlitch2m.Checked && sender.Equals(rbtnGlitch2m)) Console.WriteLine("Glitch2m Selected");
            }

            try
            {
                HackChanged();
            }
            catch (Exception) { }

            updateCommand();
            setComboCB(rbtnRetail.Checked);
        }

        private void btnXeBuildOptions_Click(object sender, EventArgs e)
        {
            XBOptions xb = new XBOptions();
            xb.ShowDialog();
            chkxesettings.Checked = true;
        }

        private void btnLaunch_Click(object sender, EventArgs e)
        {
            DashLaunch myNewForm3 = new DashLaunch();
            myNewForm3.ShowDialog();
        }

        private void txtMBname_Click(object sender, EventArgs e)
        {
            CallMB();
        }

        private void comboDash_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboDash.SelectedIndex < comboDash.Items.Count - 3)
            {
                if (variables.debugme) Console.WriteLine((Path.Combine(variables.update_path, comboDash.SelectedValue + "\\_file.ini")));
                if (!File.Exists(Path.Combine(variables.update_path, comboDash.SelectedValue + "\\_glitch2.ini")))
                {
                    rbtnGlitch2.Enabled = rbtnGlitch2.Checked = false;
                }
                else
                {
                    rbtnGlitch2.Enabled = true;
                }

                if (!File.Exists(Path.Combine(variables.update_path, comboDash.SelectedValue + "\\_glitch2m.ini")))
                {
                    rbtnGlitch2m.Enabled = false;
                }
                else
                {
                    rbtnGlitch2m.Enabled = true;
                }

                if (!File.Exists(Path.Combine(variables.update_path, comboDash.SelectedValue + "/_glitch.ini")) &&
                    !File.Exists(Path.Combine(variables.update_path, comboDash.SelectedValue + "/_glitch2.ini")))
                {
                    rbtnGlitch.Enabled = rbtnGlitch.Checked = rbtnGlitch2.Enabled = rbtnGlitch2.Checked = false;
                }
                else
                {
                    if (variables.rghable) rbtnGlitch.Enabled = true;
                }

                if (!File.Exists(Path.Combine(variables.update_path, comboDash.SelectedValue + "/_jtag.ini")))
                {
                    rbtnJtag.Enabled = rbtnJtag.Checked = false;
                }
                else
                {
                    rbtnJtag.Enabled = true;
                }

                if (!File.Exists(Path.Combine(variables.update_path, comboDash.SelectedValue + "/_retail.ini")))
                {
                    rbtnRetail.Checked = rbtnRetail.Enabled = false;
                }
                else
                {
                    rbtnRetail.Enabled = true;
                }
            }

            if (comboDash.SelectedIndex == comboDash.Items.Count - 2)
            {
                add_dash();
            }
            else if (comboDash.SelectedIndex == comboDash.Items.Count - 1)
            {
                del_dash();
            }
            else if (comboDash.SelectedIndex == comboDash.Items.Count - 3)
            {
                return;
            }
            else if (comboDash.SelectedIndex >= 0)
            {
                variables.preferredDash = comboDash.Text;
                variables.dashversion = Convert.ToInt32(comboDash.Text);
                lblDash.Text = comboDash.Text;
            }

            updateCommand();
            setComboCB();
        }

        private void checkDLPatches_CheckedChanged(object sender, EventArgs e)
        {
            variables.DashLaunchE = checkDLPatches.Checked;
            if (!checkDLPatches.Checked || !checkDLPatches.Enabled) { chkLaunch.Visible = false; chkLaunch.Checked = false; }
            else if (checkDLPatches.Checked && checkDLPatches.Enabled) chkLaunch.Visible = true;
        }

        public void setDLPatches(bool checkd)
        {
            checkDLPatches.Checked = chkLaunch.Checked = checkd;
        }

        private void btnDrive_Click(object sender, EventArgs e)
        {
            try
            {
                DriveMode();
            }
            catch (Exception) { }
        }

        private void checkDLPatches_EnabledChanged(object sender, EventArgs e)
        {
            if (!checkDLPatches.Enabled) chkLaunch.Visible = false;
            else if (checkDLPatches.Checked) chkLaunch.Visible = true;
        }

        private void chkListBoxPatches_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selected = chkListBoxPatches.SelectedIndex;
            if (selected >= 0 && selected <= 5)
            {
                if (chkListBoxPatches.GetItemChecked(selected))
                {
                    Console.WriteLine(chkListBoxPatches.Items[selected].ToString() + " Enabled");
                    if (selected == 0) patches[selected + 1] = "-a nofcrt";
                    else if (selected == 1) patches[selected + 1] = "-a noSShdd";
                    else if (selected == 2) patches[selected + 1] = "-a nointmu";
                    else if (selected == 3) patches[selected + 1] = "-a nohdd";
                    else if (selected == 4) patches[selected + 1] = "-a nohdmiwait";
                    else if (selected == 5) patches[selected + 1] = "-a nolan";
                }
                else
                {
                    Console.WriteLine(chkListBoxPatches.Items[selected].ToString() + " Disabled");
                    patches[selected + 1] = "";
                }
            }

            updateCommand();
        }

        private void chkRJtag_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRJtag.Checked) Console.WriteLine("R-Jtag Selected");
            else Console.WriteLine("R-Jtag de-Selected");
        }

        private void chkCR4_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCR4.Checked) Console.WriteLine("CR4 Selected");
            else Console.WriteLine("CR4 de-Selected");
        }

        private void checkAUDclamp_CheckedChanged(object sender, EventArgs e)
        {
            if (checkAUDclamp.Checked) Console.WriteLine("Aud_Clamp Selected");
            else Console.WriteLine("Aud_Clamp de-Selected");
        }

        private void btnGetMB_Click(object sender, EventArgs e)
        {
            getmb();
        }

        private void btnXEUpdate_Click(object sender, EventArgs e)
        {
            ThreadStart starter = delegate { xe_update(); };
            new Thread(starter).Start();
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            if (File.Exists(Path.Combine(variables.outfolder, "consoleDump.bin")))
            {
                MessageBox.Show("consoleDump.bin already exists");
                return;
            }
            string arguments;
            if (!String.IsNullOrWhiteSpace(txtOffset.Text))
            {
                arguments = "-rb " + "\"" + Path.Combine(variables.outfolder, "consoleDump.bin") + "\"";
                arguments += " " + txtOffset.Text;
                if (!String.IsNullOrWhiteSpace(txtLength.Text))
                {
                    arguments += " " + txtLength.Text;
                }
            }
            else
            {
                arguments = "-r " + "\"" + Path.Combine(variables.outfolder, "consoleDump.bin") + "\"";
            }

            if (chkShutdown.Checked) arguments += " -s";
            if (chkReboot.Checked) arguments += " -reboot";
            if (chkForceIP.Checked) arguments += " -ip " + txtIP2.Text;
            Console.WriteLine("Starting Read - please wait......");
            ThreadStart starter = delegate { xe_client(arguments); };
            new Thread(starter).Start();

        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            string arguments;
            if (!String.IsNullOrWhiteSpace(txtOffset.Text))
            {
                arguments = "-wb " + "\"" + variables.filename1 + "\"";
                arguments += " " + txtOffset.Text;
                if (!String.IsNullOrWhiteSpace(txtLength.Text))
                {
                    arguments += " " + txtLength.Text;
                }
            }
            else
            {
                arguments = "-w " + "\"" + variables.filename1 + "\"";
            }
            if (chkShutdown.Checked) arguments += " -s";
            if (chkReboot.Checked) arguments += " -reboot";
            if (chkForceIP.Checked) arguments += " -ip " + txtIP2.Text;
            Console.WriteLine("Starting Write - please wait......");
            ThreadStart starter = delegate { xe_client(arguments); };
            new Thread(starter).Start();

        }

        private void btnErase_Click(object sender, EventArgs e)
        {
            string arguments = "-eb " + txtOffset.Text;
            if (chkShutdown.Checked) arguments += " -s";
            if (chkReboot.Checked) arguments += " -reboot";
            if (chkForceIP.Checked) arguments += " -ip " + txtIP2.Text;

            ThreadStart starter = delegate { xe_client(arguments); };
            new Thread(starter).Start();
        }

        private void btnPatches_Click(object sender, EventArgs e)
        {
            string arguments = "-p";
            if (!String.IsNullOrWhiteSpace(variables.filename1))
            {
                if (MessageBox.Show("Make sure that source file is a patch file.", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel) return;
                arguments += " \"" + variables.filename1 + "\"";
            }
            if (chkShutdown.Checked) arguments += " -s";
            if (chkReboot.Checked) arguments += " -reboot";
            if (chkForceIP.Checked) arguments += " -ip " + txtIP2.Text;

            ThreadStart starter = delegate { xe_client(arguments); };
            new Thread(starter).Start();
        }

        private void btnAvatar_Click(object sender, EventArgs e)
        {

            if (variables.debugme) Console.WriteLine(Path.Combine(variables.update_path, comboDash.Text + @"\$systemupdate"));
            if (Directory.Exists(Path.Combine(variables.update_path, comboDash.Text + @"\$systemupdate")))
            {
                Console.WriteLine("Starting, please wait!");
                string upPath = Path.Combine(variables.update_path, comboDash.Text, @"\$systemupdate");
                //Path.Combine(upPath, @"\$systemupdate");
                // Console.WriteLine(Path.Combine(variables.update_path, comboDash.Text + @"\$systemupdate"));
                ThreadStart starter = delegate { xe_compatibilityAvatar(Path.Combine(variables.update_path, (comboDash.Text)), "-e "); };
                new Thread(starter).Start();
            }
            else
            {
                FolderBrowserDialog fd = new FolderBrowserDialog();
                if (fd.ShowDialog() == DialogResult.Cancel) return;
                Console.WriteLine("Starting, please wait!");
                ThreadStart starter = delegate { xe_compatibilityAvatar(fd.SelectedPath, "-e "); };
                new Thread(starter).Start();
            }
        }

        private void btnComp_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fd = new FolderBrowserDialog();
            if (fd.ShowDialog() == DialogResult.Cancel) return;


            ThreadStart starter = delegate { xe_compatibilityAvatar(fd.SelectedPath, "-c "); };
            new Thread(starter).Start();
        }

        private void chkForceIP_CheckedChanged(object sender, EventArgs e)
        {
            if (chkForceIP.Checked)
            {
                txtIP.Enabled = txtIP2.Enabled = chkForceIP2.Checked = true;
                txtIP.Text = txtIP2.Text = "";
                Console.WriteLine("ForceIP Selected");

            }
            else
            {
                txtIP.Enabled = txtIP2.Enabled = chkForceIP2.Checked = false;
                txtIP.Text = txtIP2.Text = "Autoscan LAN";
                Console.WriteLine("ForceIP de-Selected");
            }
        }

        private void txtIP2_TextChanged(object sender, EventArgs e)
        {
            txtIP.Text = txtIP2.Text;
        }

        private void chkForceIP2_CheckedChanged(object sender, EventArgs e)
        {
            if (chkForceIP2.Checked)
            {
                txtIP.Enabled = txtIP2.Enabled = chkForceIP.Checked = true;
                txtIP.Text = txtIP2.Text = "";
            }
            else
            {
                txtIP.Enabled = txtIP2.Enabled = chkForceIP.Checked = false;
                txtIP.Text = txtIP2.Text = "Autoscan LAN";
            }
        }

        private void txtIP_TextChanged(object sender, EventArgs e)
        {
            txtIP2.Text = txtIP.Text;
        }

        private void XeBuildPanel_Load(object sender, EventArgs e)
        {
            chkCR4.Visible = false;
            chkRJtag.Visible = false;
            checkAUDclamp.Visible = false;
            groupBox1.Enabled = false;
#if Dev
            btnDrive.Visible = true;
#endif
            setComboCB();
        }

        private void chkNoWrite_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNoWrite.Checked) Console.WriteLine("nowrite Selected");
            else Console.WriteLine("nowrite de-Selected");
        }

        private void chkNoAva_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNoAva.Checked) Console.WriteLine("noava Selected");
            else Console.WriteLine("noava de-Selected");
        }

        private void chkClean_CheckedChanged(object sender, EventArgs e)
        {
            if (chkClean.Checked) Console.WriteLine("clean Selected");
            else Console.WriteLine("clean de-Selected");
        }

        private void chkNoReeb_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNoReeb.Checked) Console.WriteLine("noreeb Selected");
            else Console.WriteLine("noreeb de-Selected");
        }

        private void chkShutdown_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShutdown.Checked) Console.WriteLine("shutdown Selected");
            else Console.WriteLine("shutdown de-Selected");
        }

        private void chkReboot_CheckedChanged(object sender, EventArgs e)
        {
            if (chkReboot.Checked) Console.WriteLine("reboot Selected");
            else Console.WriteLine("reboot de-Selected");
        }

        private void chkxesettings_CheckedChanged(object sender, EventArgs e)
        {
            if (chkxesettings.Checked) Console.WriteLine("Use Edited Options Selected");
            else Console.WriteLine("Use Edited Options de-Selected");
        }

        #endregion

        #region code

        void xe_update()
        {
            Classes.xebuild xe = new Classes.xebuild();
            xe.Uloadvariables(variables.dashversion, (Classes.xebuild.hacktypes)variables.ttyp, patches, chkxesettings.Checked, chkNoWrite.Checked, chkNoAva.Checked, chkClean.Checked, chkNoReeb.Checked, checkDLPatches.Checked,
                chkLaunch.Checked);

            try
            {
                string[] files = { "kv.bin", "smc.bin", "smc_config.bin", "fcrt.bin" };
                foreach (string file in files)
                {
                    if (File.Exists(Path.Combine(variables.pathforit, @"xebuild\data\" + file)))
                    {
                        if (MessageBox.Show(file + " found. Delete it?\n Unless you put it there, delete it!", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                        {
                            File.Delete(Path.Combine(variables.pathforit, @"xebuild\data\" + file));
                        }
                    }
                }
            }
            catch (Exception ex) { if (variables.debugme) Console.WriteLine(ex.ToString()); }

            Classes.xebuild.XebuildError er = xe.createxebuild();
            if (er == Classes.xebuild.XebuildError.none)
            {
                xe.xeExit += xe_xeUExit;
                xe.update();
            }
            else if (er == Classes.xebuild.XebuildError.nodash)
            {
                MessageBox.Show("No Dash Selected");
                return;
            }
            else
            {
                MessageBox.Show("Something Bad Happened");
                return;
            }
        }

        void xe_client(string arguments)
        {
            Classes.xebuild xe = new Classes.xebuild();
            xe.client(arguments);
        }

        void xe_compatibilityAvatar(string path, string command)
        {
            Classes.xebuild xe = new Classes.xebuild();
            string arguments = command + path + "\\";
            if (chkShutdown.Checked) arguments += " -s";
            if (chkReboot.Checked) arguments += " -reboot";
            if (chkForceIP.Checked) arguments += " -ip " + txtIP2.Text;

            xe.client(arguments);
        }

        void add_dash()
        {
            addDash newdash = new addDash();
            if (newdash.ShowDialog() == DialogResult.Cancel) return;
            try
            {
                AddedDash();
            }
            catch (Exception) { }
        }
        void del_dash()
        {
            Dashes.delDash deldash = new Dashes.delDash();
            deldash.ShowDialog();
            try
            {
                DeletedDash();
            }
            catch (Exception) { }
        }

        public void createxebuild_v2(bool custom, Nand.PrivateN nand)
        {
            Classes.xebuild xe = new Classes.xebuild();
            xe.loadvariables(nand._cpukey, (Classes.xebuild.hacktypes)variables.ttyp, variables.dashversion,
                variables.ctyp, patches, nand, chkxesettings.Checked, checkDLPatches.Checked,
                chkLaunch.Checked, checkAUDclamp.Checked, chkRJtag.Checked, chkCR4.Checked);

            string ini = (variables.launchpath + @"\" + variables.dashversion + @"\_" + variables.ttyp + ".ini");

            if (!custom)
            {
                if (String.IsNullOrWhiteSpace(variables.filename1))
                {
                    loadFil(ref variables.filename1, true);
                    if (String.IsNullOrWhiteSpace(variables.filename1))
                    {
                        MessageBox.Show("No File Selected");
                        return;
                    }
                }
                if (!File.Exists(variables.filename1))
                {
                    MessageBox.Show("File is missing. Ensure it wasn't moved and app can access it.");
                    return;
                }
                if (Path.GetExtension(variables.filename1) != ".bin")
                {
                    MessageBox.Show("You must select a .bin file", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                    return;
                }
                try
                {
                    string[] files = { "kv.bin", "smc.bin", "smc_config.bin", "fcrt.bin" };
                    foreach (string file in files)
                    {
                        if (File.Exists(Path.Combine(variables.pathforit, @"xebuild\data\" + file)))
                        {
                            if (MessageBox.Show(file + " found. Delete it?\n Unless you put it there, delete it!", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                            {
                                File.Delete(Path.Combine(variables.pathforit, @"xebuild\data\" + file));
                            }
                        }
                    }
                }
                catch (Exception ex) { if (variables.debugme) Console.WriteLine(ex.ToString()); }
                if (!nand.cpukeyverification(nand._cpukey))
                {
                    Console.WriteLine("Wrong cpukey");
                    return;
                }
            }
            else
            {
                string[] filesa = { "kv.bin", "smc.bin", "smc_config.bin" };
                foreach (string file in filesa)
                {
                    if (!File.Exists(Path.Combine(variables.pathforit, @"xebuild\data\" + file)))
                    {
                        MessageBox.Show(file + " is missing");
                        Process.Start(Path.Combine(variables.pathforit, @"xebuild\data"));
                        return;
                    }
                }
                Regex objAlphaPattern = new Regex("[a-fA-F0-9]{32}$");
                bool sts = objAlphaPattern.IsMatch(variables.cpkey);
                if ((variables.cpkey.Length == 32 && sts))
                {
                    if (variables.debugme) Console.WriteLine("Key verification");
                    long size = 0;
                    if (Nand.Nand.cpukeyverification(Oper.openfile(Path.Combine(variables.pathforit, @"xebuild\data\kv.bin"), ref size, 0), variables.cpkey))
                    {
                        Console.WriteLine("CpuKey is Correct");
                        if (Nand.Nand.getfcrtflag(File.ReadAllBytes(Path.Combine(variables.pathforit, @"xebuild\data\kv.bin")), variables.cpkey))
                        {
                            if (!File.Exists(Path.Combine(variables.pathforit, @"xebuild\data\fcrt.bin")))
                            {
                                MessageBox.Show("fcrt.bin is missing");
                                Process.Start(Path.Combine(variables.pathforit, @"xebuild\data"));
                                return;
                            }
                        }
                    }
                    else Console.WriteLine("Wrong CpuKey");
                }

                Forms.xeBuildOptions ldv = new Forms.xeBuildOptions();
                string cba = "", cbb = "";
                string[] files = parse_ini.parselabel(ini, variables.ctyp.Ini + "bl");
                if (files.Length >= 2)
                {
                    if (files[0].Contains("cb")) cba = files[0].Substring(files[0].IndexOf("_") + 1, files[0].IndexOf(".bin") - 4);
                    if (files[1].Contains("cbb")) cbb = files[1].Substring(files[1].IndexOf("cbb_") + 4, files[1].IndexOf(".bin") - 4);
                }
                ldv.enumeratecbs(cba, cbb);
                ldv.ShowDialog();
            }

        Start:
            switch (xe.createxebuild(custom))
            {
                case Classes.xebuild.XebuildError.nocpukey:
                    MessageBox.Show("cpukey is missing");
                    return;
                case Classes.xebuild.XebuildError.nodash:
                    MessageBox.Show("No Dash Selected");
                    return;
                case Classes.xebuild.XebuildError.noinis:
                    MessageBox.Show("Ini's are missing");
                    return;
                case Classes.xebuild.XebuildError.nobootloaders:
                    Console.WriteLine("The specified console bootloader list ({0}) is missing from the ini ({1})", variables.ctyp.Ini + "bl", ini);
                    Console.WriteLine("You can either add it manually or ask for it get added if its possible");
                    return;
                case Classes.xebuild.XebuildError.wrongcpukey:
                    MessageBox.Show("Wrong cpukey");
                    return;
                case Classes.xebuild.XebuildError.noconsole:
                    variables.ctyp = callconsoletypes(ConsoleTypes.Selected.All);
                    if (variables.ctyp.ID == -1) return;
                    else
                    {
                        xe.loadvariables(nand._cpukey, (Classes.xebuild.hacktypes)variables.ttyp, variables.dashversion,
                            variables.ctyp, patches, nand, chkxesettings.Checked, checkDLPatches.Checked,
                            chkLaunch.Checked, checkAUDclamp.Checked, chkRJtag.Checked, chkCR4.Checked);
                        goto Start;
                    }
                case Classes.xebuild.XebuildError.none:
                    copyfiles(nand._cpukey);
                    xe.xeExit += xe_xeExit;
                    xe.build();
                    break;
                default:
                    break;
            }

        }

        public void xe_xeExit(object sender, EventArgs e)
        {
            variables.changeldv = 0;
            UpdateProgres(100);

            try
            {
                File.Copy(Path.Combine(variables.pathforit, @"xebuild\options.ini"), Path.Combine(variables.pathforit, @"xebuild\data\options.ini"), true);
                chkxesettings.Checked = false;
                File.Move(Path.Combine(variables.xefolder, variables.nandflash + ".log"), Path.Combine(variables.xefolder, variables.nandflash.Substring(0, variables.nandflash.IndexOf(".")) + "(" + DateTime.Now.ToString("ddMMyyyyHHmm") + ").bin.log"));
            }
            catch (Exception ex) { if (variables.debugme) Console.WriteLine(ex.ToString()); }
            //

            if (variables.xefinished)
            {
                Console.WriteLine("Saved to {0}", variables.xefolder);
                Console.WriteLine("Image is Ready");
                variables.filename1 = Path.Combine(variables.xefolder, variables.nandflash);
                updateSourc(variables.filename1);
                //Process.Start(variables.xefolder);
            }
            else
            {
                Console.WriteLine("Failed");
            }

            try
            {
                delfiles();
            }
            catch (Exception ex) { if (variables.debugme) Console.WriteLine(ex.ToString()); }
            if (variables.debugme) Console.WriteLine("Deleted Files Successfully");
            variables.xefinished = false;
        }

        private void copyfiles(string cpukey)
        {
            string targetkey = System.IO.Path.Combine(variables.xePath, variables.cpukeypath);
            string targetnand = System.IO.Path.Combine(variables.xePath, variables.nanddump);
            System.IO.File.WriteAllText(targetkey, cpukey);
            if (String.IsNullOrEmpty(variables.filename1)) return;
            //
            FileInfo fi = new FileInfo(variables.filename1);
            if (fi.Length == 0xE0400000)
            {
                if (MessageBox.Show("Copy all 4GB data?", "Copy", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    JRunner.Functions.Copy c = new JRunner.Functions.Copy(variables.filename1, targetnand);
                    c.ShowDialog();
                }
                else
                {

                    try
                    {
                        FileStream fr = new FileStream(variables.filename1, FileMode.Open);
                        FileStream fw = new FileStream(targetnand, FileMode.Create);
                        int buffersize = 0x200;
                        byte[] buffer = new byte[buffersize];
                        for (int i = 0; i < 0x3000000; i += buffersize)
                        {
                            fr.Read(buffer, 0, buffersize);
                            fw.Write(buffer, 0, buffersize);
                        }
                        fr.Close();
                        fw.Close();
                    }
                    catch (Exception ex) { if (variables.debugme) Console.WriteLine(ex.ToString()); }

                }
            }
            /**/else /**/System.IO.File.Copy(variables.filename1, targetnand, true);
        }
        private void delfiles()
        {
            if (System.IO.File.Exists(variables.xePath + variables.nanddump))
            {
                try
                {
                    System.IO.File.Delete(variables.xePath + variables.nanddump);
                    if (variables.debugme) Console.WriteLine("Deleted {0}", variables.xePath + variables.nanddump);
                }
                catch (System.IO.IOException e)
                { MessageBox.Show(e.Message); return; }
            }
            if (System.IO.File.Exists(variables.xePath + variables.cpukeypath))
            {
                try
                { System.IO.File.Delete(variables.xePath + variables.cpukeypath); if (variables.debugme) Console.WriteLine("Deleted {0}", variables.xePath + variables.cpukeypath); }
                catch (System.IO.IOException e)
                { MessageBox.Show(e.Message); return; }
            }
            if (System.IO.File.Exists(variables.launchpath + @"\" + variables.dashversion + @"\launch.ini"))
            {
                try
                {
                    System.IO.File.Delete(variables.launchpath + @"\" + variables.dashversion + @"\launch.ini");
                    if (variables.debugme) Console.WriteLine("Deleted launch.ini");
                }
                catch (System.IO.IOException e)
                { MessageBox.Show(e.Message); return; }
            }
        }

        consoles callconsoletypes(ConsoleTypes.Selected selec, bool twomb = false, bool full = false)
        {
            ConsoleTypes myNewForm = new ConsoleTypes();
            myNewForm.sel = selec;
            myNewForm.twombread = twomb;
            myNewForm.sfulldump = full;
            myNewForm.ShowDialog();
            if (myNewForm.DialogResult == DialogResult.Cancel) return (variables.cunts[0]);
            if (myNewForm.heResult().ID == -1) return variables.cunts[0];
            variables.fulldump = myNewForm.fulldump();
            variables.twombread = myNewForm.twombdump();
            if (variables.debugme) Console.WriteLine("fulldump variable = {0}", variables.fulldump);
            setMBname(myNewForm.heResult().Text);
            return (myNewForm.heResult());
        }

        public void xe_xeUExit(object sender, EventArgs e)
        {
            variables.changeldv = 0;
            UpdateProgres(100);

            if (variables.xefinished)
            {
                Console.WriteLine("Saved to {0}", variables.xefolder);
                Console.WriteLine("Image is Ready");
                variables.filename1 = Path.Combine(variables.xefolder, variables.nandflash);
                updateSourc(variables.filename1);
                //Process.Start(variables.xefolder);
            }
            else
            {
                Console.WriteLine("Failed");
            }
            variables.xefinished = false;
        }

        #endregion

        private void btnInfo_Click(object sender, EventArgs e)
        {
            ThreadStart starter = delegate { xe_compatibilityAvatar(variables.outfolder, "-i "); };
            new Thread(starter).Start();
        }

        private void rbtnWB_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnWBNone.Checked) patches[7] = "";
            else if (rbtnWB.Checked) patches[7] = "-r WB";
            else if (rbtnWB4G.Checked) patches[7] = "-r WB4G";

            updateCommand();
        }

        private void updateCommand(bool wait = false)
        {
            if (wait) Thread.Sleep(100);
            string c = "";
            c = "-t " + (Classes.xebuild.hacktypes)variables.ttyp;
            c += " -c " + variables.ctyp.XeBuild;
            foreach (String patch in patches)
            {
                c += " " + patch;
            }
            c += " -f " + variables.dashversion;
            c += " -d data";
            c += " \"" + variables.xefolder + "\\" + variables.nandflash + "\" ";

            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex(@"[ ]{2,}", options);
            c = regex.Replace(c, @" ");

            try
            {
                txtCommand.Text = c;
            }
            catch (Exception) { }
        }

        private void txtMBname_TextChanged(object sender, EventArgs e)
        {
            new Thread(new ThreadStart(delegate { updateCommand(true); })).Start();
            new Thread(new ThreadStart(delegate { setComboCB(false, true); })).Start();
        }

        private void setComboCB(bool erase = false, bool wait = false)
        {
            if (erase)
            {
                patches[7] = "";
                return;
            }
            if (wait) Thread.Sleep(100);
            try
            {
                comboCB.Items.Clear();
                if (variables.dashversion != 0)
                {
                    string ini = (variables.launchpath + @"\" + variables.dashversion + @"\_retail.ini");
                    List<string> labels = parse_ini.getlabels(ini);

                    foreach (string s in labels)
                    {
                        if (!s.Contains("bl")) continue;
                        if (variables.ctyp.ID == -1)
                        {
                            if (s.Contains("_")) comboCB.Items.Add(new CB(s.Substring(s.IndexOf("_") + 1), true));
                            else
                            {
                                comboCB.Items.Add(new CB(Nand.ntable.getCBFromDash(getConsoleFromIni(s.Substring(0, s.IndexOf("bl"))), variables.dashversion), false));
                            }
                        }
                        else
                        {
                            if (s.Contains(variables.ctyp.Ini))
                            {
                                if (s.Contains("_")) comboCB.Items.Add(new CB(s.Substring(s.IndexOf("_") + 1), true));
                                else comboCB.Items.Add(new CB(Nand.ntable.getCBFromDash(getConsoleFromIni(variables.ctyp.Ini), variables.dashversion), false));
                            }
                        }
                    }

                    if (comboCB.Items.Count > 0) comboCB.SelectedIndex = 0;
                }
            }
            catch (Exception) { }
        }

        private void comboCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboCB.Items.Count > 0)
            {
                CB c = (CB)comboCB.SelectedItem;
                if (c.Patch) patches[7] = "-r " + c.Version;
                else patches[7] = "";
                updateCommand();
            }
        }

        private consoles getConsoleFromIni(string ini)
        {
            foreach (consoles c in variables.cunts)
            {
                if (c.ID == -1) continue;
                if (ini == c.Ini) return c;
            }
            return variables.cunts[0];
        }

        class CB
        {
            public string Version { get; set; }
            public bool Patch { get; set; }

            public CB(string v, bool p)
            {
                Version = v;
                Patch = p;
            }

            public CB(int v, bool p)
            {
                Version = v.ToString();
                Patch = p;
            }

            public override string ToString()
            {
                return Version;
            }
        }
    }
}
