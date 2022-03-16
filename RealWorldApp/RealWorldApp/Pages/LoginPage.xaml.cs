using RealWorldApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RealWorldApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async  void TapBackArrow_Tapped(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void BtnLogin_Clicked(object sender, EventArgs e)
        {
            var response = await ApiService.Login(EntEmail.Text, EntPassword.Text);

            // Store Uname & Pwd For Expired Token Later
            Preferences.Set("email", EntEmail.Text);
            Preferences.Set("password", EntPassword.Text);


            // If Login Successfull 
            if (response)
            {
                // Move To Home Page
                Application.Current.MainPage = new NavigationPage(new HomePage());
            }
            else
            {               
                await DisplayAlert(TraslatedMessages.Alert_Oops(), TraslatedMessages.Alert_Something_Went_Wrong(), TraslatedMessages.Alert_Dismiss());
            }
        }
    }
}