using EXControls;
using System.Collections;
using System.Globalization;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace EDDiscoverBuddy
{
    public partial class Form1 : Form
    {
        private int m_Ticks = 0;
        private cJournalReader m_JournalReader = new cJournalReader();
        private cEDSystemInfo EDSystemInfo = new cEDSystemInfo();

        private cJournalReader JournalReader
        {
            get { return m_JournalReader; }
            set { m_JournalReader = value; }
        }

        private int Ticks
        {
            get { return m_Ticks; }
            set { m_Ticks = value; }
        }

        public Form1()
        {
            InitializeComponent();
            JournalReader.ReadAllEntries(ref EDSystemInfo, true);
            JourneyTimer.Start();
        }
        private void JourneyTimer_Tick(object sender, EventArgs e)
        {
            JournalReader.ReadAllEntries(ref EDSystemInfo, false);
            if (EDSystemInfo.EDRunning)
            {
                if (EDSystemInfo.Jumping)
                {
                    lsvHighValuePlanets.Visible = false;
                    lsvSystemInfo.BeginUpdate();
                    lsvSystemInfo.Visible = true;
                    lsvSystemInfo.Items.Clear();
                    EXImageListViewItem lviSystemInfo = new EXImageListViewItem();

                    ArrayList images = new ArrayList();
                    if (!EDSystemInfo.NextSystem.WasDiscoveredOnline)
                        images.Add(Image.FromFile("Icons/FirstDiscovery.png"));
                    if (EDSystemInfo.NextSystem.CanRefuel)
                        images.Add(Image.FromFile("Icons/Refuel.png"));

                    if (images.Count > 0)
                    {
                        lviSystemInfo.MyImage = (Image)images[0];
                        images.RemoveAt(0);
                    }

                    EXMultipleImagesListViewSubItem subIcons = new EXMultipleImagesListViewSubItem();
                    if (images.Count > 0)
                        subIcons.MyImages = images;
                    lviSystemInfo.SubItems.Add(subIcons);

                    subIcons.Text = "Jumping to " + EDSystemInfo.NextSystem.StarSystem;
                    lsvSystemInfo.Items.Add(lviSystemInfo);
                    lsvSystemInfo.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.ColumnContent);
                    lsvSystemInfo.Columns[1].Width += images.Count * 40;
                    lsvSystemInfo.EndUpdate();
                }
                else
                {
                    lsvSystemInfo.BeginUpdate();
                    lsvSystemInfo.Visible = true;
                    lsvSystemInfo.Items.Clear();
                    EXImageListViewItem lviSystemInfo = new EXImageListViewItem();
                    ArrayList images = new ArrayList();
                    if (!EDSystemInfo.CurrentSystem.WasDiscoveredOnline)
                        images.Add(Image.FromFile("Icons/FirstDiscovery.png"));
                    if (EDSystemInfo.CurrentSystem.CanRefuel)
                        images.Add(Image.FromFile("Icons/Refuel.png"));
                    if (!EDSystemInfo.Honked)
                        images.Add(Image.FromFile("Icons/Honk.png"));
                    if (EDSystemInfo.FullyScanned)
                        images.Add(Image.FromFile("Icons/FoundAll.png"));

                    if (images.Count > 0)
                    {
                        lviSystemInfo.MyImage = (Image)images[0];
                        images.RemoveAt(0);
                    }
                    EXMultipleImagesListViewSubItem subIcons = new EXMultipleImagesListViewSubItem();
                    if (images.Count>0)
                        subIcons.MyImages = images;
                    subIcons.Text = EDSystemInfo.CurrentSystem.Bodies.Count(a => !a.FromOnline) + "/" + EDSystemInfo.BodyCount;
                    lviSystemInfo.SubItems.Add(subIcons);
                    EXImageListViewSubItem Jumps = new EXImageListViewSubItem();
                    lviSystemInfo.SubItems.Add(Jumps);
                    EXListViewSubItem currentSystem = new EXListViewSubItem();
                    lviSystemInfo.SubItems.Add(currentSystem);

                    Jumps.MyImage = Image.FromFile("Icons/Jumps.png");
                    Jumps.Text = EDSystemInfo.RemainingJumps.ToString();
                    currentSystem.Text = EDSystemInfo.CurrentSystem.StarSystem;
                    lsvSystemInfo.Items.Add(lviSystemInfo);
                    lsvSystemInfo.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.ColumnContent);
                    lsvSystemInfo.Columns[1].Width += images.Count * 40;
                    lsvSystemInfo.EndUpdate();

                    lsvHighValuePlanets.BeginUpdate();
                    while (lsvHighValuePlanets.Items.Count > 1)
                    {
                        lsvHighValuePlanets.Items.RemoveAt(1);
                    }
                    EDSystemInfo.CurrentSystem.Bodies.Where(a => a.MappedValue >= 500000 && !a.WasMapped).OrderBy(a => a.DistanceFromArrivalLS).ToList().ForEach(b => AddValuablePlanet(b));
                    if (lsvHighValuePlanets.Items.Count == 1)
                        lsvHighValuePlanets.Visible = false;
                    else
                        lsvHighValuePlanets.Visible = true;
                    lsvHighValuePlanets.EndUpdate();
                }
            }
            else
            {
                lsvSystemInfo.Visible = false;
                lsvHighValuePlanets.Visible = false;
            }
            Ticks++;
        }

        private void AddValuablePlanet(cPlanetBody b)
        {
            EXImageListViewItem lviValuable = new EXImageListViewItem();
            if (!b.WasDiscovered)
                lviValuable.MyImage = Image.FromFile("Icons/FirstDiscovery.png");
            lviValuable.SubItems.Add(new EXListViewSubItem(b.BodyName.Replace(b.StarSystem, "").Trim()));
            lviValuable.SubItems.Add(new EXListViewSubItem(((int)b.DistanceFromArrivalLS).ToString()));
            lviValuable.SubItems.Add(new EXListViewSubItem(b.MappedValue.ToString("N", CultureInfo.CreateSpecificCulture("en-US")).Substring(0, b.MappedValue.ToString("N", CultureInfo.CreateSpecificCulture("en-US")).Length - 3)));
            lviValuable.SubItems.Add(new EXListViewSubItem(b.Type));
            lsvHighValuePlanets.Items.Add(lviValuable);
            lsvHighValuePlanets.Visible = true;
        }

        private void lsvSystemInfo_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            e.Item.Selected = false;
            e.Item.Focused = false;
        }

        private void exListView2_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            e.Item.Selected = false;
            e.Item.Focused = false;
        }
    }
}