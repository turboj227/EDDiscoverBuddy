using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace EDDiscoverBuddy.EDSM
{
    public static class EDSMHelper
    {
        public static string GetBodies(string SystemName, long SystemAddress)
        {
            //https://www.edsm.net/api-system-v1/bodies?systemName=STOCK 1 SECTOR JM-U C3-0

            //return "{\"id\":24363254,\"id64\":53644116185291,\"name\":\"Dryooe Flyou OC-L d8-1561\",\"url\":\"https:\\/\\/www.edsm.net\\/en\\/system\\/bodies\\/id\\/24363254\\/name\\/Dryooe+Flyou+OC-L+d8-1561\",\"bodyCount\":24,\"bodies\":[{\"id\":216132163,\"id64\":53644116185291,\"bodyId\":0,\"name\":\"Dryooe Flyou OC-L d8-1561\",\"discovery\":{\"commander\":\"Chronoskostya\",\"date\":\"2020-06-28 01:42:00\"},\"type\":\"Star\",\"subType\":\"F (White) Star\",\"parents\":null,\"distanceToArrival\":0,\"isMainStar\":true,\"isScoopable\":true,\"age\":2348,\"spectralClass\":\"F3\",\"luminosity\":\"Vb\",\"absoluteMagnitude\":3.532516,\"solarMasses\":1.527344,\"solarRadius\":1.239990107836089,\"surfaceTemperature\":6995,\"orbitalPeriod\":null,\"semiMajorAxis\":null,\"orbitalEccentricity\":null,\"orbitalInclination\":null,\"argOfPeriapsis\":null,\"rotationalPeriod\":1.0600402540393519,\"rotationalPeriodTidallyLocked\":false,\"axialTilt\":0,\"belts\":[{\"name\":\"Dryooe Flyou OC-L d8-1561 A Belt\",\"type\":\"Rocky\",\"mass\":601090000000000,\"innerRadius\":6737200,\"outerRadius\":271630000}],\"updateTime\":\"2020-06-28 01:42:00\"},{\"id\":216132161,\"id64\":36082441135149259,\"bodyId\":1,\"name\":\"Dryooe Flyou OC-L d8-1561 1\",\"discovery\":{\"commander\":\"Chronoskostya\",\"date\":\"2020-06-28 01:41:59\"},\"type\":\"Planet\",\"subType\":\"Metal-rich body\",\"parents\":[{\"Star\":0}],\"distanceToArrival\":12,\"isLandable\":false,\"gravity\":1.8981728620414755,\"earthMasses\":1.245424,\"radius\":5166.248,\"surfaceTemperature\":2807,\"surfacePressure\":491.0432765852455,\"volcanismType\":\"Major Silicate Vapour Geysers\",\"atmosphereType\":\"Hot thick Silicate vapour\",\"atmosphereComposition\":{\"Silicates\":100},\"solidComposition\":{\"Metal\":100,\"Ice\":0,\"Rock\":0},\"terraformingState\":\"Not terraformable\",\"orbitalPeriod\":1.158531686226852,\"semiMajorAxis\":0.024971438072905673,\"orbitalEccentricity\":2.4e-5,\"orbitalInclination\":-0.004176,\"argOfPeriapsis\":103.067411,\"rotationalPeriod\":1.1661384271412036,\"rotationalPeriodTidallyLocked\":false,\"axialTilt\":0.404518,\"updateTime\":\"2022-09-15 18:30:20\"},{\"id\":20775894,\"id64\":null,\"bodyId\":null,\"name\":\"Dryooe Flyou OC-L d8-1561 2\",\"discovery\":{\"commander\":\"PoiPlexed\",\"date\":\"2017-11-21 19:27:23\"},\"type\":\"Planet\",\"subType\":\"Class III gas giant\",\"parents\":null,\"distanceToArrival\":1438,\"isLandable\":false,\"gravity\":14.978740960613097,\"earthMasses\":1999.567749,\"radius\":73691.08,\"surfaceTemperature\":596,\"surfacePressure\":0,\"volcanismType\":\"No volcanism\",\"atmosphereType\":\"No atmosphere\",\"atmosphereComposition\":{\"Hydrogen\":74.32,\"Helium\":25.68},\"solidComposition\":null,\"terraformingState\":\"Not terraformable\",\"orbitalPeriod\":1436.618888888889,\"semiMajorAxis\":2.88225669714696,\"orbitalEccentricity\":0.000141,\"orbitalInclination\":1.273514,\"argOfPeriapsis\":93.880692,\"rotationalPeriod\":1.8868062789351852,\"rotationalPeriodTidallyLocked\":false,\"axialTilt\":0.278469,\"rings\":[{\"name\":\"Dryooe Flyou OC-L d8-1561 2 A Ring\",\"type\":\"Metal Rich\",\"mass\":3440600000000,\"innerRadius\":121590,\"outerRadius\":450670}],\"reserveLevel\":\"Pristine\",\"updateTime\":\"2018-07-05 08:20:55\"},{\"id\":20537458,\"id64\":null,\"bodyId\":null,\"name\":\"Dryooe Flyou OC-L d8-1561 6\",\"discovery\":{\"commander\":\"PoiPlexed\",\"date\":\"2017-11-21 19:30:43\"},\"type\":\"Planet\",\"subType\":\"Water world\",\"parents\":null,\"distanceToArrival\":3064,\"isLandable\":false,\"gravity\":1.649858205130105,\"earthMasses\":2.620476,\"radius\":8038.061,\"surfaceTemperature\":315,\"surfacePressure\":21.413547989143844,\"volcanismType\":\"Metallic Magma\",\"atmosphereType\":\"Thick Carbon dioxide\",\"atmosphereComposition\":{\"Carbon dioxide\":57.15,\"Nitrogen\":41.01,\"Oxygen\":1.78},\"solidComposition\":null,\"terraformingState\":\"Not terraformable\",\"orbitalPeriod\":418.77462962962966,\"semiMajorAxis\":0.04443609257868936,\"orbitalEccentricity\":0.055707,\"orbitalInclination\":-5.325686,\"argOfPeriapsis\":105.878563,\"rotationalPeriod\":1.0466938838310185,\"rotationalPeriodTidallyLocked\":false,\"axialTilt\":-0.327323,\"updateTime\":\"2018-07-04 22:54:04\"}]}";

            HttpClient wc = new HttpClient();
            HttpResponseMessage response = wc.Send(new HttpRequestMessage(HttpMethod.Get, "https://www.edsm.net/api-system-v1/bodies?systemName=" + SystemName));
            string EDSMResult = "";
            if (response.StatusCode == HttpStatusCode.OK)
            {
                EDSMResult = response.Content.ReadAsStringAsync().Result;
            }
            return EDSMResult;
        }

        internal static string GetSystemInfoURL(string SystemName)
        {
            return "https://www.edsm.net/en/system/id/0/name/" + SystemName;
        }
    }
    public class EDSMBodies
    {
        //public int id { get; set; }
        public long id64 { get; set; }
        public string name { get; set; }
        //public string url { get; set; }
        //public int? bodyCount { get; set; }
        public List<Body> bodies { get; set; }
    }

    public class Body
    {
        //public int id { get; set; }
        //public long? id64 { get; set; }
        public int? bodyId { get; set; }
        public string name { get; set; }
        public Discovery discovery { get; set; }
        public string type { get; set; }
        public string subType { get; set; }
        //public List<Parent> parents { get; set; }
        public int distanceToArrival { get; set; }
        //public bool isMainStar { get; set; }
        //public bool isScoopable { get; set; }
/*        public int age { get; set; }
        public string spectralClass { get; set; }
        public string luminosity { get; set; }
        public float absoluteMagnitude { get; set; }*/
        public float solarMasses { get; set; }
        /*public float solarRadius { get; set; }
        public int surfaceTemperature { get; set; }
        public float? orbitalPeriod { get; set; }
        public float? semiMajorAxis { get; set; }
        public float? orbitalEccentricity { get; set; }
        public float? orbitalInclination { get; set; }
        public float? argOfPeriapsis { get; set; }
        public float? rotationalPeriod { get; set; }
        public bool rotationalPeriodTidallyLocked { get; set; }
        public float axialTilt { get; set; }*/
        //public string updateTime { get; set; }
        //public bool isLandable { get; set; }
        /*public float? gravity { get; set; }*/
        public float earthMasses { get; set; }
        /*public float? radius { get; set; }
        public int? surfacePressure { get; set; }
        public string volcanismType { get; set; }
        public string atmosphereType { get; set; }
        public Atmospherecomposition atmosphereComposition { get; set; }
        public object solidComposition { get; set; }*/
        public string terraformingState { get; set; }
    }

    public class Discovery
    {
        public string commander { get; set; }
        //public string date { get; set; }
    }
    /*
    public class Atmospherecomposition
    {
        public float Hydrogen { get; set; }
        public float Helium { get; set; }
    }
    public class Parent
    {
        public int Null { get; set; }
        public int Star { get; set; }
    }
    */

}
