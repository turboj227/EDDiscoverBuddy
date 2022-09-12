using EXControls;
using System.Collections;
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
                    //label1.Text = "Jumping to:" + EDSystemInfo.NextSystem.StarSystem + "\nCan Refuel:" + EDSystemInfo.NextSystem.CanRefuel + "\nDiscovered:" + EDSystemInfo.NextSystem.WasDiscoveredOnline + "\nOnline lookups:" + EDSystemInfo.OnlineLookUps;
                    lsvHighValuePlanets.Visible = false;
                    lsvSystemInfo.BeginUpdate();
                    lsvSystemInfo.Visible = true;
                    lsvSystemInfo.Items.Clear();
                    EXImageListViewItem lviSystemInfo = new EXImageListViewItem();
                    lviSystemInfo.MyImage = Image.FromFile("Icons/FirstDiscovery_" + (EDSystemInfo.NextSystem.WasDiscoveredOnline ? "disabled" : "enabled") + ".png");
                    EXMultipleImagesListViewSubItem subIcons = new EXMultipleImagesListViewSubItem();
                    ArrayList images = new ArrayList(new object[]
                              {Image.FromFile("Icons/Refuel_"+(EDSystemInfo.NextSystem.CanRefuel?"enabled":"disabled")+".png") });
                    subIcons.MyImages = images;
                    lviSystemInfo.SubItems.Add(subIcons);
                    lviSystemInfo.SubItems.Add(new EXListViewSubItem());
                    lviSystemInfo.SubItems.Add(new EXListViewSubItem());
                    lviSystemInfo.SubItems.Add(new EXListViewSubItem());

                    lviSystemInfo.SubItems[2].Text = "Jumping";
                    lviSystemInfo.SubItems[3].Text = " to";
                    lviSystemInfo.SubItems[4].Text = EDSystemInfo.NextSystem.StarSystem;
                    lsvSystemInfo.Items.Add(lviSystemInfo);
                    lsvSystemInfo.EndUpdate();
                }
                else
                {
                    lsvSystemInfo.BeginUpdate();
                    lsvSystemInfo.Visible = true;
                    lsvSystemInfo.Items.Clear();
                    EXImageListViewItem lviSystemInfo = new EXImageListViewItem();
                    lviSystemInfo.MyImage = Image.FromFile("Icons/FirstDiscovery_" + (EDSystemInfo.CurrentSystem.WasDiscoveredOnline ? "disabled" : "enabled") + ".png");
                    EXMultipleImagesListViewSubItem subIcons = new EXMultipleImagesListViewSubItem();
                    ArrayList images = new ArrayList(new object[]
                              {Image.FromFile("Icons/Refuel_"+(EDSystemInfo.CurrentSystem.CanRefuel?"enabled":"disabled")+".png"),
                        Image.FromFile("Icons/Honk_"+(EDSystemInfo.Honked?"disabled":"enabled")+".png")});
                    subIcons.MyImages = images;
                    lviSystemInfo.SubItems.Add(subIcons);
                    lviSystemInfo.SubItems.Add(new EXListViewSubItem());
                    lviSystemInfo.SubItems.Add(new EXListViewSubItem());
                    lviSystemInfo.SubItems.Add(new EXListViewSubItem());

                    lviSystemInfo.SubItems[2].Text = EDSystemInfo.CurrentSystem.Bodies.Count + "/" + EDSystemInfo.BodyCount;
                    lviSystemInfo.SubItems[3].Text = EDSystemInfo.RemainingJumps.ToString();
                    lviSystemInfo.SubItems[4].Text = EDSystemInfo.CurrentSystem.StarSystem;
                    lsvSystemInfo.Items.Add(lviSystemInfo);
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
            EXImageListViewItem lviValuable = new EXImageListViewItem(Image.FromFile("Icons/FirstDiscovery_" + (b.WasDiscovered ? "disabled" : "enabled") + ".png"));
            lviValuable.SubItems.Add(new EXListViewSubItem(b.BodyName.Replace(b.StarSystem, "").Trim()));
            lviValuable.SubItems.Add(new EXListViewSubItem(((int)b.DistanceFromArrivalLS).ToString()));
            lviValuable.SubItems.Add(new EXListViewSubItem(b.MappedValue.ToString("0,000,000")));
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