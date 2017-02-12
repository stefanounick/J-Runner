using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JRunner.Panels
{
    public partial class NandInfo : UserControl
    {
        private Nand.PrivateN nand;
        public delegate void DragDropC(string filename);
        public event DragDropC DragDropChanged;

        public NandInfo()
        {
            InitializeComponent();
        }

        public NandInfo(Nand.PrivateN Nand)
        {
            InitializeComponent();
            lblfcrt.Visible = false;
            label2blb.Visible = false;
            label2bla.Visible = false;
            textBox2BLb.Visible = false;
            setNand(Nand);
        }

        public void populateInfo()
        {
            if (nand.ok)
            {
                textBox2BLa.Text = nand.bl.CB_A.ToString();
                textBox2BLb.Text = nand.bl.CB_B.ToString();
                textBox4BL.Text = nand.bl.CD.ToString();
                textBox5BL.Text = nand.bl.CE.ToString();
                textBox6BL_p0.Text = nand.bl.CF_0.ToString();
                textBox6bl_p1.Text = nand.bl.CF_1.ToString();
                textBox7BL_p0.Text = nand.bl.CG_0.ToString();
                textBox7bl_p1.Text = nand.bl.CG_1.ToString();
                textBoxldv_0.Text = nand.uf.ldv_p0.ToString();
                textBoxldv_1.Text = nand.uf.ldv_p1.ToString();
                textBoxpd_0.Text = nand.uf.pd_0;
                textBoxpd_1.Text = nand.uf.pd_1;
                textBoxldv_cb.Text = "0";
                textBoxpd_cb.Text = "0x000000";

                if (nand.bl.CB_B != 0)
                {
                    textBox2BLb.Text = nand.bl.CB_B.ToString();
                    textBox2BLb.Visible = true;
                    label2blb.Visible = true;
                    label2bla.Visible = true;
                    label2bl.Visible = false;
                }
                else
                {
                    textBox2BLb.Visible = false;
                    label2blb.Visible = false;
                    label2bla.Visible = false;
                    label2bl.Visible = true;
                }

                string name = Nand.Nand.getConsoleName(nand, variables.flashconfig);
                consolebox.Text = name;

                if (!String.IsNullOrWhiteSpace(nand._cpukey))
                {
                    textBoxconsoleid.Text = nand.ki.consoleid;
                    txtdvdkey.Text = nand.ki.dvdkey;
                    txtosig.Text = nand.ki.osig;
                    txtSerial.Text = nand.ki.serial;

                    txtkvtype.Text = nand.ki.kvtype.Replace("0", " ");
                    txtregion.Text = "0x" + nand.ki.region + "   |   ";
                    if (nand.ki.region == "02FE") txtregion.Text += "PAL/EU";
                    else if (nand.ki.region == "00FF") txtregion.Text += "NTSC/US";
                    else if (nand.ki.region == "01FE") txtregion.Text += "NTSC/JAP";
                    else if (nand.ki.region == "01FF") txtregion.Text += "NTSC/JAP";
                    else if (nand.ki.region == "01FC") txtregion.Text += "NTSC/KOR";
                    else if (nand.ki.region == "0101") txtregion.Text += "NTSC/HK";
                    else if (nand.ki.region == "0201") txtregion.Text += "PAL/AUS";
                    else if (nand.ki.region == "7FFF") txtregion.Text += "DEVKIT";
                    lblfcrt.Visible = nand.ki.fcrtflag;
                }
                else
                {
                    textBoxconsoleid.Text = "";
                    txtdvdkey.Text = "";
                    txtosig.Text = "";
                    txtSerial.Text = "";
                    txtkvtype.Text = "";
                    txtregion.Text = "";
                    lblfcrt.Visible = false;
                }
                Console.WriteLine(name);
                nand.getbadblocks();
                if (nand.bad_blocks.Count != 0)
                {
                    string text = "";
                    int blocksize = nand.bigblock ? 0x21000 : 0x4200;
                    int reservestartpos = nand.bigblock ? 0x1E0 : 0x3E0;
                    foreach (int bblock in nand.bad_blocks)
                    {
                        text += ("• Bad Block ID @ 0x" + bblock.ToString("X") + " [Offset: 0x" + ((bblock) * blocksize).ToString("X") + "]");
                        text += Environment.NewLine;
                    }
                    if (nand.remapped_blocks.Count != 0)
                    {
                        text += Environment.NewLine;
                        text += Environment.NewLine;
                        int i = 0;
                        foreach (int bblock in nand.remapped_blocks)
                        {
                            if (bblock != -1)
                            {
                                text += ("• Bad Block ID @ 0x" + nand.bad_blocks[i].ToString("X") + " Found @ 0x" + (reservestartpos + bblock).ToString("X") + "[Offset: 0x" + (blocksize * (reservestartpos + bblock)).ToString("X") + "]");
                                text += Environment.NewLine;
                            }
                            i++;
                        }
                    }
                    else text += ("Remapped Blocks don't exist.");
                    //Console.WriteLine(text);
                    add_badblocks_tab(text);
                }
                else add_badblocks_tab("No Bad Blocks.");
            }
        }

        delegate void AddBadBlockTab(string text);
        private void add_badblocks_tab(string text)
        {
            if (txtBadBlocks.InvokeRequired)
            {
                AddBadBlockTab s = new AddBadBlockTab(add_badblocks_tab);
                this.Invoke(s, new object[] { text });
            }
            else
            {
                txtBadBlocks.Text = text;
            }
        }

        public void setNand(Nand.PrivateN Nand)
        {
            this.nand = Nand;
            populateInfo();
        }

        delegate void ShowCpuKeyTab();
        public void show_cpukey_tab()
        {
            if (tabControl1.InvokeRequired)
            {
                ShowCpuKeyTab s = new ShowCpuKeyTab(show_cpukey_tab);
                this.Invoke(s);
            }
            else
            {
                this.tabControl1.SelectedTab = this.tabPage2;
            }
        }

        public void change_tab()
        {
            this.tabControl1.BeginInvoke((Action) (() => tabControl1.SelectedTab = tabPage1));
            this.tabControl1.BeginInvoke((Action) (() => tabControl1.Refresh()));
        }

        private void NandInfo_Load(object sender, EventArgs e)
        {
            lblfcrt.Visible = false;
            label2blb.Visible = false;
            label2bla.Visible = false;
            textBox2BLb.Visible = false;
        }

        private void NandInfo_DragDrop(object sender, DragEventArgs e)
        {
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            DragDropChanged(s[0]);
        }

        private void NandInfo_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }

        private void btnAdvanced_Click(object sender, EventArgs e)
        {
            if (nand != null && nand.ok)
            {
                HexEdit.KVViewer k = new HexEdit.KVViewer(Nand.Nand.decryptkv(nand._rawkv, Oper.StringToByteArray(nand._cpukey)));
                k.Show();
            }
        }

    }
}
