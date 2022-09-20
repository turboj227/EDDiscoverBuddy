using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Globalization;

namespace EDDiscoverBuddy
{
    public partial class frmStatus : Form
    {
        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        private const UInt32 SWP_NOSIZE = 0x0001;
        private const UInt32 SWP_NOMOVE = 0x0002;
        private const UInt32 TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE;

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        frmEDOverlay EDOverlay;
        public frmStatus()
        {
            Text = "ED Discover Buddy T";
            GetInstalledVersion();
            InitializeComponent();
            EDOverlay = new frmEDOverlay(this);
        }
        private async void GetInstalledVersion()
        {
            try
            {
                PureManApplicationDeployment.PureManClickOnce ClickOnce = new PureManApplicationDeployment.PureManClickOnce("https://raw.githubusercontent.com/turboj227/EDDiscoverBuddy/master/published/");

                if (ClickOnce.IsNetworkDeployment)
                {
                    Version v = await ClickOnce.CurrentVersion();
                    Text += " - " + v.ToString();

                }
                else
                    Text += " - Not NetworkPloyment";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void frmStatus_Load(object sender, EventArgs e)
        {
            EDOverlay.Show();
            _ = SetWindowPos(EDOverlay.Handle, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
            _ = SetWindowPos(this.Handle, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
        }

        private void frmStatus_FormClosing(object sender, FormClosingEventArgs e)
        {
            EDOverlay.Close();
        }

        internal void UpdateInfo(cEDSystemInfo EDSystemInfo)
        {
            lblCommanderValue.Text = EDSystemInfo.CommanderName;
            lblIsEDRunningValue.Text = EDSystemInfo.EDRunning.ToString();
            lblCurrentSystemValue.Text = EDSystemInfo.CurrentSystem.StarSystem;
            if (EDSystemInfo.TargetSystem != null)
                lblNextSystemValue.Text = EDSystemInfo.TargetSystem.StarSystem;
            lblJumpsRemainingValue.Text = EDSystemInfo.RemainingJumps.ToString();
            lblNewDiscoveriesValue.Text = EDSystemInfo.NewDiscoveries.ToString();
            lblSurfaceScanValue.Text = EDSystemInfo.SurfaceScans.ToString();
            lblOnlineLookupsValue.Text = EDSystemInfo.OnlineLookUps.ToString();
            string PlanetValues = (EDSystemInfo.OtherPlanetValues + EDSystemInfo.CurrentSystem.GetAllPlanetValues()).ToString("N", CultureInfo.CreateSpecificCulture("en-US"));
            lblDiscoveryValuesValue.Text = PlanetValues.Substring(0, PlanetValues.Length - 3);
            string CurrentPlanetValues = EDSystemInfo.CurrentSystem.GetAllPlanetValues().ToString("N", CultureInfo.CreateSpecificCulture("en-US"));
            lblCurrentSystemPlanetValueValue.Text = CurrentPlanetValues.Substring(0, CurrentPlanetValues.Length - 3);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mnuAbout_Click(object sender, EventArgs e)
        {
            ProcessStartInfo psInfo = new ProcessStartInfo
            {
                FileName = "https://github.com/turboj227/EDDiscoverBuddy",
                UseShellExecute = true
            };
            Process.Start(psInfo);
        }

        private void mnuCheckForUpdate_Click(object sender, EventArgs e)
        {
            CheckForNewVersion();
        }
        private async void CheckForNewVersion()
        {
            try
            {
                PureManApplicationDeployment.PureManClickOnce ClickOnce = new PureManApplicationDeployment.PureManClickOnce("https://raw.githubusercontent.com/turboj227/EDDiscoverBuddy/master/published/");
                if (await ClickOnce.UpdateAvailable())
                {
                    if (MessageBox.Show("New update " + await ClickOnce.ServerVersion() + " available!\nDo you want to download it?", "New version", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        if (!await ClickOnce.Update())
                        {
                            MessageBox.Show("Failed to update new version!");
                        }
                    }
                }
                else
                    MessageBox.Show("No new update is available!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}