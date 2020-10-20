using Prism.Navigation;
using ProfileBook.Models;
using ProfileBook.Pop_ups;
using ProfileBook.Resources;
using ProfileBook.Services.ProfileService;
using ProfileBook.Services.Settings;
using ProfileBook.Views;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
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
                if (labelIsVisible != value)
                {
                    labelIsVisible = value;
                    RaisePropertyChanged($"{nameof(LabelIsVisible)}");
                }
            }
        }


        public MainPageViewModel(INavigationService navigationService,
                                 IProfileService profileService,
                                 ISettingsManager settingsManager)
                                 : base(navigationService,
                                        settingsManager)
        {
            _profileService = profileService;
            _settingsManager = settingsManager;
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            UpdateCollection();
        }

        #region Commands
        private ICommand _CreateProfileCommand;
        public ICommand CreateProfileCommand => _CreateProfileCommand ??= new Command(OnCreateProfileCommandAsync);

        private ICommand _LogoutCommand;
        public ICommand LogoutCommand => _LogoutCommand ??= new Command(OnLogoutCommandAsync);

        private ICommand _SettingsCommand;
        public ICommand SettingsCommand => _SettingsCommand ??= new Command(OnSettingsCommandAsync);

        private ICommand _EditCommand;
        public ICommand EditCommand => _EditCommand ??= new Command<Profile>(OnEditCommandAsync);

        private ICommand _DeleteCommand;
        public ICommand DeleteCommand => _DeleteCommand ??= new Command<Profile>(OnDeleteCommandAsync);
        
        private ICommand _ImageSelectedCommand;
        public ICommand ImageSelectedCommand => _ImageSelectedCommand ??= new Command<Profile>(OnImageSelectedCommandAsync);
        #endregion

        #region Methods
        private Task<bool> Confirm()
        {
            return Application.Current.MainPage.DisplayAlert("", AppResource.SureQuestion, AppResource.Yup, AppResource.Nope);
        }

        private async void OnDeleteCommandAsync(Profile profile)
        {
            bool res = await Confirm();
            if (res)
            {
                _profileService.DeleteProfile(profile.Id);
                UpdateCollection();
            }
        }

        private async void OnCreateProfileCommandAsync()
        {
            await NavigationService.NavigateAsync($"{nameof(CreateProfile)}");
        }

        private async void OnSettingsCommandAsync()
        {
            await NavigationService.NavigateAsync($"{nameof(Settings)}");
        }
        
        private async void OnImageSelectedCommandAsync(Profile profile)
        {
            await PopupNavigation.Instance.PushAsync(new ListImagePopup(profile.ImagePath));
        }

        private async void OnEditCommandAsync(Profile profile)
        {
            NavigationParameters navParams = new NavigationParameters();
            navParams.Add("profile", profile);
            await NavigationService.NavigateAsync($"{nameof(CreateProfile)}", navParams);
        }

        private async void OnLogoutCommandAsync()
        {
            bool res = await Confirm();
            if (res)
            {
                _settingsManager.AuthorizedUserID = Constants.NoAuthorizedUser;
                await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(Views.SignIn)}");
            }
        }

        private void UpdateCollection()
        {
            Profiles = new ObservableCollection<Profile>(_profileService.GetCurrentUserSortedProfiles());
            LabelIsVisible = Profiles.Count() == 0;             //true is collection is empty
            RaisePropertyChanged($"{nameof(Profiles)}");
        }
        #endregion
    }
}
