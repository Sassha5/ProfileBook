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

        private Profile profile;
        private string nickname;
        private string name;
        private string description;
        private string imageSource;

        #region Properties
        public string Nickname
        {
            get { return nickname; }
            set
            {
                nickname = value;
                RaisePropertyChanged($"{nameof(Nickname)}");
            }
        }
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                RaisePropertyChanged($"{nameof(Name)}");
            }
        }
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                RaisePropertyChanged($"{nameof(Description)}");
            }
        }
        public string ImageSource
        {
            get { return imageSource; }
            set
            {
                imageSource = value;
                RaisePropertyChanged($"{nameof(ImageSource)}");
            }
        }
        #endregion

        public CreateProfileViewModel(INavigationService navigationService,
            IProfileService profileService,
            ISettingsManager settingsManager)
            : base(navigationService, settingsManager)
        {
            _profileService = profileService;
            ImageSource = Constants.DefaultProfileImage;
            Title = "New Profile";
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            profile = parameters.GetValue<Profile>("profile");
            if (profile != null)  //true when navigated from edit button
            {
                Nickname = profile.Nickname;
                Name = profile.Name;
                Description = profile.Description;
                ImageSource = profile.ImagePath;
            }
            else
            {
                profile = new Profile
                {
                    UserId = SettingsManager.AuthorizedUserID,
                    Date = DateTime.Now
                };
            }
        }

        public ICommand Save => new Command(async () =>
        {
            profile.Nickname = this.Nickname;
            profile.Name = this.Name;
            profile.Description = this.Description;
            profile.ImagePath = ImageSource;
            _profileService.SaveProfile(profile);
            await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(Views.MainPage)}");
        });

        public ICommand ChooseImage => new Command(() =>
        {
            UserDialogs.Instance.ActionSheet(new ActionSheetConfig() 
                            { Cancel = new ActionSheetOption("Cancel") }
                            .SetTitle("Choose Type")
                            .Add("Gallery", () => PickFromGallery(), null)
                            .Add("Camera", () => TakePhoto(), null)
                        );
        });

        private async void PickFromGallery()
        {
            if (CrossMedia.Current.IsPickPhotoSupported)
            {
                MediaFile image = await CrossMedia.Current.PickPhotoAsync();
                ImageSource = image.Path;
            }
        }

        private async void TakePhoto()
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
        }
    }
}
