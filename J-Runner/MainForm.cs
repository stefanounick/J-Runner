using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Security;
using System.Security.Cryptography;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Runtime;
using System.Media;
using System.Runtime.InteropServices;
using System.Timers;
using System.Resources;
using Microsoft.Win32.SafeHandles;
using System.Management;
using RenameRegistryKey;
using System.Net.Sockets;
using System.Xml;
using System.Reflection;
using WinUsb;
using LibUsbDotNet.DeviceNotify;
using System.Security.Principal;
using Microsoft.Win32;

namespace JRunner
{
    public partial class MainForm : Form
    {
        #region shitloadofvariables
        public static TextWriter _writer = null;
        public static MainForm frmMy;
        private IDeviceNotifier devNotifier;
        int device = 0;
        IP myIP = new IP();
        public static Nand.PrivateN nand = new Nand.PrivateN();
        private NandX nandx = new NandX();
        private DemoN demon = new DemoN();
        private Panels.NandInfo nandInfo = new Panels.NandInfo();
        private Panels.NandTools nTools = new Panels.NandTools();
        private Panels.XeBuildPanel xPanel = new Panels.XeBuildPanel();
        private Panels.LDrivesInfo ldInfo = new Panels.LDrivesInfo();
        public Panels.XSVFChoice xsvfInfo = new Panels.XSVFChoice();
        List<Control> listInfo = new List<Control>();
        List<Control> listTools = new List<Control>();
        List<Control> listExtra = new List<Control>();
        public static EventWaitHandle _waitmb = new AutoResetEvent(true);
        public static readonly object _object = new object();
        public static AutoResetEvent _event1 = new AutoResetEvent(false);
        public static Nand.VNand vnand;
        public static bool usingVNand = false;
        Regex objAlphaPattern = new Regex("[a-fA-F0-9]{32}$");
        private bool x360USBDetected = false;
        #endregion

        #region Initialization

