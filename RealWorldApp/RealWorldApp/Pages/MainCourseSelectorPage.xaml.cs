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
    public partial class MainCourseSelectorPage : ContentPage
    {
        public List<MainCourseToProduct> MainCourseList;
        public Product ProductObj { get; set; }
        public MainCourseSelectorPage(List<MainCourseToProduct> mainCourseList, Product product = null)
        {
            InitializeComponent();
            MainCourseList = mainCourseList;
            CvMainCourses.ItemsSource = MainCourseList;
            ProductObj = product;           
        }

        private void CvMainCourses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var mainCourseSelected = e.CurrentSelection.FirstOrDefault() as MainCourseToProduct;
            ProductObj.MainCourseToProductId = mainCourseSelected.MainCourseProductDishesId;            
            // Go Back To Detail Page
            Navigation.PopModalAsync();
        }
    }
}