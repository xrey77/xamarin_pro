using System;
using System.Drawing;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Protech.Models;
using Xamarin.Forms;

namespace Protech
{
    public partial class MainPage : ContentPage
    {
        private const string monkeyUrl = "https://montemagno.com/monkeys.json";
        private readonly HttpClient httpClient = new HttpClient();

        public ObservableCollection<Monkey> Monkeys { get; set; } = new ObservableCollection<Monkey>();
        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (App.UserID > 0)
            {
                try
                {
                    byte[] Base64Stream = App.UserPicture;
                    userpic.Source = ImageSource.FromStream(() => new MemoryStream(Base64Stream));
                } catch(Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
            else
            {
                userpic.Source = "users.png";
            }
            IsBusy = true;
            var monkeyJson = await httpClient.GetStringAsync(monkeyUrl);
            var monkeys = JsonConvert.DeserializeObject<Monkey[]>(monkeyJson);

            Monkeys?.Clear();

            foreach (var monkey in monkeys)
            {
                Monkeys.Add(monkey);
            }
            IsBusy = false;
        }


        void OnSettingsTapped(object sender, EventArgs e)
        {
            Debug.WriteLine("Settings...");
            //await Navigation.PushAsync(new Settings(), true);
        }

        protected async void OnServicesTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Services(), true);
            //App.Current.MainPage = new NavigationPage(new Services());
            //App.Current.MainPage = new Services();
        }

        protected async void OnContactsTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Contacts(), true);
        }

        protected async  void OnProductsTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Products(), true);
        }

        protected async void OnLogInTapped(object sender, EventArgs e)
        {
            try
            {
                if (App.UserID > 0)
                {
                    bool ans = await DisplayAlert("Confirmation Message", "Are you sure you want to Log-Out ?", "Yes","No");
                    if (ans)
                    {
                        App.UserID = 0;
                        App.UserName = "";
                        App.Token = "";
                        App.eMail = "";
                        App.UserRole = "";
                        OnAppearing();
                    }
                }
                else
                {
                    await Navigation.PushAsync(new Login(), true);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        void TabViewItem_TabTapped(System.Object sender, Xamarin.CommunityToolkit.UI.Views.TabTappedEventArgs e)
        {
        }


    }
}
