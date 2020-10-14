using Prism.Navigation;
using ProfileBook.Services.Authorization;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class SignInViewModel : ViewModelBase
    {
        private readonly IAuthorizationService _authorizationService;

        public string Login { get; set; }
        public string Password { get; set; }

        public SignInViewModel(INavigationService navigationService,
            IAuthorizationService authorizationService)
            : base(navigationService)
        {
            Title = "Sign In";
            _authorizationService = authorizationService;
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            //if (_authorizationService.CheckAuthorized())              todo autologin
            //{
            //    ToMainPage();
            //}
        }

        public ICommand ClickCommand => new Command(async () =>
        {
            await NavigationService.NavigateAsync("SignUp");
        });

        public ICommand Authorize => new Command(async () =>
        {
            if (_authorizationService.Authorize(Login, Password))
            {
                ToMainPage();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Oops...", "Wrong credentials", "Cancel");
                Password = string.Empty;
                RaisePropertyChanged("Password");
            }
        });

        private async void ToMainPage()
        {
            await NavigationService.NavigateAsync("/NavigationPage/MainPage");
        }
    }
}
