using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.Design.AxImporter;

namespace EDDiscoverBuddy
{
    internal class cEDSystemInfo
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
    }
    internal class cEDSystem
    {
        public string StarSystem = "";
        internal long SystemAddress;
        public string StarClass = "";
        public bool WasDiscoveredOnline; //On EDDN
        public bool CanRefuel;
        public bool LookupOnline;
        public List<cPlanetBody> Bodies = new List<cPlanetBody>();

        internal cPlanetBody AddBody(string starSystem, long systemAddress, string bodyName, int? bodyID, string type
            , float distanceFromArrivalLS, bool isStar, bool wasMapped, bool wasDiscovered, bool fromOnline
            , float massEM, string terraformState)
        {
            //Check if Body exists
            cPlanetBody? body = Bodies.FirstOrDefault(a => a.StarSystem == starSystem && a.SystemAddress == systemAddress && a.BodyName == bodyName && (a.BodyID == bodyID || a.BodyID==null || bodyID==null));
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
            if (body!=null)
            {
                body.WasMappedByMe = true;
                body.WasMapped = true;
                body.WasMappedEfficient = mappedEfficient;
                body.CalcValue();
            }
        }

        public int GetAllPlanetValues()
        {
            return Bodies.Sum(A => A.CurrentMappedValue);
        }
    }

    internal class cPlanetBody
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
                return CalcEstimatedPlanetValue(specific_type, mass, terraform_state, options);
            }

        }

        private int CalcEstimatedPlanetValue(string specific_type, float mass, int terraform_state, CalcOptions opt)
        {

            float value = CalcPlanetValue(specific_type);
            float bonus = CalcPLanetBonus(specific_type, terraform_state);

            //# CALCULATION
            float q = 0.56591828f;
            value += bonus;
            float map_multiplier = 1.0f;

            if (opt.IsMapped)
                map_multiplier = 3.3333333333f;

                if (opt.IsFirstDiscoverer && opt.IsFirstMapper)
                    map_multiplier = 3.699622554f;

                else if (!opt.IsFirstDiscoverer && opt.IsFirstMapper)
                    map_multiplier = 8.0956f;

                if (opt.EfficiencyBonus)
                    map_multiplier *= 1.25f;

            value = (float)Math.Max((value + (value * Math.Pow(mass, 0.2f) * q)) * map_multiplier, 500f);

            if (opt.IsFirstDiscoverer)
                value *= 2.6f;

            return (int)Math.Round(value);
        }

        private float CalcPLanetBonus(string specific_type, int terraform_state)
        {
            float bonus = 0f;

            if (terraform_state > 0)
                bonus = 93328f;
            switch (Globals.PlanetType(specific_type))
            {
                //# Metal-rich body
                case 1:
                    if (terraform_state > 0)
                        bonus = 65631f;
                    break;
                //# High metal content world / Class II gas giant
                case 2:
                case 72:
                    if (terraform_state > 0)
                        bonus = 100677f;
                    break;
                //# Earth-like world / Water world
                case 31:
                    bonus = 116295f;
                    break;
                case 41:
                    if (terraform_state > 0)
                        bonus = 116295f;
                    break;
            }
            return bonus;
        }

        private float CalcPlanetValue(string specific_type)
        {

            float value = 300;
            switch (Globals.PlanetType(specific_type))
            {
                //# Metal-rich body
                case 1:
                    value = 21790;
                    break;
                //# Ammonia world
                case 51:
                    value = 96932;
                    break;
                //# Class I gas giant
                case 71:
                    value = 1656;
                    break;
                //# High metal content world / Class II gas giant
                case 2:
                case 72:
                    value = 9654;
                    break;
                //# Earth-like world / Water world
                case 31:
                case 41:
                    value = 64831;
                    break;
            }

            return value;
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
    }
    internal class CalcOptions
    {
        internal bool IsMapped = true;
        internal bool EfficiencyBonus = true;
        internal bool IsFirstDiscoverer = true;
        internal bool IsFirstMapper = true;
    }
}
