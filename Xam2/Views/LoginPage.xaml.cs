using System;
using System.Collections.Generic;
using Xam2.Models;
using Xamarin.Forms;

namespace Xam2.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            Init();
        }

        public void Init()
        {
            BackgroundColor = Constants.BackgroundColor;
            Lb1_Username.TextColor = Constants.MainTextColor;
            Lb1_Password.TextColor = Constants.MainTextColor;
            ActivitySpinner.IsVisible = false;
            LogInIcon.HeightRequest = Constants.LogInIconHeight;

            Entry_Username.Completed += (s, e) => Entry_Password.Focus();
            Entry_Password.Completed += (s, e) => SignInProcedure(s, e);
        }

        public void SignInProcedure(object sender, EventArgs e)
        {
            Models.User user = new User(Entry_Username.Text,
                Entry_Password.Text) ;
            if (!user.CheckInfo())
                DisplayAlert("Login","Login Success","Ok") ;
            else
                DisplayAlert("Login", "Login Failed, empty username or "
                    + "password", "Ok") ;
        }
    }
}
