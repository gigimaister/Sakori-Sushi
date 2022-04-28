using RealWorldApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RealWorldApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaidSideDishSelectorPage : ContentPage
    {
        public ObservableCollection<PaidSideDish> PaidSideDishesList;
        public Product ProductObj { get; set; }
        public PaidSideDishSelectorPage(ObservableCollection<PaidSideDish> paidSideDishesList, Product product = null)
        {
            InitializeComponent();
            PaidSideDishesList = paidSideDishesList;          
            CvPaidSideDishes.ItemsSource = PaidSideDishesList;
            ProductObj = product;         
        }
        // Selection Changed CvPaid List
        private void CvSideDishes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var pSideList = e.CurrentSelection;
            if (pSideList.Count > 0) 
            {
                if(ProductObj.PaidSideDishes != null) if(ProductObj.PaidSideDishes.Count > 0) ProductObj.PaidSideDishes.Clear();
                ProductObj.PaidSideDishes = new List<PaidSideDish>();
                foreach (var pSDSelection in pSideList)
                {                   
                    ProductObj.PaidSideDishes.Add(pSDSelection as PaidSideDish);
                }
            }

        }

        // Btn Finish Selecting
        private void BtnFinishSelecting_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        // Go Back To Detail Page
        private void BtnGoBack_Clicked(object sender, EventArgs e)
        {
            // Clear List If Not Empty
            if(ProductObj.PaidSideDishes != null) ProductObj.PaidSideDishes.Clear();
            Navigation.PopModalAsync();
        }
    }
}