using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using VkNet.Enums.Filters;
using VkNet.Model;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class login : ContentDialog
    {
        public login()
        {
            this.InitializeComponent();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            try
            {
                singlton.api.Authorize(new ApiAuthParams()
                {
                    Login = loginTB.Text,
                    Password = passwordTB.Text,
                    ApplicationId = singlton.appId,
                    Settings = Settings.All
                });
            }
            catch (Exception ex)
            {
                Title = ex.Message+"\n"+ loginTB.Text + passwordTB.Text;
                passwordTB.BorderBrush = new SolidColorBrush(Colors.Red);
                loginTB.BorderBrush = new SolidColorBrush(Colors.Red);
                args.Cancel = true;
            }
        }

    }
}
