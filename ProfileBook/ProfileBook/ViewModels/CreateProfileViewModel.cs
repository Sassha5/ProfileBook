using Acr.UserDialogs;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Navigation;
using ProfileBook.Models;
using ProfileBook.Services.ProfileService;
using ProfileBook.Services.Settings;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class CreateProfileViewModel : ViewModelBase
    {
        private readonly IProfileService _profileService;
        private readonly IUserDialogs _userDialogs;

        private Profile _profile;
        
        #region Properties
        private string _nickname;
        public string Nickname
        {
            get => _nickname;
            set => SetProperty(ref _nickname, value);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private string _imageSource;
        public string ImageSource
        {
            get => _imageSource;
            set => SetProperty(ref _imageSource, value);
        }
        #endregion

        public CreateProfileViewModel(INavigationService navigationService,
            IProfileService profileService,
            ISettingsManager settingsManager,
            IUserDialogs userDialogs)
            : base(navigationService, settingsManager)
        {
            _userDialogs = userDialogs;
            _profileService = profileService;
            ImageSource = Constants.DefaultProfileImage;
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.TryGetValue("profile", out Profile _profile))  //true when navigated from edit button
            {
                Nickname = _profile.Nickname;
                Name = _profile.Name;
                Description = _profile.Description;
                ImageSource = _profile.ImagePath;
            }
            else
            {
                _profile = new Profile
                {
                    UserId = SettingsManager.AuthorizedUserID,
                    Date = DateTime.Now
                };
            }
        }

        private ICommand _SaveCommand;
        public ICommand SaveCommand => _SaveCommand ??= new Command(OnSaveCommandAsync);

        private ICommand _ImageClickedCommand;
        public ICommand ImageClickedCommand => _ImageClickedCommand ??= new Command(OnImageClickedCommand);

        private void OnImageClickedCommand()
        {
            _userDialogs.ActionSheet(new ActionSheetConfig()
            { Cancel = new ActionSheetOption("Cancel") }
                            .SetTitle("Choose Type")
                            .Add("Gallery", () => PickFromGalleryAsync(), null)
                            .Add("Camera", () => TakePhotoAsync(), null)
                        );
        }
        private async void OnSaveCommandAsync()
        {
            _profile.Nickname = this.Nickname;
            _profile.Name = this.Name;
            _profile.Description = this.Description;
            _profile.ImagePath = ImageSource;
            _profileService.SaveProfile(_profile);
            await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(Views.MainPage)}");
        }

        private async void PickFromGalleryAsync()
        {
            if (CrossMedia.Current.IsPickPhotoSupported)
            {
                MediaFile image = await CrossMedia.Current.PickPhotoAsync();
                ImageSource = image.Path;
            }
            else
            {
                _userDialogs.Alert("Not supported", "Sorry", "Ok"); //move to resources
            }
        }

        private async void TakePhotoAsync()
        {
            if (CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsTakePhotoSupported)
            {
                MediaFile image = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    Name = $"{DateTime.Now:dd.MM.yyyy_hh.mm.ss}.jpg"
                });

                if (image == null)
                    return;

                ImageSource = image.Path;
            }
            else
            {
                _userDialogs.Alert("Not supported", "Sorry", "Ok"); //move to resources
            }
        }
    }
}
