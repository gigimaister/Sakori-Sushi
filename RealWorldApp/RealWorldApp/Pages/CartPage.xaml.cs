using RealWorldApp.Models;
using RealWorldApp.Services;
using System;
using System.Collections.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RealWorldApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartPage : ContentPage
    {
        public ObservableCollection<ShoppingCartItem> ShoppingCartItemCollection;
        public CartPage()
        {
            InitializeComponent();
            ShoppingCartItemCollection = new ObservableCollection<ShoppingCartItem>();
            GetShoppingCartItems();
            GetTotalPrice();
        }

        // GET Total Price
        private async void GetTotalPrice()
        {
            var totalPrice = await ApiService.GetCartSubTotal(Preferences.Get("userId", 0));
            LblTotalPrice.Text = totalPrice.subTotal.ToString();
        }

        // GET Shopping Cart Items
        private async void GetShoppingCartItems()
        {
            var shoppingCartItems = await ApiService.GetShoppingCartItems(Preferences.Get("userId", 0));

            foreach (var cartItem in shoppingCartItems)
            {
                ShoppingCartItemCollection.Add(cartItem);
            }

            LvShoppingCart.ItemsSource = ShoppingCartItemCollection;
        }

        // Back Arrow Tapped
        private void TapBack_Tapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        // Clear Cart
        private async void TapClearCart_Tapped(object sender, EventArgs e)
        {
            var response = await ApiService.ClearShoppingCart(Preferences.Get("userId", 0));
            if (response)
            {
                await DisplayAlert("", TraslatedMessages.Alert_Cleared_Cart(), TraslatedMessages.Alert_Dismiss());
                LvShoppingCart.ItemsSource = null;
                LblTotalPrice.Text = "0";               
            }
            else
            {
                await DisplayAlert(TraslatedMessages.Alert_Oops(), TraslatedMessages.Alert_Something_Went_Wrong(), TraslatedMessages.Alert_Dismiss());
            }
        }

        // Proceed Button Clicked
        private void BtnProceed_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new PlaceOrderPage(Convert.ToDouble(LblTotalPrice.Text)));
        }
    }
}