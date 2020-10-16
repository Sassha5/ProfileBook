using Prism.Navigation;
using ProfileBook.Enums;
using ProfileBook.Models;
using ProfileBook.Services.ProfileService;
using ProfileBook.Services.Settings;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly IProfileService _profileService;
        private readonly ISettingsManager _settingsManager;

        public ObservableCollection<Profile> Profiles { get; set; }


        public MainPageViewModel(INavigationService navigationService,
            IProfileService profileService,
            ISettingsManager settingsManager)
            : base(navigationService)
        {
            _profileService = profileService;
            _settingsManager = settingsManager;
            Title = "Main Page";
            UpdateCollection();
        }

        private void UpdateCollection()
        {
            var profiles = _profileService.GetUserProfiles(_settingsManager.AuthorizedUserID);
            switch (_settingsManager.SortingType)
            {
                case (int)Sorting.Date:
                    Profiles = new ObservableCollection<Profile>(profiles.OrderBy(x => x.Date).ToList());
                    break;
                case (int)Sorting.Name:
                    Profiles = new ObservableCollection<Profile>(profiles.OrderBy(x => x.Name).ToList());
                    break;
                case (int)Sorting.Nickname:
                    Profiles = new ObservableCollection<Profile>(profiles.OrderBy(x => x.Nickname).ToList());
                    break;
                default:
                    break;
            }
            RaisePropertyChanged("Profiles");
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            UpdateCollection();
        }

        public ICommand CreateProfile => new Command(async () =>
        {
            await NavigationService.NavigateAsync($"{nameof(CreateProfile)}");
        });

        public ICommand Logout => new Command(async () =>
        {
            bool result = await Application.Current.MainPage.DisplayAlert("", "Sure?", "Sure", "Nope");
            if (result)
            {
                _settingsManager.AuthorizedUserID = Constants.NoAuthorizedUser;
                await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(Views.SignIn)}");
            }
        });

        public ICommand Settings => new Command(async () =>
        {
            await NavigationService.NavigateAsync("Settings");
        });

        public ICommand Edit => new Command(async (object arg) =>
        {
            Profile profile = arg as Profile;
            NavigationParameters navParams = new NavigationParameters();
            navParams.Add("profile", profile);
            await NavigationService.NavigateAsync($"{nameof(CreateProfile)}", navParams);
        });

        public ICommand Delete => new Command(async (object arg) =>
        {
            bool result = await Application.Current.MainPage.DisplayAlert("", "Sure?", "Sure", "Nope");
            if (result)
            {
                Profile profile = arg as Profile;
                _profileService.DeleteProfile(profile.Id);
                UpdateCollection();
            }
        });
    }
}
