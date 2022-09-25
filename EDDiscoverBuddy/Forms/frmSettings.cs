using EDDiscoverBuddy.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EDDiscoverBuddy.Forms
{
    public partial class frmSettings : Form
    {
        public frmSettings()
        {
            InitializeComponent();
            txtMinPlanetValue.Text = Settings.InterestedFrom.ToString();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                int value;
                if (int.TryParse(txtMinPlanetValue.Text, out value))
                {
                    if (value >= 0)
                    {
                        //Store value
                        Settings.InterestedFrom = value;
                    }
                    else
                        throw new Exception("Planet value needs to be 0 or heigher!");
                }
                else
                    throw new Exception("Planet value needs to be 0 or heigher!");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
