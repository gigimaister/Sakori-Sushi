using RealWorldApp.Models;
using RealWorldApp.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RealWorldApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductListPage : ContentPage
    {
        // ObservCol For Product By Category
        public ObservableCollection<ProductByCategory> ProductByCategoryCollection;

        // Bool To Prevent Duoble Click
        public bool IsClickedOnce;
        public ProductListPage(int categoryId, string categoryName)
        {
            InitializeComponent();
            LblCategoryName.Text = categoryName;
            ProductByCategoryCollection = new ObservableCollection<ProductByCategory>();
            GetProducts(categoryId);      
        }

        // GET Products
        private async void GetProducts(int id)
        {
            var products = await ApiService.GetProductByCategory(id);
            foreach (var product in products)
            {
                ProductByCategoryCollection.Add(product);
            }
            CvProducts.ItemsSource = ProductByCategoryCollection;
        }

        // Back Arrow Tapped
        private  void TapBack_Tapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        // When User Click On Product => Go To Product Detail Page
        private  void CvProducts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Prevent Double Click
            if (IsClickedOnce) 
            {
                // When Navigting Back To Home Page We Want Unchecked Categories
                ((CollectionView)sender).SelectedItem = null;
                return;
            } 
            IsClickedOnce = true;

            // Get Current Category Selection
            var currentSelection = e.CurrentSelection.FirstOrDefault() as ProductByCategory;
            // If Selection Is Null Do Nothing
            if (currentSelection == null) return;
            // Go To Product Detail Page
            Navigation.PushModalAsync(new ProductDetailPage(currentSelection.id));
            // When Navigting Back To Product List Page We Want Unchecked Categories
            ((CollectionView)sender).SelectedItem = null;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            // Init Duplicate Click Preventor
            IsClickedOnce = false;

        }
    }
}