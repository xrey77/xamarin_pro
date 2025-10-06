using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Protech.Models;
using Xamarin.Forms;

namespace Protech
{
    public partial class Login : ContentPage
    {
        private const string loginUrl = "http://192.168.1.16:4000/login";
        public ObservableCollection<UserLogin> Users { get; set; } = new ObservableCollection<UserLogin>();

        private readonly HttpClient httpClient = new HttpClient();

        public Login()
        {
            InitializeComponent();
            BindingContext = this;
        }

        protected async void OnLogin(object sender, EventArgs e)
        {

            if (UserName.Text.Length == 0)
            {
                await DisplayAlert("Alert Message", "Please enter Username..", "OK");
                return;
            }

            if (Password.Text.Length == 0)
            {
                await DisplayAlert("Alert Message", "Please enter Password..", "OK");
                return;
            }

            var content = new StringContent(
                JsonConvert.SerializeObject(new
                {
                    username = UserName.Text,
                    password = Password.Text,
                }));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await httpClient.PostAsync(loginUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var xdata = JsonConvert.DeserializeObject<UserLogin>(result);
                    if (xdata.Isactivated == "0")
                    {
                        await DisplayAlert("Alert Message", "Please activate your account, go to your Email Inbox and click the Activate Account Button.", "OK");
                        return;
                    }
                    App.Token = xdata.Token;
                    App.UserID = xdata.Id;
                    App.UserName = xdata.Username;
                    App.UserRole = xdata.Role;
                    App.UserPicture = Convert.FromBase64String(xdata.Profilepic);
                    var fullScreenMainPage = new MainPage();
                    NavigationPage.SetHasNavigationBar(fullScreenMainPage, false);
                    App.Current.MainPage = new NavigationPage(fullScreenMainPage);
            }
            else
                {
                var errorMsg = await response.Content.ReadAsStringAsync();
                var xmsg = JsonConvert.DeserializeObject<ErroMessage>(errorMsg);
                await DisplayAlert("Alert Message", xmsg.Message, "OK");
                }


        }

        protected async void OnRegister(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Register(), true);
        }

        protected async void OnForgot(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ForgotPassword(), true);
        }


    }
}
