using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace VkStatusTranslate
{
    static public class singlton
    {
        public static string login = "";
        public static string password = "";
        public static ulong appId = 1;
        public static ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        public static string status = "";
    }
}
