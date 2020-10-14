using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ProfileBook.Enums;
using ProfileBook.Services.Settings;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProfileBook.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private readonly ISettingsManager _settingsManager;
        private bool buttonNameIsChecked;
        private bool buttonNicknameIsChecked;
        private bool buttonDateIsChecked;

        public bool ButtonNameIsChecked
        {
            get { return buttonNameIsChecked; }
            set 
            {
                buttonNameIsChecked = value;
                _settingsManager.SortingType = (int)Sorting.Name; 
            }
        }

        public bool ButtonNicknameIsChecked
        {
            get { return buttonNicknameIsChecked; }
            set { _settingsManager.SortingType = (int)Sorting.Nickname; }
        }
        public bool ButtonDateIsChecked
        {
            get { return buttonDateIsChecked; }
            set { _settingsManager.SortingType = (int)Sorting.Date; }
        }

        public SettingsViewModel(INavigationService navigationService, ISettingsManager settingsManager)
            : base(navigationService)
        {
            Title = "Settings";
            _settingsManager = settingsManager;
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (_settingsManager.SortingType == (int)Sorting.Name)
            {
                ButtonNameIsChecked = true; //why not checking in view?
            }
        }
    }
}
