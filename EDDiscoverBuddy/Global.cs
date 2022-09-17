using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDDiscoverBuddy
{
    public static class Globals
    {
        public static bool In(this string source, params string[] list)
        {
            if (null == source) throw new ArgumentNullException("source");
            return list.Contains(source, StringComparer.OrdinalIgnoreCase);
        }

        public static int StarType(string source)
        {
            switch (source.ToLower().Trim())
            {
                // Main sequence
                case "o (blue-white) star": return 1;

                case "b (blue-white) star": return 2;
                case "b (blue-white super giant) star": return 201;

                case "a (blue-white) star": return 3;
                case "a (blue-white super giant) star": return 301;

                case "f (white) star": return 4;
                case "f (white super giant) star": return 401;

                case "g (white-yellow) star": return 5;
                case "g (white-yellow super giant) star": return 5001;

                case "k (yellow-orange) star": return 6;
                case "k (yellow-orange giant) star": return 601;

                case "m (red dwarf) star": return 7;
                case "m (red giant) star": return 701;
                case "m (red super giant) star": return 702;

                case "l (brown dwarf) star": return 8;
                case "t (brown dwarf) star": return 9;
                case "y (brown dwarf) star": return 10;

                // proto stars
                case "t tauri star": return 11;
                case "herbig ae/be star": return 12;
                // wolf-rayet
                case "wolf-rayet star": return 21;
                case "wolf-rayet n star": return 22;
                case "wolf-rayet nc star": return 23;
                case "wolf-rayet c star": return 24;
                case "wolf-rayet o star": return 25;

                // carbon stars
                case "cs star": return 31; // check in game
                case "c star": return 32;
                case "cn star": return 33;
                case "cj star": return 34; // check in game
                case "ch star": return 35; // check in game
                case "chd star": return 36; // check in game

                case "ms-type star": return 41; // check in game
                case "s-type star": return 42; // check in game

                // white dwarfs
                case "white dwarf (d) star": return 51;
                case "white dwarf (da) star": return 501;
                case "white dwarf (dab) star": return 502;
                case "white dwarf (dao) star": return 503;
                case "white dwarf (daz) star": return 504;
                case "white dwarf (dav) star": return 505;
                case "white dwarf (db) star": return 506;
                case "white dwarf (dbz) star": return 507;
                case "white dwarf (dbv) star": return 508;
                case "white dwarf (do) star": return 509;
                case "white dwarf (dov) star": return 510;
                case "white dwarf (dq) star": return 511;
                case "white dwarf (dc) star": return 512;
                case "white dwarf (dcv) star": return 513;
                case "white dwarf (dx) star": return 514;

                case "neutron star": return 91;
                case "black hole": return 92; // check in game
                case "supermassive black hole": return 93; // check in game

                case "x": return 94; // exotic?? // check in game

                case "rogueplanet": return 111; // check in game
                case "nebula": return 112; // check in game
                case "stellarremnantnebula": return 113; // check in game


                // Main sequence
                case "o": return 1;

                case "b": return 2;
                case "b_bluewhitesupergiant": return 201;

                case "a": return 3;
                case "a_bluewhitesupergiant": return 301;

                case "f": return 4;
                case "f_whitesupergiant": return 401;

                case "g": return 5;
                case "g_whitesupergiant": return 5001;

                case "k": return 6;
                case "k_orangegiant": return 601;

                case "m": return 7;
                case "m_redgiant": return 701;
                case "m_redsupergiant": return 702;

                case "l": return 8;
                case "t": return 9;
                case "y": return 10;

                // Proto stars
                case "tts": return 11;
                case "aebe": return 12;

                // Wolf-Rayet
                case "w": return 21;
                case "wn": return 22;
                case "wnc": return 23;
                case "wc": return 24;
                case "wo": return 25;

                // carbon stars
                case "cs": return 31;
                case "c": return 32;
                case "cn": return 33;
                case "cj": return 34;
                case "ch": return 35;
                case "chd": return 36;

                case "ms": return 41;
                case "s": return 42;

                // white dwarfs
                case "d": return 51;
                case "da": return 501;
                case "dab": return 502;
                case "dao": return 503;
                case "daz": return 504;
                case "dav": return 505;
                case "db": return 506;
                case "dbz": return 507;
                case "dbv": return 508;
                case "do": return 509;
                case "dov": return 510;
                case "dq": return 511;
                case "dc": return 512;
                case "dcv": return 513;
                case "dx": return 514;

                // Others
                case "n": return 91;

                case "h": return 92;
                case "supermassiveblackhole": return 93;

                default:
                    return 0;
            }
        }

        internal static int PlanetType(string specific_type)
        {

            switch (specific_type.ToLower().Trim())
            {
                case "Metal-rich body": return 1;
                case "High metal content world": return 2;
                case "Rocky body": return 11;
                case "Rocky Ice world": return 12; // Check in game
                case "Icy body": return 21;
                case "Earth-like world": return 31;
                case "Water world": return 41;
                case "Water giant": return 42; // Check in game
                case "Water giant with life": return 43; // Check in game    
                case "Ammonia world": return 51;
                case "Gas giant with water-based life": return 61; // Check in game
                case "Gas giant with ammonia-based life": return 62; // Check in game
                case "Class I gas giant": return 71;
                case "Class II gas giant": return 72;
                case "Class III gas giant": return 73;
                case "Class IV gas giant": return 74;
                case "Class V gas giant": return 75;
                case "Helium-rich gas giant": return 81;
                case "Helium gas giant": return 82;
                case "metal rich body": return 1;
                case "metal-rich body": return 1;
                case "high metal content body": return 2;
                case "high metal content world": return 2;

                case "rocky body": return 11;

                case "rocky ice body": return 12;
                case "rocky ice world": return 12;

                case "icy body": return 21;

                case "earthlike body": return 31;
                case "earthlike": return 31;
                case "earth-like world": return 31;

                case "water world": return 41;
                case "water giant": return 42;
                case "water giant with life": return 43;

                case "ammonia world": return 51;

                case "gas giant with water based life": return 61;
                case "gas giant with water-based life": return 61;
                case "gas giant with ammonia based life": return 62;
                case "gas giant with ammonia-based life": return 62;

                case "sudarsky class i gas giant": return 71;
                case "class i gas giant": return 71;
                case "sudarsky class ii gas giant": return 72;
                case "class ii gas giant": return 72;
                case "sudarsky class iii gas giant": return 73;
                case "class iii gas giant": return 73;
                case "sudarsky class iv gas giant": return 74;
                case "class iv gas giant": return 74;
                case "sudarsky class v gas giant": return 75;
                case "class v gas giant": return 75;

                case "helium rich gas giant": return 81;
                case "helium-rich gas giant": return 81;
                case "helium gas giant": return 82;
                default:
                    return 0;
            }
        }
    }
}
