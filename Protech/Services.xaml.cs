using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using Xamarin.Forms;
using Protech.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Protech
{
    public partial class Services : ContentPage
    {
        private const string serviceDataUrl = "http://192.168.1.16:4000/getallservice";
        private readonly HttpClient httpClient = new HttpClient();

        public ObservableCollection<ServiceListModel> serviceData { get; set; } = new ObservableCollection<ServiceListModel>();


        public Services()
        {
            InitializeComponent();
            BindingContext = this;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            IsBusy = true;
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + App.Token);
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(serviceDataUrl);
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    var serviceDatas = JsonConvert.DeserializeObject<ServiceListModel[]>(data);
                    serviceData?.Clear();
                    foreach (var item in serviceDatas)
                    {
                        serviceData.Add(item);
                    }
                    ServicesList.ItemsSource = serviceData;
                    IsBusy = false;

                }
                else
                {
                    var errorMsg = await response.Content.ReadAsStringAsync();
                    var xmsg = JsonConvert.DeserializeObject<ErroMessage>(errorMsg);
                    await DisplayAlert("Alert Message", xmsg.Message, "OK");
                }
            }
            catch(Exception ex)
            {
                await DisplayAlert("Alert Message", "Error !, check your connection.." + ex.Message, "OK");
            }

        
        }

        protected async void OnServicesAddTapped(object sender, EventArgs e)
        {
            App.serviceID = 0;
            App.pageCaption = "Add New Record";
            await Navigation.PushAsync(new AddServices());
        }



        protected async void EditItem(object sender, EventArgs e)
        {
            var mi = sender as TextCell;
            var item = mi.CommandParameter as ServiceListModel;
            App.serviceID = item.Id;
            App.servicedatalist.Insert(0, item.ServiceName);
            App.servicedatalist.Insert(1,item.Descriptions);
            App.servicedatalist.Insert(2,item.ServiceFee);
            App.servicedatalist.Insert(3,item.PaymentMode);
            App.pageCaption = "Edit Service Name";

            await Navigation.PushAsync(new AddServices());

            //DisplayAlert("Alert Message", item.Id.ToString(), "OK");

        }




    }
}
