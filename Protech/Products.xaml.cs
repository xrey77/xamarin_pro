using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Protech
{
    public partial class Products : ContentPage
    {
        public Products()
        {
            InitializeComponent();
        }
        protected async void OnHomeTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
            //App.Current.MainPage = new NavigationPage(new MainPage());
            //App.Current.MainPage = new MainPage();
        }


        protected async void OnProductsAddTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddProducts());
            //App.Current.MainPage = new NavigationPage(new AddProducts());
            //App.Current.MainPage = new AddProducts();
        }

    }
}
