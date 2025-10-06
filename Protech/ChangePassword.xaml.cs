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
    public partial class ChangePassword : ContentPage
    {
        private const string changeUrl = "http://192.168.1.16:4000/forgot/";
        public ObservableCollection<MailtokenModel> Users { get; set; } = new ObservableCollection<MailtokenModel>();

        private readonly HttpClient httpClient = new HttpClient();

        public ChangePassword()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            Email.Text = App.eMail;
        }

        async void OnChangePassword(object sender, EventArgs e)
        {
            if (Email.Text == null)
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
            if (Username.Text == null)
            {
                await DisplayAlert("Alert Message", "Please enter your Username", "OK");
                return;
            }
            if (Password.Text == null)
            {
                await DisplayAlert("Alert Message", "Please enter new Password", "OK");
                return;
            }

            var content = new StringContent(
                JsonConvert.SerializeObject(new
                {
                    username = Username.Text,
                    password = Password.Text
                }));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await httpClient.PutAsync(changeUrl + Email.Text, content);
            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Confirmation Message", "You have successfully changed your password...", "OK");
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
