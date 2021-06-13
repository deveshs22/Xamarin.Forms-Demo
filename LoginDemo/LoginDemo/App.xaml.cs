using LoginDemo.Models;
using LoginDemo.Services;
using LoginDemo.Views;

using System;

using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LoginDemo
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            DependencyService.Register<LoginService>();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            Shell.Current.GoToAsync($"//{nameof(LoginPage)}");

            // AutoLogin();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private async void AutoLogin()
        {
            var loginService = DependencyService.Get<ILoginService>();
            var userName = await SecureStorage.GetAsync(Constants.UserIdKey);
            var password = await SecureStorage.GetAsync(Constants.PwdKey);

            if (!string.IsNullOrEmpty(userName) && await loginService.CheckLogin(userName, password))
            {
                await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
                return;
            }

            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
    }
}
