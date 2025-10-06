using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Protech.Models;
using Xamarin.Forms;

namespace Protech
{
    public partial class AddContacts : ContentPage
    {
        private const string addcontactUrl = "http://192.168.1.16:4000/";
        readonly Uri baseUri = new Uri(addcontactUrl);


        private HttpClient _httpClient;

        public AddContacts()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (App.serviceID > 0)
            {
                IdnoLabel.IsVisible = true;
                IDNO.IsVisible = true;
                IDNO.Text = App.serviceID.ToString();
                ContactName.Text = App.servicedatalist[0].ToString();
                ContactAddress.Text = App.servicedatalist[1].ToString();
                ContactEmail.Text = App.servicedatalist[2].ToString();
                ContactMobileno.Text = App.servicedatalist[3].ToString();
            }
            else
            {
                IdnoLabel.IsVisible = false;
                IDNO.IsVisible = false;
            }
            addContactTitle.Text = App.pageCaption;

        }

        async void OnSaveContactsTapped(object sender, EventArgs e)
        {
            if (ContactName.Text == null || ContactName.Text == "")
            {
                await DisplayAlert("Alert Message", "Please enter Contact Name.....", "OK");
                return;
            }
            if (ContactAddress.Text == null || ContactAddress.Text == "")
            {
                await DisplayAlert("Alert Message", "Please enter Business Address.....", "OK");
                return;
            }
            if (ContactEmail.Text == null || ContactEmail.Text == "")
            {
                await DisplayAlert("Alert Message", "Please Email Address.....", "OK");
                return;
            }
            if (ContactEmail.Text.ToString().IndexOf("@") == -1)
            {
                await DisplayAlert("Alert Message", "Please enter correct Email Address", "OK");
                return;
            }
            if (ContactEmail.Text.ToString().IndexOf(".") == -1)
            {
                await DisplayAlert("Alert Message", "Please enter correct Email Address", "OK");
                return;
            }
            if (ContactMobileno.Text == null || ContactMobileno.Text == "")
            {
                await DisplayAlert("Alert Message", "Please enter Mobile No......", "OK");
                return;
            }
            bool retval = await DisplayAlert("Confirmation", "Do you want want to save this data?", "Yes", "No");
            if (retval)
            {

                var content = new StringContent(
                    JsonConvert.SerializeObject(new
                    {
                        contact_name = ContactName.Text,
                        contact_address = ContactAddress.Text,
                        contact_email = ContactEmail.Text,
                        contact_mobileno = ContactMobileno.Text,
                        isActive = 1
                    }),System.Text.Encoding.UTF8,"application/json");

                _httpClient = new HttpClient();
                _httpClient.MaxResponseContentBufferSize = 256000;
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + App.Token);

                if (App.serviceID > 0)
                {
                    try
                    {
                        
                        var uri = new Uri(baseUri, "api/updatecontact/" + App.serviceID.ToString());
                        HttpResponseMessage response = await _httpClient.PutAsync(uri, content);
                        if (response.IsSuccessStatusCode)
                        {
                            await DisplayAlert("Alert Message", "Data has been Updated Successfully.....", "OK");
                            App.serviceID = 0;
                            return;
                        }
                        else
                        {
                            var errorMsg = await response.Content.ReadAsStringAsync();
                            var xmsg = JsonConvert.DeserializeObject<ErroMessage>(errorMsg);
                            await DisplayAlert("Alert Message", xmsg.Message, "OK");
                        }
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("Alert Message", "Error !, check your connection.." + ex.Message, "OK");
                    }
                }
                else
                {
                    try
                    {
                        var uri = new Uri(baseUri, "api/createcontact");
                        HttpResponseMessage response = await _httpClient.PostAsync(uri, content);
                        if (response.IsSuccessStatusCode)
                        {
                            string data = await response.Content.ReadAsStringAsync();
                            Debug.WriteLine("data..." + data);
                            await DisplayAlert("Alert Message", "Data has been Successfully inserted.....", "OK");
                        }
                        else
                        {
                            var errorMsg = await response.Content.ReadAsStringAsync();
                            var xmsg = JsonConvert.DeserializeObject<ErroMessage>(errorMsg);
                            await DisplayAlert("Alert Message", xmsg.Message, "OK");
                        }
                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("Alert Message", "Error !, check your connection.." + ex.Message, "OK");
                    }


                }
            }

            else
            {
                await DisplayAlert("Alert Message", "You have cancelled the data processing.", "OK");
            }

        }

    }
}
