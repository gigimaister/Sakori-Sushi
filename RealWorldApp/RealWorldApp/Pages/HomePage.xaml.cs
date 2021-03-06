using RealWorldApp.Models;
using RealWorldApp.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RealWorldApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        // ObservCol For Popular Products
        public ObservableCollection<PopularProduct> ProductCollection;
        // Bool To Prevent Duoble Click
        public bool IsClickedOnce;

        // ObservCol For Categories
        public ObservableCollection<Category> CategoriesCollection;
        public HomePage()
        {
            
            InitializeComponent();          
            ProductCollection = new ObservableCollection<PopularProduct>();
            CategoriesCollection = new ObservableCollection<Category>();         
            GetPopularProducts();
            GetCategories();           
            LblUserName.Text = Preferences.Get(Constants.Preference.UserName, TraslatedMessages.Alert_Default_User_Name());
            CvProducts.IsVisible = true;
            GridMain.IsVisible = true;
            GridLottieAnimation.IsVisible = false;
        }


        // Get Categories
        private async void GetCategories()
        {
            var categories = await ApiService.GetCategories();
            foreach (var category in categories)
            {
                CategoriesCollection.Add(category);
            }
            // Attach Xaml Cv To Collection
            CvCategories.ItemsSource = CategoriesCollection;
        }

        // Get Popular Products
        private async void GetPopularProducts()
        {
            var products = await ApiService.GetPopularProducts();
            foreach (var product in products)
            {
                ProductCollection.Add(product);
            }
            // Attach Xaml Cv To Collection
            CvProducts.ItemsSource = ProductCollection;
        }

        // Hamburger Menu Tapped
        private async void ImgMenu_Tapped(object sender, EventArgs e)
        {
            GridOverlay.IsVisible = true;
            await SlMenu.TranslateTo(0, 0, 100, Easing.Linear);
        }

        // To Close Menu(Grid Overlay) When User Click Out Side Of Menu
        private void TapCloseMenu_Tapped(object sender, EventArgs e)
        {
            CloseHamburgerMenu();
        }

        // Category Clicked
        private void CvCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Prevent Double Click
            if (IsClickedOnce)
            {
                // When Navigting Back To Product List Page We Want Unchecked Categories
                ((CollectionView)sender).SelectedItem = null;
                return;
            }
            IsClickedOnce = true;
            // Get Current Category Selection
            var currentSelection = e.CurrentSelection.FirstOrDefault() as Category;
            // If Selection Is Null Do Nothing
            if (currentSelection == null) return;
            // Go To ProductListPage
            Navigation.PushModalAsync(new ProductListPage(currentSelection.id, currentSelection.name));
            // When Navigting Back To HomePage We Want Unchecked Categories
            ((CollectionView)sender).SelectedItem = null;
        }

        // Popular Products Clicked 
        private void CvProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Prevent Double Click
            if (IsClickedOnce)
            {
                // When Navigting Back To Product List Page We Want Unchecked Categories
                ((CollectionView)sender).SelectedItem = null;
                return;
            }            
            IsClickedOnce = true;
            // Get Current Category Selection
            var currentSelection = e.CurrentSelection.FirstOrDefault() as PopularProduct;
            // If Selection Is Null Do Nothing
            if (currentSelection == null) return;
            // Go To Product Detail Page
            Navigation.PushModalAsync(new ProductDetailPage(currentSelection.id));
            // When Navigting Back To Product List Page We Want Unchecked Categories
            ((CollectionView)sender).SelectedItem = null;
        }

        // Cart Icon Tapped
        private void TapCartIcon_Tapped(object sender, EventArgs e)
        {
            // Prevent Double Click
            if (IsClickedOnce) return;
            IsClickedOnce = true;
            Navigation.PushModalAsync(new CartPage());         
        }

        #region Menu
        // My Orders Tapped
        private void TapOrders_Tapped(object sender, EventArgs e)
        {
            // Prevent Double Click
            if (IsClickedOnce) return;
            IsClickedOnce = true;
            Navigation.PushModalAsync(new OrdersPage());
        }

        // Contact Us Tapped
        private void TapContact_Tapped(object sender, EventArgs e)
        {
            // Prevent Double Click
            if (IsClickedOnce) return;
            IsClickedOnce = true;
            Navigation.PushModalAsync(new ContactPage());
        }

        // Cart Menu Tapped
        private void TapCart_Tapped(object sender, EventArgs e)
        {
            // Prevent Double Click
            if (IsClickedOnce) return;
            IsClickedOnce = true;
            Navigation.PushModalAsync(new CartPage());
        }

        // Logout
        private void TapLogout_Tapped(object sender, EventArgs e)
        {
            // Prevent Double Click
            if (IsClickedOnce) return;
            IsClickedOnce = true;
            Preferences.Set(Constants.Preference.AccessToken, string.Empty);
            Preferences.Set(Constants.Preference.TokenExpirationTime, 0);
            Application.Current.MainPage = new NavigationPage(new SignUpPage());


        }
        #endregion

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            // We Want To Override Because When We Navigate To Home Page We Want Call GET  Totatl Cart Items
            var response = await ApiService.GetTotalCartItems(Preferences.Get(Constants.Preference.UserId, 0));
            LblTotalItems.Text = response.totalItems.ToString();
            // Init Duplicate Click Preventor
            IsClickedOnce = false;
            

        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            // Close Hamburger Menu (When We Come Back To Main Page We Want Hamburger Menu Closed)
            CloseHamburgerMenu();
        }
        private async void CloseHamburgerMenu()
        {
            await SlMenu.TranslateTo(250, 0, 100, Easing.Linear);
            GridOverlay.IsVisible = false;
        } 
    }
}