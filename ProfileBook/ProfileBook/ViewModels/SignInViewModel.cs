using Prism.Navigation;
using ProfileBook.Resources;
using ProfileBook.Services.Authorization;
using ProfileBook.Services.Settings;
using System.Reflection;
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
                               : base(navigationService, 
                                      settingsManager)
        {
            _authorizationService = authorizationService;
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.TryGetValue($"{nameof(Login)}", out string login)) //true when navigated from sign up
            {
                Login = login;

                RaisePropertyChanged($"{nameof(Login)}");
            }
        }

        private ICommand _SignUpCommand;
        public ICommand SignUpCommand => _SignUpCommand ??= new Command(OnSignUpCommandAsync);
        
        private ICommand _AuthorizeCommand;
        public ICommand AuthorizeCommand => _AuthorizeCommand ??= new Command(OnAuthorizeCommandAsync);

        private async void OnSignUpCommandAsync()
        {
            await NavigationService.NavigateAsync($"{nameof(Views.SignUp)}");
        }

        private async void OnAuthorizeCommandAsync()
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
        }
    }
}
