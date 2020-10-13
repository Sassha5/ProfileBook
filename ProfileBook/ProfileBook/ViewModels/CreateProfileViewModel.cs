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

        public string Nickname { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ImageSource ProfileImage { get; set; }


        public CreateProfileViewModel(INavigationService navigationService,
            IProfileService profileService,
            ISettingsManager settingsManager)
            : base(navigationService)
        {
            _profileService = profileService;
            _settingsManager = settingsManager;
            Title = "New Profile";
        }

        public ICommand Add => new Command(async () =>
        {
            _profileService.SaveProfile(new Profile
            {
                Nickname = this.Nickname,
                Name = this.Name,
                Description = this.Description,
                UserId = _settingsManager.AuthorizedUserID
            });
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
                    Name = $"{DateTime.Now.ToString("dd.MM.yyyy_hh.mm.ss")}.jpg"
                });

                if (file == null)
                    return;

                ProfileImage = ImageSource.FromFile(file.Path);
            }
            //if (CrossMedia.Current.IsPickPhotoSupported)
            //{
            //    MediaFile photo = await CrossMedia.Current.PickPhotoAsync();
            //    ProfileImage = ImageSource.FromFile(photo.Path);
            //}
        });
    }
}
