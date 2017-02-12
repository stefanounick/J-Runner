using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Threading;

namespace JRunner.Forms
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
            this.AcceptButton = btnOK;
            this.CancelButton = btnCancel;
            btnOK.DialogResult = DialogResult.OK;
            btnCancel.DialogResult = DialogResult.Cancel;
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            if (variables.deletefiles) chkfiles.Checked = true;
            if (String.IsNullOrEmpty(variables.IPend) || String.IsNullOrEmpty(variables.IPstart)) IP.initaddresses();
            txtIPEnd.Text = variables.IPend;
            txtIPStart.Text = variables.IPstart;
            txtfolder.Text = variables.outfolder;
            txtIP.Text = variables.ip;
            txtsuccom.Text = variables.soundcompare;
            txtsuccess.Text = variables.soundsuccess;
            txterror.Text = variables.sounderror;
            numericUpDown1.Value = variables.delay;
            txtNandflash.Text = variables.nandflash;
            Filecheckbox.Checked = variables.checkfiles;
            AutoExtractcheckBox.Checked = variables.autoExtract;
            modderbut.Checked = variables.modder;
            almovebut.Checked = !variables.allmove;
            if (variables.soundcompare != "") chksuccom.Checked = true;
            if (variables.sounderror != "") chkerror.Checked = true;
            if (variables.soundsuccess != "") chksuccess.Checked = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "(*.wav)|*.wav";
            openFileDialog1.Title = "Select a File";
            //openFileDialog1.InitialDirectory = variables.currentdir;
            openFileDialog1.RestoreDirectory = false;
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtsuccom.Text = openFileDialog1.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "(*.wav)|*.wav";
            openFileDialog1.Title = "Select a File";
            //openFileDialog1.InitialDirectory = variables.currentdir;
            openFileDialog1.RestoreDirectory = false;
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtsuccess.Text = openFileDialog1.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "(*.wav)|*.wav";
            openFileDialog1.Title = "Select a File";
            //openFileDialog1.InitialDirectory = variables.currentdir;
            openFileDialog1.RestoreDirectory = false;
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txterror.Text = openFileDialog1.FileName;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.folderBrowserDialog1.ShowNewFolderButton = true;
            this.folderBrowserDialog1.RootFolder = Environment.SpecialFolder.Desktop;



            DialogResult outres = folderBrowserDialog1.ShowDialog();
            if (outres == DialogResult.OK)
            {
                if (!Directory.Exists(Path.Combine(folderBrowserDialog1.SelectedPath, "output")))
                {
                    Directory.CreateDirectory(Path.Combine(folderBrowserDialog1.SelectedPath, "output"));
                }

                txtfolder.Text = Path.Combine(folderBrowserDialog1.SelectedPath, "output");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (!chksuccess.Checked) txtsuccess.Text = "";
                if (!chksuccom.Checked) txtsuccom.Text = "";
                if (!chkerror.Checked) txterror.Text = "";
                variables.soundcompare = txtsuccom.Text;
                variables.sounderror = txterror.Text;
                variables.soundsuccess = txtsuccess.Text;
                variables.IPstart = txtIPStart.Text;
                variables.IPend = txtIPEnd.Text;
                variables.delay = (int)numericUpDown1.Value;
                if (variables.debugme) Console.WriteLine("outfolderchanged = true\noutfolder = {0}", variables.outfolder);
                variables.ip = txtIP.Text;
                variables.checkfiles = Filecheckbox.Checked;
                variables.nandflash = txtNandflash.Text;
                variables.autoExtract = AutoExtractcheckBox.Checked;
                variables.modder = modderbut.Checked;
                variables.allmove = !almovebut.Checked;
                if (chkfolder.Checked)
                {
                    if (txtfolder.Text == "")
                    {
                        variables.outfolder = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "output");
                    }
                    else
                    {
                        variables.outfolder = txtfolder.Text;
                    }
                }
            }
            catch (Exception ex) { if (variables.debugme) Console.WriteLine(ex.ToString()); }
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void almovebut_CheckedChanged(object sender, EventArgs e)
        {
            variables.allmove = !almovebut.Checked;
        }

        private void modderbut_CheckedChanged(object sender, EventArgs e)
        {
           variables.modder = modderbut.Checked  ;
        }

       

    


    }
}
