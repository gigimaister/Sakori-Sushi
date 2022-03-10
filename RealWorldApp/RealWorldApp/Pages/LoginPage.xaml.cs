using RealWorldApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            // If Login Successfull 
            if (response)
            {
                Application.Current.MainPage = new NavigationPage(new HomePage());
            }
            else
            {               
                await DisplayAlert(TraslatedMessages.Alert_Oops(), TraslatedMessages.Alert_Something_Went_Wrong(), TraslatedMessages.Alert_Dismiss());
            }
        }
    }
}