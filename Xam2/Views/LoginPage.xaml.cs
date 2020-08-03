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
            Lbl_Username.TextColor = Constants.MainTextColor;
            Lbl_Password.TextColor = Constants.MainTextColor;
            ActivitySpinner.IsVisible = false;
            LogInIcon.HeightRequest = Constants.LogInIconHeight;
            App.StartCheckIfInternet(Lbl_nointernet, this);

            Entry_Username.Completed += (s, e) => Entry_Password.Focus();
            Entry_Password.Completed += (s, e) => SignInProcedure(s, e);
        }

        async void SignInProcedure(object sender, EventArgs e)
        {
            User user = new User(Entry_Username.Text, Entry_Password.Text);
            if (!user.CheckInfo())
            {
                DisplayAlert("Login", "Login Successful", "Ok");
                var result = await App.RestService.Login(user);
                if (result != null)
                {
                    App.UserDatabase.SaveUser(user);
                }
            }
            else
            {
                DisplayAlert("Login", "Login not correct, empty username or" +
                    " password", "Ok");
            }
        }


    }
}
