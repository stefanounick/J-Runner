using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CommPort;
using System.Runtime.InteropServices;
using System.IO;
using Microsoft.Win32.SafeHandles;
using Microsoft.Win32;

namespace JRunner.Forms
{
    public partial class Comport : Form
    {
        CommunicationManager comm = new CommunicationManager();

        public Comport()
        {
            InitializeComponent();
        }

        private void Comport_Load(object sender, EventArgs e)
        {
            LoadValues();
            SetDefaults();
            SetControlState();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (rbtnHex.Checked) comm.CurrentTransmissionType = CommPort.CommunicationManager.TransmissionType.Hex;
            else comm.CurrentTransmissionType = CommPort.CommunicationManager.TransmissionType.Text;
            comm.PortName = cboPort.Text;
            comm.Parity = cboParity.Text;
            comm.StopBits = cboStop.Text;
            comm.DataBits = cboData.Text;
            comm.BaudRate = cboBaud.Text;
            comm.DisplayWindow = textBox1;
            if (cboPort.Text == "") return;
            try
            {
                comm.OpenPort();
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }

            cmdOpen.Enabled = false;
            cmdClose.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                cmdClose.Enabled = false;
                cmdOpen.Enabled = true;
                comm.ClosePort();
            }
            catch (Exception ex) { Console.WriteLine(ex.InnerException); }
        }

        private void SetDefaults()
        {
            try
            {
                cboPort.SelectedIndex = 0;
                cboBaud.SelectedText = "115200";
                cboBaud.Items.Add("115200");
                cboParity.SelectedIndex = 0;
                cboStop.SelectedIndex = 1;
                cboData.SelectedIndex = 1;
            }
            catch (System.ArgumentOutOfRangeException) { MessageBox.Show("No COM ports were found"); }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
        }

        private void LoadValues()
        {
            try
            {
                comm.SetPortNameValues(cboPort);
                //if (variables.COMPort != "" && cboPort.Items.Count > 0) cboPort.SelectedText = variables.COMPort;
                if (cboPort.Items.Count > 0) cboPort.SelectedIndex = 0;
                comm.SetParityValues(cboParity);
                comm.SetStopBitValues(cboStop);
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
        }

        private void SetControlState()
        {
            cmdClose.Enabled = false;
        }

        private void Comport_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                comm.ClosePort();
            }
            catch (Exception ex) { Console.WriteLine(ex.InnerException); }
        }

        private void cboPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            variables.COMPort = cboPort.SelectedItem.ToString();
        }

        private void Comport_KeyUp(object sender, KeyEventArgs e)
        {
        }

    }
}
