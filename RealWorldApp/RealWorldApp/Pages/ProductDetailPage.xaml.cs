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
        public Product ProductObj { get; set; }
        public bool IsCvSideDishMultiple { get; set; }
        // Bool To Prevent Duoble Click
        public bool IsClickedOnce;
        public ProductDetailPage(int productId)
        {
            InitializeComponent();

            // Side Dish Lists
            MeatSideDish = new ObservableCollection<SideDish>();
            FishSideDish = new ObservableCollection<SideDish>();
            VegSideDish = new ObservableCollection<SideDish>();
            ProductObj = new Product();
            
            GetProductDetails(productId);           
            _productId = productId;
        }     

        // Get Product Details
        private async void GetProductDetails(int productId)
        {
            var product = await ApiService.GetProductById(productId);
            ProductObj = product;

            LblName.Text = product.name;
            LblDetail.Text = product.GetFullDetail();
            ImgProduct.Source = product.FullImageUrl;
            LblPrice.Text = product.price.ToString();
            LblTotalPrice.Text = LblPrice.Text;

            // Check If Product Selectable Then We Want To Check What Kind Of Side Dishes We Can Add
            if (product.IsProductSelectable)
            {
                // Meat List
                if (product.IsMeatSelect)
                {
                    // Set Meat Btn True
                    BtnMeatSelect.IsVisible = true;
                   var meatSideDishes = await ApiService.GetSideDishSelection((int)MainDish.Meat);
                   foreach(var meatDish in meatSideDishes)
                    {
                        MeatSideDish.Add(meatDish);
                    }                  
                }
                // Fish List
                if (product.IsFishSelect)
                {
                    // Set Fish Btn True
                    BtnFishSelect.IsVisible = true;
                    var fishSideDishes = await ApiService.GetSideDishSelection((int)MainDish.Fish);
                    foreach (var fishDish in fishSideDishes)
                    {
                        FishSideDish.Add(fishDish);
                    }                                       
                }
                // Veg List
                if (product.IsVegSelect)
                {
                    // Set Veg Btn True
                    BtnVegSelect.IsVisible = true;
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
            // Prevent Double Click
            if (IsClickedOnce) return;
            IsClickedOnce = true;
            Navigation.PopModalAsync();
        }

        // Cart Controls
        #region Cart Controls
        // Add To Cart Button
        private async void BtnAddToCart_Clicked(object sender, System.EventArgs e)
        {
            var addToCart = new AddToCart();
            addToCart.Qty = LblQty.Text;
            addToCart.Price = LblPrice.Text;
            addToCart.TotalAmount = LblTotalPrice.Text;
            addToCart.ProductId = _productId;
            addToCart.CustomerId = Preferences.Get("userId", 0);
            addToCart.SideDishes = ProductObj.SideDishList;

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
        #endregion

        // Side Dishes Btns
        #region SideDishes Buttons

        // Meat Btn List
        private void BtnMeatSelect_Clicked(object sender, EventArgs e)
        {
            // Prevent Double Click
            if (IsClickedOnce) return;
            IsClickedOnce = true;
            // Determine CvSideDishes Single Or Multiple
            if (ProductObj.MaxMeatSelect > 1) IsCvSideDishMultiple = true;
            Navigation.PushModalAsync(new SideDishSelectorPage(MeatSideDish, IsCvSideDishMultiple, ProductObj));
        }

        // Fish Btn List
        private void BtnFishSelect_Clicked(object sender, EventArgs e)
        {
            // Prevent Double Click
            if (IsClickedOnce) return;
            IsClickedOnce = true;
            // Determine CvSideDishes Single Or Multiple
            if (ProductObj.MaxFishSelect > 1) IsCvSideDishMultiple = true;
            Navigation.PushModalAsync(new SideDishSelectorPage(FishSideDish, IsCvSideDishMultiple, ProductObj));
        }

        // Veg Btn List
        private void BtnVegSelect_Clicked(object sender, EventArgs e)
        {
            // Prevent Double Click
            if (IsClickedOnce) return;
            IsClickedOnce = true;
            // Determine CvSideDishes Single Or Multiple
            if (ProductObj.MaxVegSelect > 1) IsCvSideDishMultiple = true;
            Navigation.PushModalAsync(new SideDishSelectorPage(VegSideDish, IsCvSideDishMultiple, ProductObj));
        }
        #endregion

        protected  override void OnAppearing()
        {
            // Set CvSideDish Default as false
            IsCvSideDishMultiple = false;
            LblDetail.Text = ProductObj.GetFullDetail();
            // Init Duplicate Click Preventor
            IsClickedOnce = false;
        }
    }
}