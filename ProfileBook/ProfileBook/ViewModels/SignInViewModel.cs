using Prism.Navigation;
using ProfileBook.Services.Authorization;
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
            IAuthorizationService authorizationService)
            : base(navigationService)
        {
            Title = "Sign In";
            _authorizationService = authorizationService;
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (_authorizationService.CheckAuthorized())
            {
                Device.BeginInvokeOnMainThread(async () => await ToMainPage());
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
                await ToMainPage();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Oops...", "Wrong credentials", "Cancel");
                Password = string.Empty;
                RaisePropertyChanged("Password");
            }
        });

        private async Task<INavigationResult> ToMainPage()
        {
            return await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(Views.MainPage)}");
        }
    }
}
