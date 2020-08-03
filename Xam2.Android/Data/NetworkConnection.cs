using System;
using Xam2.Data;
using Xam2.Droid.Data;
using Android.App;
using Android.Content;
using Android.Net;

[assembly: Xamarin.Forms.Dependency(typeof(NetworkConnection))]

namespace Xam2.Droid.Data
{
    public class NetworkConnection : INetworkConnection
    {
        public NetworkConnection()
        {
        }

        public bool IsConnected { get; set; }

        public void CheckNetworkConnection()
        {
            var ConnectivityManager = (ConnectivityManager)Application
                .Context.GetSystemService(Context.ConnectivityService);
            var ActiveNetworkInfo = ConnectivityManager.ActiveNetworkInfo;
            IsConnected = (ActiveNetworkInfo != null
                && ActiveNetworkInfo.IsConnectedOrConnecting);
            
        }
    }
}
