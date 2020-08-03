using System;
using CoreFoundation;
using Xam2.Data;
using Xam2.iOS.Data;
using System.Text;
using SystemConfiguration;
namespace Xam2.iOS.Data
{
    public class NetworkConnection : INetworkConnection
    {
        public NetworkConnection()
        {
        }

        public void CheckNetworkConnection()
        {
            InternetStatus();
        }

        public bool IsConnected { get; set; }

        public bool InternetStatus()
        {
            NetworkReachabilityFlags flags;
            bool defaultNetworkAvailable = IsNetworkAvailable(out flags);
            bool result = ((defaultNetworkAvailable
                && (flags & NetworkReachabilityFlags.IsDirect) != 0)
                || flags == 0);
            return !result;
        }

        private event EventHandler ReachabilityChanged;
        private void OnChange(NetworkReachabilityFlags flags)
        {
            var h = ReachabilityChanged;
            if (h != null)
                h(null, EventArgs.Empty);
            
        }

        private NetworkReachability defaultNetworkReachability;

        public bool IsNetworkAvailable(out NetworkReachabilityFlags flags)
        {
            if (defaultNetworkReachability == null)
            {
                defaultNetworkReachability = new NetworkReachability(
                    new System.Net.IPAddress(0));
                defaultNetworkReachability.SetNotification(OnChange);
                defaultNetworkReachability.Schedule(CFRunLoop.Current,
                    CFRunLoop.ModeDefault);
            }
            if (!defaultNetworkReachability.TryGetFlags(out flags))
            {
                return false;
            }
            return IsReachableWIthoutRequiringConnection(flags);
        }

        private bool IsReachableWIthoutRequiringConnection(NetworkReachabilityFlags flags)
        {
            bool IsReachable =
                (flags & NetworkReachabilityFlags.Reachable) != 0;
            bool noConnectionRequired =
                (flags & NetworkReachabilityFlags.ConnectionRequired) == 0;

            if ((flags & NetworkReachabilityFlags.IsWWAN) != 0)
                noConnectionRequired = false;

            return (IsReachable && noConnectionRequired);
        }
    }
}
