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
using Windows.UI.Core.Preview;
using Windows.ApplicationModel.Core;

namespace VkStatusTranslate
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
                Windows.Storage.StorageFile sampleFile = await storageFolder.GetFileAsync("pass.txt");
                var lp = (await Windows.Storage.FileIO.ReadTextAsync(sampleFile)).Split('\n');
                singlton.api.Authorize(new ApiAuthParams()
                {
                    Login = lp[1],
                    Password = lp[0],
                    ApplicationId = singlton.appId,
                    Settings = Settings.All
                });
            }
            catch (Exception ex)
            {
                answTextBlock.Text = ex.Message;
            }
            DiagnosticAccessStatus diagnosticAccessStatus = await AppDiagnosticInfo.RequestAccessAsync();
            var requestStatus = await Windows.ApplicationModel.Background.BackgroundExecutionManager.RequestAccessAsync();

            DispatcherTimer timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 0, 20, 0) }; // 1 секунда
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private async void Timer_Tick(object sender, object e)
        {
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            try
            {
                var s = "";
                Windows.Storage.StorageFile sampleFile = await storageFolder.GetFileAsync("procs.txt");
                list.Items.Clear();
                foreach (var i in (await Windows.Storage.FileIO.ReadTextAsync(sampleFile)).Split('\n'))
                {
                    if (i.IndexOf('~') != -1)
                    {
                        if (i.Split('~')[0] == "default" && s == "")
                        {
                            s = i.Split('~')[1];
                        }
                        else
                        {
                            foreach (ProcessDiagnosticInfo ii in ProcessDiagnosticInfo.GetForProcesses())
                            {
                                if (i.Split('~')[0] == ii.ExecutableFileName)
                                {
                                    s = i.Split('~')[1];
                                }
                            }
                        }
                        Grid g = new Grid() { HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Stretch };
                        ListViewItem lvi = new ListViewItem() { Content = g, HorizontalAlignment = HorizontalAlignment.Stretch, HorizontalContentAlignment = HorizontalAlignment.Stretch };
                        g.Children.Add(new TextBlock() { Text = i.Split('~')[0], HorizontalAlignment = HorizontalAlignment.Left });
                        g.Children.Add(new TextBlock() { Text = i.Split('~')[1], HorizontalAlignment = HorizontalAlignment.Right });
                        list.Items.Add(lvi);
                    }
                }
                if (s != singlton.status)
                    singlton.status = s;
                answTextBlock.Text = s;
                //await Windows.Storage.FileIO.WriteTextAsync(sampleFile, "Test~System\nexplorer.exe~играет в проводник windows");
            }
            catch (FileNotFoundException ex)
            {
                Windows.Storage.StorageFile sampleFile = await storageFolder.CreateFileAsync("procs.txt", Windows.Storage.CreationCollisionOption.ReplaceExisting);
                answTextBlock.Text = ex.Message;
                await Windows.Storage.FileIO.WriteTextAsync(sampleFile, "default~ ");
            }
            catch (Exception ex)
            {
                answTextBlock.Text = ex.Message;
            }
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            singlton.status = "";
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var a = new AddProcess();
            await a.ShowAsync();
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            /*var a = new login();
            await a.ShowAsync();*/
            loginGrd.Width = loginGrd.Width == 315 ? 0 : 315;
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            try
            {
                /*singlton.api.Authorize(new ApiAuthParams()
                {
                    Login = singlton.login,
                    Password = singlton.password,
                    ApplicationId = singlton.appId,
                    Settings = Settings.All
                });*/
                Windows.Storage.StorageFile sampleFile = await storageFolder.GetFileAsync("pass.txt");
                await Windows.Storage.FileIO.WriteTextAsync(sampleFile, passwordTB.Text + "\n" + loginTB.Text);
                await CoreApplication.RequestRestartAsync("-fastInit -level 1 -foo");
            }
            catch (FileNotFoundException)
            {
                Windows.Storage.StorageFile sampleFile = await storageFolder.CreateFileAsync("pass.txt", Windows.Storage.CreationCollisionOption.ReplaceExisting);
                await Windows.Storage.FileIO.WriteTextAsync(sampleFile, passwordTB.Text + "\n" + loginTB.Text);
                Process.GetCurrentProcess().Close();
            }
            catch (Exception ex)
            {
                answTextBlock.Text = ex.Message;
            }
        }
    }
}
