using Acr.UserDialogs;
using Prism.Navigation;
using ProfileBook.Enums;
using ProfileBook.Resources;
using ProfileBook.Services.Registration;
using ProfileBook.Services.Settings;
using ProfileBook.Views;
using System.Threading.Tasks;
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
                    await _userDialogs.AlertAsync(AppResource.LoginIsTaken, AppResource.Oops, AppResource.Damn); break;
                case Status.LoginIsTooLong:
                    await _userDialogs.AlertAsync(AppResource.LoginIsTooLong, AppResource.Oops, AppResource.Ok); break;
                case Status.LoginIsTooShort:
                    await _userDialogs.AlertAsync(AppResource.LoginIsTooShort, AppResource.Oops, AppResource.Ok); break;
                case Status.LoginStartsWithNumber:
                    await _userDialogs.AlertAsync(AppResource.LoginStartsWithNumber, AppResource.Oops, AppResource.Damn); break;
                case Status.PasswordIsTooLong:
                    await _userDialogs.AlertAsync(AppResource.PasswordIsTooLong, AppResource.Oops, AppResource.Ok); break;
                case Status.PasswordIsTooShort:
                    await _userDialogs.AlertAsync(AppResource.PasswordIsTooShort, AppResource.Oops, AppResource.Damn); break;
                case Status.PasswordIsWeak:
                    await _userDialogs.AlertAsync(AppResource.PasswordIsWeak, AppResource.Oops, AppResource.Sure); break;
                case Status.PasswordsAreNotEqual:
                    await _userDialogs.AlertAsync(AppResource.PasswordsAreNotEqual, AppResource.Oops, AppResource.Thanks); break;
                case Status.Success:
                    await _userDialogs.AlertAsync(AppResource.RedirectingToSignIn, AppResource.Success, AppResource.Finally);
                    RegistrationSuccess();
                    break;
                default:
                    await _userDialogs.AlertAsync(AppResource.Unknown, AppResource.Oops, AppResource.Damn); break;
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
