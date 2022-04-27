using RealWorldApp.Models;
using RealWorldApp.Services;
using System;
using System.Collections.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;

namespace RealWorldApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartPage : ContentPage
    {
        // ObservCol For Shopping CartItems
        public ObservableCollection<ShoppingCartItem> ShoppingCartItemCollection;
        // Bool To Prevent Duoble Click
        public bool IsClickedOnce;
        // Bool Prevent For Proceed Buttin Click
        public bool IsProceedBtnClick;
        public CartPage()
        {
            InitializeComponent();
            ShoppingCartItemCollection = new ObservableCollection<ShoppingCartItem>();
            //GetShoppingCartItems();
            //GetTotalPrice();        
        }

        // GET Total Price
        private async void GetTotalPrice()
        {
            var totalPrice = await ApiService.GetCartSubTotal(Preferences.Get(Constants.Preference.UserId, 0));
            LblTotalPrice.Text = totalPrice.subTotal.ToString();
        }

        // GET Shopping Cart Items
        private async void GetShoppingCartItems()
        {
            List<ShoppingCartItem> shoppingCartItems = new List<ShoppingCartItem>();
            shoppingCartItems = await ApiService.GetShoppingCartItems(Preferences.Get(Constants.Preference.UserId, 0));

            foreach (var cartItem in shoppingCartItems)
            {
                ShoppingCartItemCollection.Add(cartItem);
            }

            LvShoppingCart.ItemsSource = ShoppingCartItemCollection;

            // If We Don't Have Any Items In Cart Disabled Proceed Button
            if (LvShoppingCart.ItemsSource == null || ShoppingCartItemCollection.Count == 0)
            {
                BtnProceed.IsEnabled = false;
                return;
            }
        }

        // Back Arrow Tapped
        private void TapBack_Tapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        // Clear Cart
        private async void TapClearCart_Tapped(object sender, EventArgs e)
        {
            // If We Don't Have Any Items In Cart Show Alert
            if (LvShoppingCart.ItemsSource == null || ShoppingCartItemCollection.Count == 0)
            {
                await DisplayAlert("", TraslatedMessages.Alert_No_Items_In_Cart(), TraslatedMessages.Alert_Dismiss());
                return;
            }

            // Alert User If Delete
            bool answer = await DisplayAlert("", TraslatedMessages.Alert_Delete_All_Products(), TraslatedMessages.Alert_Yes(), TraslatedMessages.Alert_No());
            if (!answer) return;
            
            var response = await ApiService.ClearShoppingCart(Preferences.Get(Constants.Preference.UserId, 0));
            if (response)
            {
                await DisplayAlert("", TraslatedMessages.Alert_Cleared_Cart(), TraslatedMessages.Alert_Dismiss());
                LvShoppingCart.ItemsSource = null;
                LblTotalPrice.Text = "0";
                BtnProceed.IsEnabled = false;
            }
            else
            {
                await DisplayAlert(TraslatedMessages.Alert_Oops(), TraslatedMessages.Alert_Something_Went_Wrong(), TraslatedMessages.Alert_Dismiss());
            }
        }

        // Proceed Button Clicked
        private void BtnProceed_Clicked(object sender, EventArgs e)
        {
            // Prevent Double Click
            if (IsClickedOnce) return;
            IsClickedOnce = true;
            Navigation.PushModalAsync(new PlaceOrderPage(Convert.ToDouble(LblTotalPrice.Text)));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();          
            // Init Duplicate Click Preventor
            IsClickedOnce = false;
            ShoppingCartItemCollection.Clear();
            GetShoppingCartItems();
            GetTotalPrice();

        }

        private void LvShoppingCart_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            // If Selection Is Null Do Nothing
            if (e.SelectedItem == null) return;
            // Get Current Category Selection
            var currentSelection = e.SelectedItem as ShoppingCartItem;
            
            // Add Selected SideDishes (If Any) To Product To Be Pushed To Edit Page
            if (currentSelection.SideDishToCarts.Count > 0)
            {
                currentSelection.Product.SideDishList = new List<SideDish>();
                foreach (var sDish in currentSelection.SideDishToCarts)
                {
                    currentSelection.Product.SideDishList.Add(sDish.SideDish);
                }
            }
           
           
            // Go To Product Detail Page
            Navigation.PushModalAsync(new ProductDetailPage(currentSelection.Product.id, currentSelection));
            // When Navigting Back To Product List Page We Want Unchecked Categories
            ((ListView)sender).SelectedItem = null;
            
        }
    }
}