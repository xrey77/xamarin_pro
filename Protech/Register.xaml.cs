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
    public partial class Register : ContentPage
    {
        private const string loginUrl = "http://192.168.1.16:4000/register";
        public ObservableCollection<UserRegistration> Users { get; set; } = new ObservableCollection<UserRegistration>();

        private readonly HttpClient httpClient = new HttpClient();

        public Register()
        {
            InitializeComponent();
            BindingContext = this;
        }

        protected async void OnSaveRegister(object sender, EventArgs e)
        {
            if (Firstname.Text.Length == 0)
            {
                await DisplayAlert("Alert Message", "Please enter First Name", "OK");
                return;
            }
            if (Lastname.Text.Length == 0)
            {
                await DisplayAlert("Alert Message", "Please enter Last Name", "OK");
                return;
            }
            if (Email.Text.Length == 0)
            {
                await DisplayAlert("Alert Message", "Please enter Email Address", "OK");
                return;
            }
            if (Email.Text.ToString().IndexOf("@") == -1)
            {
                await DisplayAlert("Alert Message", "Please enter correct Email Address", "OK");
                return;
            }
            if (Email.Text.ToString().IndexOf(".") == -1)
            {
                await DisplayAlert("Alert Message", "Please enter correct Email Address", "OK");
                return;
            }
            if (Username.Text.Length == 0)
            {
                await DisplayAlert("Alert Message", "Please enter Username", "OK");
                return;
            }
            if (Password.Text.Length == 0)
            {
                await DisplayAlert("Alert Message", "Please enter Password", "OK");
                return;
            }

            var content = new StringContent(
                JsonConvert.SerializeObject(new
                {
                    firstname = Firstname.Text,
                    lastname = Lastname.Text,
                    email = Email.Text,
                    username = Username.Text,
                    password = Password.Text,
                }));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await httpClient.PostAsync(loginUrl, content);
            if (response.IsSuccessStatusCode)
            {
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
    }
}
