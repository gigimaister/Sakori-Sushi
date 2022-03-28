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
        private ObservableCollection<SideDish> _sideDishesList;
        public SideDishSelectorPage(ObservableCollection<SideDish> sideDishesList)
        {
            InitializeComponent();
            _sideDishesList = sideDishesList;
            CvSideDishes.ItemsSource = _sideDishesList;
        }
    }
}