using System;
using System.Collections;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Protech
{
    public partial class App : Application
    {
        public static string eMail="";
        public static string Token = "";
        public static int UserID = 0;
        public static string UserName = "";
        public static string UserRole = "";
        public static byte[] UserPicture;
        public static string pageCaption = "";
        public static int serviceID = 0;
        public static string productID = "";
        public static string contactID = "";
        public static ArrayList servicedatalist = new ArrayList();

        public App()
        {
            InitializeComponent();
            var fullScreenMainPage = new MainPage();
            NavigationPage.SetHasNavigationBar(fullScreenMainPage, false);
            MainPage = new NavigationPage(fullScreenMainPage);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
