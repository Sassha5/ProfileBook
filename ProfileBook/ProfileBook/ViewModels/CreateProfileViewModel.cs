using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Models;
using ProfileBook.Services.ProfileService;
using ProfileBook.Services.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private string imagePath;

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

        public string ImagePath { 
            get { return imagePath; }
            set
            {
                imagePath = value;
                RaisePropertyChanged("Description");
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
            ImagePath = "pic_profile.png";
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
            profile.Nickname = this.Nickname;
            profile.Name = this.Name;
            profile.Description = this.Description;
            _profileService.SaveProfile(profile);
            await NavigationService.NavigateAsync("/NavigationPage/MainPage");
        });

        public ICommand ChooseImage => new Command(async () =>
        {
            if (CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsTakePhotoSupported)
            {
                MediaFile file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    SaveToAlbum = true,
                    Directory = "Sample",
                    Name = $"{DateTime.Now:dd.MM.yyyy_hh.mm.ss}.jpg"
                });

                if (file == null)
                    return;

                ImagePath = file.Path;
            }
            //if (CrossMedia.Current.IsPickPhotoSupported)
            //{
            //    MediaFile photo = await CrossMedia.Current.PickPhotoAsync();
            //    ProfileImage = ImageSource.FromFile(photo.Path);
            //}
        });
    }
}
