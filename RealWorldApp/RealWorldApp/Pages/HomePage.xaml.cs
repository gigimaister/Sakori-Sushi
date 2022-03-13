using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RealWorldApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        // Hamburger Menu Tapped
        private async void ImgMenu_Tapped(object sender, EventArgs e)
        {
            GridOverlay.IsVisible = true;
            await SlMenu.TranslateTo(0, 0, 400, Easing.Linear);
        }

        // To Close Menu(Grid Overlay) When User Click Out Side Of Menu
        private async void TapCloseMenu_Tapped(object sender, EventArgs e)
        {
            await SlMenu.TranslateTo(250, 0, 400, Easing.Linear);
            GridOverlay.IsVisible = false;
        }
    }
}