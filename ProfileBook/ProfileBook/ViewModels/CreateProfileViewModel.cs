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
        private readonly ISettingsManager _settingsManager;

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
                RaisePropertyChanged("Nickname");
            }
        }
        public string Name 
        {
            get { return name; }
            set
            {
                name = value;
                RaisePropertyChanged("Name");
            }
        }
        public string Description 
        {
            get { return description; }
            set
            {
                description = value;
                RaisePropertyChanged("Description");
            }
        }
        public string ImageSource { 
            get { return imageSource; }
            set
            {
                imageSource = value;
                RaisePropertyChanged("ImageSource");
            }
        }
        #endregion

        public CreateProfileViewModel(INavigationService navigationService,
            IProfileService profileService,
            ISettingsManager settingsManager)
            : base(navigationService)
        {
            _profileService = profileService;
            _settingsManager = settingsManager;
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
                    UserId = _settingsManager.AuthorizedUserID,
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

        public ICommand ChooseImage => new Command(async () =>
        {
            //if (CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsTakePhotoSupported)
            //{
            //    image = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            //    {
            //        SaveToAlbum = true,
            //        Directory = "Sample",
            //        Name = $"{DateTime.Now:dd.MM.yyyy_hh.mm.ss}.jpg"
            //    });

            //    if (image == null)
            //        return;

            //    ImageSource = image.Path;
            //}

            if (CrossMedia.Current.IsPickPhotoSupported)
            {
                MediaFile image = await CrossMedia.Current.PickPhotoAsync();
                ImageSource = image.Path;
            }
        });
    }
}
