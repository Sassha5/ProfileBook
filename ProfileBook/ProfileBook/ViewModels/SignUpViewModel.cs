using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Models;
using ProfileBook.Services.Registration;
using ProfileBook.Services.Repository;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class SignUpViewModel : ViewModelBase
    {
        private readonly IRegistrationService _registrationService;
        private readonly IRepository _repository;

        public string Login { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }


        public SignUpViewModel(INavigationService navigationService,
            IRegistrationService registrationService,
            IRepository repository)
            : base(navigationService)
        {
            _repository = repository;
            _registrationService = registrationService;
            Title = "Sign Up";
        }

        public ICommand Register => new Command(async () =>
        {
            if (!_repository.FindUser(Login) && Password.Equals(ConfirmPassword))
            {
                _registrationService.Register(Login, Password);
                await NavigationService.NavigateAsync("/NavigationPage/MainPage");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Oops...", "Something goes not as planned...", "cancel");
            }
        });
    }
}
