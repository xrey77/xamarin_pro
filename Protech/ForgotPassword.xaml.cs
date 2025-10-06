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
    public partial class ForgotPassword : ContentPage
    {

        private const string emailUrl = "http://192.168.1.16:4000/emailtoken";
        public ObservableCollection<MailtokenModel> Users { get; set; } = new ObservableCollection<MailtokenModel>();

        private readonly HttpClient httpClient = new HttpClient();


        public ForgotPassword()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
        }

        async void OnSendMailToken(object sender, EventArgs e)
        {
            //string result = await DisplayPromptAsync("Question 1", "What's your name?");
            //Debug.WriteLine(result);

            //string action = await DisplayActionSheet("ActionSheet: SavePhoto?", "Cancel", "Delete", "Photo Roll", "Email");
            //Debug.WriteLine("Action: " + action);

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
            App.eMail = Email.Text;

            var content = new StringContent(
                JsonConvert.SerializeObject(new
                {
                    email = Email.Text,
                }));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await httpClient.PostAsync(emailUrl, content);
            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Alert Message", "Please check your Email Inbox for eMail Token.", "OK");
                await Navigation.PushAsync(new EmailToken(), true);

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
