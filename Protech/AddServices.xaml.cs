using System;
using System.Reflection;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using Xamarin.Forms;
using Protech.Models;

namespace Protech
{
    public partial class AddServices : ContentPage
    {
        private const string addserviceUrl = "http://192.168.1.16:4000/createservice";
        private const string updateserviceUrl = "http://192.168.1.16:4000/updateservice/";
        private readonly HttpClient httpClient = new HttpClient();

        public AddServices()
        {
            InitializeComponent();
            BindingContext = new ServiceViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (App.serviceID > 0)
            {

                IdnoLabel.IsVisible = true;
                IDNO.IsVisible = true;
                IDNO.Text = App.serviceID.ToString();
                ServiceName.Text = App.servicedatalist[0].ToString();
                Descriptions.Text = App.servicedatalist[1].ToString();
                ServiceFee.Text = App.servicedatalist[2].ToString();
                PaymentMode.Text = App.servicedatalist[3].ToString();
            }
            else
            {
                IdnoLabel.IsVisible = false;
                IDNO.IsVisible = false;
            }
            addServiceTitle.Text = App.pageCaption;

        }



        protected async void OnSaveServicesTapped(object sender, EventArgs e)
        {
            bool retval = await DisplayAlert("Confirmation", "Do you want want to save this data?", "Yes","No");
            if (retval)
            {

                var content = new StringContent(
                    JsonConvert.SerializeObject(new {
                        servicename = serviceView.ServiceName,
                        descriptions = serviceView.Descriptions,
                        servicefee = Math.Round(decimal.Parse(serviceView.ServiceFee),2),
                        paymentmode = serviceView.PaymentMode
                    }));
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    HttpResponseMessage response = null;
                    if (App.serviceID > 0)
                    {
                        try { 
                            response = await httpClient.PutAsync(updateserviceUrl + App.serviceID.ToString(), content);
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
                      try { 
                            response = await httpClient.PostAsync(addserviceUrl, content);
                            if (response.IsSuccessStatusCode)
                            {
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
