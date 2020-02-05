using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using VkNet;
using VkNet.Model;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using VkNet.Enums.Filters;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Diagnostics;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace VkStatusTranslate
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (var i in (Dictionary<string, string>)singlton.localSettings.Values["Proc"]) 
                {
                    foreach (var ii in Process.GetProcesses()) 
                    {
                        if (i.Key == ii.ProcessName) 
                        {
                            singlton.status = i.Value;
                        }
                    }
                }
            }
            catch (Exception ex) 
            {
                singlton.localSettings.Values["Proc"] = new Dictionary<string, string>();
            }

            VkApi api = new VkApi();
            api.Authorize(new ApiAuthParams()
            {
                Login = singlton.login,
                Password = singlton.password,
                ApplicationId = singlton.appId,
                Settings = Settings.All
            });
            var respone = api.Status.Set("test1");
            answTextBlock.Text = respone.ToString();
        }
    }
}
