using RealWorldApp.Models;
using RealWorldApp.Services;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RealWorldApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlaceOrderPage : ContentPage
    {
        private double _tatoalPrice;
        public PlaceOrderPage(double totalPrice)
        {
            InitializeComponent();
            _tatoalPrice = totalPrice;
        }

        // Back Arrow Tapped
        private void TapBack_Tapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        // Place Order Button Clicked
        private async void BtnPlaceOrder_Clicked(object sender, EventArgs e)
        {
            var order = new Order();
            order.fullName = EntName.Text;
            order.phone = EntPhone.Text;
            order.address = EntAddress.Text;
            order.userId = Preferences.Get(Constants.Preference.UserId, 0);
            order.orderTotal = _tatoalPrice;

            var response = await ApiService.PlaceOrder(order);

            // If Order Posted Successefuly
            if (response != null)
            {
                await DisplayAlert("", "TO DO", "OK");
                Application.Current.MainPage = new NavigationPage(new HomePage());
            }
            else
            {
                await DisplayAlert(TraslatedMessages.Alert_Oops(), TraslatedMessages.Alert_Something_Went_Wrong(), TraslatedMessages.Alert_Dismiss());
            }
        }
    }
}