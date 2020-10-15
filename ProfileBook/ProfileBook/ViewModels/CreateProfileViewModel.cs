using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Navigation;
using ProfileBook.Models;
using ProfileBook.Services.ProfileService;
using ProfileBook.Services.Settings;
using System;
using System.IO;
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
        private ImageSource imageSource;
        private MediaFile image;

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

        public ImageSource ImageSource { 
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
            if (profile != null)
            {
                Nickname = profile.Nickname;
                Name = profile.Name;
                Description = profile.Description;
                ImageSource = profile.ImageSource;
            }
        }

        public ICommand Save => new Command(async () =>
        {
            if (profile == null)
            {
                profile = new Profile
                {
                    UserId = _settingsManager.AuthorizedUserID,
                    Date = DateTime.Now
                };
            }
            if (image != null)
            {
                byte[] byteImage = MediaFileToByteArray(image);
                profile.ImageData = byteImage;
            }
            profile.Nickname = this.Nickname;
            profile.Name = this.Name;
            profile.Description = this.Description;
            _profileService.SaveProfile(profile);
            await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(Views.MainPage)}");
        });

        private byte[] MediaFileToByteArray(MediaFile image)
        {
            using (var memoryStream = new MemoryStream())
            {
                image.GetStream().CopyTo(memoryStream);
                image.Dispose();
                return memoryStream.ToArray();
            }
        }

        public ICommand ChooseImage => new Command(async () =>
        {
            //if (CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsTakePhotoSupported)
            //{
            //    MediaFile file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            //    {
            //        SaveToAlbum = true,
            //        Directory = "Sample",
            //        Name = $"{DateTime.Now:dd.MM.yyyy_hh.mm.ss}.jpg"
            //    });

            //    if (file == null)
            //        return;

            //    ImagePath = file.Path;
            //}

            if (CrossMedia.Current.IsPickPhotoSupported)
            {
                image = await CrossMedia.Current.PickPhotoAsync();
                ImageSource = image.Path;
            }
        });
    }
}
