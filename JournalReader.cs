using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using EDScoutBuddy.EDJournal;
using EDScoutBuddy.EDSM;
using System.Windows.Forms;

namespace EDScoutBuddy
{
    internal class cJournalReader
    {
        private string m_ElitePath = "";
        private string m_JournalFilter = "";
        private string m_CurrentJournalFile = "";
        private string m_LastLogEntry = "";

        public string LastLogEntry
        {
            get { return m_LastLogEntry; }
            set { m_LastLogEntry = value; }
        }
        public string CurrentJournalFile
        {
            get { return m_CurrentJournalFile; }
            set { m_CurrentJournalFile = value; }
        }

        private void ProcessLogFile(ref cEDSystemInfo EDSystemInfo, string allText, bool ProcessAll)
        {
            StringReader AlllogEntries = new StringReader(allText);
            string? LogEntry;

            bool newEntry = false;
            string newLastEntry = "";
            while ((LogEntry = AlllogEntries.ReadLine()) != null)
            {
                if (newEntry || ProcessAll)
                {
                    JournalEvent? Event = JsonConvert.DeserializeObject<JournalEvent>(LogEntry);

                    if (Event != null)
                    {
                        //check event
                        switch (Event.@event)
                        {
                            case "FSDTarget": //Arrive in new System
                                {
                                    JournalFSDTarget? FSDTarget = JsonConvert.DeserializeObject<JournalFSDTarget>(LogEntry);
                                    if (FSDTarget != null)
                                    {
                                        //New next system
                                        EDSystemInfo.RemainingJumps = FSDTarget.RemainingJumpsInRoute;
                                        /*EDSystemInfo.Honked = false;
                                        EDSystemInfo.BodyCount = 0;
                                        EDSystemInfo.FullyScanned = false;*/
                                    }
                                }
                                break;
                            case "FSDJump":         //Jump Completed
                                {
                                    JournalFSDJump? FSDJump = JsonConvert.DeserializeObject<JournalFSDJump>(LogEntry);
                                    if (FSDJump != null)
                                    {
                                        EDSystemInfo.Jumping = false;
                                        EDSystemInfo.CurrentSystem.StarSystem = FSDJump.StarSystem;
                                        EDSystemInfo.CurrentSystem.SystemAddress = FSDJump.SystemAddress;
                                        EDSystemInfo.CurrentSystem.StarClass = EDSystemInfo.NextSystem.StarClass;
                                        EDSystemInfo.CurrentSystem.CanRefuel = EDSystemInfo.NextSystem.StarClass.In("K", "G", "B", "F", "O", "A", "M");
                                        EDSystemInfo.CurrentSystem.LookupOnline = true;
                                        EDSystemInfo.BodyCount = 0;
                                        EDSystemInfo.Honked = false;
                                        EDSystemInfo.CurrentSystem.Bodies.Clear();
                                    }
                                }
                                break;
                            case "FSSDiscoveryScan":    //Honk
                                {
                                    JournalFSSDiscoveryScan? FSSDiscoveryScan = JsonConvert.DeserializeObject<JournalFSSDiscoveryScan>(LogEntry);
                                    if (FSSDiscoveryScan != null)
                                    {
                                        EDSystemInfo.BodyCount = FSSDiscoveryScan.BodyCount;
                                        EDSystemInfo.Honked = true;
                                        EDSystemInfo.FullyScanned = FSSDiscoveryScan.Progress == 1;
                                    }
                                }
                                break;
                            case "Scan":                //Scan body (auto or FFS)
                                {
                                    JournalScan? Scan = JsonConvert.DeserializeObject<JournalScan>(LogEntry);
                                    if (Scan != null)
                                    {
                                        if (Scan.StarType != null || Scan.PlanetClass != null)
                                        {//Found body or star
                                            EDSystemInfo.CurrentSystem.AddBody(Scan.StarSystem, Scan.SystemAddress, Scan.BodyName
                                                , Scan.BodyID, Scan.PlanetClass != null ? Scan.PlanetClass : Scan.StarType, Scan.DistanceFromArrivalLS, Scan.StarType != null
                                                , Scan.WasMapped, Scan.WasDiscovered, false, Scan.MassEM, Scan.TerraformState);
                                        }
                                    }
                                }
                                break;
                            case "FSSAllBodiesFound":   //Completly scanned system
                                {
                                    JournalFSSAllBodiesFound? FSSAllBodiesFound = JsonConvert.DeserializeObject<JournalFSSAllBodiesFound>(LogEntry);
                                    if (FSSAllBodiesFound != null)
                                    {
                                        EDSystemInfo.FullyScanned = true;
                                        EDSystemInfo.BodyCount = FSSAllBodiesFound.Count;
                                    }
                                }
                                break;
                            case "StartJump":           //Start jumping
                                {
                                    JournalStartJump? StartJump = JsonConvert.DeserializeObject<JournalStartJump>(LogEntry);
                                    if (StartJump != null && StartJump.JumpType == "Hyperspace")
                                    {
                                        EDSystemInfo.Jumping = true;

                                        EDSystemInfo.NextSystem.StarSystem = StartJump.StarSystem;
                                        EDSystemInfo.NextSystem.SystemAddress = StartJump.SystemAddress;
                                        EDSystemInfo.NextSystem.StarClass = StartJump.StarClass;
                                        EDSystemInfo.NextSystem.WasDiscoveredOnline = false; //On EDDN
                                        EDSystemInfo.NextSystem.CanRefuel = StartJump.StarClass.In("K", "G", "B", "F", "O", "A", "M");
                                        EDSystemInfo.NextSystem.LookupOnline = true;
                                        EDSystemInfo.NextSystem.Bodies.Clear();

                                    }
                                }
                                break;
                            case "SAAScanComplete":         //Planet Mapped
                                {
                                    JournalSAAScanComplete? SAAScanComplete = JsonConvert.DeserializeObject<JournalSAAScanComplete>(LogEntry);
                                    if (SAAScanComplete != null)
                                    {
                                        EDSystemInfo.CurrentSystem.PlanetMapped(SAAScanComplete.BodyName, SAAScanComplete.BodyID, SAAScanComplete.ProbesUsed <= SAAScanComplete.EfficiencyTarget);
                                    }
                                }
                                break;
                        }
                    }
                }
                else
                if (LogEntry == LastLogEntry)
                {
                    newEntry = true;
                }
                newLastEntry = LogEntry;
            }
            LastLogEntry = newLastEntry;
            CheckIfEDIsRunning(ref EDSystemInfo);
            if (EDSystemInfo.CurrentSystem.LookupOnline)
            {
                EDSystemInfo.OnlineLookUps++;
                string result = EDScoutBuddy.EDSM.EDSMHelper.GetBodies(EDSystemInfo.CurrentSystem.StarSystem, EDSystemInfo.CurrentSystem.SystemAddress);
                
                EDSMBodies? OnlineBodies = JsonConvert.DeserializeObject<EDSMBodies>(result);
                EDSystemInfo.CurrentSystem.WasDiscoveredOnline = (OnlineBodies != null && OnlineBodies.name != null);
                if (OnlineBodies!= null && OnlineBodies.bodies != null)
                {
                    foreach (Body body in OnlineBodies.bodies )
                    {
                        EDSystemInfo.CurrentSystem.AddBody(OnlineBodies.name, OnlineBodies.id64, body.name
                            , body.bodyId, body.subType, body.distanceToArrival, body.type == "star"
                            , false, body.discovery!=null, true, body.type=="Star"?body.solarMasses: body.earthMasses, body.terraformingState);
                    }
                }
                EDSystemInfo.CurrentSystem.LookupOnline = false;
            }
            if (EDSystemInfo.NextSystem.LookupOnline)
            {
                EDSystemInfo.OnlineLookUps++;
                string result = EDScoutBuddy.EDSM.EDSMHelper.GetBodies(EDSystemInfo.NextSystem.StarSystem, EDSystemInfo.NextSystem.SystemAddress);
                
                EDSMBodies? OnlineBodies = JsonConvert.DeserializeObject<EDSMBodies>(result);
                EDSystemInfo.NextSystem.WasDiscoveredOnline = (OnlineBodies != null && OnlineBodies.name != null);
                if (OnlineBodies != null && OnlineBodies.bodies!=null)
                {
                    foreach (Body body in OnlineBodies.bodies)
                    {
                        EDSystemInfo.NextSystem.AddBody(OnlineBodies.name, OnlineBodies.id64, body.name
                            , body.bodyId, body.subType, body.distanceToArrival, body.type == "star"
                            , false, body.discovery != null, true, body.type == "Star" ? body.solarMasses : body.earthMasses, body.terraformingState);
                    }
                }
                EDSystemInfo.NextSystem.LookupOnline = false;
            }

        }

