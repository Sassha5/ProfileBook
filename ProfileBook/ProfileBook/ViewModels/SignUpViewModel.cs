using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class SignUpViewModel : ViewModelBase
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }


        public SignUpViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Sign Up Page";
        }

        public ICommand Register => new Command<string>(async (url) =>
        {
            //if (!App.database.FindUser(Login) && Password.Equals(ConfirmPassword))
            //{
                App.client.Register(Login, Password);
                await NavigationService.NavigateAsync("MainPage");
            //}
            //else
            //{
            //    await Application.Current.MainPage.DisplayAlert("Oops...", "Something goes not as planned...", "cancel");
            //}
        });
    }
}
