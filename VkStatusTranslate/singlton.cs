using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;
using Windows.Storage;

namespace VkStatusTranslate
{
    static public class singlton
    {
        public static string login = "-";
        public static string password = "-";
        public static ulong appId = 6739698;
        public static ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        public static VkApi api = new VkApi();
        private static string statusV = "";
        public static string status { get => statusV; set 
            {
                statusV = value;
                var a = api.Status.Set(value/* + " (статус добавлен автоматически через https://github.com/OOjeser/VkStatusTranslate)"*/).ToString();
            } }

        
    }
}
