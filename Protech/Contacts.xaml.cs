using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Protech.Models;
using Xamarin.Forms;

namespace Protech
{
    public partial class Contacts : ContentPage
    {
        private const string contactDataUrl = "http://192.168.1.16:4000/api/getcontacts";
        private HttpClient _httpClient;

        public ObservableCollection<ContactModel> contactData { get; set; } = new ObservableCollection<ContactModel>();

        public Contacts()
        {
            InitializeComponent();
            BindingContext = this;
        }

        protected async override void OnAppearing()
        {
            IsBusy = true;
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + App.Token);
            try { 
                HttpResponseMessage response = await _httpClient.GetAsync(contactDataUrl);
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    var allcontactDatas = JsonConvert.DeserializeObject<ContactModel[]>(data);
                    contactData?.Clear();
                    foreach (var item in allcontactDatas)
                    {
                        contactData.Add(item);
                    }
                    ContactList.ItemsSource = contactData;
                    IsBusy = false;

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

        protected async void OnContactsAddTapped(object sender, EventArgs e)
        {
            App.serviceID = 0;
            App.pageCaption = "Add new Contact";
            await Navigation.PushAsync(new AddContacts());
        }

        protected async void EditItem(object sender, EventArgs e)
        {
            var mi = sender as TextCell;
            var item = mi.CommandParameter as ContactModel;
            App.serviceID = item.Id;
            App.servicedatalist.Insert(0, item.Contact_Name);
            App.servicedatalist.Insert(1, item.Contact_Address);
            App.servicedatalist.Insert(2, item.Contact_Email);
            App.servicedatalist.Insert(3, item.Contact_Mobileno);
            App.pageCaption = "Edit Contact";
            await Navigation.PushAsync(new AddContacts());
        }

    }
}
