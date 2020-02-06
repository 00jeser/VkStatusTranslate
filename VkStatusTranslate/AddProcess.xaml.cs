using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System.Diagnostics;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Диалоговое окно содержимого" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace VkStatusTranslate
{
    public sealed partial class AddProcess : ContentDialog
    {
        public AddProcess()
        {
            this.InitializeComponent();
            foreach (ProcessDiagnosticInfo ii in ProcessDiagnosticInfo.GetForProcesses())
            {
                procces.Items.Add(ii.ExecutableFileName);
            }
        }

        private async void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            title.BorderBrush = new SolidColorBrush(new Color() { R = 255, G = 255, B = 255, A = 40 });
            procces.BorderBrush = new SolidColorBrush(new Color() { R = 255, G = 255, B = 255, A = 40 });
            if (title.Text == "")
            {
                title.BorderBrush = new SolidColorBrush(Colors.Red);
                args.Cancel = true;
            }
            if (procces.Text == "")
            {
                procces.BorderBrush = new SolidColorBrush(Colors.Red);
                args.Cancel = true;
            }
            if (args.Cancel) return;
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile sampleFile = await storageFolder.GetFileAsync("procs.txt");
            var txt = await Windows.Storage.FileIO.ReadTextAsync(sampleFile);
            await Windows.Storage.FileIO.WriteTextAsync(sampleFile, txt + "\n" + (procces.Text + "~" + title.Text));
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
