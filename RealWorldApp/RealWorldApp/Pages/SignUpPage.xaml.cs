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
    public partial class SignUpPage : ContentPage
    {
        public SignUpPage()
        {
            InitializeComponent();
        }

        // Btn SignUp Click
        private async void BtnSignUp_Clicked(object sender, EventArgs e)
        {
            // If Passwords Doesn't Match
            if (!EntPassword.Text.Equals(EntConfirmPassword.Text))
            {
                await DisplayAlert(TraslatedMessages.Alert_Pwd_Missmatch(), TraslatedMessages.Alert_pls_Check_pwds(), TraslatedMessages.Alert_Dismiss());
            }

            else
            {
                // ApiService Register Method
                bool response = await ApiService.RegisterUser(EntName.Text, EntEmail.Text, EntPassword.Text);

                if (response)
                {
                    await DisplayAlert(TraslatedMessages.Alert_Hello_Kampai(), TraslatedMessages.Alert_Account_Created(), TraslatedMessages.Alert_Dismiss());
                }
                else
                {
                    await DisplayAlert(TraslatedMessages.Alert_Oops(), TraslatedMessages.Alert_Something_Went_Wrong(), TraslatedMessages.Alert_Dismiss());
                }
            }

        }
    }
}