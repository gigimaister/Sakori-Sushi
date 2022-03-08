using RealWorldApp.Pages;
using System;
using System.Globalization;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RealWorldApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            // Get Device Language And Store It Locally
            var deviceLanguage = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;           
            Preferences.Set("deviceLanguage", deviceLanguage);

            MainPage = new SignUpPage();
            
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
