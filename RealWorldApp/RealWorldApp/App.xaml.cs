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

            // Get Access Token For One Time Login
            var accesstoken = Preferences.Get("accessToken", string.Empty);

            // If No Token Redirect To Register Page
            if (string.IsNullOrEmpty(accesstoken))
            {
                MainPage = new NavigationPage(new SignUpPage());
            }
            else
            {
                MainPage = new NavigationPage(new HomePage());
            }
            
            
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
