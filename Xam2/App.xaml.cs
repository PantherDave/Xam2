using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xam2.Data;
using Xam2.Views;
using Xam2.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Xam2
{
    public partial class App : Application
    {
        public static TokenDatabaseController tokenDatabase;
        public static UserDatabaseController userDatabase;
        public static RestService restService;
        private static Label labelScreen;
        private static Page currentpage;
        private static Timer timer;
        private static bool hasInternet;
        private static bool noInterShow;

        public App()
        {
            InitializeComponent();

            MainPage = new LoginPage();
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

        public static UserDatabaseController UserDatabase
        {
            get
            {
                if (userDatabase == null)
                    userDatabase = new UserDatabaseController();
                
                return userDatabase;
            }
        }

        public static TokenDatabaseController TokenDatabase
        {
            get
            {
                if (tokenDatabase == null)
                    tokenDatabase = new TokenDatabaseController();

                return tokenDatabase;
            }
        }

        public static RestService RestService
        {
            get
            {
                if (restService == null)
                    restService = new RestService();
                return restService ;
            }
        }

        public static void StartCheckIfInternet(Label label, Page page)
        {
            labelScreen = label;
            label.Text = Constants.NotInternetText;
            label.IsVisible = false;
            currentpage = page;
            if (timer == null)
            {
                timer = new Timer((e) =>
                {
                    CheckIfInternetOverTime();
                }, null, 10, (int)TimeSpan.FromSeconds(3).TotalMilliseconds);
            }
        }

        public static void CheckIfInternetOverTime()
        {
            INetworkConnection networkConnection =
                DependencyService.Get<INetworkConnection>();
            if (!networkConnection.IsConnected)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    if (hasInternet)
                    {
                        if (!noInterShow)
                        {
                            hasInternet = false;
                            labelScreen.IsVisible = true;
                            await ShowDisplayAlert();
                        }
                    }
                });
            }
        }

        public static async Task<bool> CheckIfInternet()
        {
            INetworkConnection networkConnection = DependencyService
                .Get<INetworkConnection>();
            networkConnection.CheckNetworkConnection();

            return networkConnection.IsConnected;

        }

        public static async Task<bool> CheckIfInternetAlert()
        {
            INetworkConnection networkConnection = DependencyService
                .Get<INetworkConnection>();
            networkConnection.CheckNetworkConnection();

            if (!networkConnection.IsConnected)
            {
                if (!noInterShow)
                    await ShowDisplayAlert();

                return false;
            }

            return true;
        }

        private static async Task ShowDisplayAlert()
        {
            noInterShow = false;
            await currentpage.DisplayAlert("Internet", "Device has no " +
                "internet, please reconnect", "Ok");
            noInterShow = false;
        }
    }
}
