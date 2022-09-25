using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.Design.AxImporter;

namespace EDDiscoverBuddy
{
    public class cEDSystemInfo
    {
        public string CommanderName = "";
        public int NewDiscoveries = 0;
        public int SurfaceScans = 0;
        public int OtherPlanetValues = 0;
        public int OnlineLookUps = 0;
        public bool Jumping = false;
        public bool Honked = false;
        public int BodyCount = 0;
        internal int RemainingJumps = 0;
        internal bool FullyScanned = false;
        public bool EDRunning = false;
        public cEDSystem CurrentSystem = new cEDSystem();
        public cEDSystem NextSystem = new cEDSystem();
        public cEDSystem TargetSystem = new cEDSystem();

        internal int getBodyCount()
        {
            return FullyScanned ? BodyCount : CurrentSystem.getBodyCount();
        }

        internal string GetSystemInfoURL(bool ForCurrentSystem)
        {
            if (ForCurrentSystem)
            {
                if (CurrentSystem != null && CurrentSystem.StarSystem!= null)
                    return EDDiscoverBuddy.EDSM.EDSMHelper.GetSystemInfoURL(CurrentSystem.StarSystem);
            }
            else
            {
                if (TargetSystem != null && TargetSystem.StarSystem != null)
                    return EDDiscoverBuddy.EDSM.EDSMHelper.GetSystemInfoURL(TargetSystem.StarSystem);
            }

            return "";
        }
    }
    public class cEDSystem
    {
        public string StarSystem = "";
        internal long SystemAddress;
        public string StarClass = "";
        public bool WasDiscoveredOnline; //On EDDN
        public bool CanRefuel;
        public bool LookupOnline;
        public List<cPlanetBody> Bodies = new List<cPlanetBody>();

        public bool WasDiscovered { get { return WasDiscoveredOnline || Bodies.Exists(a => a.WasDiscovered); } }

        internal cPlanetBody AddBody(string starSystem, long systemAddress, string bodyName, int? bodyID, string type
            , float distanceFromArrivalLS, bool isStar, bool wasMapped, bool wasDiscovered, bool fromOnline
            , float massEM, string terraformState)
        {
            //Check if Body exists
            cPlanetBody? body = Bodies.FirstOrDefault(a => a.StarSystem == starSystem && a.SystemAddress == systemAddress && a.BodyName == bodyName && (a.BodyID == bodyID || a.BodyID == null || bodyID == null));
            if (body == null)
            {
                body = new cPlanetBody(starSystem, systemAddress, bodyName, bodyID, type, distanceFromArrivalLS, isStar
                    , wasMapped, wasDiscovered, fromOnline, massEM, terraformState);
                Bodies.Add(body);
            }
            else if (!fromOnline)
                body.update(starSystem, systemAddress, bodyName, bodyID, type, distanceFromArrivalLS, isStar, wasMapped, wasDiscovered, fromOnline, massEM, terraformState);
            return body;
        }

        internal void PlanetMapped(string bodyName, int bodyID, bool mappedEfficient)
        {
            cPlanetBody? body = Bodies.FirstOrDefault(a => a.StarSystem == StarSystem && a.SystemAddress == SystemAddress && a.BodyName == bodyName);
            if (body != null)
            {
                body.WasMappedByMe = true;
                //body.WasMapped = true;
                body.WasMappedEfficient = mappedEfficient;
                body.CalcValue();
            }
        }

        public int GetAllPlanetValues()
        {
            return Bodies.Sum(A => A.CurrentMappedValue);
        }

        internal int getBodyCount()
        {
            return Bodies.Count(a => !a.FromOnline);
        }
    }

    public class cPlanetBody
    {
        public string StarSystem = "";
        internal long SystemAddress;
        public string BodyName = "";
        public int? BodyID;
        public string Position = "";
        public string Type = "";
        public float DistanceFromArrivalLS;
        public bool IsStar;
        public bool WasMapped;
        public bool WasDiscovered;
        public bool FromOnline;
        public int MaxedMappedValue = 0;
        public int CurrentMappedValue = 0;
        internal bool WasMappedEfficient;
        public float MassEM;
        public string TerraformState;
        public bool WasMappedByMe;

