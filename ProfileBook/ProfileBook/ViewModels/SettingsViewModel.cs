using Prism.Navigation;
using ProfileBook.Enums;
using ProfileBook.Localization;
using ProfileBook.Services.Settings;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProfileBook.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        #region Properties
        private bool _buttonNameIsChecked;
        public bool ButtonNameIsChecked
        {
            get => _buttonNameIsChecked;
            set
            {
                _buttonNameIsChecked = value;
                if (value) { SettingsManager.SortingType = (int)Sorting.Name; }
            }
        }

        private bool _buttonNicknameIsChecked;
        public bool ButtonNicknameIsChecked
        {
            get => _buttonNicknameIsChecked;
            set
            {
                _buttonNicknameIsChecked = value;
                if (value) { SettingsManager.SortingType = (int)Sorting.Nickname; }
            }
        }

        private bool _buttonDateIsChecked;
        public bool ButtonDateIsChecked
        {
            get => _buttonDateIsChecked;
            set
            {
                _buttonDateIsChecked = value;
                if (value) { SettingsManager.SortingType = (int)Sorting.Date; }
            }
        }

        private bool _darkThemeIsChecked;
        public bool DarkThemeIsChecked
        {
            get { return _darkThemeIsChecked; }
            set
            {
                _darkThemeIsChecked = value;
                if (value) { SettingsManager.Theme = (int)OSAppTheme.Dark; }
                else { SettingsManager.Theme = (int)OSAppTheme.Light; }
            }
        }

        private string _selectedLanguage;
        public string SelectedLanguage
        {
            get { return _selectedLanguage; }
            set
            {
                if (!string.IsNullOrEmpty(value)) { _selectedLanguage = value; }
                SettingsManager.Language = SelectedLanguage;
            }
        }
        public List<string> Languages { get; set; }
        #endregion


        public SettingsViewModel(INavigationService navigationService, ISettingsManager settingsManager)
            : base(navigationService, settingsManager)
        {
            Languages = new List<string>(System.Enum.GetNames(typeof(Languages)));

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
    }
}
