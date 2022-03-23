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
    public partial class OrdersDetailPage : ContentPage
    {
        // ObservCol For Orders By User
        public ObservableCollection<OrderDetail> OrdersDetailCollection;
        public int OrderId;
        public double OrderTotal;
        public OrdersDetailPage(int orderId, double orderTotal)
        {
            // Get Order Id & Order Total From Last Page
            OrderId = orderId;
            OrderTotal = orderTotal;

            InitializeComponent();
            OrdersDetailCollection = new ObservableCollection<OrderDetail>();
            GetOrderItems();

        }

        // GET Order Items
        private async void GetOrderItems()
        {
            // Get Tapped Order
            var orders = await ApiService.GetOrderDetails(OrderId);
            
            // Get All Products Associated With Order From Navigation Prop (OrderDetails)
            foreach (var orderDetail in orders.FirstOrDefault().orderDetails)
            {
                OrdersDetailCollection.Add(orderDetail);
            }

            // Bind To Xaml
            LvOrdersProducts.ItemsSource = OrdersDetailCollection;
        }

        // Back Arrow Tapped
        private void TapBack_Tapped(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        // Set Ordr Total At The Bottom Page
        protected override void OnAppearing()
        {
            LblTotalAmount.Text = OrderTotal.ToString();
        }
        
    }
}