using System;

using Xamarin.Forms;

namespace GitRepUserList
{
    public partial class App : Application
    {
        public static string BaseUrl = "https://api.github.com";

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new Views.UserProfilePage());
        }
    }
}
