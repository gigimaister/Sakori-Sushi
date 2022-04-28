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
        public ObservableCollection<PaidSideDish> PaidSideDish { get; set; }
        public Product ProductObj { get; set; }
        public ShoppingCartItem ShoppingCartItem { get; set; }
        public bool IsCvSideDishMultiple { get; set; }
        // Bool To Prevent Duoble Click
        public bool IsClickedOnce;
        public ProductDetailPage(int productId, ShoppingCartItem _shoppingCartItem = null)
        {
            InitializeComponent();

            // Side Dish (Also Paid) Lists
            MeatSideDish = new ObservableCollection<SideDish>();
            FishSideDish = new ObservableCollection<SideDish>();
            VegSideDish = new ObservableCollection<SideDish>();
            PaidSideDish = new ObservableCollection<PaidSideDish>();
            ProductObj = new Product();
            
            // Check If We Edit The Product Instead Of Post New One
            if(_shoppingCartItem != null)
            {
                ShoppingCartItem = _shoppingCartItem;
                SetProductForEdit(ShoppingCartItem);
            }
            else
            {
                GetProductDetails(productId);
            }
            _productId = productId;                   
        }

        private async void GetPaidDishes()
        {           
            var paidSideDishes = await ApiService.GetPaidSideDishes();
            foreach (var pDish in paidSideDishes)
            {
                PaidSideDish.Add(pDish);
            }
        }

        // Get Product Details
        private async void GetProductDetails(int productId)
        {
            var product = await ApiService.GetProductById(productId);
            ProductObj = product;

            LblName.Text = product.name;
            LblDetail.Text = product.GetPaidSDFullDetail(product.GetFullDetail());
            ImgProduct.Source = product.FullImageUrl;
            LblPrice.Text = product.price.ToString();
            LblTotalPrice.Text = LblPrice.Text;
            // Get All SideDishes
            await GetAllSideDish(product);
            // If Product Has Paid SideDishes Selection, Call PaidSideDishes && Set BtnPaidSDSelect True
            if (product.HasPaidSideDish) { GetPaidDishes(); BtnPaidSDSelect.IsVisible = true;} 
                    
        }

        // Set Product For Edit
        private async  void SetProductForEdit(ShoppingCartItem sCartItem)
        {
            ProductObj = sCartItem.Product;

            LblName.Text = ProductObj.name;
            LblDetail.Text = ProductObj.GetPaidSDFullDetail(ProductObj.GetFullDetail());
            ImgProduct.Source = ProductObj.FullImageUrl;
            LblPrice.Text = ProductObj.price.ToString();
            LblTotalPrice.Text = sCartItem.totalAmount.ToString();
            LblQty.Text = ShoppingCartItem.qty.ToString();
            // Get All SideDishes
            await GetAllSideDish(ProductObj);
            // If Product Has Paid SideDishes Selection, Call PaidSideDishes && Set BtnPaidSDSelect True
            if (ProductObj.HasPaidSideDish) { GetPaidDishes(); BtnPaidSDSelect.IsVisible = true; }
        }

        // Get All SideDish
        private async Task GetAllSideDish(Product product)
        {
            // Check If Product Selectable Then We Want To Check What Kind Of Side Dishes We Can Add
            if (product.IsProductSelectable)
            {
                // Meat List
                if (product.IsMeatSelect)
                {
                    // Set Meat Btn True
                    BtnMeatSelect.IsVisible = true;
                    var meatSideDishes = await ApiService.GetSideDishSelection((int)MainDish.Meat);
                    foreach (var meatDish in meatSideDishes)
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
        private void TapBack_Tapped(object sender, EventArgs e)
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
            addToCart.PaidSideDishes = ProductObj.PaidSideDishes;

            var validatorList = ProductObj.ProductDetailValidator();
            // If We Have Validator Errors Promt User
            if (validatorList.Count > 0)
            {
                foreach(var validatorMessage in validatorList)
                {
                    await DisplayAlert("", validatorMessage, TraslatedMessages.Alert_Dismiss());
                }
                return;
            }

            bool response;

            // If We In Edit Mode
            if(ShoppingCartItem != null)
            {
                response = await ApiService.EditItemInCart(Preferences.Get(Constants.Preference.UserId, 0), addToCart);
            }
            else
            {
                response = await ApiService.AddItemsInCart(addToCart);
            }
             

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
        private async void TapDecrement_Tapped(object sender, EventArgs e)
        {
            if (LblQty.Text == "1") 
            { 
                // If We In Edit Mode
                if(ShoppingCartItem != null)
                {
                    // Alert User If Delete
                    bool answer = await DisplayAlert("", TraslatedMessages.Alert_Delete_Product(), TraslatedMessages.Alert_Yes(), TraslatedMessages.Alert_No());
                    if (answer)
                    {
                        var response = await ApiService.ClearShoppingCartItem(Preferences.Get(Constants.Preference.UserId, 0), ShoppingCartItem.id);

                        if (response)
                        {
                            await DisplayAlert("", TraslatedMessages.Alert_Product_Was_Clear(), TraslatedMessages.Alert_Dismiss());
                            await Navigation.PopModalAsync();

                        }
                        else
                        {
                            await DisplayAlert(TraslatedMessages.Alert_Oops(), TraslatedMessages.Alert_Something_Went_Wrong(), TraslatedMessages.Alert_Dismiss());
                        }
                    }
                }
                return;
            }
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

        // Paid SD Btn List 
        private void BtnPaidSDSelect_Clicked(object sender, EventArgs e)
        {
            // Prevent Double Click
            if (IsClickedOnce) return;
            IsClickedOnce = true;
            // Set CvSideDishes To Multiple
            IsCvSideDishMultiple = true;
            Navigation.PushModalAsync(new PaidSideDishSelectorPage(PaidSideDish, ProductObj));
        }
        #endregion

        protected  override void OnAppearing()
        {
            // Set CvSideDish Default as false
            IsCvSideDishMultiple = false;
            LblDetail.Text = ProductObj.GetPaidSDFullDetail(ProductObj.GetFullDetail());
            // Init Duplicate Click Preventor
            IsClickedOnce = false;
        }

      
    }
}