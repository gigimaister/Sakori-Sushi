using RealWorldApp.Models;
using RealWorldApp.Services;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RealWorldApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductDetailPage : ContentPage
    {
        private int _productId;
        
        public ProductDetailPage(int productId)
        {
            InitializeComponent();
            GetProductDetails(productId);
            _productId = productId;
        }

        // Get Product Details
        private async void GetProductDetails(int productId)
        {
            var product = await ApiService.GetProductById(productId);
            LblName.Text = product.name;
            LblDetail.Text = product.detail;
            ImgProduct.Source = product.FullImageUrl;
            LblPrice.Text = product.price.ToString();
            LblTotalPrice.Text = LblPrice.Text;

        }

        // X Button Click => Back To Product List Page
        private void TapBack_Tapped(object sender, System.EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        // Add To Cart Button
        private async void BtnAddToCart_Clicked(object sender, System.EventArgs e)
        {            
            var addToCart = new AddToCart();
            addToCart.Qty = LblQty.Text;
            addToCart.Price = LblPrice.Text;
            addToCart.TotalAmount = LblTotalPrice.Text;
            addToCart.ProductId = _productId;
            addToCart.CustomerId = Preferences.Get("userId", 0);

            var response = await ApiService.AddItemsInCart(addToCart);

            // If Added Successfuly
            if (response)
            {
                await DisplayAlert("", TraslatedMessages.Alert_Added_Items_To_Cart(), TraslatedMessages.Alert_Dismiss());
            }
            else
            {
                await DisplayAlert(TraslatedMessages.Alert_Oops(), TraslatedMessages.Alert_Something_Went_Wrong(), TraslatedMessages.Alert_Dismiss());
            }
        }

        // Plus Button Tapped
        private void TapIncrement_Tapped(object sender, System.EventArgs e)
        {
            var LblQtyInt = Convert.ToInt32(LblQty.Text);
            LblQtyInt += 1;
            LblQty.Text = LblQtyInt.ToString();
        }

        // Minus Button Tapped
        private void TapDecrement_Tapped(object sender, EventArgs e)
        {
            if (LblQty.Text == "1") return;
            var LblQtyInt = Convert.ToInt32(LblQty.Text);
            LblQtyInt -= 1;           
            LblQty.Text = LblQtyInt.ToString();
        }
    }
}