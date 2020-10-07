using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class SignInViewModel : ViewModelBase
    {
        public SignInViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Sign In Page";
        }

        public string Login { get; set; }
        public string Password { get; set; }

        public ICommand ClickCommand => new Command<string>(async (url) =>
        {
            await NavigationService.NavigateAsync("SignUp");
        });

        public ICommand Authorize => new Command<string>(async (url) =>
        {
            if (App.client.Authorize(Login, Password))
            {
                await NavigationService.NavigateAsync("MainPage");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Oops...", "Wrong credentials", "cancel");
            }
        });
    }
}
