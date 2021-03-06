using RealWorldApp.Services;
using System;
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

                // If Successfull Logon
                if (response)
                {
                    await DisplayAlert(TraslatedMessages.Alert_Hello_Kampai(), TraslatedMessages.Alert_Account_Created(), TraslatedMessages.Alert_Dismiss());
                    // Redirect To Login
                    await Navigation.PushModalAsync(new LoginPage());
                }
                else
                {
                    await DisplayAlert(TraslatedMessages.Alert_Oops(), TraslatedMessages.Alert_Something_Went_Wrong(), TraslatedMessages.Alert_Dismiss());
                }
            }

        }

        // User Clicked Login
        private async void BtnLogin_Clicked(object sender, EventArgs e)
        {        
            // Redirect To Login
            await Navigation.PushModalAsync(new LoginPage());

            // Set Register Values To Empty When Leaving Page
            EntName.Text = string.Empty;
            EntEmail.Text = string.Empty;
            EntPassword.Text = string.Empty;
            EntConfirmPassword.Text = string.Empty;

        }
    }
}