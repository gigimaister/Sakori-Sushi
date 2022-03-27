using RealWorldApp.Models;
using RealWorldApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RealWorldApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductDetailPage : ContentPage
    {
        private int _productId;
        public ObservableCollection<SideDish> MeatSideDish { get; set; }
        public ObservableCollection<SideDish> FishSideDish { get; set; }
        public ObservableCollection<SideDish> VegSideDish { get; set; }

        public ProductDetailPage(int productId)
        {
            InitializeComponent();

            // Side Dish Lists
            MeatSideDish = new ObservableCollection<SideDish>();
            FishSideDish = new ObservableCollection<SideDish>();
            VegSideDish = new ObservableCollection<SideDish>();

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

            // Check If Product Selectable Then We Want To Check What Kind Of Side Dishes We Can Add
            if (product.IsProductSelectable)
            {
                // Meat List
                if (product.IsMeatSelect)
                {
                   var meatSideDishes = await ApiService.GetSideDishSelection((int)MainDish.Meat);
                   foreach(var meatDish in meatSideDishes)
                    {
                        MeatSideDish.Add(meatDish);
                    }                  
                }
                // Fish List
                if (product.IsFishSelect)
                {
                    var fishSideDishes = await ApiService.GetSideDishSelection((int)MainDish.Fish);
                    foreach (var fishDish in fishSideDishes)
                    {
                        FishSideDish.Add(fishDish);
                    }                                       
                }
                // Veg List
                if (product.IsVegSelect)
                {
                    var vegSideDishes = await ApiService.GetSideDishSelection((int)MainDish.Veg);
                    foreach (var vegDish in vegSideDishes)
                    {
                        VegSideDish.Add(vegDish);
                    }
                }
            }          
            
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
            // Update Labael Total Price
            LblTotalPrice.Text = (LblQtyInt * Convert.ToInt32(LblPrice.Text)).ToString();
        }

        // Minus Button Tapped
        private void TapDecrement_Tapped(object sender, EventArgs e)
        {
            if (LblQty.Text == "1") return;
            var LblQtyInt = Convert.ToInt32(LblQty.Text);
            LblQtyInt -= 1;           
            LblQty.Text = LblQtyInt.ToString();
            // Update Labael Total Price
            LblTotalPrice.Text = (LblQtyInt * Convert.ToInt32(LblPrice.Text)).ToString();
        }
    }
}