        public MainForm()
        {
            InitializeComponent();
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            pnlInfo.Controls.Add(nandInfo);
            listInfo.Add(nandInfo);
            pnlTools.Controls.Add(nTools);
            listTools.Add(nTools);
            pnlExtra.Controls.Add(xPanel);
            listExtra.Add(xPanel);
            setUp();
        }
        private void deleteonstartup()
        {
            if (System.IO.File.Exists(variables.pathforit + @"\JRunner_old.exe"))
            {
                try
                { System.IO.File.Delete(variables.pathforit + @"\JRunner_old.exe"); }
                catch (Exception e)
                { MessageBox.Show(e.Message); return; }
            }
            if (System.IO.File.Exists(variables.pathforit + @"\updater.exe"))
            {
                try
                { System.IO.File.Delete(variables.pathforit + @"\updater.exe"); }
                catch (Exception e)
                { MessageBox.Show(e.Message); return; }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

            deleteonstartup();

            frmMy = this;

            this.Text = "J-Runner v" + StaticVersion.Betaversion + "." + StaticVersion.version + " Beta (" + StaticVersion.Build + ")";

            _writer = new TextBoxStreamWriter(txtConsole);
            Console.SetOut(_writer);
            //// BH
            if (variables.location != new Point(0, 0))
            {
                this.StartPosition = FormStartPosition.Manual;
                this.Location = variables.location;
                if (!Screen.FromControl(this).Bounds.Contains(this.Location))
                {
                    this.DesktopLocation = new Point(100, 100);
                }
            }
            // BH
            if (InvokeRequired)
            {
                this.Invoke(new EventHandler(MainForm_Load), new object[] { sender, e });
                return;
            }


            settings();

            new Thread(on_load).Start();

            deviceinit();
        }
        void setUp()
        {
            demon.UpdateBloc += updateBlocks;
            demon.UpdateProgres += updateProgress;
            demon.updateFlas += demon_updateFlas;
            demon.updateMod += demon_updateMod;
            demon.UpdateVer += demon_UpdateVer;
            nTools.ReadClick += btnReadClick;
            nTools.CreateEccClick += btnCreateECCClick;
            nTools.WriteEccClick += btnWriteECCClick;
            nTools.WriteClick += btnWriteClick;
            nTools.CPUDBClick += btnCPUDBClick;
            nTools.ProgramCRClick += btnProgramCRClick;
            nTools.XeBuildClick += btnXeBuildClick;
            nTools.IterChange += nTools_IterChange;
            xsvfInfo.CloseCRClick += xsvfInfo_CloseCRClick;
            xsvfInfo.ProgramCRClick += xsvfInfo_ProgramCRClick;
            xPanel.DeletedDash += xPanel_DeletedDash;
            xPanel.AddedDash += xPanel_AddedDash;
            xPanel.HackChanged += xPanel_HackChanged;
            xPanel.CallMB += xPanel_CallMB;
            xPanel.loadFil += xPanel_loadFil;
            xPanel.updateSourc += xPanel_updateSourc;
            xPanel.UpdateProgres += updateProgress;
            xPanel.DriveMode += xPanel_DriveMode;
            xPanel.getmb += xPanel_getmb;
            nandInfo.DragDropChanged += nandInfo_DragDropChanged;
            nandx.UpdateProgres += updateProgress;
            nandx.UpdateBloc += updateBlocks;

            ldInfo.UpdateProgres += updateProgress;
            ldInfo.UpdateBloc += updateBlocks;
            ldInfo.UpdateSourc += xPanel_updateSourc;
            ldInfo.CloseLDClick += ldInfo_CloseLDClick;
            ldInfo.doCompar += ldInfo_doCompar;
            ldInfo.UpdateAdditional += ldInfo_UpdateAdditional;

#if Dev
            driveinfo.RefreshClick += driveinfo_RefreshClick;
            driveinfo.IntroClick += driveinfo_IntroClick;
            driveinfo.OutroClick += driveinfo_OutroClick;
            driveinfo.ReadClick += driveinfo_ReadClick;
            driveinfo.EraseClick += driveinfo_EraseClick;
            driveinfo.WriteClick += driveinfo_WriteClick;
            driveinfo.SPIClick += driveinfo_SPIClick;
            driveinfo.SelectionChanged += driveinfo_SelectionChanged;

            dTools.LOEraseClick += dTools_LOEraseClick;
            dTools.PhatKeyClick += dTools_PhatKeyClick;
            dTools.BenQULClick += dTools_BenQULClick;
            dTools.SlimKeyClick += dTools_SlimKeyClick;
            dTools.SlimUnlockClick += dTools_SlimUnlockClick;
            dTools.SammyUnlockClick += dTools_SammyUnlockClick;

            fwInfo.SpoofClick += fwInfo_SpoofClick;
            fwInfo.VerifyKeySClick += fwInfo_VerifyKeySClick;
            fwInfo.LTUClick += fwInfo_LTUClick;
            fwInfo.UpdateTargetTxt += fwInfo_UpdateTargetTxt;
            fwInfo.UpdateClear += fwInfo_UpdateClear;
            fwInfo.FixSerialClick += fwInfo_FixSerialClick;
            fwInfo.ManualSpoofClick += fwInfo_ManualSpoofClick;
#endif
        }

        private void deviceinit()
        {
            devNotifier = DeviceNotifier.OpenDeviceNotifier();
            devNotifier.OnDeviceNotify += onDevNotify;

            showDemon(DemoN.FindDemon());
            if (!DemoN.DemonDetected)
            {
                LibUsbDotNet.Main.UsbRegDeviceList mDevList = LibUsbDotNet.UsbDevice.AllDevices;
                foreach (LibUsbDotNet.Main.UsbRegistry devic in mDevList)
                {
                    if (devic.Pid == 0x0004 && devic.Vid == 0xFFFF)
                    {
                        nTools.setImage(global::JRunner.Properties.Resources.NANDX);
                        device = 2;
                    }
                    else if (devic.Pid == 0x8338 && devic.Vid == 0x11d4)
                    {
                        nTools.setImage(global::JRunner.Properties.Resources.JRP);
                        jRPToolStripMenuItem.Visible = true;
                        device = 1;
                    }
                    else if (devic.Vid == 0x11d4 && devic.Pid == 0x8333)
                    {
                        x360USBDetected = true;
                    }
                }
            }
        }

        private void on_load()
        {
            printstartuptext();

            new Thread(createdirectories).Start();

            check_dash(); // configures the dashes dropdown

            try
            {
                if (xPanel.getComboDash().Items.Count == 4)
                {
                    xPanel.getComboDash().SelectedIndex = 0;
                    variables.dashversion = Convert.ToInt32(xPanel.getComboDash().Text);
                }
                else
                {
                    if (variables.dashes_all.Contains(variables.preferredDash))
                    {
                        xPanel.getComboDash().SelectedIndex = variables.dashes_all.IndexOf(variables.preferredDash);
                        variables.dashversion = Convert.ToInt32(xPanel.getComboDash().Text);
                    }
                    else if (xPanel.getComboDash().Items.Count > 3) xPanel.BeginInvoke((Action)(() => xPanel.getComboDash().SelectedIndex = (xPanel.getComboDash().Items.Count - 3)));
                }
            }
            catch (InvalidOperationException)
            {
            }

        }
        private void createdirectories()
        {
            if (!System.IO.Directory.Exists(System.IO.Path.Combine(variables.pathforit, "output")))
            {
                try
                {
                    System.IO.Directory.CreateDirectory(System.IO.Path.Combine(variables.pathforit, "output"));
                }
                catch (System.IO.DirectoryNotFoundException)
                {
                    variables.pathforit = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    System.IO.Directory.CreateDirectory(System.IO.Path.Combine(variables.pathforit, "output"));
                }
            }
            if (Directory.GetFiles(variables.outfolder, "*", SearchOption.TopDirectoryOnly).Length > 0)
            {

                Console.WriteLine("WARNING! - Your selected working directory already contains files!");

                Console.WriteLine("You can view these files by using 'Show Working Folder' Button");
                Console.WriteLine("");
            }
            if (!Directory.Exists(variables.AppData))
            {
                Directory.CreateDirectory(variables.AppData);
            }
#if Dev1      
            Console.WriteLine("JF Features enabled");
#endif
        }
        private void printstartuptext()
        {
            Console.WriteLine("===================================================");
            Console.WriteLine("{0:F}", DateTime.Now);
            Console.WriteLine("");
            Console.Write("J-Runner v{0}.{1} Beta ({2}) ", StaticVersion.Betaversion, StaticVersion.version, StaticVersion.Build);
            Console.WriteLine("Started");
            Console.WriteLine("");
            Console.WriteLine("");
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            if (!principal.IsInRole(WindowsBuiltInRole.Administrator) && System.Environment.OSVersion.Version.Minor == 2)
            {
                Console.WriteLine("It's advised to run in administrator mode on Windows 8/10");
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            savesettings();
            string file = Path.Combine(variables.pathforit, "Log.txt");
            if (File.Exists(file))
            {
                try
                {
                    string[] data = File.ReadAllLines(file);
                    int last = 0;
                    for (int i = 0; i < data.Length; i++)
                    {
                        if (data[i] == "===================================================" && i != data.Length - 1) { last = i + 1; }
                    }
                    DateTime convertedDate = DateTime.Parse(data[last]);
                    if ((DateTime.Now - convertedDate).Days > 0)
                    {
                        string logdir = Path.Combine(variables.pathforit, "Logs");
                        if (!Directory.Exists(logdir)) Directory.CreateDirectory(logdir);
                        string newfile = Path.Combine(logdir, "Log-" + convertedDate.ToShortDateString() + ".txt");
                        if (!File.Exists(newfile)) File.Move(file, newfile);
                    }
                }
                catch (Exception ex) { txtConsole.Text += ex.Message + Environment.NewLine; }
            }
            File.AppendAllText(file, "\n" + txtConsole.Text);
        }

        #endregion

        #region Panels

        void demon_UpdateVer(string version)
        {
            FWVersion.Text = version;
        }

        void demon_updateMod(DemoN.Demon_Modes mode)
        {
            if (mode == DemoN.Demon_Modes.FIRMWARE)
            {
                FlashStatus.Visible = true;
                FlashVersion.Visible = true;
                FWStatus.Text = "FW: ";
            }
            else
            {
                demon.getBootloaderVersion();
                FWStatus.Text = "BL: ";
            }
            ModeVersion.Text = mode.ToString();
        }

        void demon_updateFlas(DemoN.Demon_Switch flash)
        {
            FlashVersion.Text = flash.ToString();
        }

        #region LDrivesPanel

        void ldInfo_CloseLDClick()
        {
            listInfo.Remove(ldInfo);
            pnlInfo.Controls.Remove(ldInfo);
            pnlInfo.Controls.Add(listInfo[listInfo.Count - 1]);
        }

        void ldInfo_UpdateAdditional(string file)
        {
            txtFilePath2.Text = file;
            variables.filename2 = file;
        }

        void ldInfo_doCompar()
        {
            comparenands();
        }

        #endregion

        #region xebuild Panel

        void xPanel_CallMB()
        {
            variables.ctyp = callconsoletypes(ConsoleTypes.Selected.All);
        }

        void xPanel_HackChanged()
        {
            xsvfInfo.dostuff();

            string createecctext = variables.eccmethod ? "Create ECC*" : "Create ECC";
            nTools.setbtnCreateECC(createecctext);
            nTools.setbtnWriteECC("Write ECC");

            if (xPanel.getRbtnGlitchChecked()) variables.ttyp = variables.hacktypes.glitch;
            else if (xPanel.getRbtnJtagChecked())
            {
                variables.ttyp = variables.hacktypes.jtag;
                nTools.setbtnCreateECC("Create Xell-\nReloaded");
                nTools.setbtnWriteECC("Write Xell-\nReloaded");
            }
            else if (xPanel.getRbtnRGH2Checked()) variables.ttyp = variables.hacktypes.glitch2;
            else if (xPanel.getRbtnGlitch2mChecked()) variables.ttyp = variables.hacktypes.glitch2m;
            else if (xPanel.getRbtnRetailChecked()) variables.ttyp = variables.hacktypes.retail;
            else variables.ttyp = variables.hacktypes.nothing;

        }

        void xPanel_AddedDash()
        {
            ThreadStart starte = delegate { check_dashes(true); };
            Thread th = new Thread(starte);
            th.IsBackground = true;
            th.Start();
        }

        void xPanel_DeletedDash()
        {
            check_dash();
        }

        void xPanel_getmb()
        {
            getmbtype();
        }

        void xPanel_updateSourc(string filename)
        {
            txtFilePath1.Text = filename;
            variables.filename1 = filename;
            nand_init();
        }

        void xPanel_loadFil(ref string filename, bool erase = false)
        {
            loadfile(ref filename, ref txtFilePath1, erase);
        }

        #endregion

        #region XSVF Panel

        void xsvfInfo_ProgramCRClick()
        {
            if (xsvfInfo.heResult() == -1) return;
            string file;
            if (variables.debugme) Console.WriteLine(xsvfInfo.heResult());
            bool demon = xsvfInfo.deResult();
            if (variables.debugme) Console.WriteLine("demon {0}", demon);
            if (demon)
            {
                if (variables.debugme) Console.WriteLine(variables.demon_xsvf[xsvfInfo.heResult() - 1]);
                file = (variables.demon_xsvf[xsvfInfo.heResult() - 1]);
            }
            else
            {
                if (variables.debugme) Console.WriteLine(variables.xsvf[xsvfInfo.heResult() - 1]);
                file = (variables.xsvf[xsvfInfo.heResult() - 1]);
            }
            programcr(file);

        }

        void xsvfInfo_CloseCRClick()
        {
            listInfo.Remove(xsvfInfo);
            pnlInfo.Controls.Remove(xsvfInfo);
            pnlInfo.Controls.Add(listInfo[listInfo.Count - 1]);
            pnlTools.Enabled = true;
        }

        #endregion

#if Dev
        void NandMode()
        {
            jf.Dispose();
            jf = null;
            driveinfo.clearAll();
            listInfo.Remove(driveinfo);
            pnlInfo.Controls.Remove(driveinfo);
            pnlInfo.Controls.Add(listInfo[listInfo.Count - 1]);

            fwInfo.clearSource();
            fwInfo.clearTarget();
            txtFilePath1.Text = variables.filename1 = "";
            txtFilePath2.Text = variables.filename2 = "";
            listTools.Remove(fwInfo);
            pnlTools.Controls.Remove(fwInfo);
            pnlTools.Controls.Add(listTools[listTools.Count - 1]);

            listExtra.Remove(dTools);
            pnlExtra.Controls.Remove(dTools);
            pnlExtra.Controls.Add(listExtra[listExtra.Count - 1]);

            btnLoadFile2.Text = "Load Extra";
            lblCpuKey.Text = "CPU Key";
            groupBox8.Enabled = true;
            comparebutton.Text = "Nand Compare";
            variables.current_mode = variables.JR_MODE.MODEJR;

            ThreadStart thr = delegate { (new Firmware.CK3i()).doStuff(Firmware.CK3i.Commands.MANUAL); };
            new Thread(thr).Start();
        }
#endif
        void xPanel_DriveMode()
        {
#if Dev
            pnlInfo.Controls.Clear();
            pnlInfo.Controls.Add(driveinfo);
            if (listInfo.Contains(driveinfo)) listInfo.Remove(driveinfo);
            listInfo.Add(driveinfo);

            pnlTools.Controls.Clear();
            pnlTools.Controls.Add(fwInfo);
            if (listTools.Contains(fwInfo)) listTools.Remove(fwInfo);
            listTools.Add(fwInfo);

            pnlExtra.Controls.Clear();
            pnlExtra.Controls.Add(dTools);
            if (listExtra.Contains(dTools)) listExtra.Remove(dTools);
            listExtra.Add(dTools);

            btnLoadFile2.Text = "Load Target";
            lblCpuKey.Text = "DVD Key";
            groupBox8.Enabled = false;
            comparebutton.Text = "Nand Mode";
            variables.current_mode = variables.JR_MODE.MODEFW;
            jf = new Firmware.JF(x360USBDetected);
            jf.StatusC += jf_StatusC;
            jf.TabUpdate += jf_TabUpdate;
            jf.FlashchipUpdate += jf_FlashchipUpdate;
            jf.DrivePropertiesUpdate += jf_DrivePropertiesUpdate;
            jf.SPIButtonEnable += jf_SPIButtonEnable;
            jf.SPIButtonUpdate += jf_SPIButtonUpdate;
            jf.SaveFileDialog += saveFile;
            jf.TargetTextBoxUpdate += jf_TargetTextBoxUpdate;
            jf.TypeUpdate += jf_TypeUpdate;
            jf.UpdateBloc += updateBlocks;
            jf.UpdateProgres += updateProgress;
#else
            if (x360USBDetected) { }
#endif
        }


        void jf_TargetTextBoxUpdate(string filename)
        {
            txtFilePath2.Text = filename;
        }

        delegate void SaveFileCallback(byte[] temp, string name, string filter);
        void saveFile(byte[] temp, string name, string filter)
        {
            SaveFileCallback d = new SaveFileCallback(jf_SaveFileDialog);
            this.Invoke(d, new object[] { temp, name, filter });
        }

        void jf_SaveFileDialog(byte[] temp, string name, string filter)
        {
            if (temp != null)
            {
                SaveFileDialog savefile = new SaveFileDialog();
                savefile.FileName = name;
                savefile.Filter = filter;
                if (savefile.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllBytes(savefile.FileName, temp);
                    variables.filename1 = savefile.FileName;
                    txtFilePath1.Text = variables.filename1;
                }
            }
        }

  

        void nTools_IterChange(int iter)
        {
            variables.NoReads = iter;
        }

        void nandInfo_DragDropChanged(string filename)
        {
            this.txtFilePath1.Text = filename;
            variables.filename1 = filename;
            erasevariables();
            if (Path.GetExtension(filename) == ".bin")
            {
                nand_init();
            }
        }

        void inter_updateXebuild(string progress)
        {
            xebuildVersion.Text = progress;
        }
        void inter_updateDashlaunch(string progress)
        {
            DashLaunchVersion.Text = progress;
        }

        #region JF
#if Dev
        #region JF class
        void jf_TypeUpdate(Firmware.X360USB.Drive_Types types)
        {
            driveinfo.setType(types);
        }

        void jf_SPIButtonUpdate(byte status)
        {
            driveinfo.setSPIbutton(status);
        }

        void jf_SPIButtonEnable(bool enable)
        {
            driveinfo.enableSPIbutton(enable);
        }

        void jf_DrivePropertiesUpdate(Firmware.X360USB.DriveProperties d)
        {
            driveinfo.setDriveProperties(d);
        }

        void jf_TabUpdate(Firmware.FW.fw f, Firmware.JF.fwFile ff)
        {
            if (ff == Firmware.JF.fwFile.SOURCE) fwInfo.setSource(f);
            else if (ff == Firmware.JF.fwFile.TARGET) fwInfo.setTarget(f);
        }
        void jf_FlashchipUpdate(Firmware.X360USB.FlashChip f)
        {
            driveinfo.setFlashChip(f);
        }


        #endregion

        #region DriveTools

        void dTools_SammyUnlockClick()
        {
            new Thread(jf.SammyUnlock).Start();
        }

        void dTools_SlimUnlockClick()
        {
            new Thread(jf.SlimUnlock).Start();
        }

        void dTools_BenQULClick()
        {
            new Thread(jf.BenQUL).Start();
        }

        void dTools_LOEraseClick()
        {
            new Thread(jf.LOErase).Start();
        }

        void dTools_PhatKeyClick()
        {
            new Thread(jf.PhatKey).Start();
        }

        void dTools_SlimKeyClick()
        {
            new Thread(jf.SlimKey).Start();
        }

        #endregion

        #region Drive Info

        void driveinfo_RefreshClick()
        {
            new Thread(jf.Refresh).Start();
        }

        void driveinfo_SelectionChanged()
        {
            if (jf != null) jf.setType(driveinfo.getType());
        }

        void driveinfo_SPIClick()
        {
            new Thread(jf.UnLockSPI).Start();
        }

        void driveinfo_WriteClick()
        {
            ThreadStart th = delegate { jf.Write(); };
            Thread t = new Thread(th);
            t.Start();
        }

        void driveinfo_EraseClick()
        {
            new Thread(jf.Erase).Start();
        }

        void driveinfo_ReadClick()
        {
            new Thread(jf.Read).Start();
        }

        void driveinfo_OutroClick()
        {
            new Thread(jf.Outro).Start();
        }

        void driveinfo_IntroClick()
        {
            ThreadStart th = delegate { jf.Intro(); };
            Thread t = new Thread(th);
            t.Start();
        }

        #endregion

        #region FW Info

        void fwInfo_VerifyKeySClick(Firmware.JF.fwFile f)
        {
            ThreadStart th = delegate { jf.FWKeyVer(f); };
            Thread t = new Thread(th);
            t.Start();
        }

        void fwInfo_UpdateClear(bool source)
        {
            if (source)
            {
                variables.filename1 = "";
                txtFilePath1.Text = "";
            }
            else
            {
                variables.filename2 = "";
                txtFilePath2.Text = "";
            }
        }

        void fwInfo_SpoofClick()
        {
            jf.SpoofSourceToTarget();
        }

        void fwInfo_LTUClick()
        {
            FileInfo fi = new FileInfo(variables.filename1);
            if (nand.ok)
            {
                if (nand.cpukeyverification(nand._cpukey))
                {
                    byte[] fcrt = nand.exctractFSfile("fcrt.bin");
                    if (fcrt != null)
                    {

                        byte[] t = Firmware.JF.responses(fcrt, Oper.StringToByteArray(nand._cpukey), nand.ki.dvdkey);

                        if (t != null)
                        {

                            ThreadStart th = delegate { jf.CreateLTU(t, Oper.StringToByteArray(nand.ki.dvdkey)); };
                            Thread thr = new Thread(th);
                            thr.Start();
                        }
                    }
                }
            }
            else if (fi.Length == 0x2542)
            {
                ThreadStart th = delegate { jf.CreateLTU(File.ReadAllBytes(variables.filename1), Oper.StringToByteArray(txtCPUKey.Text)); };
                Thread t = new Thread(th);
                t.Start();
            }
            else
            {
                ThreadStart th = delegate { jf.CreateLTU(new byte[0x2542], Oper.StringToByteArray(txtCPUKey.Text)); };
                Thread t = new Thread(th);
                t.Start();
            }
        }

        void fwInfo_UpdateTargetTxt(string filename)
        {
            txtFilePath2.Text = filename;
            variables.filename2 = filename;
            File.WriteAllBytes(filename, jf.fwTarget.fwArray);
        }

        void fwInfo_ManualSpoofClick()
        {
            throw new NotImplementedException();
        }

        void fwInfo_FixSerialClick()
        {
            throw new NotImplementedException();
        }

        #endregion

#endif
        #endregion

        #endregion

        #region EXEs
        #region LPT

        public void unpack_lpt()
        {
            /*
            if (!File.Exists(Path.Combine(variables.pathforit,"\\inpout32.dll")))
            {
                SaveResourceToDisc("Resources.inpout32.dll", "inpout32.dll", variables.pathforit);
            }
            /**/
            //if (!File.Exists(variables.AppData + "\\inpout32.dll"))
            {
                SaveResourceToDisc("Resources.inpout32.dll", "inpout32.dll", variables.AppData);
            }
            //if (!File.Exists(variables.AppData + "\\LPT_XSVF_Player.exe"))
            {
                SaveResourceToDisc("Resources.LPT_XSVF_Player.exe", "LPT_XSVF_Player.exe", variables.AppData);
            }
            //*/
        }
        private void CopyStream(Stream input, Stream output)
        {
            // Insert null checking here for production
            byte[] buffer = new byte[8192];

            int bytesRead;
            while ((bytesRead = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, bytesRead);
            }
        }
        private void SaveResourceToDisc(string resourceName, string outputName, string servicePath)
        {
            Assembly a = Assembly.GetExecutingAssembly();
            //string servicePath = variables.AppData;
            string resourceFile = Path.Combine(servicePath, outputName);
            if (!File.Exists(resourceFile))
            {
                //Get our namespace.
                string my_namespace = a.GetName().Name.ToString();

                using (Stream st = a.GetManifestResourceStream(my_namespace + "." + resourceName))
                {
                    using (Stream output = new FileStream(resourceFile, FileMode.CreateNew, FileAccess.Write, FileShare.Write))
                    {
                        byte[] buffer = new byte[32768];
                        int read;

                        while ((read = st.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            output.Write(buffer, 0, read);
                        }
                    }
                }
            }
        }

        public void call_lpt_player(string file, string port)
        {
            if (variables.debugme) Console.WriteLine("File: {0} | Port: {1}", file, port);
            System.Diagnostics.Process pProcess = new System.Diagnostics.Process();
            pProcess.StartInfo.FileName = variables.AppData + @"\LPT_XSVF_Player.exe";
            pProcess.StartInfo.Arguments = "\"" + file + "\"" + " " + port;
            pProcess.StartInfo.UseShellExecute = false;
            pProcess.StartInfo.WorkingDirectory = variables.pathforit;
            pProcess.StartInfo.RedirectStandardInput = true;
            pProcess.StartInfo.RedirectStandardOutput = true;
            pProcess.StartInfo.CreateNoWindow = true;
            pProcess.Exited += new EventHandler(lpt_Exited);
            pProcess.OutputDataReceived += new System.Diagnostics.DataReceivedEventHandler(process_OutputDataReceived);
            try
            {
                pProcess.Start();
                pProcess.BeginOutputReadLine();
                pProcess.WaitForExit();
                if (pProcess.HasExited)
                {
                    pProcess.CancelOutputRead();
                }
            }
            catch (Exception objException)
            {
                MessageBox.Show(objException.ToString());
            }
        }

        #endregion

        private void lpt_Exited(object sender, System.EventArgs e)
        {
            try
            {
                File.Delete(variables.AppData + @"\LPT_XSVF_Player.exe");
                File.Delete(variables.AppData + @"\inpout32.dll");
            }
            catch (Exception) { }

        }
        void process_OutputDataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
        {
            Console.WriteLine(e.Data);
        }

        #endregion

        #region Basic Functions

        #region Nand
        //////////////////////////////////////////////

        void nandcustom(string function, string filename, int size, int startblock, int length)
        {
            Nandsize sizex = Nandsize.S0;
            if (size == 16) sizex = Nandsize.S16;
            else if (size == 64) sizex = Nandsize.S64;
            else if (size == 256) sizex = Nandsize.S256;
            else if (size == 512) sizex = Nandsize.S512;

            ThreadStart starter = null;
            if (!DemoN.DemonDetected)
            {
                if (function == "Read")
                {
                    if (!usingVNand) starter = delegate { nandx.read(filename, sizex, true, startblock, length); };
                    else starter = delegate { vnand.read_v2(filename, startblock, length); };

                }
                else if (function == "Erase")
                {
                    if (!usingVNand) starter = delegate { nandx.erase(sizex, startblock, length); };
                    else starter = delegate { vnand.erase_v2(startblock, length); };
                }
                else if (function == "Write")
                {
                    if (!usingVNand) starter = delegate { nandx.write(filename, sizex, startblock, length); };
                    else starter = delegate { vnand.write_v2(filename, startblock, length); };
                }
                else if (function == "Xsvf")
                {
                    if (nTools.getRbtnUSB())
                    {
                        starter = delegate { nandx.xsvf(filename); };
                    }
                    else
                    {
                        unpack_lpt(); //changed
                        starter = delegate { call_lpt_player(filename, nTools.getLptPort()); };
                        //starter = delegate { LPT_XSVF.lxsvf(filename, txtLPTPort.Text, true); };
                    }
                }
            }
            else
            {
                if (function == "Read") starter = delegate { demon.read(filename, startblock, length); };
                else if (function == "Erase") starter = delegate { demon.erase(startblock, length); };
                else if (function == "Write")
                {
                    starter = delegate { demon.write(filename, startblock, length); };
                }
                else if (function == "Xsvf") starter = delegate { demon.xsvf(filename); };
            }
            try
            {
                new Thread(starter).Start();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        void nandcustom()
        {
            NandProArg cnaform = new NandProArg();
            cnaform.RunClick += cnaform_RunClick;
            cnaform.Show();
        }

        void cnaform_RunClick(string function, string filename, int size, int startblock, int length)
        {
            if (size == 0 && function != "Xsvf") return;
            nandcustom(function, filename, size, startblock, length);
        }
        //////////////////////////////////////////////
        void programcr(string filex)
        {
            string file = "";
            /* if (comboRGH.SelectedIndex == 0)
             {
                 if (variables.ctypeselected == -1) variables.ctypeselected = callconsoletypes();
                 if (variables.ctypeselected == -1) return;
                 if (variables.ctypeselected > 4 && variables.ctypeselected != 9 && variables.ctypeselected != 8) variables.nanduser = 4;
                 else variables.nanduser = variables.ctypeselected;
                 file = variables.pathforit + @"\common\xsvf\" + variables.crtype[variables.nanduser - 1] + ".xsvf";
             }
             else*/
            {
                //filex = callxsvf();
                if (filex == "") return;
                file = variables.pathforit + @"\common\xsvf\" + filex + ".xsvf";
            }

            Console.WriteLine("Programming Coolrunner");
            Console.WriteLine(file);

            if (DemoN.DemonDetected)
            {
                demon.xsvf(file);
            }
            else
            {
                if (nTools.getRbtnUSB())
                {
                    ThreadStart starter = delegate { nandx.xsvf(file); };
                    new Thread(starter).Start();
                    _waitmb.Set();
                }
                else
                {
                    unpack_lpt(); //changed
                    //ThreadStart starter = delegate { LPT_XSVF.lxsvf(file, txtLPTPort.Text, true); };
                    //new Thread(starter).Start();
                    ThreadStart starter = delegate { call_lpt_player(file, nTools.getLptPort()); };
                    new Thread(starter).Start();
                }
            }
        }
        //////////////////////////////////////////////
        NandX.Errors getmbtype(bool stealth = false)
        {
            if (!stealth) Console.WriteLine("Checking Console..");
            string flashconfig = "";
            NandX.Errors error = nandx.getflashmb(ref flashconfig, stealth);
            variables.flashconfig = flashconfig;
            if (error != NandX.Errors.None)
            {
                if (!stealth) Console.WriteLine("Can not continue");
                return error;
            }
            Console.WriteLine(variables.flashconfig);
            if (flashconfig == ("008A3020"))
            {
                variables.ctyp = variables.cunts[6];
                //textBox2.Text = "008A3020";
                Console.WriteLine(variables.ctyp.Text);
                xPanel.setMBname(variables.ctyp.Text);
            }
            else if (flashconfig == ("00AA3020"))
            {
                variables.ctyp = variables.cunts[7];
                //textBox2.Text = "00AA3020";
                Console.WriteLine(variables.ctyp.Text);
                xPanel.setMBname(variables.ctyp.Text);
            }
            else if (flashconfig == "C0462002")
            {
                error = NandX.Errors.WrongConfig;

                Console.WriteLine("This flashconfig belongs to Corona 4GB.\n As of now you cant read it via SPI.\n Please use an SD card reader.");
                return error;
            }
            else if (flashconfig == ("01198010"))
            {
                Console.WriteLine("Xenon, Zephyr, Opus, Falcon");
            }
            else if (flashconfig == ("00023010"))
            {
                Console.WriteLine("Trinity, Jasper 16MB");
            }
            else if (flashconfig == ("00043000"))
            {
                variables.ctyp = variables.cunts[10];
                //textBox2.Text = "00AA3020";
                Console.WriteLine(variables.ctyp.Text);
                xPanel.setMBname(variables.ctyp.Text);
            }
            try
            {
                if (!Encoding.ASCII.GetString(Oper.returnportion(variables.conf, 0, 50)).Contains("Microsoft"))
                {
                    if (variables.debugme) Console.WriteLine(Encoding.ASCII.GetString(Oper.returnportion(variables.conf, 0, 50)));
                    error = NandX.Errors.WrongHeader;
                }
            }
            catch (Exception ex) { if (variables.debugme) Console.WriteLine(ex.ToString()); }
            getcb_v(flashconfig);
            _waitmb.Set();
            return error;
        }
        void getcb_v(string flashconfig)
        {
            if (variables.debugme) Console.WriteLine("\nGetting cb {0}", flashconfig);
            try
            {
                if (variables.conf != null)
                {
                    int temp = Nand.Nand.getcb_build(variables.conf);
                    Console.WriteLine("CB Version: {0}", temp);
                    if (temp >= 9188 && temp <= 9250)
                    {
                        variables.ctyp = variables.cunts[1];
                        xPanel.setMBname(variables.ctyp.Text);
                        Console.WriteLine(variables.ctyp.Text);
                    }
                    else if (temp >= 4558 && temp <= 4580)
                    {
                        variables.ctyp = variables.cunts[3];
                        xPanel.setMBname(variables.ctyp.Text);
                        Console.WriteLine(variables.ctyp.Text);
                    }
                    else if (temp >= 6712 && temp <= 6780)
                    {
                        if (flashconfig == "01198010")
                        {
                            variables.ctyp = variables.cunts[5];
                            xPanel.setMBname(variables.ctyp.Text);
                            Console.WriteLine("Jasper 16MB Small Block Controller");
                        }
                        else if (flashconfig == "00023010")
                        {
                            variables.ctyp = variables.cunts[4];
                            xPanel.setMBname(variables.ctyp.Text);
                            Console.WriteLine(variables.ctyp.Text);
                        }
                    }
                    else if (temp >= 13121 && temp <= 13200)
                    {
                        variables.ctyp = variables.cunts[10];
                        xPanel.setMBname(variables.ctyp.Text);
                        Console.WriteLine(variables.ctyp.Text);
                    }
                    else if ((temp >= 1888 && temp <= 1960) || (temp >= 7373 && temp <= 7378) || temp == 8192)
                    {
                        variables.ctyp = variables.cunts[8];
                        xPanel.setMBname(variables.ctyp.Text);
                        Console.WriteLine(variables.ctyp.Text);
                    }
                    else if (temp >= 5761 && temp <= 5780)
                    {
                        variables.ctyp = variables.cunts[2];
                        Console.WriteLine("Falcon/Opus");
                    }
                    else
                    {
                        //if (variables.smcmbtype < variables.console_types.Length && variables.smcmbtype >= 0) consolebox.Text = variables.console_types[variables.smcmbtype];
                    }


                }
                else
                {
                    if (variables.debugme) Console.WriteLine("No conf file");
                }
            }
            catch (Exception) { }
            variables.conf = null;
        }

        /// <summary>
        /// 1 - read, 2 write, 3 writeecc
        /// </summary>
        /// <param name="function"></param>
        void getconsoletype(int function, int writelength = 0)
        {
            NandX.Errors error = 0;
            ConsoleTypes.Selected sel = ConsoleTypes.Selected.All;
            bool twombread = false;
            bool sfulldump = false;
            if (function == 1 && variables.ctyp.ID != 11)
            {
                error = getmbtype(true);
                if (error == NandX.Errors.NoFlashConfig) return;
                if (variables.ctyp.ID == 6 || variables.ctyp.ID == 7) sel = ConsoleTypes.Selected.BigBlock;
                if (xPanel.getRbtnJtagChecked() || xPanel.getRbtnGlitchChecked() || xPanel.getRbtnRGH2Checked()) twombread = true;
                sfulldump = true;
            }

            if (variables.ctyp.Nsize != Nandsize.S16 && variables.ctyp.ID != 11 && !DemoN.DemonDetected) variables.ctyp = callconsoletypes(sel, twombread, sfulldump);

            if (variables.ctyp.ID == -1 && !DemoN.DemonDetected) return;

            if (function == 1)
            {
                if (variables.ctyp.ID == 11)
                {
                    calldrives(variables.outfolder + "\\nanddump1.bin", Panels.LDrivesInfo.Function.Read);
                    return;
                }
                else
                {
                    try
                    {
                        ThreadStart starter = delegate { readnand(error); };
                        new Thread(starter).Start();
                    }
                    catch (Exception ex) { Console.WriteLine(ex.Message); }
                }
            }
            else if (function == 2)
            {
                if (variables.ctyp.ID == 11)
                {
                    calldrives(variables.filename1, Panels.LDrivesInfo.Function.Write);
                    return;
                }
                else
                {
                    try
                    {
                        ThreadStart starter = delegate { writenand(false); };
                        new Thread(starter).Start();
                    }
                    catch (Exception ex) { Console.WriteLine(ex.Message); }
                }
            }
            else if (function == 3)
            {
                if (variables.ctyp.ID == 11)
                {
                    calldrives(variables.filename1, Panels.LDrivesInfo.Function.Write);
                    return;
                }
                else
                {
                    try
                    {
                        ThreadStart starter = delegate { writenand(true, writelength); };
                        new Thread(starter).Start();
                    }
                    catch (Exception ex) { Console.WriteLine(ex.Message); }
                }
            }
        }

        void readnand(NandX.Errors error)
        {
            //int error = 0;
            int twomb = 0;
            if (usingVNand) error = NandX.Errors.None;
            if (!DemoN.DemonDetected)
            {
                //error = getmbtype();
                if (error != NandX.Errors.None && error != NandX.Errors.WrongHeader) return;
                if (variables.debugme) Console.WriteLine("Read Nand");

                #region nandsize
                if ((variables.ctyp.ID == 6 || variables.ctyp.ID == 7) && !variables.fulldump)
                {
                    variables.nandsizex = Nandsize.S64;
                }
                else if (variables.ctyp.ID == 0)
                {
                    variables.nandsizex = Nandsize.S16;
                }
                else
                {
                    if (variables.debugme) Console.WriteLine(variables.ctyp.ID);
                    variables.nandsizex = variables.ctyp.Nsize;
                }
                #endregion

                //if (getmbtype() != 0) return;
                if (variables.twombread) twomb = 0x7C;
            }
            int j = 1;
            for (j = 1; j <= nTools.getNumericIterations(); )
            {
                if (variables.debugme) Console.Write(j);
                _waitmb.WaitOne();
                lock (_object)
                {
                    if (variables.debugme) Console.WriteLine(j);
                    _waitmb.Reset();
                    Thread.Sleep(1000);
                    if (j == 2)
                    {
                        if (File.Exists(Path.Combine(variables.pathforit, variables.filename)))
                        {
                            this.txtFilePath1.Text = System.IO.Path.Combine(variables.pathforit, variables.filename1);
                            nand_init();
                        }
                    }
                    else if (j >= 3)
                    {
                        if (File.Exists(Path.Combine(variables.pathforit, variables.filename)))
                        {
                            this.txtFilePath2.Text = System.IO.Path.Combine(variables.pathforit, variables.filename2);
                            new Thread(comparenands).Start();
                        }
                    }

                    variables.filename = variables.outfolder + "\\nanddump" + j + ".bin";
                    Console.WriteLine("Reading Nand to {0}", variables.filename);
                    variables.iterations = j;
                    if (File.Exists(variables.filename))
                    {
                        if (DialogResult.Cancel == MessageBox.Show("File already exists, it will be DELETED!. Press ok to continue", "About to overwrite a nanddump", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning))
                        {
                            Console.WriteLine("Cancelled");
                            return;
                        };
                        if (!DemoN.DemonDetected)
                        {
                            if (error == NandX.Errors.WrongHeader)
                            {
                                if (DialogResult.Cancel == MessageBox.Show("Header seems to be wrong! This shouldnt happen for stock image! Are you really sure you want to overwrite your previously dumped image???", "Wrong Header", MessageBoxButtons.OKCancel, MessageBoxIcon.Error))
                                {
                                    Console.WriteLine("Cancelled");
                                    return;
                                };
                            }
                        }
                    }
                    if (variables.debugme) Console.WriteLine("Starting Reading");

                    if (DemoN.DemonDetected)
                    {
                        demon.read(variables.filename);
                    }
                    else
                    {
                        variables.reading = true;
                        if (!usingVNand)
                        {
                            if (nandx.read(variables.filename, variables.nandsizex, false, 0x0, twomb) != NandX.Errors.None) return;
                        }
                        else vnand.read_v2(variables.filename, 0, twomb);
                        variables.reading = false;
                    }
                    j++;
                    //else Console.WriteLine("Nandpro is already running");
                }
            }
            if (j == 2)
            {
                if (File.Exists(Path.Combine(variables.pathforit, variables.filename)))
                {
                    this.txtFilePath1.BeginInvoke((Action) (() => txtFilePath1.Text = System.IO.Path.Combine(variables.pathforit, variables.filename1)));

                    nand_init();
                }
            }
            else if (j >= 3)
            {
                if (File.Exists(Path.Combine(variables.pathforit, variables.filename)))
                {
                    this.txtFilePath2.BeginInvoke((Action)(() => txtFilePath2.Text = System.IO.Path.Combine(variables.pathforit, variables.filename)));
                    new Thread(comparenands).Start();
                }

            }
        }
        //////////////////////////////////////////////
        /// <summary>
        ///  Write
        /// </summary>
        /// <param name="ecc"></param>
        void writenand(bool ecc, int writelength = 0)
        {
            if (String.IsNullOrWhiteSpace(variables.filename1)) loadfile(ref variables.filename1, ref this.txtFilePath1, true);
            if (String.IsNullOrWhiteSpace(variables.filename1)) return;
            if (!File.Exists(variables.filename1)) return;
            if (DemoN.DemonDetected)
            {
                demon.write(variables.filename1);
                if (Path.GetExtension(variables.filename1) == ".ecc")
                {
                    if (variables.tempfile != "")
                    {
                        variables.filename1 = variables.tempfile;
                        txtFilePath1.Text = variables.tempfile;
                    }
                }
            }
            else
            {
                //if (textBox2.Text != "008A3020" && textBox2.Text != "00AA3020") ctypeselected = 0;

                double len = new FileInfo(variables.filename1).Length;
                if (variables.debugme) Console.WriteLine("File Length = {0} | Expected 69206016 for a 64MB nand", len);
                if ((variables.ctyp.ID == 6 || variables.ctyp.ID == 7) && (len == 69206016))
                {
                    variables.nandsizex = Nandsize.S64;
                }
                else if (variables.ctyp.ID == 0)
                {
                    variables.nandsizex = Nandsize.S16;
                }
                else
                {
                    variables.nandsizex = variables.ctyp.Nsize;
                }

                if (Path.GetExtension(variables.filename1) == ".ecc")
                {
                    if (!ecc)
                    {
                        Console.WriteLine("You need an .bin image");
                        return;
                    }
                    NandX.Errors result = NandX.Errors.None;
                    if (!usingVNand) result = nandx.write(variables.filename1, variables.nandsizex, 0, 0x50, true, true);
                    else vnand.write_v2(variables.filename1, 0, 0x50, true, true);
                    Thread.Sleep(500);
                    if (variables.tempfile != "" && result == NandX.Errors.None)
                    {
                        variables.filename1 = variables.tempfile;
                        txtFilePath1.Text = variables.tempfile;
                    }
                }
                else if (Path.GetExtension(variables.filename1) == ".bin")
                {
                    if (ecc)
                    {
                        Console.WriteLine("You need an .ecc image");
                        return;
                    }
                    if (!usingVNand) nandx.write(variables.filename1, variables.nandsizex, 0, writelength);
                    else vnand.write_v2(variables.filename1, 0, writelength);
                    //NandX.write(ref txtBlocks, ref progressBar1, variables.filename1, variables.nandsizex, 0, 0);
                }
            }
        }
        void writefusion()
        {
            if (String.IsNullOrWhiteSpace(variables.filename1)) loadfile(ref variables.filename1, ref this.txtFilePath1, true);
            //if (textBox2.Text != "008A3020" && textBox2.Text != "00AA3020") ctypeselected = 0;
            if (String.IsNullOrWhiteSpace(variables.filename1)) return;
            if (!File.Exists(variables.filename1)) return;
            if (DemoN.DemonDetected)
            {
                demon.write_fusion(variables.filename1);
                try
                {
                    SoundPlayer success = new SoundPlayer(Properties.Resources.chime);
                    if (variables.soundsuccess != "") success.SoundLocation = variables.soundsuccess;
                    success.Play();
                }
                catch (Exception ex) { if (variables.debugme) Console.WriteLine(ex.ToString()); };
            }
            else
            {
                if (variables.ctyp.ID == -1) variables.ctyp = callconsoletypes(ConsoleTypes.Selected.All);
                if (variables.ctyp.ID == -1) return;
                double len = new FileInfo(variables.filename1).Length;
                if (variables.debugme) Console.WriteLine("File Length = {0} | Expected 69206016 for a 64MB nand", len);
                if ((variables.ctyp.ID == 6 || variables.ctyp.ID == 7) && (len == 69206016))
                {
                    variables.nandsizex = Nandsize.S64;
                }
                else if (variables.ctyp.ID == 0)
                {
                    variables.nandsizex = Nandsize.S16;
                }
                else
                {
                    variables.nandsizex = variables.ctyp.Nsize;
                }

                if (Path.GetExtension(variables.filename1) == ".bin")
                {
                    if (!usingVNand) nandx.write(variables.filename1, variables.nandsizex, 0, 0, true, false);
                    else vnand.write_v2(variables.filename1, 0, 0, true, false);
                }
            }
        }
        void writexell()
        {
            if (String.IsNullOrWhiteSpace(variables.filename1)) loadfile(ref variables.filename1, ref this.txtFilePath1, true);
            if (String.IsNullOrWhiteSpace(variables.filename1)) return;
            if (!File.Exists(variables.filename1)) return;
            //if (textBox2.Text != "008A3020" && textBox2.Text != "00AA3020") ctypeselected = 0;
            if (DemoN.DemonDetected)
            {
                demon.write(variables.filename1);
                if (Path.GetExtension(variables.filename1) == ".bin")
                {
                    variables.filename1 = "";
                    txtFilePath1.Text = "";
                    if (variables.tempfile != "")
                    {
                        variables.filename1 = variables.tempfile;
                        txtFilePath1.Text = variables.tempfile;
                    }
                }
            }
            else
            {
                double len = new FileInfo(variables.filename1).Length;
                if (variables.debugme) Console.WriteLine("File Length = {0} | Expected 69206016 for a 64MB nand", len);

                NandX.Errors result = NandX.Errors.None;
                if (!usingVNand) result = nandx.write(variables.filename1, Nandsize.S16, 0, 0x50);
                else vnand.write_v2(variables.filename1, 0, 0x50);

                if (variables.tempfile != "" && result == NandX.Errors.None)
                {
                    variables.filename1 = variables.tempfile;
                    txtFilePath1.Text = variables.tempfile;
                }
            }
        }
        #endregion

        #region Small Stuff

        void movework()
        {
            if (variables.debugme) Console.WriteLine("Moving All files from output folder to {0}", variables.xefolder);
            String l_sDirectoryName = variables.xefolder;
            DirectoryInfo l_dDirInfo = new DirectoryInfo(l_sDirectoryName);
            if (l_dDirInfo.Exists == false)
                Directory.CreateDirectory(l_sDirectoryName);
            List<String> MyFiles = Directory.GetFiles(variables.outfolder, "*.*", SearchOption.AllDirectories).ToList();
            foreach (string file in MyFiles)
            {
                if (variables.debugme) Console.WriteLine("Moving {0}", file);
                FileInfo mFile = new FileInfo(file);
                if (new FileInfo(l_dDirInfo + "\\" + mFile.Name).Exists == false)//to remove name collusion
                    mFile.MoveTo(l_dDirInfo + "\\" + mFile.Name);
                else
                {
                    string flname = Path.GetFileNameWithoutExtension(mFile.Name);
                    int number = 1;
                    if (flname.Contains("(") && flname.Contains(")"))
                    {
                        number = Convert.ToInt32(flname.Substring(flname.IndexOf("("), 1)) + 1;
                        if (!File.Exists(l_dDirInfo + "\\" + Path.GetFileNameWithoutExtension(mFile.Name) + "(" + number + ")" + mFile.Extension))
                        {
                            mFile.MoveTo(l_dDirInfo + "\\" + Path.GetFileNameWithoutExtension(mFile.Name) + "(" + number + ")" + mFile.Extension);
                        }
                    }
                    else
                    {
                        if (!File.Exists(l_dDirInfo + "\\" + Path.GetFileNameWithoutExtension(mFile.Name) + "(1)" + mFile.Extension))
                        {
                            mFile.MoveTo(l_dDirInfo + "\\" + Path.GetFileNameWithoutExtension(mFile.Name) + "(1)" + mFile.Extension);
                        }
                    }
                }
            }
        }

        void movework_v2()
        {
            if (variables.reading) return;
            Thread.Sleep(2000);
            variables.xefolder = Path.Combine(Directory.GetParent(variables.outfolder).FullName, nand.ki.serial);

            //updateS((variables.filename1.Replace(variables.outfolder, variables.xefolder)));
            Console.WriteLine("Moving All files from output folder to {0}", variables.xefolder);
            String l_sDirectoryName = variables.xefolder;
            DirectoryInfo l_dDirInfo = new DirectoryInfo(l_sDirectoryName);
            if (l_dDirInfo.Exists == false)
                Directory.CreateDirectory(l_sDirectoryName);
            List<String> MyFiles = Directory.GetFiles(variables.outfolder, "*.*", SearchOption.TopDirectoryOnly).ToList();
            List<String> myfolders = Directory.GetDirectories(variables.outfolder, "*.*", SearchOption.TopDirectoryOnly).ToList();
            foreach (string fold in myfolders)
            {
                try
                {

                    string name = Path.GetFileName(fold);
                    // if (Directory.Exists(l_dDirInfo + "\\" + fold)) Directory.Delete(l_dDirInfo + "\\" + fold);
                    if (variables.debugme) Console.WriteLine("Moving {0}", fold);


                    if ((fold.Contains(nand.ki.serial)) || ((variables.custname != "") && (fold.Contains(variables.custname))))
                    {
                        System.IO.Directory.Move(fold, Path.Combine(variables.xefolder, name));
                        variables.custname = "";
                    }

                }
                catch (System.IO.IOException e)
                {
                    Console.WriteLine(e.Message);
                }

            }
            foreach (string file in MyFiles)
            {
                if (variables.debugme) Console.WriteLine("Moving {0}", file);
                FileInfo mFile = new FileInfo(file);
                if (new FileInfo(l_dDirInfo + "\\" + mFile.Name).Exists == false)//to remove name collusion
                    mFile.MoveTo(l_dDirInfo + "\\" + mFile.Name);
                else
                {
                    string flname = Path.GetFileNameWithoutExtension(mFile.Name);
                    int number = 1;
                    if (flname.Contains("(") && flname.Contains(")"))
                    {
                        //Console.WriteLine(flname.Substring(0,flname.IndexOf("(")));
                        //number = Convert.ToInt32(flname.Substring(flname.IndexOf("("), 1)) ;
                        string Nflname = flname.Substring(0, flname.IndexOf("("));

                        do
                        {
                            number++;
                        } while (File.Exists(l_dDirInfo + "\\" + Nflname + "(" + number + ")" + mFile.Extension));
                        if (!File.Exists(l_dDirInfo + "\\" + Nflname + "(" + number + ")" + mFile.Extension))
                        {
                            mFile.MoveTo(l_dDirInfo + "\\" + Nflname + "(" + number + ")" + mFile.Extension);
                        }
                    }
                    else
                    {
                        do
                        {
                            number++;
                        } while (File.Exists(l_dDirInfo + "\\" + Path.GetFileNameWithoutExtension(mFile.Name) + "(" + number + ")" + mFile.Extension));
                        if (!File.Exists(l_dDirInfo + "\\" + Path.GetFileNameWithoutExtension(mFile.Name) + "(" + number + ")" + mFile.Extension))
                        {
                            mFile.MoveTo(l_dDirInfo + "\\" + Path.GetFileNameWithoutExtension(mFile.Name) + "(" + number + ")" + mFile.Extension);
                        }
                    }
                }



            }
            variables.filename1 = variables.filename1.Replace(variables.outfolder, variables.xefolder);
            txtFilePath1.Text = variables.filename1;
        }

        void nand_init()
        {
            //erasevariables();
#if Dev
            if (variables.current_mode == variables.JR_MODE.MODEFW)
            {
                fwInfo.setSource(jf.fwInit(Firmware.JF.fwFile.SOURCE));
            }
            else
            {
                ThreadStart starter = delegate { nandinit(); };
                new Thread(starter).Start();
            }
#else
            ThreadStart starter = delegate { nandinit(); };
            new Thread(starter).Start();
#endif
        }

        void erasevariables()
        {
            variables.fulldump = false; variables.twombread = false;
            variables.ctyp = variables.cunts[0]; variables.gotvalues = false;
            variables.cpkey = "";
            //variables.outfolder = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "output");
            xPanel.setMBname("");
            txtCPUKey.Text = "";
            variables.flashconfig = "";
            /*if (variables.changeldv != 0)
            {
                string cfldv = "cfldv=";
                string[] edit = { cfldv };
                string[] delete = { };
                parse_ini.edit_ini(Path.Combine(variables.pathforit, @"xeBuild\data\options.ini"), edit, delete);
             * */
            variables.changeldv = 0;
            //}
            //btnCheckBadBlocks.Visible = true;
        }

        private void updatecptextbox()
        {
            if (variables.debugme) Console.WriteLine("Event wait");
            _event1.WaitOne();
            if (variables.debugme) Console.WriteLine("Event Started");
            if (variables.debugme) Console.WriteLine(variables.cpkey);
            this.txtCPUKey.Text = variables.cpkey;
        }

        private static void savekvinfo(string savefile)
        {
            try
            {
                if (!nand.ok) return;
                TextWriter tw = new StreamWriter(savefile);
                tw.WriteLine("*******************************************");
                tw.WriteLine("*******************************************");
                string console_type = "";
                if (nand.bl.CB_A >= 9188 && nand.bl.CB_A <= 9250)
                {
                    console_type = "Trinity";
                }
                else if (nand.bl.CB_A >= 13121 && nand.bl.CB_A <= 13200)
                {
                    console_type = "Corona";
                    if (nand.noecc) console_type += " 4GB";
                }
                else if (nand.bl.CB_A >= 6712 && nand.bl.CB_A <= 6780) console_type = "Jasper";
                else if (nand.bl.CB_A >= 4558 && nand.bl.CB_A <= 4590) console_type = "Zephyr";
                else if ((nand.bl.CB_A >= 1888 && nand.bl.CB_A <= 1960) || nand.bl.CB_A == 7373 || nand.bl.CB_A == 8192) console_type = "Xenon";
                else if (nand.bl.CB_A >= 5761 && nand.bl.CB_A <= 5780) console_type = "Falcon/Opus";
                else
                {
                    if (variables.smcmbtype < variables.console_types.Length && variables.smcmbtype >= 0) console_type = variables.console_types[variables.smcmbtype];
                }
                tw.WriteLine("Console Type: {0}", console_type);
                tw.WriteLine("");
                tw.WriteLine("Cpu Key: {0}", variables.cpkey);
                tw.WriteLine("");
                tw.WriteLine("KV Type: {0}", nand.ki.kvtype.Replace("0", ""));
                tw.WriteLine("");
                tw.WriteLine("MFR Date: {0}", nand.ki.mfdate);
                tw.WriteLine("");
                tw.WriteLine("Console ID: {0}", nand.ki.consoleid);
                tw.WriteLine("");
                tw.WriteLine("Serial: {0}", nand.ki.serial);
                tw.WriteLine("");
                string region = "";
                if (nand.ki.region == "02FE") region = "PAL/EU";
                else if (nand.ki.region == "00FF") region = "NTSC/US";
                else if (nand.ki.region == "01FE") region = "NTSC/JAP";
                else if (nand.ki.region == "01FF") region = "NTSC/JAP";
                else if (nand.ki.region == "01FC") region = "NTSC/KOR";
                else if (nand.ki.region == "0101") region = "NTSC/HK";
                else if (nand.ki.region == "0201") region = "PAL/AUS";
                else if (nand.ki.region == "7FFF") region = "DEVKIT";
                tw.WriteLine("Region: {0} | {1}", nand.ki.region, region);
                tw.WriteLine("");
                tw.WriteLine("Osig: {0}", nand.ki.osig);
                tw.WriteLine("");
                tw.WriteLine("DVD Key: {0}", nand.ki.dvdkey);
                tw.WriteLine("");
                tw.WriteLine("*******************************************");
                tw.WriteLine("*******************************************");
                tw.Close();
                Console.WriteLine("KV Info saved to file");
            }
            catch (Exception ex) { if (variables.debugme) Console.WriteLine(ex.ToString()); Console.WriteLine("Failed"); }
        }

        void comparenands()
        {
            if (variables.filename1 == null || variables.filename2 == null) { MessageBox.Show("Input all Files"); return; }
            if (!File.Exists(variables.filename1) || !File.Exists(variables.filename2)) return;
            else
            {
                FileInfo inf = new FileInfo(variables.filename1);
                string time = "";
                if (inf.Length > 64 * 1024 * 1024) time = "Takes a while on big nands";
                Console.WriteLine("Comparing...{0}", time);
                try
                {
                    byte[] temp1 = Nand.BadBlock.find_bad_blocks_b(variables.filename1, true);
                    byte[] temp2 = Nand.BadBlock.find_bad_blocks_b(variables.filename2, true);

                    string temp1_hash = Oper.GetMD5HashFromFile(temp1);
                    string temp2_hash = Oper.GetMD5HashFromFile(temp2);

                    temp1 = null;
                    temp2 = null;
                    //filecompareresult = FileEquals(filename1, filename2);
                    if (temp1_hash == temp2_hash)
                    {
                        Console.WriteLine("Nands are the same");
                        try
                        {
                            SoundPlayer success = new SoundPlayer(Properties.Resources.chime);
                            if (variables.soundcompare != "") success.SoundLocation = variables.soundcompare;
                            success.Play();
                        }
                        catch (Exception ex) { if (variables.debugme) Console.WriteLine(ex.ToString()); };
                        try
                        {
                            string md5file = Path.Combine(Directory.GetParent(variables.filename1).ToString(), "checksum.md5");
                            string hash1 = Oper.GetMD5HashFromFile(variables.filename1);
                            string hash2 = Oper.GetMD5HashFromFile(variables.filename2);
                            if (File.Exists(md5file))
                            {
                                File.AppendAllText(md5file, "\n");
                                File.AppendAllText(md5file, hash1 + " *" + Path.GetFileName(variables.filename1) + "\n");
                                File.AppendAllText(md5file, hash1 + " *" + Path.GetFileName(variables.filename2) + "\n");
                            }
                            else
                            {
                                using (StreamWriter file = new StreamWriter(Path.Combine(Directory.GetParent(variables.filename1).ToString(), "checksum.md5")))
                                {
                                    file.WriteLine("# MD5 checksums generated by J-Runner");
                                    file.WriteLine("{0} *{1}", hash1, Path.GetFileName(variables.filename1));
                                    file.WriteLine("{0} *{1}", hash2, Path.GetFileName(variables.filename2));
                                }
                            }
                            if (variables.deletefiles)
                            {
                                File.Delete(variables.filename2);
                                txtFilePath2.Text = "";
                            }
                        }
                        catch (Exception ex) { if (variables.debugme) Console.WriteLine(ex.ToString()); }
                    }
                    else
                    {
                        try
                        {
                            SoundPlayer error = new SoundPlayer(Properties.Resources.Error);
                            if (variables.sounderror != "") error.SoundLocation = variables.sounderror;
                            error.Play();
                        }
                        catch (Exception ex) { if (variables.debugme) Console.WriteLine(ex.ToString()); };

                        if (MessageBox.Show("Files do not match!\nShow Differences?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                        {
                            FileEquals(variables.filename1, variables.filename2);
                        }
                        txtFilePath1.Text = "";
                        txtFilePath2.Text = "";
                        variables.filename1 = "";
                        variables.filename2 = "";
                    }
                }
                catch (Exception ex) { Console.WriteLine(ex.InnerException.ToString()); }
            }
        }

        public static string parsecpukey(string filename)
        {
            if (Path.GetExtension(filename) == ".txt")
            {
                Regex objAlphaPattern = new Regex("[a-fA-F0-9]{32}$");
                string[] cpu = File.ReadAllLines(filename);
                string cpukey = "";
                bool check = false;
                int i = 0;
                foreach (string line in cpu)
                {
                    if (objAlphaPattern.Match(line).Success) i++;
                    if (i > 1) check = true;
                }
                foreach (string line in cpu)
                {
                    if (check)
                    {
                        if (line.ToUpper().Contains("CPU"))
                        {
                            cpukey = (objAlphaPattern.Match(line).Value);
                        }
                    }
                    else
                    {
                        cpukey = (objAlphaPattern.Match(line).Value);
                        break;
                    }
                    //Console.WriteLine(objAlphaPattern.Match(line).Value);
                }
                if (Nand.Nand.VerifyKey(Oper.StringToByteArray(cpukey))) return cpukey;
                else return "";
            }
            else return "";
        }

        private long CRCbl(string filename)
        {
            crc32 crc = new crc32();
            long hashData = 0;
            if (File.Exists(filename))
            {
                byte[] fileb = File.ReadAllBytes(filename);
                fileb = editbl(fileb);
                hashData = crc.CRC(fileb);
            }
            return hashData;
        }
        private byte[] editbl(byte[] bl)
        {
            int length = Oper.ByteArrayToInt(Oper.returnportion(bl, 0xC, 4));
            if (bl[0] == 0x43 && bl[1] == 0x42)
            {
                for (int i = 0x10; i < 0x40; i++) bl[i] = 0x0;
            }
            else if (bl[0] == 0x43 && bl[1] == 0x44)
            {
                for (int i = 0x10; i < 0x20; i++) bl[i] = 0x0;
            }
            else if (bl[0] == 0x43 && bl[1] == 0x45)
            {
                for (int i = 0x10; i < 0x20; i++) bl[i] = 0x0;
            }
            else if (bl[0] == 0x43 && bl[1] == 0x46)
            {
                for (int i = 0x20; i < 0x230; i++) bl[i] = 0x0;
            }
            else if (bl[0] == 0x43 && bl[1] == 0x47)
            {
                for (int i = 0x10; i < 0x20; i++) bl[i] = 0x0;
            }
            return Oper.returnportion(bl, 0, length);
        }
        bool editblini(string file, string label, string cba, string cbb = "")
        {
            string bla;
            string blb;
            bool splitcb = true;
            if (String.IsNullOrWhiteSpace(cbb)) splitcb = false;
            if (!splitcb)
            {
                if (!File.Exists(Path.Combine(variables.pathforit, "common", "cb_" + cba + ".bin")))
                {
                    Console.WriteLine("{0} not found. Trying to download file..", "common/cb_" + cba + ".bin");
                }
                if (!File.Exists(Path.Combine(variables.pathforit, "common", "cb_" + cba + ".bin")))
                {
                    Console.WriteLine("{0} not found. Insert it manually on the common folder", "cb_" + cba + ".bin");
                    return false;
                }
                bla = "cb_" + cba + ".bin," + CRCbl(Path.Combine(variables.pathforit, "common", "cb_" + cba + ".bin")).ToString("x8");
                blb = "none,00000000";
            }
            else
            {
                if (!File.Exists(Path.Combine(variables.pathforit, "common", "cba_" + cba + ".bin")))
                {
                    Console.WriteLine("{0} not found. Trying to download file..", "common/cba_" + cba + ".bin");
                }
                if (!File.Exists(Path.Combine(variables.pathforit, "common", "cbb_" + cbb + ".bin")))
                {
                    Console.WriteLine("{0} not found. Trying to download file..", "common/cbb_" + cbb + ".bin");
                }
                if (!File.Exists(Path.Combine(variables.pathforit, "common", "cba_" + cba + ".bin")))
                {
                    Console.WriteLine("{0} not found. Insert it manually on the common folder", "cba_" + cba + ".bin");
                    return false;
                }
                if (!File.Exists(Path.Combine(variables.pathforit, "common", "cbb_" + cbb + ".bin")))
                {
                    Console.WriteLine("{0} not found. Insert it manually on the common folder", "cbb_" + cba + ".bin");
                    return false;
                }
                bla = "cba_" + cba + ".bin," + CRCbl(Path.Combine(variables.pathforit, "common", "cba_" + cba + ".bin")).ToString("x8");
                blb = "cbb_" + cbb + ".bin," + CRCbl(Path.Combine(variables.pathforit, "common", "cbb_" + cbb + ".bin")).ToString("x8");
            }
            Console.WriteLine("Editing File..");
            string[] lines = File.ReadAllLines(file);
            int i = 0;
            for (; i < lines.Length; i++)
            {
                if (lines[i] == "") continue;
                else if (lines[i].Contains('[') && lines[i].Contains(label) && lines[i].Contains(']')) break;
            }
            lines[i + 1] = bla;
            lines[i + 2] = blb;
            File.WriteAllLines(file, lines);
            Console.WriteLine("Done");
            return true;
        }

        static int FileEquals(string fileName1, string fileName2)
        {
            // Check the file size and CRC equality here.. if they are equal...    
            try
            {
                using (var file1 = new FileStream(fileName1, FileMode.Open))
                using (var file2 = new FileStream(fileName2, FileMode.Open))
                    return StreamEquals(file1, file2);
            }
            catch (System.IO.IOException)
            {
                return -1;
            }
        }
        static int StreamEquals(Stream stream1, Stream stream2)
        {
            const int bufferSize = 0x4200;
            int count = 0;
            byte[] buffer1 = new byte[bufferSize]; //buffer size
            byte[] buffer2 = new byte[bufferSize];
            while (true)
            {
                count += 0x4200;
                int count1 = stream1.Read(buffer1, 0, bufferSize);
                int count2 = stream2.Read(buffer2, 0, bufferSize);

                if (count1 != count2)
                    return 0;

                if (count1 == 0)
                    return 1;

                // You might replace the following with an efficient "memcmp"
                if (!buffer1.Take(count1).SequenceEqual(buffer2.Take(count2)))
                    Console.WriteLine("0x{0:X4}", (count - 0x4200) / 0x4200);
            }
        }

        #endregion

        #region Nand Manipulation

        void nandinit()
        {
            bool movedalready = false;
            if (String.IsNullOrEmpty(variables.filename1)) return;
            if (!File.Exists(variables.filename1))
            {
                MessageBox.Show("File is missing");
                return;
            }
            try
            {
                progressBar.BeginInvoke((Action)(() => progressBar.Value = progressBar.Minimum));
                if (Path.GetExtension(variables.filename1) != ".bin") return;
                variables.gotvalues = true;

                bool sts = objAlphaPattern.IsMatch(variables.cpkey);

                string cpufile = Path.Combine(Path.GetDirectoryName(variables.filename1), "cpukey.txt");
                if (File.Exists(cpufile) && !(variables.cpkey.Length == 32 && sts))
                {
                    variables.cpkey = parsecpukey(cpufile);
                }

                if ((variables.cpkey.Length != 32 || !objAlphaPattern.IsMatch(variables.cpkey))) variables.cpkey = "";
                Console.WriteLine("Initializing {0}..", Path.GetFileName(variables.filename1));
                nandInfo.change_tab();
                nand = new Nand.PrivateN(variables.filename1, variables.cpkey);
                if (!nand.ok) return;

                if (!String.IsNullOrEmpty(nand._cpukey))
                {
                    variables.cpkey = nand._cpukey;
                    txtCPUKey.Text = variables.cpkey;

                }

                if (string.IsNullOrEmpty(variables.cpkey))
                {
                    if (variables.debugme) Console.WriteLine("KV CRC: {0:X}", nand.kvcrc());
                    if (variables.debugme) Console.WriteLine("Searching Registry Entrys");
                    try
                    {
                        variables.cpkey = CpuKeyDB.getkey_s(nand.kvcrc(), xPanel.getDataSet());
                        txtCPUKey.Text = variables.cpkey;
                    }
                    catch (NullReferenceException ex) { Console.WriteLine(ex.ToString()); }
                    if (!String.IsNullOrEmpty(variables.cpkey))
                    {
                        if (variables.debugme) Console.WriteLine("Found key in registry");
                        nand.cpukeyverification(variables.cpkey);
                        if (variables.debugme) Console.WriteLine("allmove ", variables.allmove);
                        if (variables.debugme) Console.WriteLine(!variables.filename1.Contains(nand.ki.serial));
                        if (variables.debugme) Console.WriteLine(variables.filename1.Contains(variables.outfolder));
                        if ((variables.allmove) && (!variables.filename1.Contains(nand.ki.serial)) && (variables.filename1.Contains(variables.outfolder)))
                        {
                            if (!movedalready)
                            {
                                Thread Go = new Thread(movework_v2);
                                Go.Start();
                                movedalready = true;
                            }
                        }
                    }

                }
                else
                {
                    if (!CpuKeyDB.getkey_s(variables.cpkey, xPanel.getDataSet()))
                    {
                        if (variables.debugme) Console.WriteLine("Key verification");
                        if (nand.cpukeyverification(variables.cpkey))
                        {
                            Console.WriteLine("CpuKey is Correct");
                            if (variables.debugme) Console.WriteLine("Adding key to registry");
                            CpuKeyDB.regentries entry = new CpuKeyDB.regentries();
                            entry.kvcrc = nand.kvcrc().ToString("X");
                            entry.serial = nand.ki.serial;
                            entry.cpukey = variables.cpkey;
                            entry.extra = Nand.Nand.getConsoleName(nand, variables.flashconfig);
                            entry.dvdkey = nand.ki.dvdkey;
                            entry.osig = nand.ki.osig;
                            entry.region = nand.ki.region;


                            bool reg = CpuKeyDB.addkey_s(entry, xPanel.getDataSet());
                            if (variables.autoExtract && reg)
                            {
                                if (variables.debugme) Console.WriteLine("Auto File Extraction Initiated");
                                extractFilesFromNand();

                            }
                            if (reg) nandInfo.show_cpukey_tab();
                            txtCPUKey.Text = variables.cpkey;
                            if ((!variables.filename1.Contains(nand.ki.serial)) && (variables.filename1.Contains(variables.outfolder)))
                            {
                                if (!movedalready)
                                {
                                    Thread Go = new Thread(movework_v2);
                                    Go.Start();
                                    movedalready = true;
                                }
                            }
                        }

                        else Console.WriteLine("Wrong CpuKey");
                    }
                }

                nandInfo.setNand(nand);

                progressBar.Value = progressBar.Maximum / 3;

                if (variables.debugme) Console.WriteLine("----------------------");
                variables.ctyp = variables.cunts[0];
                variables.ctyp = Nand.Nand.getConsole(nand, variables.flashconfig);
                xPanel.setMBname(variables.ctyp.Text);

                variables.jtagable = false;
                variables.rghable = true;

                switch (Nand.ntable.getHackfromCB(nand.bl.CB_A))
                {
                    case Classes.xebuild.hacktypes.glitch:
                        xPanel.setRbtnGlitchChecked(true);
                        break;
                    case Classes.xebuild.hacktypes.glitch2:
                        xPanel.setRbtnRGH2Checked(true);
                        break;
                    case Classes.xebuild.hacktypes.jtag:
                        xPanel.setRbtnJtagChecked(true);
                        break;
                    default:
                        xPanel.setRbtnRetailChecked(true);
                        break;
                }
                ///

                if ((nand.bl.CF_0 > nand.bl.CF_1 ? nand.bl.CF_0 : nand.bl.CF_1) > 14719)
                {
                    if (!xPanel.getRbtnJtagChecked()) nTools.setbtnCreateECC("Create ECC*");
                    variables.eccmethod = true;
                }
                else
                {
                    if (!xPanel.getRbtnJtagChecked()) nTools.setbtnCreateECC("Create ECC");
                    variables.eccmethod = false;
                }


                if ((nand.bl.CF_0 > nand.bl.CF_1 ? nand.bl.CF_0 : nand.bl.CF_1) >= 15574) xPanel.setRJtagVisible(true);
                else xPanel.setRJtagVisible(false);

                if (String.IsNullOrEmpty(variables.cpkey)) variables.gotvalues = false;
                else variables.gotvalues = true;
                Console.WriteLine("Nand Initialization Finished");


                progressBar.Value = progressBar.Maximum;
                if (variables.debugme) Console.WriteLine("allmove ", variables.allmove);
                if (variables.debugme) Console.WriteLine(!variables.filename1.Contains(nand.ki.serial));
                if (variables.debugme) Console.WriteLine(variables.filename1.Contains(variables.outfolder));
                if ((variables.allmove) && (!variables.filename1.Contains(nand.ki.serial)) && (variables.filename1.Contains(variables.outfolder)))
                {
                    if (!movedalready)
                    {
                        Thread Go = new Thread(movework_v2);
                        Go.Start();
                        movedalready = true;
                    }
                }
            }
            catch (SystemException ex)
            {
                Console.WriteLine(ex.GetType().ToString());
                if (variables.debugme) Console.WriteLine(ex.ToString());
                return;
            }
        }

        string load_dgx()
        {
            if (variables.ctyp.ID != 11 && variables.ctyp.ID != 1 && variables.ctyp.ID != 10) variables.ctyp = callconsoletypes(ConsoleTypes.Selected.AllX);
            if (Path.GetExtension(variables.filename1) == ".bin")
            {
                variables.tempfile = variables.filename1;
            }
            string cr4 = "";
            if (xPanel.getCR4Checked()) cr4 = "_CR4";
            switch (variables.ctyp.ID)
            {
                case 1:
                    variables.filename1 = Path.Combine(variables.pathforit, "common", "ECC", variables.DGX_trinity + cr4 + ".ecc");
                    break;
                case 2:
                    variables.filename1 = Path.Combine(variables.pathforit, "common", "ECC", variables.DGX_falcon + cr4 + ".ecc");
                    break;
                case 3:
                    variables.filename1 = Path.Combine(variables.pathforit, "common", "ECC", variables.DGX_zephyr + ".ecc");
                    break;
                case 4:
                case 5:
                case 6:
                case 7:
                    variables.filename1 = Path.Combine(variables.pathforit, "common", "ECC", variables.DGX_jasper + cr4 + ".ecc");
                    break;
                case 9:
                    variables.filename1 = Path.Combine(variables.pathforit, "common", "ECC", variables.DGX_falcon + cr4 + ".ecc");
                    break;
                case 10:
                    variables.filename1 = Path.Combine(variables.pathforit, "common", "ECC", variables.DGX_Corona + cr4 + ".ecc");
                    break;
                case 11:
                    variables.filename1 = Path.Combine(variables.pathforit, "common", "ECC", variables.DGX_Corona4GB + cr4 + ".ecc");
                    break;
                default:
                    return "";
            }
            txtFilePath1.Text = variables.filename1;
            return variables.filename1;
        }

        void createecc_v2()
        {
            //Thread.CurrentThread.Join();

            if (xPanel.getRbtnRetailChecked()) Console.WriteLine("!You are creating an ecc image and you have selected {0}!", variables.ttyp);
            else if (xPanel.getRbtnJtagChecked()) Console.WriteLine("!You are creating an ecc image and you have selected {0}!", variables.ttyp);
            //savedir();

            if (File.Exists(variables.filename1))
            {
                if (variables.debugme) Console.WriteLine("Filename1 = {0}", variables.filename1);
                if (Path.GetExtension(variables.filename1) == ".bin")
                {
                    variables.tempfile = variables.filename1;
                    progressBar.Value = progressBar.Minimum;
                    int result = 0;
                    try
                    {
                        bool sts = objAlphaPattern.IsMatch(txtCPUKey.Text);

                        ECC ecc = new ECC();
                        result = ecc.creatergh2ecc(variables.filename1, variables.outfolder, ref this.progressBar, txtCPUKey.Text);
                        /*
                        if (comboRGH.SelectedIndex == 0)
                        {
                            result = Nand.createeccimage(variables.filename1, variables.outfolder, ref this.progressBar1);
                        }
                        else
                        {
                            Console.WriteLine("Constructing an rgh2 ecc image");
                            if (sts) result = ECC.creatergh2(variables.filename1, variables.outfolder, ref this.progressBar1, cpukeytext.Text);
                            else result = ECC.creatergh2(variables.filename1, variables.outfolder, ref this.progressBar1);
                        }
                        */
                    }
                    catch (Exception ex) { if (variables.debugme) Console.WriteLine(ex.ToString()); }
                    if (result == 1)
                    {
                        variables.filename1 = Path.Combine(variables.outfolder, "image_00000000.ecc");
                        txtFilePath1.Text = variables.filename1;
                    }
                    else if (result == 5)
                    {
                        progressBar.Value = progressBar.Maximum;
                    }
                    else Console.WriteLine("Failed to create ecc image");
                }
            }
        }

        void createxell()
        {
            if (String.IsNullOrWhiteSpace(variables.filename1))
            {
                loadfile(ref variables.filename1, ref this.txtFilePath1, true);
                if (String.IsNullOrWhiteSpace(variables.filename1))
                {
                    MessageBox.Show("No File Selected");
                    return;
                }
            }
            if (variables.ctyp.ID == -1) variables.ctyp = callconsoletypes(ConsoleTypes.Selected.JTAG);
            if (variables.ctyp.ID == -1) return;
            if (File.Exists(variables.filename1))
            {
                variables.tempfile = variables.filename1;
                if (variables.debugme) Console.WriteLine("Filename1 = {0}", variables.filename1);
                if (Path.GetExtension(variables.filename1) == ".bin")
                {
                    byte[] Keyraw = Nand.Nand.getrawkv(variables.filename1);
                    long size1 = 0;
                    string xellfile;
                    if (variables.ctyp.ID == 1) return;
                    else if (variables.ctyp.ID == 8) xellfile = "xenon.bin";
                    else if (variables.ctyp.ID == 2)
                    {
                        if (xPanel.getAudClampChecked()) xellfile = "falcon_hack_aud_clamp.bin";
                        else xellfile = "falcon.bin";
                    }
                    else if (variables.ctyp.ID == 3)
                    {
                        if (xPanel.getAudClampChecked()) xellfile = "zephyr_hack_aud_clamp.bin";
                        else xellfile = "zephyr.bin";
                    }
                    else if (variables.ctyp.ID == 4 || variables.ctyp.ID == 5)
                    {
                        if (xPanel.getAudClampChecked()) xellfile = "jasper_hack_aud_clamp.bin";
                        else xellfile = "jasper.bin";
                    }
                    else if (variables.ctyp.ID == 6 || variables.ctyp.ID == 7)
                    {
                        if (xPanel.getAudClampChecked()) xellfile = "jasper_hack_bigblock_aud_clamp.bin";
                        else xellfile = "jasper_bb.bin";
                    }
                    else return;
                    if (variables.debugme) Console.WriteLine(xellfile);


                    byte[] xellous = Oper.openfile(Path.Combine(variables.pathforit, "common\\xell\\" + xellfile), ref size1, 0);
                    if (variables.debugme) Console.WriteLine("{0} file loaded successfully", xellfile);
                    if (variables.debugme) Console.WriteLine("{0:X} | {1:X}", xellous.Length, Keyraw.Length);

                    Buffer.BlockCopy(Keyraw, 0, xellous, 0x4200, 0x4200);

                    if (xPanel.getRJtagChecked())
                    {
                        int layout = 0;
                        if (variables.ctyp.ID == 6 || variables.ctyp.ID == 7) layout = 2;
                        else if (variables.ctyp.ID == 4 || variables.ctyp.ID == 5) layout = 1;
                        byte[] SMC;
                        byte[] smc_len = new byte[4], smc_start = new byte[4];
                        Buffer.BlockCopy(xellous, 0x78, smc_len, 0, 4);
                        Buffer.BlockCopy(xellous, 0x7C, smc_start, 0, 4);
                        SMC = new byte[Oper.ByteArrayToInt(smc_len)];
                        Buffer.BlockCopy(Nand.Nand.unecc(xellous), Oper.ByteArrayToInt(smc_start), SMC, 0, Oper.ByteArrayToInt(smc_len));
                        SMC = Nand.Nand.addecc_v2(Nand.Nand.encrypt_SMC(Nand.Nand.patch_SMC(Nand.Nand.decrypt_SMC(SMC))), true, 0, layout);
                        Buffer.BlockCopy(SMC, 0, xellous, (Oper.ByteArrayToInt(smc_start) / 0x200) * 0x210, (Oper.ByteArrayToInt(smc_len) / 0x200) * 0x210);
                    }

                    variables.filename1 = Path.Combine(variables.outfolder, xellfile);
                    if (variables.debugme) Console.WriteLine(variables.filename1);
                    Oper.savefile(xellous, variables.filename1);
                    if (variables.debugme) Console.WriteLine("Saved Successfully");
                    txtFilePath1.Text = variables.filename1;
                    Console.WriteLine("XeLL file created Successfully {0}", xellfile);
                }
            }
        }

        #endregion

        #region Forms

        void loadfile(ref string filename, ref TextBox tx, bool erase = false)
        {

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "(*.bin;*.ecc)|*.bin;*.ecc|(*.hex)|*.hex|All files (*.*)|*.*";
            openFileDialog1.Title = "Select a File";
            if (variables.FindFolder != "")
            {
                openFileDialog1.InitialDirectory = variables.FindFolder;
                variables.FindFolder = "";
            }
            else openFileDialog1.InitialDirectory = variables.currentdir;
            openFileDialog1.RestoreDirectory = false;

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (erase) erasevariables();
                filename = openFileDialog1.FileName;
                if (!String.IsNullOrWhiteSpace(filename)) tx.Text = filename;
            }
            variables.currentdir = filename;
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
            //if (variables.debugme) Console.WriteLine(myNewForm.heResult());
            xPanel.setMBname(myNewForm.heResult().Text);
            return (myNewForm.heResult());
        }

        void callcpukeydb()
        {
            CpuKeyDB mycpukeydb = new CpuKeyDB();
            mycpukeydb.Show();
            mycpukeydb.FormClosed += new FormClosedEventHandler(mycpukeydb_FormClosed);
        }

        string callxsvf()
        {
            xsvf_types mForm = new xsvf_types();
            mForm.ShowDialog();
            if (mForm.DialogResult != DialogResult.OK) return ("");
            if (mForm.heResult() == -1) return "";
            if (variables.debugme) Console.WriteLine(mForm.heResult());
            bool demon = mForm.deResult();
            if (variables.debugme) Console.WriteLine("demon {0}", demon);
            if (demon)
            {
                if (variables.debugme) Console.WriteLine(variables.demon_xsvf[mForm.heResult() - 1]);
                return (variables.demon_xsvf[mForm.heResult() - 1]);
            }
            else
            {
                if (variables.debugme) Console.WriteLine(variables.xsvf[mForm.heResult() - 1]);
                return (variables.xsvf[mForm.heResult() - 1]);
            }
        }

        void calldrives(string filename = "", Panels.LDrivesInfo.Function f = Panels.LDrivesInfo.Function.ReadWrite)
        {
            ldInfo.setup(f);
            pnlInfo.Controls.Clear();
            pnlInfo.Controls.Add(ldInfo);
            if (listInfo.Contains(ldInfo)) listInfo.Remove(ldInfo);
            listInfo.Add(ldInfo);
            //Forms.LDrives ld = new Forms.LDrives(filename, f, numericiter);
            //ld.Show();
            //ld.FormClosed += ld_FormClosed;
        }

        void ld_FormClosed(object sender, FormClosedEventArgs e)
        {
            string getfilename = "";
            getfilename = Forms.LDrives.filename;
            if (!string.IsNullOrEmpty(getfilename) && Forms.LDrives.fu != Forms.LDrives.Function.Write)
            {
                txtFilePath1.Text = getfilename;
                variables.filename1 = getfilename;
            }
            if (Forms.LDrives.fu == Forms.LDrives.Function.Read && Forms.LDrives.files.Count > 0)
            {
                int i = 0;
                int cnt = Forms.LDrives.files.Count;
                foreach (string filename in Forms.LDrives.files)
                {
                    if (i == 0)
                    {
                        txtFilePath1.Text = filename;
                        variables.filename1 = filename;
                        nand_init();
                    }
                    else if (i == 1)
                    {
                        txtFilePath2.Text = filename;
                        variables.filename2 = filename;
                        new Thread(comparenands).Start();
                    }
                    i++;
                }
            }
            //nand_init();
        }

        void mycpukeydb_FormClosed(object sender, FormClosedEventArgs e)
        {
            txtCPUKey.Text = variables.cpkey;
        }

        #endregion

        #endregion

        #region User Input

        void updateProgress(int progress)
        {
            progressBar.BeginInvoke((Action)(() => progressBar.Value = progress));
        }

        void updateBlocks(String progress)
        {
            txtBlocks.BeginInvoke((Action) (() => txtBlocks.Text = progress));
        }

        #region Menu Bar

        #region Tools

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
        public void extractFilesFromNand()
        {
            Console.WriteLine("Extracting..");
            if (!nand.ok) return;
            string tmpout = "";
            if ((variables.modder) && (variables.custname != ""))
            {
                tmpout = Path.Combine(variables.outfolder, "Extracts-" + variables.custname);
            }
            else
            {
                tmpout = Path.Combine(variables.outfolder, "Extracts-" + nand.ki.serial);
            }

            if (Directory.Exists(tmpout) == false)
            {
                Directory.CreateDirectory(tmpout);
            }

            Console.WriteLine("Saving SMC_en.bin");
            Oper.savefile(Nand.Nand.encrypt_SMC(nand._smc), Path.Combine(tmpout, "SMC_en.bin"));
            Console.WriteLine("Saving SMC_dec.bin");
            Oper.savefile(nand._smc, Path.Combine(tmpout, "SMC_dec.bin"));
            Console.WriteLine("Saving KV_en.bin");
            Oper.savefile(nand._rawkv, Path.Combine(tmpout, "KV_en.bin"));

            if (!String.IsNullOrEmpty(nand._cpukey))
            {
                Console.WriteLine("Saving KV_dec.bin");
                Oper.savefile(Nand.Nand.decryptkv(nand._rawkv, Oper.StringToByteArray(nand._cpukey)), Path.Combine(tmpout, "KV_dec.bin"));
            }
            Console.WriteLine("Saving smc_config.bin");
            nand.getsmcconfig();
            Oper.savefile(nand._smc_config, Path.Combine(tmpout, "smc_config.bin"));

            if (variables.ctyp.ID == 1 || variables.ctyp.ID == 10 || variables.ctyp.ID == 11)
            {
                byte[] t;
                Console.WriteLine("Working...");
                byte[] fcrt = nand.exctractFSfile("fcrt.bin");
                if (fcrt != null)
                {
                    Console.WriteLine("Saving fcrt_en.bin");
                    Oper.savefile(fcrt, Path.Combine(tmpout, "fcrt_en.bin"));
                    byte[] fcrt_dec;
                    if (Nand.Nand.decrypt_fcrt(fcrt, Oper.StringToByteArray(nand._cpukey), out fcrt_dec))
                    {
                        Console.WriteLine("Saving fcrt_dec.bin");
                        File.WriteAllBytes(Path.Combine(tmpout, "fcrt_dec.bin"), fcrt_dec);
                    }
                    t = responses(fcrt, Oper.StringToByteArray(nand._cpukey), nand.ki.dvdkey);

                    if (t != null)
                    {

                        Console.WriteLine("Saving C-R.bin");
                        File.WriteAllBytes(Path.Combine(tmpout, "C-R.bin"), t);
                        Console.WriteLine("Saving key.bin");
                        File.WriteAllBytes(Path.Combine(tmpout, "key.bin"), Oper.StringToByteArray(nand.ki.dvdkey));
                        Console.WriteLine("Done");
                        Console.WriteLine("Save location {0}", tmpout);
                    }
                    else Console.WriteLine("Failed to create C-R.bin");
                }
                else Console.WriteLine("Failed to find fcrt.bin");
            }
            else Console.WriteLine("Finished");
        }
        public static byte[] responses(byte[] fcrt, byte[] cpukey, string dvdkey = "")
        {
            byte[] fcrt_dec;
            if (Nand.Nand.decrypt_fcrt(fcrt, cpukey, out fcrt_dec))
            {
                byte[] rfct = new byte[0x1F6 * 0x13];
                Oper.removeByteArray(ref fcrt_dec, 0, 0x140);
                Random rnd = new Random();
                int[] randomNumbers = Enumerable.Range(0, 502).OrderBy(i => rnd.Next()).ToArray();
                int counter = 0;
                while (counter < (rfct.Length / 0x13))
                {
                    byte[] cr = Oper.returnportion(fcrt_dec, counter * 0x20, 0x20);
                    Oper.removeByteArray(ref cr, 2, 0x10 - 3);
                    Buffer.BlockCopy(cr, 0, rfct, randomNumbers[counter] * cr.Length, cr.Length);
                    counter++;
                }
                for (int i = 0; i < 0x1f6; i++)
                {
                    if (Oper.allsame(Oper.returnportion(fcrt_dec, i * 0x20, 0x10), 0x00)) continue;
                    for (int j = i + 1; j < 0x1f6; j++)
                    {
                        if (Oper.allsame(Oper.returnportion(fcrt_dec, j * 0x20, 0x10), 0x00)) continue;
                        if (rfct[i * 0x13] == rfct[j * 0x13] &&
                            rfct[(i * 0x13) + 1] == rfct[(j * 0x13) + 1] &&
                            rfct[(i * 0x13) + 2] == rfct[(j * 0x13) + 2])
                        {
                            if (variables.debugme) Console.WriteLine("You're FUCKED");
                        }
                    }
                }
                return encryptFirmware(rfct, variables.xor, rfct.Length);
            }
            return null;
        }
        private static byte swapBits(byte chunk, int[] bits)
        {
            byte result = 0;
            //var bit = (b & (1 << bits[i])) != 0;
            int i;
            for (i = 0; i < 8; i++)
            {
                byte bit = (byte)((chunk & (1 << bits[i])) >> bits[i]);
                result = (byte)((result << 1) | bit);
            }
            return result;
        }
        private static byte[] encryptFirmware(byte[] inputBuffer, byte[] XorList, int size)
        {
            int[] encryptBits = { 3, 2, 7, 6, 1, 0, 5, 4 };
            int i;
            byte bt, done;
            byte[] outputBuffer = new byte[size];
            for (i = 0; i < size; i++)
            {
                bt = (byte)(inputBuffer[i] ^ XorList[i]);
                done = swapBits(bt, encryptBits);
                outputBuffer[i] = done;
            }
            return outputBuffer;
        }
        private void extractFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            extractFilesFromNand();

        }

        private void logPostToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ThreadStart starter = delegate { nandx.log_post(); };
            new Thread(starter).Start();
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!File.Exists(variables.filename1) && Path.GetExtension(variables.filename1) != ".hex")
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "(*.hex)|*.hex|All files (*.*)|*.*";
                openFileDialog1.Title = "Select a File";
                //openFileDialog1.InitialDirectory = variables.currentdir;
                openFileDialog1.RestoreDirectory = false;
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    variables.filename1 = openFileDialog1.FileName;
                }
                else return;
                if (variables.filename1 != null) this.txtFilePath1.Text = variables.filename1;
            }
            variables.currentdir = variables.filename1;
            ThreadStart starter = delegate { HID.program(ref this.progressBar); };
            Thread start = new Thread(starter);
            start.IsBackground = true;
            start.Start();
        }

