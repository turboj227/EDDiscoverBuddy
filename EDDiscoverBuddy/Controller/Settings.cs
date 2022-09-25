using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDDiscoverBuddy.Controller
{
    internal static class Settings
    {
        public static string ReadFrom = "C:\\Users\\jvand\\Saved Games\\Frontier Developments\\Elite Dangerous\\Journal.2022-09-24T194343.01.log";
        public static bool StatsFromStart = true;
        public static bool ShowIngameOverlay = true;
        public static bool StatusScreenOnTop = true;
        public static int InterestedFrom = 800000;
        //ED Journal folder
        //ED Journal File filter settings
        //Position overlay
        //Color overlay
        //Font overlay
        public static int StatusWindowPositionTop = 275;
        public static int StatusWindowPositionLeft = 1560;
        //Save settings in config file, read file. when corrupt use default values
    }
}
