using Prism.Navigation;
using ProfileBook.Models;
using ProfileBook.Pop_ups;
using ProfileBook.Resources;
using ProfileBook.Services.ProfileService;
using ProfileBook.Services.Settings;
using Rg.Plugins.Popup.Services;
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
        private bool labelIsVisible;

        public ObservableCollection<Profile> Profiles { get; set; }
        public bool LabelIsVisible 
        {
            get { return labelIsVisible; } 
            set
            {
                if(labelIsVisible != value)
                {
                    labelIsVisible = value;
                    RaisePropertyChanged($"{nameof(LabelIsVisible)}");
                }
            }
        }


        public MainPageViewModel(INavigationService navigationService,
            IProfileService profileService,
            ISettingsManager settingsManager)
            : base(navigationService)
        {
            _profileService = profileService;
            _settingsManager = settingsManager;
            Title = "Main Page";
        }

        private void UpdateCollection()
        {
            Profiles = new ObservableCollection<Profile>(_profileService.GetCurrentUserSortedProfiles());
            LabelIsVisible = Profiles.Count() == 0;             //true is collection is empty
            RaisePropertyChanged($"{nameof(Profiles)}");
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
            if (await Confirm())
            {
                _settingsManager.AuthorizedUserID = Constants.NoAuthorizedUser;
                await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(Views.SignIn)}");
            }
        });

        public ICommand Settings => new Command(async () =>
        {
            await NavigationService.NavigateAsync($"{nameof(Settings)}");
        });

        public ICommand Edit => new Command(async (object arg) =>
        {
            NavigationParameters navParams = new NavigationParameters();
            navParams.Add("profile", arg as Profile);
            await NavigationService.NavigateAsync($"{nameof(CreateProfile)}", navParams);
        });

        public ICommand Delete => new Command(async (object arg) =>
        {
            if (await Confirm())
            {
                _profileService.DeleteProfile((arg as Profile).Id);
                UpdateCollection();
            }
        });

        public ICommand OnImageSelected => new Command(async (object arg) =>
        {
            await PopupNavigation.Instance.PushAsync(new ListImagePopup((arg as Profile).ImagePath));
        });

        private async System.Threading.Tasks.Task<bool> Confirm()
        {
            return await Application.Current.MainPage.DisplayAlert("", AppResource.SureQuestion, AppResource.Yup, AppResource.Nope);
        }
    }
}
