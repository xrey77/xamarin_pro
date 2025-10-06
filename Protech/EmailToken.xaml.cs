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
    public partial class EmailToken : ContentPage
    {

        private const string tokenUrl = "http://192.168.1.16:4000/validatemailtoken";
        public ObservableCollection<MailtokenModel> Users { get; set; } = new ObservableCollection<MailtokenModel>();

        private readonly HttpClient httpClient = new HttpClient();

        public EmailToken()
        {
            InitializeComponent();
        }

        async void OnMailToken(object sender, EventArgs e)
        {
            if (MailToken.Text == null) {
                await DisplayAlert("Alert Message", "Please enter eMail Token", "OK");
                return;
            }

            if (MailToken.Text.Length < 4)
            {
                await DisplayAlert("Alert Message", "eMail Token should be 4 digits", "OK");
                return;
            }


            var content = new StringContent(
                JsonConvert.SerializeObject(new
                {
                    mailtoken = MailToken.Text,
                }));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await httpClient.PostAsync(tokenUrl, content);
            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Confirmation Message", "eMail Token, Accepted...", "OK");
                await Navigation.PushAsync(new ChangePassword(), true);

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