        private void CheckIfEDIsRunning(ref cEDSystemInfo EDSystemInfo)
        {
            JournalEvent? Event = JsonConvert.DeserializeObject<JournalEvent>(LastLogEntry);
            EDSystemInfo.EDRunning = true;
            if (Event != null)
            {
                //check event
                switch (Event.@event)
                {
                    case "Shutdown":
                    case "Fileheader":
                        {
                            EDSystemInfo.EDRunning = false;
                        }
                        break;
                    case "Music":
                        {
                            JournalMusic? Music = JsonConvert.DeserializeObject<JournalMusic>(LastLogEntry);
                            if (Music != null)
                            {
                                if (Music.MusicTrack.ToLower()== "mainmenu")
                                    EDSystemInfo.EDRunning = false;
                            }
                            else
                                EDSystemInfo.EDRunning = false;
                        }
                        break;
                }
            }
            else
                EDSystemInfo.EDRunning = false;
        }

        public string JournalFilter
        {
            get { return m_JournalFilter; }
            set { m_JournalFilter = value; }
        }
        public string ElitePath
        {
            get { return m_ElitePath; }
            set { m_ElitePath = value; }
        }

        public cJournalReader()
        {
            ElitePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Saved Games\\Frontier Developments\\Elite Dangerous\\");
            JournalFilter = "Journal.*T*.log";
        }

        public void ReadAllEntries(ref cEDSystemInfo EDSystemInfo, bool CatchUp)
        {
            //Get all Journal files
            string[] JournalFiles = Directory.GetFiles(ElitePath, JournalFilter);

            //Read all journal files or only last one(s)
            string allText = "";
            JournalFiles.Where(W => W.CompareTo(CurrentJournalFile) >= 0 || CurrentJournalFile == "")
                .OrderBy(o => o).ToList().ForEach(f => allText += ReadJournal(f));

            //Process all Journal files data
            ProcessLogFile(ref EDSystemInfo, allText, CatchUp);
            
            //Get last Journal file for next run
            CurrentJournalFile = JournalFiles.Where(W => W.CompareTo(CurrentJournalFile) >= 0 || CurrentJournalFile == "").OrderBy(o => o).LastOrDefault("");
        }

        private string ReadJournal(string JournalFile)
        {
            string allText = "";
            using (var fs = new FileStream(JournalFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var sr = new StreamReader(fs, Encoding.Default))
                {
                    allText = sr.ReadToEnd();

                    sr.Close();
                }
                fs.Close();
            }
            return allText;
        }

    }


}
