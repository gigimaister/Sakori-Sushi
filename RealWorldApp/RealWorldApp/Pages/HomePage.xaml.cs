using RealWorldApp.Models;
using RealWorldApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}