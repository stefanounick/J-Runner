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
    public partial class XSVFChoice : UserControl
    {
        int hresult = 0;
        bool demon = false;
        private List<RadioButton> _radioButtonGroup = new List<RadioButton>();

        public delegate void ClickedProgramCR();
        public event ClickedProgramCR ProgramCRClick;
        public delegate void ClickedCloseCR();
        public event ClickedCloseCR CloseCRClick;

        public XSVFChoice()
        {
            InitializeComponent();
            btnProgram.DialogResult = DialogResult.OK;
            btnCancel.DialogResult = DialogResult.Cancel;
            var d = GetAll(this, typeof(RadioButton));
            foreach (RadioButton a in d)
            {
                _radioButtonGroup.Add(a);
                
            }
        }

        /// <summary>
        /// Trinity 1
        /// Falcon 2
        /// Zephyr 3
        /// Jasper 4
        /// Xenon 5
        /// Opus 6
        /// RGH2 A 7
        /// RGH2 B 8
        /// RGH2 C 9
        /// RGH2 D 10
        /// Falcon2 11
        /// Cor1 12
        /// Cor2 13
        /// Cor3 14
        /// Cor4 15
        /// Cor5 16
        /// Cor6 17
        /// Cor7 18
        /// Cor8 19
        /// </summary>
        /// <returns></returns>
        public int heResult()
        {
            if ((radiobtnTrinity).Checked)
            {
                hresult = 1;
            }
            else if ((radiobtnFalcon).Checked)
            {
                hresult = 2;
            }
            else if ((radiobtnZephyr).Checked)
            {
                hresult = 3;
            }
            else if ((radiobtnJasper).Checked)
            {
                hresult = 4;
            }
            else if ((radiobtnXenon).Checked)
            {
                hresult = 5;
            }
            else if ((radiobtnOpus).Checked)
            {
                hresult = 6;
            }
            else if ((radiobtnA).Checked)
            {
                hresult = 7;
            }
            else if ((radiobtnB).Checked)
            {
                hresult = 8;
            }
            else if ((radiobtnC).Checked)
            {
                hresult = 9;
            }
            else if ((radiobtnD).Checked)
            {
                hresult = 10;
            }
            else if (radiobtnFalcon2.Checked)
            {
                hresult = 11;
            }
            else if (trinityBut2.Checked)
            {
                hresult = 1;
            }
            else if (CorButton02.Checked)
            {
                hresult = 12;
            }
            else if (CorButton12.Checked)
            {
                hresult = 13;
            }
            else if (CorButton13.Checked)
            {
                hresult = 14;
            }
            else if (CorButton22.Checked)
            {
                hresult = 15;
            }
            else if (CorButton23.Checked)
            {
                hresult = 16;
            }
            else if (CorButton32.Checked)
            {
                hresult = 17;
            }
            else if (CorButton33.Checked)
            {
                hresult = 18;
            }
            else if (CorButton43.Checked)
            {
                hresult = 19;
            }
            else if (CorButton31.Checked)
            {
                hresult = 20;
            }
            else if (CorButton42.Checked)
            {
                hresult = 21;
            }
            else if (CorButton52.Checked)
            {
                hresult = 22;
            }
            else if (RGXcorABut.Checked)
            {
                hresult = 23;
            }
            else if (RGXcorBBut.Checked)
            {
                hresult = 24;
            }
            else if (RGXcorCBut.Checked)
            {
                hresult = 25;
            }
            else if (RGXtrinButA.Checked)
            {
                hresult = 26;
            }
            else if (RGXtrinButB.Checked)
            {
                hresult = 27;
            }
            else if (RGXtrinButC.Checked)
            {
                hresult = 28;
            }
            else if (RGXtrinButD.Checked)
            {
                hresult = 29;
            }
            else hresult = -1;
            return hresult;
        }
        public bool deResult()
        {
            if (demon) return true;
            else return false;
        }

        enum Console_Types
        {
            radiobtnTrinity = 1,
            radiobtnFalcon = 2,
            radiobtnZephyr = 3,
            radiobtnJasper = 4,
            radiobtnXenon = 5,
            radiobtnOpus = 6

        }

        private void xsvf_types_Load(object sender, EventArgs e)
        {
            dostuff();
            if (variables.ctyp.ID != -1)
            {
                if (variables.ctyp.ID == 1)
                {
                   trinityBut2.Checked = true;
                }
                else if (variables.ctyp.ID == 2)
                {
                    radiobtnFalcon.Checked = true;
                }
                else if (variables.ctyp.ID == 3)
                {
                    radiobtnZephyr.Checked = true;
                }
                else if (variables.ctyp.ID == 4 || variables.ctyp.ID == 5
                    || variables.ctyp.ID == 6 || variables.ctyp.ID == 7)
                {
                    radiobtnJasper.Checked = true;
                }
                else if (variables.ctyp.ID == 8)
                {
                    radiobtnXenon.Checked = true;
                }
                else if (variables.ctyp.ID == 9)
                {
                    radiobtnOpus.Checked = true;
                }
                else if (variables.ctyp.ID == 10)
                {
                    groupBox1.Enabled = true;
                    groupBox2.Enabled = false;
                    groupBox3.Enabled = false;
                }
            }
        }

        public void dostuff()
        {
            //if (variables.ttyp != variables.hacktypes.glitch2) groupBox3.Enabled = false;
            //if (variables.ttyp != variables.hacktypes.glitch) groupBox2.Enabled = false;

            //if (variables.ttyp == variables.hacktypes.glitch2) groupBox3.Enabled = true;
            //else groupBox2.Enabled = true;

            if ((variables.ttyp == variables.hacktypes.jtag) || (variables.ttyp == variables.hacktypes.retail))
            {
                groupBox1.Enabled = false;
                groupBox2.Enabled = false;
                groupBox3.Enabled = false;
                groupBox4.Enabled = false;
                //radiobtnTrinity.Enabled = false;
            }
            else
            {
                if (variables.ttyp != variables.hacktypes.glitch2 && variables.ttyp != variables.hacktypes.glitch2m) groupBox3.Enabled = false;
                if (variables.ttyp != variables.hacktypes.glitch) groupBox2.Enabled = false;

                if (variables.ttyp == variables.hacktypes.glitch2 || variables.ttyp == variables.hacktypes.glitch2m) groupBox3.Enabled = true;
                else groupBox2.Enabled = true;
                groupBox1.Enabled = true;
                //groupBox2.Enabled = true;
                //groupBox3.Enabled = true;
                groupBox4.Enabled = true;
                //radiobtnTrinity.Enabled = true;
            }
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            
            RadioButton rb = (RadioButton)sender;
            if (rb.Checked)
            {


                foreach (RadioButton other in _radioButtonGroup)
                {
                    if (other == rb)
                    {
                        continue;
                    }
                    other.Checked = false;
                    
                }
            }
        }
        public IEnumerable<Control> GetAll(Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>();

            return controls.SelectMany(ctrl => GetAll(ctrl, type)).Concat(controls).Where(c => c.GetType() == type);
        }

        private void checkDemoN_CheckedChanged(object sender, EventArgs e)
        {
            demon = checkDemoN.Checked;
            CorButton31.Enabled = CorButton42.Enabled = CorButton52.Enabled = checkDemoN.Checked;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                CloseCRClick();
            }
            catch (Exception) { }
        }

        private void btnProgram_Click(object sender, EventArgs e)
        {
            try
            {
                ProgramCRClick();
                
            }
            catch (Exception) { }
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {
            MessageBox.Show("Do NOT attempt to program a CR3 Pro or R-JTAG chip. Doing so will overwrite the factory firmware and render your chip useless!", "WARNING THESE ARE FOR DGX/RGX ONLY!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void trinityBut2_CheckedChanged(object sender, EventArgs e)
        {

        }
      
    }
}
