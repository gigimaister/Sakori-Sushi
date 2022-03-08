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
                await DisplayAlert(HebrewMessages.Alert_Pwd_Missmatch, HebrewMessages.Alert_pls_Check_pwds, HebrewMessages.Alert_Dismiss);
            }
            else
            {

                // ApiService Register Method
                bool response = await ApiService.RegisterUser(EntName.Text, EntEmail.Text, EntPassword.Text);
                if (response)
                {
                    await DisplayAlert(HebrewMessages.Alert_Hello_Kampai, HebrewMessages.Alert_Account_Created, HebrewMessages.Alert_Dismiss);
                }
                else
                {
                    await DisplayAlert(HebrewMessages.Alert_Oops, HebrewMessages.Alert_Something_Went_Wrong, HebrewMessages.Alert_Dismiss);
                }
            }

        }
    }
}