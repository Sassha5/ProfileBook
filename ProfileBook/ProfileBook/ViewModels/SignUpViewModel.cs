using Acr.UserDialogs;
using Prism.Navigation;
using ProfileBook.Enums;
using ProfileBook.Services.Registration;
using ProfileBook.Services.Settings;
using ProfileBook.Views;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class SignUpViewModel : ViewModelBase
    {
        private readonly IRegistrationService _registrationService;
        private readonly IUserDialogs _userDialogs;

        public string Login { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }


        public SignUpViewModel(INavigationService navigationService,
            ISettingsManager settingsManager,
            IRegistrationService registrationService,
            IUserDialogs userDialogs)
            : base(navigationService, settingsManager)
        {
            _userDialogs = userDialogs;
            _registrationService = registrationService;
        }

        private ICommand _RegisterCommand;
        public ICommand RegisterCommand => _RegisterCommand ??= new Command(OnRegisterCommandAsync);

        private async void OnRegisterCommandAsync()
        {
            switch (_registrationService.Validate(Login, Password, ConfirmPassword))
            {
                case Status.LoginIsTaken:
                    await _userDialogs.AlertAsync(Resources["LoginIsTaken"], Resources["Oops"], Resources["Damn"]); break;
                case Status.LoginIsTooLong:
                    await _userDialogs.AlertAsync(Resources["LoginIsTooLong"], Resources["Oops"], Resources["Ok"]); break;
                case Status.LoginIsTooShort:
                    await _userDialogs.AlertAsync(Resources["LoginIsTooShort"], Resources["Oops"], Resources["Ok"]); break;
                case Status.LoginStartsWithNumber:
                    await _userDialogs.AlertAsync(Resources["LoginStartsWithNumber"], Resources["Oops"], Resources["Damn"]); break;
                case Status.PasswordIsTooLong:
                    await _userDialogs.AlertAsync(Resources["PasswordIsTooLong"], Resources["Oops"], Resources["Ok"]); break;
                case Status.PasswordIsTooShort:
                    await _userDialogs.AlertAsync(Resources["PasswordIsTooShort"], Resources["Oops"], Resources["Damn"]); break;
                case Status.PasswordIsWeak:
                    await _userDialogs.AlertAsync(Resources["PasswordIsWeak"], Resources["Oops"], Resources["Sure"]); break;
                case Status.PasswordsAreNotEqual:
                    await _userDialogs.AlertAsync(Resources["PasswordsAreNotEqual"], Resources["Oops"], Resources["Thanks"]); break;
                case Status.Success:
                    await _userDialogs.AlertAsync(Resources["RedirectingToSignIn"], Resources["Success"], Resources["Finally"]);
                    RegistrationSuccess();
                    break;
                default:
                    await _userDialogs.AlertAsync(Resources["Unknown"], Resources["Oops"], Resources["Damn"]); break;
            }
        }
        private async void RegistrationSuccess()
        {
            _registrationService.Register(Login, Password);
            NavigationParameters navParams = new NavigationParameters();
            navParams.Add($"{nameof(Login)}", Login);
            await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(SignIn)}", navParams);
        }
    }
}
