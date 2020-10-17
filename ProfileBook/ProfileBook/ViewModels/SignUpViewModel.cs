using Prism.Navigation;
using ProfileBook.Enums;
using ProfileBook.Resources;
using ProfileBook.Services.Registration;
using ProfileBook.Views;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class SignUpViewModel : ViewModelBase
    {
        private readonly IRegistrationService _registrationService;

        public string Login { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }


        public SignUpViewModel(INavigationService navigationService,
            IRegistrationService registrationService)
            : base(navigationService)
        {
            _registrationService = registrationService;
            Title = "Sign Up";
        }

        public ICommand Register => new Command(async () =>
        {
            switch (_registrationService.Validate(Login, Password, ConfirmPassword))
            {
                case Status.LoginIsTaken:
                    await Application.Current.MainPage.DisplayAlert(AppResource.Oops, AppResource.LoginIsTaken, AppResource.Damn); ;
                    break;
                case Status.LoginIsTooLong:
                    await Application.Current.MainPage.DisplayAlert(AppResource.Oops, AppResource.LoginIsTooLong, AppResource.Ok);
                    break;
                case Status.LoginIsTooShort:
                    await Application.Current.MainPage.DisplayAlert(AppResource.Oops, AppResource.LoginIsTooShort, AppResource.Ok);
                    break;
                case Status.LoginStartsWithNumber:
                    await Application.Current.MainPage.DisplayAlert(AppResource.Oops, AppResource.LoginStartsWithNumber, AppResource.Damn);
                    break;
                case Status.PasswordIsTooLong:
                    await Application.Current.MainPage.DisplayAlert(AppResource.Oops, AppResource.PasswordIsTooLong, AppResource.Ok);
                    break;
                case Status.PasswordIsTooShort:
                    await Application.Current.MainPage.DisplayAlert(AppResource.Oops, AppResource.PasswordIsTooShort, AppResource.Damn);
                    break;
                case Status.PasswordIsWeak:
                    await Application.Current.MainPage.DisplayAlert(AppResource.Oops, AppResource.PasswordIsWeak, AppResource.Sure);
                    break;
                case Status.PasswordsAreNotEqual:
                    await Application.Current.MainPage.DisplayAlert(AppResource.Oops, AppResource.PasswordsAreNotEqual, AppResource.Thanks);
                    break;
                case Status.Success:
                    await Application.Current.MainPage.DisplayAlert(AppResource.Success, AppResource.RedirectingToSignIn, AppResource.Finally);
                    RegistrationSuccess();
                    break;
                default:
                    await Application.Current.MainPage.DisplayAlert(AppResource.Oops, AppResource.Unknown, AppResource.Damn);
                    break;
            }
        });

        private async void RegistrationSuccess()
        {
            _registrationService.Register(Login, Password);
            NavigationParameters navParams = new NavigationParameters();
            navParams.Add($"{nameof(Login)}", Login);
            await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(SignIn)}", navParams);
        }
    }
}
