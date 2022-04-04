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
    public partial class SideDishSelectorPage : ContentPage
    {
        public ObservableCollection<SideDish> SideDishesList;
        private readonly bool _isCvMultiple;
        public Product ProductObj { get; set; }        
        public SideDishSelectorPage(ObservableCollection<SideDish> sideDishesList, bool isCvMultiple = false, Product product = null)
        {
            InitializeComponent();
            SideDishesList = sideDishesList;
            _isCvMultiple = isCvMultiple;
            CvSideDishes.ItemsSource = SideDishesList;
            ProductObj = product;
            // If More Than 1 Side Dish -> Make Cv Multiple
            if(isCvMultiple) CvSideDishes.SelectionMode = SelectionMode.Multiple;
        }


        // CvItem Clicked
        private void CvSideDishes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var currentSideDish = e.CurrentSelection.LastOrDefault() as SideDish;
            // If Cv Single
            if (!_isCvMultiple)
            {
                if(ProductObj.SideDishList is null)
                {
                    ProductObj.SideDishList = new List<SideDish>();
                    // Add The New One
                    ProductObj.SideDishList.Add(currentSideDish);
                }
                else
                {
                    // If We Already Have SideDish Object In The List
                    if (ProductObj.GetSideDishCount(currentSideDish.MainDishId) > 0)
                    {
                        // Remove It (Old One) From The List & Add Current One
                        ProductObj.SideDishList.RemoveAll(x => x.MainDishId == currentSideDish.MainDishId);
                    }
                    // Add The New One
                    ProductObj.SideDishList.Add(currentSideDish);
                }                             
                // Go Back To Detail Page
                Navigation.PopModalAsync();             
            }
            else
            // Cv Multiple
            {
                // If Null List
                if(ProductObj.SideDishList is null)
                {
                    // Checking If We Have NULL SideDish With MainDish Id
                    if (currentSideDish is null) return;
                    // If We Reached Our Max SideDishes
                    if (e.CurrentSelection.Count == ProductObj.GetMaxSideDishByMainId(currentSideDish.MainDishId))
                    {
                        ProductObj.SideDishList = new List<SideDish>();
                        foreach (var sideDish in e.CurrentSelection)
                        {
                            ProductObj.SideDishList.Add((SideDish)sideDish);
                        }
                    }
                    else return;
                }
                // Not Null List
                else
                {
                    // Checking If We Have NULL SideDish With MainDish Id
                    if (currentSideDish is null) return;
                    // If We Reached Our Max SideDishes
                    if (e.CurrentSelection.Count == ProductObj.GetSideDishCount(currentSideDish.MainDishId))
                    {
                        // If We Already Have SideDish Objects In The List
                        if (ProductObj.GetSideDishCount(currentSideDish.MainDishId) > 0)
                        {
                            // Remove It (Old One) From The List & Add Current One
                            ProductObj.SideDishList.RemoveAll(x => x.MainDishId == currentSideDish.MainDishId);
                        }
                        foreach (var sideDish in e.CurrentSelection)
                        {
                            ProductObj.SideDishList.Add((SideDish)sideDish);
                        }
                    }
                    else return;
                }
                // Go Back To Detail Page
                Navigation.PopModalAsync();
            }                             
        }        
    }
}