        public cPlanetBody(string starSystem, long systemAddress, string bodyName, int? bodyID, string type, float distanceFromArrivalLS, bool isStar, bool wasMapped, bool wasDiscovered, bool fromOnline, float massEM, string terraformState)
        {
            WasMappedByMe = false;
            update(starSystem, systemAddress, bodyName, bodyID, type, distanceFromArrivalLS, isStar, wasMapped, wasDiscovered, fromOnline, massEM, terraformState);
        }
        public void update(string starSystem, long systemAddress, string bodyName, int? bodyID, string type, float distanceFromArrivalLS, bool isStar, bool wasMapped, bool wasDiscovered, bool fromOnline, float massEM, string terraformState)
        {
            StarSystem = starSystem;
            SystemAddress = systemAddress;
            BodyName = bodyName;
            BodyID = bodyID;
            Type = type;
            DistanceFromArrivalLS = distanceFromArrivalLS;
            IsStar = isStar;
            WasMapped = wasMapped;
            WasDiscovered = wasDiscovered || fromOnline;
            FromOnline = fromOnline;
            MassEM = massEM;
            TerraformState = terraformState;
            if (!fromOnline)
                CalcValue();
        }
        internal void CalcValue()
        {

            int terraform_state = 0;
            switch (TerraformState)
            {
                case "Candidate for terraforming":
                case "Terraformable":
                case "Terraformed":
                case "Terraforming":
                    terraform_state = 1;
                    break;
            }

            CalcOptions options = new CalcOptions();
            options.EfficiencyBonus = true;
            options.IsMapped = true;
            options.IsFirstDiscoverer = !WasDiscovered;
            options.IsFirstMapper = !WasMapped;

            MaxedMappedValue = CalcEstimatedValue(IsStar, Type, MassEM, terraform_state, options);

            options.EfficiencyBonus = WasMappedEfficient;
            options.IsMapped = WasMappedByMe;
            CurrentMappedValue = CalcEstimatedValue(IsStar, Type, MassEM, terraform_state, options);

        }

        private int CalcEstimatedValue(bool isStar, string specific_type, float mass, int terraform_state, CalcOptions options)
        {

            if (IsStar)
            {
                return CalcEstimatedStarValue(specific_type, mass);
            }
            else
            {
                //return CalcEstimatedPlanetValue(specific_type, mass, terraform_state, options);
                return GetBodyValue(CalcPLanetKValue(specific_type,terraform_state), mass, options.IsFirstDiscoverer, options.IsMapped, options.IsFirstMapper, options.EfficiencyBonus, true, false);
            }

        }

        private int CalcPLanetKValue(string specific_type, int terraform_state)
        {
            int bonus = 0;

            //TODO: check these items:
            //Gas giant with water-based life
            //Gas giant with ammonia-based life
            switch (Globals.PlanetType(specific_type))
            {
                //# Metal-rich body
                case 1:
                        bonus = 21790;
                    break;
                //# Ammonia world
                case 51:
                    bonus = 96932;
                    break;
                //# Class I gas giant
                case 71:
                    bonus = 1656;
                    break;
                //# High metal content world / Class II gas giant
                case 2:
                case 72:
                    bonus = 9654;
                    if (terraform_state > 0)
                        bonus = 100677;
                    break;
                //# Earth-like world / Water world
                case 31:
                case 41:
                    bonus = 64831;
                    if (terraform_state > 0)
                        bonus = 116295;
                    break;
                default:
                    {
                        bonus = 300;
                        if (terraform_state > 0)
                            bonus = 93328;
                    }
                    break;
            }
            return bonus;
        }
        private int CalcEstimatedStarValue(string specific_type, float mass)
        {

            float value = 1200;

            switch (Globals.StarType(specific_type))
            {
                //# White Dwarf Star
                case 51:
                case >= 501 and <= 514:
                    value = 14057;
                    break;
                //# Neutron Star, Black Hole
                case 91:
                case 92:
                    value = 22628;
                    break;
                //# Super-massive Black Hole
                case 93:
                    //# this is applying the same scaling to the 3.2 value as a normal black hole, not confirmed in game
                    value = 33.5678f;
                    break;
            }

            return (int)Math.Round(value + (mass * value / 66.25f));

        }

        static int GetBodyValue(int k, double mass, bool isFirstDiscoverer, bool isMapped, bool isFirstMapped, bool withEfficiencyBonus, bool isOdyssey, bool isFleetCarrierSale)
        {
            const double q = 0.56591828;
            double mappingMultiplier = 1;
            if (isMapped)
            {
                if (isFirstDiscoverer && isFirstMapped)
                {
                    mappingMultiplier = 3.699622554;
                }
                else if (isFirstMapped)
                {
                    mappingMultiplier = 8.0956;
                }
                else
                {
                    mappingMultiplier = 3.3333333333;
                }
            }
            double value = (k + k * q * Math.Pow(mass, 0.2)) * mappingMultiplier;
            if (isMapped)
            {
                if (isOdyssey)
                {
                    value += ((value * 0.3) > 555) ? value * 0.3 : 555;
                }
                if (withEfficiencyBonus)
                {
                    value *= 1.25;
                }
            }
            value = Math.Max(500, value);
            value *= (isFirstDiscoverer) ? 2.6 : 1;
            value *= (isFleetCarrierSale) ? 0.75 : 1;
            return (int)Math.Round(value);
        }

    }
    internal class CalcOptions
    {
        internal bool IsMapped = true;
        internal bool EfficiencyBonus = true;
        internal bool IsFirstDiscoverer = true;
        internal bool IsFirstMapper = true;
    }
}
