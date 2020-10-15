using Prism.Navigation;
using ProfileBook.Enums;
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
                    await Application.Current.MainPage.DisplayAlert("Oops...", "Looks like someone already took this login", "Damn...");
                    break;
                case Status.LoginIsTooLong:
                    await Application.Current.MainPage.DisplayAlert("Oh my...", "You've got such a long login...", ";)");
                    break;
                case Status.LoginIsTooShort:
                    await Application.Current.MainPage.DisplayAlert("Oops...", "Add few more letters, login is too short", "Ok");
                    break;
                case Status.LoginStartsWithNumber:
                    await Application.Current.MainPage.DisplayAlert("No.", "Do not start login with a number. I dont like it.", "Damn...");
                    break;
                case Status.PasswordIsTooLong:
                    await Application.Current.MainPage.DisplayAlert("What?!", "Why is there a limit on a pass length? Idk", "Lol");
                    break;
                case Status.PasswordIsTooShort:
                    await Application.Current.MainPage.DisplayAlert("Bro...", "I know that size don't matter, but this is too short of a pass", ":(");
                    break;
                case Status.PasswordIsWeak:
                    await Application.Current.MainPage.DisplayAlert("Stronga!", "Your pass is weak! Better start lifting.", "Sure");
                    break;
                case Status.PasswordsAreNotEqual:
                    await Application.Current.MainPage.DisplayAlert("Hey", "You are supposed to enter the same pass in both fields", "Ah... Thanks!");
                    break;
                case Status.Success:
                    _registrationService.Register(Login, Password);
                    await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(MainPage)}");
                    break;
                default:
                    await Application.Current.MainPage.DisplayAlert("Oops...", "Unknown Status Code", "Damn...");
                    break;
            }
        });
    }
}
