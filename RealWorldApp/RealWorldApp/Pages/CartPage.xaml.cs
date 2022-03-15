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
    }
}