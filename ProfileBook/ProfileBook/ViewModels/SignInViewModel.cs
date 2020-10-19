using Prism.Navigation;
using ProfileBook.Resources;
using ProfileBook.Services.Authorization;
using ProfileBook.Services.Settings;
using System.Threading.Tasks;
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
            ISettingsManager settingsManager,
            IAuthorizationService authorizationService)
            : base(navigationService, settingsManager)
        {
            Title = "Sign In";
            _authorizationService = authorizationService;
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            string login = parameters.GetValue<string>($"{nameof(Login)}");
            if (login != null) //true when navigated from Sign Up
            {
                Login = login;
                RaisePropertyChanged($"{nameof(Login)}");
            }
        }

        public ICommand SignUp => new Command(async () =>
        {
            await NavigationService.NavigateAsync($"{nameof(Views.SignUp)}");
        });

        public ICommand Authorize => new Command(async () =>
        {
            if (_authorizationService.Authorize(Login, Password))
            {
                await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(Views.MainPage)}");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert(AppResource.Oops, AppResource.WrongCredentials, AppResource.Ok);
                Password = string.Empty;
                RaisePropertyChanged($"{nameof(Password)}");
            }
        });
    }
}
