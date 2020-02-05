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
using Windows.System.Diagnostics;
using Windows.UI;
using Windows.System;

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

        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            DiagnosticAccessStatus diagnosticAccessStatus = await AppDiagnosticInfo.RequestAccessAsync();
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            try
            {
                var s = "";
                Windows.Storage.StorageFile sampleFile = await storageFolder.GetFileAsync("procs.txt");
                foreach (var i in (await Windows.Storage.FileIO.ReadTextAsync(sampleFile)).Split('\n')) 
                {
                    foreach (ProcessDiagnosticInfo ii in ProcessDiagnosticInfo.GetForProcesses()) 
                    {
                        s += " " + ii.ExecutableFileName;
                        if (i.Split('~')[0] == ii.ExecutableFileName) 
                        {
                            singlton.status = i.Split('~')[1];
                        }
                    }
                    Grid g = new Grid() { HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Stretch };
                    ListViewItem lvi = new ListViewItem() { Content = g, HorizontalAlignment = HorizontalAlignment.Stretch, HorizontalContentAlignment = HorizontalAlignment.Stretch };
                    g.Children.Add(new TextBlock() { Text = i.Split('~')[0], HorizontalAlignment = HorizontalAlignment.Left });
                    g.Children.Add(new TextBlock() { Text = i.Split('~')[1], HorizontalAlignment = HorizontalAlignment.Right });
                    list.Items.Add(lvi);
                }
                answTextBlock.Text = singlton.status;
                //await Windows.Storage.FileIO.WriteTextAsync(sampleFile, "Test~System\nexplorer.exe~в проводнике");
            }
            catch (Exception ex) 
            {
                Windows.Storage.StorageFile sampleFile = await storageFolder.CreateFileAsync("procs.txt", Windows.Storage.CreationCollisionOption.ReplaceExisting);
                answTextBlock.Text = ex.Message;
                await Windows.Storage.FileIO.WriteTextAsync(sampleFile, "Test~System");
            }

            /*VkApi api = new VkApi();
            api.Authorize(new ApiAuthParams()
            {
                Login = singlton.login,
                Password = singlton.password,
                ApplicationId = singlton.appId,
                Settings = Settings.All
            });
            var respone = api.Status.Set("test1");
            answTextBlock.Text = respone.ToString();*/
        }
    }
}
