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
    public partial class OrdersPage : ContentPage
    {
        public ObservableCollection<OrderByUser> OrdersCollection;
        public OrdersPage()
        {
            InitializeComponent();
            OrdersCollection = new ObservableCollection<OrderByUser>();
            GetOrderItems();
        }

        // GET Order Items
        private async void GetOrderItems()
        {
            var orders = await ApiService.GetOrdersByUser(Preferences.Get("userId", 0));

            foreach (var order in orders)
            {
                OrdersCollection.Add(order);
            }
            // Bind To Xaml
            LvOrders.ItemsSource = OrdersCollection;
        }

        // Back Arrow Tapped
        private void TapBack_Tapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}