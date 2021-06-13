using LoginDemo.Models;
using LoginDemo.Services;
using LoginDemo.Views;

using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Essentials;
using Xamarin.Forms;

namespace LoginDemo.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly ILoginService loginService;

        private string userName;
        private string password;
        private bool isRemember;

        public string UserName { get => userName; set => SetProperty(ref userName, value); }
        public string Password { get => password; set => SetProperty(ref password, value); }
        public bool IsRemember { get => isRemember; set => SetProperty(ref isRemember, value); }

        public Command LoginCommand { get; }
        public Command LoadCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
            LoadCommand = new Command(ExecuteLoad);
            loginService = DependencyService.Get<ILoginService>();
        }

        private async void ExecuteLoad()
        {
            UserName = await SecureStorage.GetAsync(Constants.UserIdKey);
            Password = await SecureStorage.GetAsync(Constants.PwdKey);

            if (!string.IsNullOrEmpty(UserName))
                IsRemember = true;
        }

        private async void OnLoginClicked(object obj)
        {
            if (await loginService.CheckLogin(UserName, Password))
            {
                if (IsRemember)
                {
                    await SecureStorage.SetAsync(Constants.UserIdKey, UserName);
                    await SecureStorage.SetAsync(Constants.PwdKey, Password);
                }
                else
                {
                    SecureStorage.Remove(Constants.UserIdKey);
                    SecureStorage.Remove(Constants.PwdKey);
                }

                await Shell.Current.GoToAsync($"//{nameof(AboutPage)}");
                return;
            }

            await AppShell.Current.DisplayAlert("Login Failed", "Invalid Username or Password.", "Ok");
        }
    }
}
