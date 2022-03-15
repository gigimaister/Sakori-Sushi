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

        // ObservCol For Categories
        public ObservableCollection<Category> CategoriesCollection;
        public HomePage()
        {
            InitializeComponent();
            ProductCollection = new ObservableCollection<PopularProduct>();
            CategoriesCollection = new ObservableCollection<Category>();
            GetPopularProducts();
            GetCategories();
            LblUserName.Text = Preferences.Get("userName", TraslatedMessages.Alert_Default_User_Name());
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
        private async void TapCloseMenu_Tapped(object sender, EventArgs e)
        {
            await SlMenu.TranslateTo(250, 0, 100, Easing.Linear);
            GridOverlay.IsVisible = false;
        }

        // Category Clicked
        private void CvCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
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
            Navigation.PushModalAsync(new CartPage());
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            // We Want To Override Because When We Navigate To Home Page We Want Call GET  Totatl Cart Items
            var response = await ApiService.GetTotalCartItems(Preferences.Get("userId", 0));
            LblTotalItems.Text = response.totalItems.ToString();
        }
    }
}