        private void sMCConfigViewerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Forms.SMCConfig frm = new Forms.SMCConfig();
            frm.Show();
        }

        private void toolStripHexEditor_Click(object sender, EventArgs e)
        {
            HexEdit.HexViewer hv = new HexEdit.HexViewer(txtFilePath1.Text);
            hv.Show();
        }

        #endregion

        #region Advanced

        private void checkSecdataToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(variables.filename1))
            {
                MessageBox.Show("A nand must be loaded as source");
                return;
            }
            if (nand == null || !nand.ok) return;
            if (String.IsNullOrWhiteSpace(txtCPUKey.Text))
            {
                MessageBox.Show("A Matching CPU Key must be entered");
                return;
            }

            byte[] secdata = nand.exctractFSfile("secdata.bin");
            Nand.Nand.DecryptSecData(secdata, Oper.StringToByteArray(txtCPUKey.Text));
        }

        private void getLatestSystemUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ThreadStart starter = delegate { downloadLatestUpdate(); };
            new Thread(starter).Start();
        }

        private void RaterLaunchtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            POST ps = new POST();
            ps.Show();
        }

        private void writeDGXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Have you made a dump first?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.No) return;
            load_dgx();
            Console.WriteLine("Manually selected to write >14719 ECC");
            getconsoletype(3);
        }

        private void CustomXeBuildMenuItem_Click(object sender, EventArgs e)
        {
            Forms.CustomXebuild CX = new Forms.CustomXebuild();
            CX.ShowDialog();
            if (CX.DialogResult == DialogResult.Cancel) return;
            else if (CX.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                Classes.xebuild xe = new Classes.xebuild();
                xe.xeExit += xPanel.xe_xeExit;
                ThreadStart starter = delegate { xe.build(CX.getString()); };
                Thread thr = new Thread(starter);
                thr.IsBackground = true;
                thr.Start();
            }
        }

        private void corona4GBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            calldrives();
        }

        private void customNandProCommandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nandcustom();
        }

        private void writeFusionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ThreadStart starter = delegate { writefusion(); };
            new Thread(starter).Start();
        }

        private void patchNandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            variables.cpkey = txtCPUKey.Text;
            patch patchform = new patch();
            patchform.frm1 = this;
            patchform.Show();
            //long size = 0;
            //Nand.savefile(Nand.encrypt_CB_cpukey(Nand.openfile(filename1, ref size, 0), Nand.openfile(filename2, ref size, 0), Nand.StringToByteArray(cpukeytext.Text)), "test.bin");
        }

        private void changeLDVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.xeBuildOptions xbo = new Forms.xeBuildOptions();
            xbo.disableAdv();
            xbo.ShowDialog();
        }

        private void createAnImageWithoutNanddumpbinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nand = new Nand.PrivateN();
            nand._cpukey = txtCPUKey.Text;
            string kvfile = Path.Combine(variables.pathforit, @"xebuild\data\kv.bin");
            if (File.Exists(kvfile))
            {
                nand._rawkv = File.ReadAllBytes(kvfile);
                nand.updatekvval();

            }
            ThreadStart starter = delegate { xPanel.createxebuild_v2(true, nand); };
            new Thread(starter).Start();
        }

        #endregion

        #region Dev

        private void xValToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.XValue x = new Forms.XValue();
            x.Show();
        }

        private void checkSecdataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(variables.filename1))
            {
                MessageBox.Show("A nand must be loaded as source");
                return;
            }
            if (nand == null || !nand.ok) return;
            if (String.IsNullOrWhiteSpace(txtCPUKey.Text))
            {
                MessageBox.Show("A Matching CPU Key must be entered");
                return;
            }

            byte[] secdata = nand.exctractFSfile("secdata.bin");
            Nand.Nand.DecryptSecData(secdata, Oper.StringToByteArray(txtCPUKey.Text));
        }

        private void pirsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pirs.STFS p = new Pirs.STFS(txtFilePath1.Text, txtFilePath2.Text);
            p.Show();
        }

        private void debugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (variables.debugme)
            {
                Console.WriteLine("Debug Mode off");
                variables.debugme = false;
            }
            else
            {
                Console.WriteLine("Debug Mode on");
                variables.debugme = true;
            }
        }

        private void shutdownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nandx.PowerDown();
        }

        private void bootloaderModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nandx.Update();
        }

        private void powerOnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nandx.PowerUp();
        }

        #endregion

        #region Menu Buttons

        private void changelogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("wordpad.exe", "changelog.nfo");
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            JRunner.Forms.Settings mForm = new JRunner.Forms.Settings();
            mForm.ShowDialog();
        }

        #endregion

        #endregion

        #region Buttons

        #region Basic Buttons

        void btnExit_Click(object sender, System.EventArgs e)
        {
            Application.ExitThread();
            Application.Exit();
            Environment.Exit(0);
        }

        void btnReadClick()
        {
            //getmbtype();
            //if (textBox2.Text != "008A3020" && textBox2.Text != "00AA3020") ctypeselected = 0;
            if (variables.modder)
            {
                JRunner.Forms.custform CFrom = new JRunner.Forms.custform();

                var diaresult = CFrom.ShowDialog();
            }
            getconsoletype(1);
            //ThreadStart readna = new ThreadStart(readnand);
            //Thread readnt = new Thread(readna);
            //readnt.IsBackground = true;
            //readnt.Start();
            //_waitmb.Set();
        }

        void btnCreateECCClick()
        {
            if (String.IsNullOrWhiteSpace(variables.filename1))
            {
                loadfile(ref variables.filename1, ref this.txtFilePath1, true);
                if (String.IsNullOrWhiteSpace(variables.filename1))
                {
                    MessageBox.Show("No File Selected");
                    return;
                }
            }

            if (nTools.getbtnCreateECC() == "Create ECC")
            {
                Thread thr = new Thread(createecc_v2);
                thr.IsBackground = true;
                thr.Start();
            }
            else if (nTools.getbtnCreateECC() == "Create ECC*")
            {
                if (nand == null || !nand.ok) return;
                byte[] kv = new byte[0x4200];
                if (nand.noecc) kv = nand._rawkv;
                else
                {
                    FileStream infile = new FileStream(nand._filename, FileMode.Open, FileAccess.Read);
                    BinaryReader file = new BinaryReader(infile);
                    file.BaseStream.Seek(0x4200, SeekOrigin.Begin);
                    file.Read(kv, 0, 0x4200);
                    infile.Close();
                }
                if (String.IsNullOrWhiteSpace(load_dgx())) return;
                File.Copy(variables.filename1, Path.Combine(variables.outfolder, "image_00000000.ecc"), true);
                variables.filename1 = Path.Combine(variables.outfolder, "image_00000000.ecc");
                txtFilePath1.Text = variables.filename1;
                Nand.Nand.injectRawKV(variables.filename1, kv);
                Console.WriteLine("ECC created");
            }
            else createxell();
        }

        void btnProgramCRClick()
        {
            pnlInfo.Controls.Clear();
            pnlInfo.Controls.Add(xsvfInfo);
            if (listInfo.Contains(xsvfInfo)) listInfo.Remove(xsvfInfo);
            listInfo.Add(xsvfInfo);
            pnlTools.Enabled = false;
        }

        void btnWriteECCClick()
        {
            if (nTools.getbtnWriteECC().Contains("Xell"))
            {
                if (variables.debugme) Console.WriteLine("xell");
                ThreadStart starter = delegate { writexell(); };
                new Thread(starter).Start();
            }
            else
            {
                getconsoletype(3);
            }
        }

        void btnWriteClick()
        {
            if ((ModifierKeys & Keys.Control) == Keys.Control)
            {
                getconsoletype(2, 0x50);
            }
            else
            {
                getconsoletype(2);
            }
        }

        private void btnXeBuildClick()
        {
            ThreadStart starter = delegate { xPanel.createxebuild_v2(false, nand); };
            new Thread(starter).Start();
        }

        #endregion

        #region File Buttons

        void btnLoadFile1_Click(object sender, System.EventArgs e)
        {
            loadfile(ref variables.filename1, ref this.txtFilePath1, true);
            nand_init();
        }

        void btnLoadFile2_Click(object sender, System.EventArgs e)
        {
            loadfile(ref variables.filename2, ref this.txtFilePath2);
            if (variables.debugme) Console.WriteLine("filename2/currentdir = {0}", variables.filename2);
#if Dev
            if (variables.current_mode == variables.JR_MODE.MODEFW)
            {
                fwInfo.setTarget(jf.fwInit(Firmware.JF.fwFile.TARGET));
            }
#endif
        }

        void comparebutton_Click(object sender, System.EventArgs e)
        {
            if (comparebutton.Text.Contains("Nand Mode"))
            {
#if Dev
                NandMode();
#endif
            }
            else new Thread(comparenands).Start();
        }

        #endregion

        #region Function Buttons

        private void btnWorkingFolder_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(nand.ki.serial))
            {
                if (variables.xefolder != null && variables.xefolder != "")
                {
                    if (Directory.Exists(variables.xefolder)) Process.Start(variables.xefolder);
                }
                else if (Directory.Exists(Path.Combine(Directory.GetParent(variables.outfolder).FullName, nand.ki.serial)))
                {
                    Process.Start(Path.Combine(Directory.GetParent(variables.outfolder).FullName, nand.ki.serial));
                }
                else Process.Start(variables.outfolder);
            }
            else
            {
                Process.Start(variables.outfolder);
            }

        }

        private void btnCPUDBClick()
        {
            callcpukeydb();
        }

        private void btnInit_Click(object sender, EventArgs e)
        {
            nand_init();
        }

        #endregion

        #region IP stuff

        private void btnIPGetCPU_Click(object sender, EventArgs e)
        {
            ThreadStart starter = delegate { myIP.IP_GetCpuKey(txtIP.Text); };
            new Thread(starter).Start();
            if (variables.debugme) Console.WriteLine("-----{0}--------", variables.cpkey);
            new Thread(updatecptextbox).Start();
        }

        private void btnScanner_Click(object sender, EventArgs e)
        {
            ThreadStart starter = delegate { myIP.IPScanner(ref this.progressBar); };
            new Thread(starter).Start();
            if (variables.debugme) Console.WriteLine("-----{0}--------", variables.cpkey);
            new Thread(updatecptextbox).Start();
        }

        private void btnCOM_Click(object sender, EventArgs e)
        {
            Forms.Comport cm = new Forms.Comport();
            cm.Show();
        }

        #endregion

        #endregion

        #region Keyboard Events

        void txtCPUKey_TextChanged(object sender, System.EventArgs e)
        {
            if (variables.current_mode == variables.JR_MODE.MODEFW) return;
            if (txtCPUKey.Text.Length == 32)
            {
                if (objAlphaPattern.IsMatch(txtCPUKey.Text))
                {
                    if (Nand.Nand.VerifyKey(Oper.StringToByteArray(txtCPUKey.Text)))
                    {
                        variables.cpkey = txtCPUKey.Text;
                        if (!variables.gotvalues && File.Exists(variables.filename1))
                        {
                            nand_init();
                        }
                    }
                    else Console.WriteLine("Bad CPU Key");
                }
            }
        }

        private void txtIP_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ThreadStart starter = delegate { myIP.IP_GetCpuKey(txtIP.Text); };
                new Thread(starter).Start();
                if (variables.debugme) Console.WriteLine("-----{0}--------", variables.cpkey);
                new Thread(updatecptextbox).Start();
            }
        }

        #endregion

        #region Clicks

        private void txtConsole_DoubleClick(object sender, EventArgs e)
        {
            File.AppendAllText(Path.Combine(variables.pathforit, "tempLog.txt"), txtConsole.Text);
            System.Diagnostics.Process.Start(Path.Combine(variables.pathforit, "tempLog.txt"));
            Thread.Sleep(1000);
            File.Delete(Path.Combine(variables.pathforit, "tempLog.txt"));
        }

        #endregion

        #region Drag & Drops

        void txtFilePath1_DragDrop(object sender, DragEventArgs e)
        {
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            this.txtFilePath1.Text = s[0];
            variables.filename1 = s[0];
            if (variables.current_mode != variables.JR_MODE.MODEFW) erasevariables();
            if (Path.GetExtension(s[0]) == ".bin")
            {
                nand_init();
            }

        }
        void txtFilePath1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }
        void txtFilePath2_DragDrop(object sender, DragEventArgs e)
        {
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            this.txtFilePath2.Text = s[0];
            variables.filename2 = s[0];
#if Dev
            if (variables.current_mode == variables.JR_MODE.MODEFW)
            {
                fwInfo.setTarget(jf.fwInit(Firmware.JF.fwFile.TARGET));
            }
#endif
        }
        void txtFilePath2_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }
        void txtCPUKey_DragDrop(object sender, DragEventArgs e)
        {
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if (File.Exists(s[0]))
            {
                FileInfo f = new FileInfo(s[0]);
                if (f.Length == 16) txtCPUKey.Text = Oper.ByteArrayToString(File.ReadAllBytes(s[0]));
            }
            if (Path.GetExtension(s[0]) == ".txt")
            {
                Regex objAlphaPattern = new Regex("[a-fA-F0-9]{32}$");
                string[] cpu = File.ReadAllLines(s[0]);
                string cpukey = "";
                bool check = false;
                int i = 0;
                foreach (string line in cpu)
                {
                    if (objAlphaPattern.Match(line).Success) i++;
                    if (i > 1) check = true;
                }
                foreach (string line in cpu)
                {
                    if (check)
                    {
                        if (line.ToUpper().Contains("CPU"))
                        {
                            cpukey = (objAlphaPattern.Match(line).Value);
                        }
                    }
                    else
                    {
                        cpukey = (objAlphaPattern.Match(line).Value);
                        break;
                    }
                    //Console.WriteLine(objAlphaPattern.Match(line).Value);
                }
                txtCPUKey.Text = cpukey;
            }

        }
        void txtCPUKey_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }

        #endregion

        #region KeyUp

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                variables.escapeloop = true;
                ThreadStart starter = delegate { escapedloop(); };
                new Thread(starter).Start();
            }
            if (e.KeyCode == Keys.F1)
            {
                Application.Restart();
            }
            else if (e.KeyCode == Keys.F2 && e.Control)
            {
                if (variables.debugme)
                {
                    Console.WriteLine("Debug Mode off");
                    variables.debugme = false;
                }
                else
                {
                    Console.WriteLine("Debug Mode on");
                    variables.debugme = true;
                }
            }
            else if (e.Control && e.KeyCode == Keys.F3)
            {
                if (!variables.extractfiles) { variables.extractfiles = true; Console.WriteLine("Extract Files On"); }
                else { variables.extractfiles = false; Console.WriteLine("Extract Files Off"); }
            }
            else if (e.Control && e.KeyCode == Keys.S)
            {
                ThreadStart starter = delegate { demon.Read_Serial_Port(); };
                new Thread(starter).Start();
            }
            else if (e.Control && e.KeyCode == Keys.F8)
            {
                if (variables.filename1 == null || variables.filename2 == null) return;
                long size1 = 0;
                long size2 = 0;
                byte[] file1 = Oper.openfile(variables.filename1, ref size1, 0);
                byte[] file2 = Oper.openfile(variables.filename2, ref size2, 0);
                Oper.savefile(Oper.concatByteArrays(file1, Oper.returnportion(file2, file1.Length, file2.Length - file1.Length), file1.Length, file2.Length - file1.Length), "stocknand.bin");
            }
            else if (e.KeyCode == Keys.F2)
            {
                getmbtype();
            }
            else if (e.KeyCode == Keys.F3)
            {
                //if (variables.filename1 != null) Nand.BadBlock.find_bad_blocks(variables.filename1, false);
            }
            else if (e.KeyCode == Keys.F4)
            {
            }
            else if (e.KeyCode == Keys.F5)
            {
            }
            else if (e.KeyCode == Keys.F6)
            {
            }
            else if (e.KeyCode == Keys.F7)
            {
            }
            else if (e.KeyCode == Keys.F8)
            {
            }
            else if (e.KeyCode == Keys.F9)
            {
                new Thread(trykeys).Start();
            }
            else if (e.KeyCode == Keys.F10)
            {
            }
            else if (e.KeyCode == Keys.F11)
            {
                //new Thread(test2).Start();
            }
            else if (e.KeyCode == Keys.F12)
            {
            }
        }

        void escapedloop()
        {
            Thread.Sleep(2000);
            variables.escapeloop = false;
        }

        #endregion

        #endregion

        #region Demon
        bool showingdemon = false;
        protected override void WndProc(ref Message m)
        {
            try
            {
                // The OnDeviceChange routine processes WM_DEVICECHANGE messages.
                if (m.Msg == DeviceManagement.WM_DEVICECHANGE)
                {
                    if ((m.WParam.ToInt32() == DeviceManagement.DBT_DEVICEREMOVECOMPLETE))
                    {
                        //Console.WriteLine(DemoN.BootloaderPathName);
                        if (DemoN.DemonManagement.DeviceNameMatch(m, DemoN.DemonPathName))
                        {
                            DemoN.DemonDetected = false;
                            showDemon(false);
                        }
                    }
                    else if (m.WParam.ToInt32() == DeviceManagement.DBT_DEVICEARRIVAL)
                    {
                        if (!HID.BootloaderDetected) HID.FindBootloader();
                    }
                    else
                    {
                        if (!HID.BootloaderDetected) HID.FindBootloader();
                        if (!showingdemon) showDemon(DemoN.FindDemon());
                    }
                }
                // Let the base form process the message.

                base.WndProc(ref m);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        private void showDemon(bool show)
        {
            if (variables.debugme) Console.WriteLine("ShowDemoN {0}", show);
            showingdemon = true;
            if (show)
            {
                Thread.Sleep(100);
                demon.get_mode();

                if (DemoN.mode == DemoN.Demon_Modes.FIRMWARE)
                {
                    demon.get_firmware();
                    demon.get_external_flash(false);
                }

                nTools.setImage(global::JRunner.Properties.Resources.demon);
                demoNToolStripMenuItem.Visible = true;
                ModeStatus.Visible = true;
                ModeVersion.Visible = true;
                FWStatus.Visible = true;
                FWVersion.Visible = true;
            }
            else
            {
                nTools.setImage(null);
                if (device == 1) nTools.setImage(global::JRunner.Properties.Resources.JRP);
                else if (device == 2) nTools.setImage(global::JRunner.Properties.Resources.NANDX);
                //Console.WriteLine(device);
                ModeStatus.Visible = false;
                ModeVersion.Visible = false;
                demoNToolStripMenuItem.Visible = false;
                FWStatus.Visible = false;
                FWVersion.Visible = false;
                FlashStatus.Visible = false;
                FlashVersion.Visible = false;
            }
            showingdemon = false;
        }
        private void onDevNotify(object sender, DeviceNotifyEventArgs e)
        {
            try
            {
                if (variables.debugme) Console.WriteLine("DevNotify - {0}", e.mDevice.Name);
                if (variables.debugme) Console.WriteLine("EventType - {0}", e.EventType);
                if (e.EventType == EventType.DeviceArrival)
                {
                    if (e.mDevice.IdVendor == 0x11d4 && e.mDevice.IdProduct == 0x8338)
                    {
                        if (!DemoN.DemonDetected)
                        {
                            nTools.setImage(global::JRunner.Properties.Resources.JRP);
                        }
                        jRPToolStripMenuItem.Visible = true;
                        device = 1;
                    }
                    else if (e.mDevice.IdVendor == 0xFFFF && e.mDevice.IdProduct == 0x004)
                    {
                        if (!DemoN.DemonDetected)
                        {
                            nTools.setImage(global::JRunner.Properties.Resources.NANDX);
                        }
                        device = 2;
                    }
                    else if (e.mDevice.IdVendor == 0x11d4 && e.mDevice.IdProduct == 0x8333)
                    {
                        x360USBDetected = true;
                        //if (jf != null) jf.X360USBDetected(true);
                    }
                    else if (e.mDevice.IdVendor == 0x11d4 && e.mDevice.IdProduct == 0x8339)
                    {
                        //if (jf != null) jf.CK3iDetected(true);
                    }
                    //Console.WriteLine(e.mDevice.Name);
                }
                else if (e.EventType == EventType.DeviceRemoveComplete)
                {
                    //Console.WriteLine(DemoN.BootloaderPathName);
                    if (e.mDevice.IdVendor == 0x11d4 && e.mDevice.IdProduct == 0x8334)
                    {
                        HID.BootloaderDetected = false;
                        Console.WriteLine("Bootloader Removed.");
                    }
                    else if (e.mDevice.IdVendor == 0xFFFF && e.mDevice.IdProduct == 0x004)
                    {
                        if (!DemoN.DemonDetected) nTools.setImage(null);
                        device = 0;
                    }
                    else if (e.mDevice.IdVendor == 0x11d4 && e.mDevice.IdProduct == 0x8338)
                    {
                        if (!DemoN.DemonDetected) nTools.setImage(null);
                        jRPToolStripMenuItem.Visible = false;
                        device = 0;
                    }
                    else if (e.mDevice.IdVendor == 0x11d4 && e.mDevice.IdProduct == 0x8333)
                    {
                        x360USBDetected = false;
                        //if (jf != null) jf.X360USBDetected(false);
                    }
                    else if (e.mDevice.IdVendor == 0x11d4 && e.mDevice.IdProduct == 0x8339)
                    {
                        //if (jf != null) jf.CK3iDetected(false);
                    }
                }
            }
            catch (Exception ex) { if (variables.debugme) Console.WriteLine(ex.ToString()); }
        }

        private void toggleNANDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            demon.toggle();
        }
        private void powerOnToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            demon.Power_On();
        }
        private void powerOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            demon.Power_Off();
        }
        private void updateFwToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!File.Exists(variables.filename1) && Path.GetExtension(variables.filename1) != ".bin")
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "(*.bin)|*.bin|All files (*.*)|*.*";
                openFileDialog1.Title = "Select a File";
                openFileDialog1.RestoreDirectory = false;
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    variables.filename1 = openFileDialog1.FileName;
                }
                else return;
                if (variables.filename1 != null) this.txtFilePath1.Text = variables.filename1;
            }
            ThreadStart starter = delegate { demon.Update_DemoN(variables.filename1); };
            Thread start = new Thread(starter);
            start.Start();
        }
        private void getInvalidBlocksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                List<int> invalidblocks = new List<int>();
                demon.get_Invalid_Blocks(ref invalidblocks);
            }
            catch (Exception ex) { if (variables.debugme) Console.WriteLine(ex.ToString()); }
        }
        #endregion

        private string GetIP()
        {
            string host = Dns.GetHostName();
            IPAddress[] localIPs = Dns.GetHostAddresses(host);
            string localIP = "?";
            int i = 0, t = 0;
            foreach (IPAddress iptest in localIPs)
            {
                if (Dns.GetHostEntry(host).AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                {
                    if (t == 0)
                    {
                        localIP = Dns.GetHostEntry(host).AddressList[i].ToString();
                    }
                    t++;
                }
                i++;
            }
            return localIP;
        }

        private void downloadLatestUpdate()
        {
            try
            {
                string file = "";
                file = download("http://www.xbox.com/system-update-usb", "");
                if (File.Exists(file))
                {
                    using (Ionic.Zip.ZipFile zip = Ionic.Zip.ZipFile.Read(file))
                    {
                        string dash = file.Split('_')[1];
                        if (Directory.Exists(Path.Combine(variables.pathforit, "xeBuild", dash)))
                        {
                            Console.WriteLine("Extracting to {0}", Path.Combine(variables.pathforit, "xeBuild", dash));
                            zip.ExtractAll(Path.Combine(variables.pathforit, "xeBuild", dash), Ionic.Zip.ExtractExistingFileAction.OverwriteSilently);
                            Console.WriteLine("Done");
                        }
                    }
                }
            }
            catch (Exception)
            {
                Process.Start("http://www.xbox.com/system-update-usb");
            }
        }

        public string download(string link, string disk)
        {
            String filename = "";
            Uri url = new Uri(link);
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
            System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
            response.Close();
            filename = Path.GetFileName(response.ResponseUri.LocalPath);
            Console.WriteLine("Downloading {0}", filename);
            Int64 iSize = response.ContentLength;
            Int64 iRunningByteTotal = 0;
            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                using (System.IO.Stream streamRemote = client.OpenRead(new Uri(link)))
                {
                    using (Stream streamLocal = new FileStream(Path.Combine(disk, filename), FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        int iByteSize = 0;
                        byte[] byteBuffer = new byte[iSize];
                        while ((iByteSize = streamRemote.Read(byteBuffer, 0, byteBuffer.Length)) > 0)
                        {
                            streamLocal.Write(byteBuffer, 0, iByteSize);
                            iRunningByteTotal += iByteSize;
                            double dIndex = (double)(iRunningByteTotal);
                            double dTotal = (double)byteBuffer.Length;
                            double dProgressPercentage = (dIndex / dTotal);
                            int iProgressPercentage = (int)(dProgressPercentage * 100);
                            updateBlocks(String.Format("{0}%", iProgressPercentage));
                            updateProgress(iProgressPercentage);
                        }
                        streamLocal.Close();
                    }
                    streamRemote.Close();
                }
            }
            Console.WriteLine("Done");
            return filename;
        }

        #region Settings & Dashes

        public void trykeys()
        {
            if (variables.filename1 == null || variables.filename1 == "") return;
            long size = 0;
            byte[] keyvault = Nand.Nand.getkv(Oper.openfile(variables.filename1, ref size, 0));
            int index = 0;
            RegistryKey cpukeydb = Registry.CurrentUser.CreateSubKey("CPUKey_DB", RegistryKeyPermissionCheck.ReadWriteSubTree);
            foreach (string valueName in cpukeydb.GetValueNames())
            {
                if (valueName == "Index")
                {
                    index = Convert.ToInt32(cpukeydb.GetValue(valueName));
                    Console.WriteLine("Checking against {0} keys. Might take some time.", index);
                    for (int i = 1; i <= index; i++)
                    {
                        try
                        {
                            RegistryKey cpukeys = cpukeydb.OpenSubKey(i.ToString(), true);
                            if (cpukeys.GetValue("Deleted") != null)
                            {
                                RegistryUtilities.RenameSubKey(cpukeydb, index.ToString(), i.ToString());
                                index = index - 1;
                                cpukeys.SetValue("Index", (object)i); ;
                                cpukeydb.SetValue("Index", (object)index);
                                cpukeys.DeleteValue("Deleted");
                                //continue;
                            }
                            if (Nand.Nand.cpukeyverification(keyvault, cpukeys.GetValue("cpukey").ToString()))
                            {
                                variables.cpkey = cpukeys.GetValue("cpukey").ToString();
                                txtCPUKey.Text = cpukeys.GetValue("cpukey").ToString();
                                Console.WriteLine("Key found");
                                return;
                            }
                        }
                        catch (SystemException ex)
                        {
                            if (variables.debugme) Console.WriteLine(ex.ToString());
                            continue;
                        }
                        catch (Exception ex)
                        {
                            if (variables.debugme) Console.WriteLine(ex.ToString());
                            continue;
                        }
                    }
                    Console.WriteLine("Key wasnt found in Database");
                }
            }

        }

        void savesettings()
        {
            xml x = new xml();
            try
            {
                x.create(variables.settingsfile);
                x.start();
                foreach (string name in variables.settings)
                {
                    switch (name)
                    {
                        case "xebuild":
                            x.write(name, variables.xebuild);
                            break;
                        case "FileChecks":
                            x.write(name, variables.checkfiles.ToString());
                            break;
                        case "location":
                            x.write(name, variables.location.ToString());
                            break;
                        case "COMPort":
                            x.write(name, variables.COMPort);
                            break;
                        case "Errorsound":
                            x.write(name, variables.sounderror);
                            break;
                        case "Comparesound":
                            x.write(name, variables.soundcompare);
                            break;
                        case "Successsound":
                            x.write(name, variables.soundsuccess);
                            break;
                        case "Delay":
                            x.write(name, variables.delay.ToString());
                            break;
                        case "DashLaunchE":
                            x.write(name, variables.DashLaunchE.ToString());
                            break;
                        case "IP":
                            x.write(name, variables.ip);
                            break;
                        case "NoReads":
                            x.write(name, variables.NoReads.ToString());
                            break;
                        case "IPStart":
                            x.write(name, variables.IPstart);
                            break;
                        case "IPEnd":
                            x.write(name, variables.IPend);
                            break;
                        case "XebuildName":
                            x.write(name, variables.nandflash);
                            break;
                        case "dashlaunch":
                            x.write(name, variables.dashlaunch);
                            break;
                        case "preferredDash":
                            x.write(name, variables.preferredDash);
                            break;
                        case "KeepFiles":
                            x.write(name, variables.deletefiles.ToString());
                            break;
                        case "WorkingDir":
                            x.write(name, variables.outfolder);
                            break;
                        case "LPTport":
                            x.write(name, variables.LPTport);
                            break;
                        case "AutoExtract":
                            x.write(name, variables.autoExtract.ToString());
                            break;
                        case "AllMove":
                            x.write(name, variables.allmove.ToString());
                            break;
                        case "Modder":
                            x.write(name, variables.modder.ToString());
                            break;
                        default:
                            break;
                    }
                }
            }
            finally
            {
                x.end();
                x.close();
            }
        }
        void loadsettings()
        {
            if (File.Exists(variables.settingsfile))
            {
                xml x = new xml();
                x.load(File.ReadAllText(variables.settingsfile));
                foreach (string name in variables.settings)
                {
                    string val = x.readsetting(name);
                    bool bvalue;
                    switch (name)
                    {
                        case "xebuild":
                            string xmd5 = Oper.GetMD5HashFromFile(variables.update_path + "xeBuild.exe").ToUpper();

                            if (variables.xebuilds.ContainsKey(xmd5.ToUpper()))
                            {
                                if (variables.debugme) Console.WriteLine("Known xebuild md5 found");
                                xebuildVersion.Text = variables.xebuilds[xmd5.ToUpper()];
                                variables.xebuild = variables.xebuilds[xmd5.ToUpper()];
                            }
                            else
                            {
                                xebuildVersion.Text = val;
                                variables.xebuild = val;
                            }
                            break;
                        case "FileChecks":
                            bvalue = true;
                            if (!bool.TryParse(val, out bvalue)) bvalue = true;
                            variables.checkfiles = bvalue;
                            break;
                        case "location":
                            int xy = 0;
                            int y = 0;
                            if (!String.IsNullOrWhiteSpace(val))
                            {
                                var g = Regex.Replace(val, @"[\{\}a-zA-Z=]", "").Split(',');
                                int.TryParse(g[0], out xy);
                                int.TryParse(g[1], out y);
                            }
                            variables.location = new Point(xy, y);
                            break;
                        case "COMPort":
                            variables.COMPort = val;
                            break;
                        case "Errorsound":
                            variables.sounderror = val;
                            break;
                        case "Comparesound":
                            variables.soundcompare = val;
                            break;
                        case "Successsound":
                            variables.soundsuccess = val;
                            break;
                        case "Delay":
                            int ivalue = 0;
                            int.TryParse(val, out ivalue);
                            variables.delay = ivalue;
                            break;
                        case "DashLaunchE":
                            bvalue = false;
                            bool.TryParse(val, out bvalue);
                            variables.DashLaunchE = bvalue;
                            xPanel.setDLPatches(bvalue);
                            break;
                        case "IP":
                            if (!string.IsNullOrWhiteSpace(val))
                            {
                                variables.ip = val;
                                txtIP.Text = val;
                            }
                            else
                            {
                                string localIP = GetIP();
                                txtIP.Text = (localIP.Remove(localIP.LastIndexOf('.')));
                            }
                            break;
                        case "NoReads":
                            decimal dvalue = 4;
                            decimal.TryParse(val, out dvalue);
                            if (dvalue == 0) dvalue = 4;
                            nTools.setNumericIterations(dvalue);
                            variables.NoReads = dvalue;
                            break;
                        case "IPStart":
                            variables.IPstart = val;
                            break;
                        case "IPEnd":
                            variables.IPend = val;
                            break;
                        case "XebuildName":
                            if (!string.IsNullOrWhiteSpace(val)) variables.nandflash = val;
                            break;
                        case "dashlaunch":
                            string dlmd5 = Oper.GetMD5HashFromFile(variables.update_path + "launch.xex").ToUpper();

                            if (variables.dls.ContainsKey(dlmd5.ToUpper()))
                            {
                                if (variables.debugme) Console.WriteLine("Known dl md5 found");
                                DashLaunchVersion.Text = variables.dls[dlmd5.ToUpper()];
                                variables.dashlaunch = variables.dls[dlmd5.ToUpper()];
                            }
                            else
                            {
                                DashLaunchVersion.Text = val;
                                variables.dashlaunch = val;
                            }
                            break;
                        case "preferredDash":
                            variables.preferredDash = val;
                            break;
                        case "KeepFiles":
                            bvalue = false;
                            bool.TryParse(val, out bvalue);
                            variables.deletefiles = bvalue;
                            break;
                        case "WorkingDir":
                            if (!string.IsNullOrWhiteSpace(val))
                            {
                                variables.outfolder = val;
                                if (!Directory.Exists(variables.outfolder))
                                {
                                    try
                                    {
                                        Directory.CreateDirectory(variables.outfolder);
                                    }
                                    catch (System.IO.DirectoryNotFoundException)
                                    {
                                        variables.outfolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                                        Directory.CreateDirectory(variables.outfolder);
                                    }
                                }
                            }
                            break;
                        case "LPTport":
                            nTools.setLptPort(val);
                            variables.LPTport = val;
                            break;
                        case "AutoExtract":
                            bvalue = true;
                            if (!bool.TryParse(val, out bvalue)) bvalue = true;
                            variables.autoExtract = bvalue;
                            break;
                        case "AllMove":
                            bvalue = true;
                            if (!bool.TryParse(val, out bvalue)) bvalue = true;
                            variables.allmove = bvalue;
                            break;
                        case "Modder":
                            bvalue = true;
                            if (!bool.TryParse(val, out bvalue)) bvalue = true;
                            variables.modder = bvalue;
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        void settings()
        {
            loadsettings();
        }

        void add_dash()
        {
            addDash newdash = new addDash();
            newdash.ShowDialog();
            //check_dash();
            ThreadStart starte = delegate { check_dashes(true); };
            Thread th = new Thread(starte);
            th.IsBackground = true;
            th.Start();
            //check_dash();
        }
        void del_dash()
        {
            Dashes.delDash deldash = new Dashes.delDash();
            deldash.ShowDialog();
            check_dash();
        }

        void check_dash()
        {
            DataTable dashtable = xPanel.getDataSet().DataTable2;
            int counter = 0;
            dashtable.Rows.Clear();
            Thread.Sleep(10);
            if (Directory.Exists(Path.Combine(variables.currentdir, "xeBuild")))
            {
                try
                {
                    variables.dashes_all = new List<string>();
                    Regex regex = new Regex("^[0-9]+$");

                    foreach (string a in Directory.GetDirectories(Path.Combine(variables.currentdir, "xeBuild")))
                    {
                        if (regex.IsMatch(Path.GetFileNameWithoutExtension(a))) variables.dashes_all.Add(Path.GetFileNameWithoutExtension(a));
                    }
                    variables.dashes_all.Sort();
                    if (variables.debugme) Console.WriteLine("Checking dashes");
                    foreach (string valueName in variables.dashes_all)
                    {
                        if (variables.debugme) Console.WriteLine(valueName);
                        DataRow dashcombo = dashtable.NewRow();
                        dashcombo[0] = counter;
                        dashcombo[1] = valueName;
                        dashtable.Rows.Add(dashcombo);
                        counter++;
                    }
                }
                catch (NullReferenceException) { }
            }
            Thread.Sleep(10);
            DataRow dashrows = dashtable.NewRow();
            dashrows[0] = counter;
            dashrows[1] = "-------------";
            dashtable.Rows.Add(dashrows);
            counter++;
            DataRow dashrows1 = dashtable.NewRow();
            dashrows1[0] = counter;
            dashrows1[1] = "Add Dash";
            dashtable.Rows.Add(dashrows1);
            counter++;
            DataRow dashrows2 = dashtable.NewRow();
            dashrows2[0] = counter;
            dashrows2[1] = "Del Dash";
            dashtable.Rows.Add(dashrows2);
            counter++;
            Thread.Sleep(10);
            try
            {
                if (xPanel.getComboDash().Items.Count == 4)
                {
                    xPanel.getComboDash().SelectedIndex = 1;
                    xPanel.getComboDash().SelectedIndex = 0;
                    int n = 0;
                    bool isNumeric = int.TryParse(xPanel.getComboDash().Text, out n);
                    if (isNumeric) variables.dashversion = n;
                }
                else
                {
                    if (variables.dashes_all.Contains(variables.preferredDash))
                    {
                        if (xPanel.getComboDash().Items.Count >= variables.dashes_all.IndexOf(variables.preferredDash)) xPanel.getComboDash().SelectedIndex = variables.dashes_all.IndexOf(variables.preferredDash);
                        int n = 0;
                        bool isNumeric = int.TryParse(xPanel.getComboDash().Text, out n);
                        if (isNumeric) variables.dashversion = n;
                    }
                    else if (xPanel.getComboDash().Items.Count > 3) xPanel.BeginInvoke((Action)(() => xPanel.getComboDash().SelectedIndex = (xPanel.getComboDash().Items.Count - 3)));
                }
            }
            catch (InvalidOperationException)
            {
            }
            Thread.Sleep(100);
        }

        void check_dashes(bool check = false)
        {
            variables.dashes_all.Sort();
            if (check) check_dash();
        }

        #endregion

        private void toolStripMenuItemVNand_Click(object sender, EventArgs e)
        {
            Nand.VNandForm f = new Nand.VNandForm();
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                vnand = new Nand.VNand(f.filename, f.console, f.flashconfig, f.BadBlocks);
                vnand.create();
                usingVNand = true;
            }
        }

        private void hDDToolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.XB1HDD x = new Forms.XB1HDD();
            x.Show();
        }

        private void cBFuseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Nand.CB_Fuse cb = new Nand.CB_Fuse();
            cb.ShowDialog();
        }



    }
}


/*
Trinity (00023010)
Xenon, Zephyr, Opus, Falcon (01198010)
Jasper (16MB 00023010)
Jasper (16MB 01198010 Small Block)
Jasper (256MB 008A3020)
Jasper (512MB 00AA3020)
Corona (16MB 00043000)
*/