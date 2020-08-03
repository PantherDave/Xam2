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

        void Init()
        {
            BackgroundColor = Constants.BackgroundColor;
            Lb1_Username.TextColor = Constants.MainTextColor;
            Lb1_Password.TextColor = Constants.MainTextColor;
            ActivitySpinner.IsVisible = false;
            LogInIcon.HeightRequest = Constants.LogInIconHeight;

            Entry_Username.Completed += (s, e) => Entry_Password.Focus();
            Entry_Password.Completed += (s, e) => SignInProcedure(s, e);
        }

        void SignInProcedure(object sender, EventArgs e)
        {
            User user = new User(Entry_Username.Text, Entry_Password.Text);
            if (!user.CheckInfo())
            {
                DisplayAlert("Login", "Login Successful", "Ok");
                App.UserDatabase.SaveUser(user);
            }
            else
            {
                DisplayAlert("Login", "Login not correct, empty username or" +
                    " password", "Ok");
            }
        }


    }
}
