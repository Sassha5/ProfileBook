using Prism.Navigation;
using ProfileBook.Enums;
using ProfileBook.Localization;
using ProfileBook.Services.Settings;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private bool buttonNameIsChecked;
        private bool buttonNicknameIsChecked;
        private bool buttonDateIsChecked;
        private bool darkThemeIsChecked;
        private string _selectedLanguage;

        #region Properties
        public bool ButtonNameIsChecked
        {
            get { return buttonNameIsChecked; }
            set
            {
                buttonNameIsChecked = value;
                if (value == true) SettingsManager.SortingType = (int)Sorting.Name;
            }
        }
        public bool ButtonNicknameIsChecked
        {
            get { return buttonNicknameIsChecked; }
            set
            {
                buttonNicknameIsChecked = value;
                if (value == true) SettingsManager.SortingType = (int)Sorting.Nickname;
            }
        }
        public bool ButtonDateIsChecked
        {
            get { return buttonDateIsChecked; }
            set
            {
                buttonDateIsChecked = value;
                if (value == true) SettingsManager.SortingType = (int)Sorting.Date;
            }
        }
        public bool DarkThemeIsChecked
        {
            get { return darkThemeIsChecked; }
            set
            {
                darkThemeIsChecked = value;
                if (value == true) SettingsManager.Theme = (int)OSAppTheme.Dark;
                else SettingsManager.Theme = (int)OSAppTheme.Light;
            }
        }
        public string SelectedLanguage
        {
            get { return _selectedLanguage; }
            set
            {
                if(value != null) _selectedLanguage = value;
                SetLanguage();
            }
        }
        public List<string> Languages { get; set; }
        #endregion


        public SettingsViewModel(INavigationService navigationService, ISettingsManager settingsManager)
            : base(navigationService, settingsManager)
        {
            Title = "Settings";
            Languages = new List<string>();
            foreach (string s in System.Enum.GetNames(typeof(Languages))) { Languages.Add(s); }
            SelectedLanguage = SettingsManager.Language;
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            switch (SettingsManager.SortingType)
            {
                case (int)Sorting.Name:
                    ButtonNameIsChecked = true;
                    RaisePropertyChanged($"{nameof(ButtonNameIsChecked)}");
                    break;
                case (int)Sorting.Nickname:
                    ButtonNicknameIsChecked = true;
                    RaisePropertyChanged($"{nameof(ButtonNicknameIsChecked)}");
                    break;
                case (int)Sorting.Date:
                    ButtonDateIsChecked = true;
                    RaisePropertyChanged($"{nameof(ButtonDateIsChecked)}");
                    break;
                default:
                    break;
            }

            if (Application.Current.RequestedTheme == OSAppTheme.Dark)
            {
                DarkThemeIsChecked = true;
                RaisePropertyChanged($"{nameof(DarkThemeIsChecked)}");
            }
        }

        private void SetLanguage()
        {
            SettingsManager.Language = SelectedLanguage;
            MessagingCenter.Send<object, CultureChangedMessage>(this,
                    string.Empty, new CultureChangedMessage(SelectedLanguage));
        }
    }
}
