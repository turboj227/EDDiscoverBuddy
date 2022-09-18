using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDDiscoverBuddy.EDJournal
{
    public class JournalEvent
    {
        public DateTime timestamp { get; set; }
        public string @event { get; set; }
    }

    public class JournalFSDJump
    {
        //public DateTime timestamp { get; set; }
        //public string _event { get; set; }
        /*public bool Taxi { get; set; }
        public bool Multicrew { get; set; }*/
        public string StarSystem { get; set; }
        public long SystemAddress { get; set; }
        /*public float[] StarPos { get; set; }
        public string SystemAllegiance { get; set; }
        public string SystemEconomy { get; set; }
        public string SystemEconomy_Localised { get; set; }
        public string SystemSecondEconomy { get; set; }
        public string SystemSecondEconomy_Localised { get; set; }
        public string SystemGovernment { get; set; }
        public string SystemGovernment_Localised { get; set; }
        public string SystemSecurity { get; set; }
        public string SystemSecurity_Localised { get; set; }
        public int Population { get; set; }
        public string Body { get; set; }
        public int BodyID { get; set; }
        public string BodyType { get; set; }
        public float JumpDist { get; set; }
        public float FuelUsed { get; set; }
        public float FuelLevel { get; set; }*/
    }

    public class JournalFSSDiscoveryScan
    {
        //public DateTime timestamp { get; set; }
        //public string _event { get; set; }
        public float Progress { get; set; }
        public int BodyCount { get; set; }
        //public int NonBodyCount { get; set; }
        //public string SystemName { get; set; }
        //public long SystemAddress { get; set; }
    }

    /*
    public class Parent
    {
        public int Ring { get; set; }
        public int Star { get; set; }
    }
    */
    /*
    public class Ring
    {
        public string Name { get; set; }
        public string RingClass { get; set; }
        public float MassMT { get; set; }
        public float InnerRad { get; set; }
        public float OuterRad { get; set; }
    }
    */
    public class JournalScan
    {
        //public DateTime timestamp { get; set; }
        //public string _event { get; set; }
        //public string ScanType { get; set; }
        public string BodyName { get; set; }
        public int BodyID { get; set; }
        //public Parent[] Parents { get; set; }
        public string StarSystem { get; set; }
        public long SystemAddress { get; set; }
        public float DistanceFromArrivalLS { get; set; }
        /*public bool TidalLock { get; set; }*/
        public string TerraformState { get; set; }
        public string PlanetClass { get; set; }
        /*public string Atmosphere { get; set; }
        public string AtmosphereType { get; set; }
        public Atmospherecomposition[] AtmosphereComposition { get; set; }
        public string Volcanism { get; set; }*/
        public float MassEM { get; set; }
        /*public float Radius { get; set; }
        public float SurfaceGravity { get; set; }
        public float SurfaceTemperature { get; set; }
        public float SurfacePressure { get; set; }*/
        //public bool Landable { get; set; }
        /*public Composition Composition { get; set; }
        public float SemiMajorAxis { get; set; }
        public float Eccentricity { get; set; }
        public float OrbitalInclination { get; set; }
        public float Periapsis { get; set; }
        public float OrbitalPeriod { get; set; }
        public float AscendingNode { get; set; }
        public float MeanAnomaly { get; set; }
        public float RotationPeriod { get; set; }
        public float AxialTilt { get; set; }*/
        public bool WasDiscovered { get; set; }
        public bool WasMapped { get; set; }
        public string StarType { get; set; }
        /*public int Subclass { get; set; }
        public float StellarMass { get; set; }
        public float AbsoluteMagnitude { get; set; }
        public int Age_MY { get; set; }
        public string Luminosity { get; set; }*/
        //public Ring[] Rings { get; set; }
    }

    /*public class Composition
    {
        public float Ice { get; set; }
        public float Rock { get; set; }
        public float Metal { get; set; }
    }*/
    /*
    public class Atmospherecomposition
    {
        public string Name { get; set; }
        public float Percent { get; set; }
    }*/

    public class JournalFSSAllBodiesFound
    {
        //public DateTime timestamp { get; set; }
        //public string _event { get; set; }
        //public string SystemName { get; set; }
        //public long SystemAddress { get; set; }
        public int Count { get; set; }
    }

    public class JournalFSDTarget
    {
        //public DateTime timestamp { get; set; }
        //public string _event { get; set; }
        public string Name { get; set; }
        public long SystemAddress { get; set; }
        public string StarClass { get; set; }
        public int RemainingJumpsInRoute { get; set; }
    }

    public class JournalStartJump
    {
        //public DateTime timestamp { get; set; }
        //public string _event { get; set; }
        public string JumpType { get; set; }
        public string StarSystem { get; set; }
        public long SystemAddress { get; set; }
        public string StarClass { get; set; }
    }

    public class JournalSAAScanComplete
    {
        //public DateTime timestamp { get; set; }
        //public string _event { get; set; }
        public string BodyName { get; set; }
        //public long SystemAddress { get; set; }
        public int BodyID { get; set; }
        public int ProbesUsed { get; set; }
        public int EfficiencyTarget { get; set; }
    }

    public class JournalMusic
    {
        //public DateTime timestamp { get; set; }
        //public string _event { get; set; }
        public string MusicTrack { get; set; }
    }

    public class JournalLoadGame
    {
        //public DateTime timestamp { get; set; }
        //public string _event { get; set; }
        //public string FID { get; set; }
        public string Commander { get; set; }
        /*public bool Horizons { get; set; }
        public bool Odyssey { get; set; }
        public string Ship { get; set; }
        public int ShipID { get; set; }
        public string ShipName { get; set; }
        public string ShipIdent { get; set; }
        public float FuelLevel { get; set; }
        public float FuelCapacity { get; set; }
        public string GameMode { get; set; }
        public long Credits { get; set; }
        public int Loan { get; set; }
        public string language { get; set; }
        public string gameversion { get; set; }
        public string build { get; set; }*/
    }

}
