﻿using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Models;
using ProfileBook.Services.ProfileService;
using ProfileBook.Services.Settings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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
            var profiles = _profileService.GetProfiles().Where(x => x.UserId == _settingsManager.AuthorizedUserID);
            Profiles = new ObservableCollection<Profile>();
            foreach (Profile profile in profiles)
            {
                Profiles.Add(profile);
            }
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            UpdateCollection();
        }

        public ICommand CreateProfile => new Command(async () =>
        {
            await NavigationService.NavigateAsync("CreateProfile");
        });

        public ICommand Logout => new Command(async () =>
        {
            _settingsManager.AuthorizedUserID = -1;
            await NavigationService.NavigateAsync("/NavigationPage/SignIn");
        });

        public ICommand Settings => new Command(async () =>
        {
            await NavigationService.NavigateAsync("Settings");
        });

        public ICommand Edit => new Command(async () =>
        {
            await Application.Current.MainPage.DisplayAlert("hi", "hello", "cancel");
            //await NavigationService.NavigateAsync("Settings");
        });

        public ICommand Delete => new Command(async (object arg) =>
        {
            await Application.Current.MainPage.DisplayAlert("hi", "hello", "cancel");
            //Profile profile = arg as Profile;
            //App.ProfilesDatabase.DeleteItem(profile.Id);
        });
    }
}
