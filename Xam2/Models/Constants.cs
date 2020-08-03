using System;
using Xamarin.Forms;
namespace Xam2.Models
{
    public class Constants
    {
        public static bool isDev = true ;
        public static Color BackgroundColor = Color.FromRgb(58, 155, 215) ;
        public static Color MainTextColor = Color.White ;
        public static int LogInIconHeight = 120;

        public static string LogInUrl = "https://test.com/api/Auth/Login";

        public Constants()
        {
        }
    }
}
