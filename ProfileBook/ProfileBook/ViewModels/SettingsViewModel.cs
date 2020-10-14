using Prism.Navigation;
using ProfileBook.Enums;
using ProfileBook.Services.Settings;

namespace ProfileBook.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private readonly ISettingsManager _settingsManager;
        private bool buttonNameIsChecked;
        private bool buttonNicknameIsChecked;
        private bool buttonDateIsChecked;

        #region Properties
        public bool ButtonNameIsChecked
        {
            get { return buttonNameIsChecked; }
            set
            {
                buttonNameIsChecked = value;
                if (value == true) _settingsManager.SortingType = (int)Sorting.Name;
            }
        }

        public bool ButtonNicknameIsChecked
        {
            get { return buttonNicknameIsChecked; }
            set
            {
                buttonNicknameIsChecked = value;
                if (value == true) _settingsManager.SortingType = (int)Sorting.Nickname;
            }
        }
        public bool ButtonDateIsChecked
        {
            get { return buttonDateIsChecked; }
            set
            {
                buttonDateIsChecked = value;
                if (value == true) _settingsManager.SortingType = (int)Sorting.Date;
            }
        }
        #endregion

        public SettingsViewModel(INavigationService navigationService, ISettingsManager settingsManager)
            : base(navigationService)
        {
            Title = "Settings";
            _settingsManager = settingsManager;
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            switch (_settingsManager.SortingType)
            {
                case (int)Sorting.Name:
                    ButtonNameIsChecked = true;
                    RaisePropertyChanged("ButtonNameIsChecked");
                    break;
                case (int)Sorting.Nickname:
                    ButtonNicknameIsChecked = true;
                    RaisePropertyChanged("ButtonNicknameIsChecked");
                    break;
                case (int)Sorting.Date:
                    ButtonDateIsChecked = true;
                    RaisePropertyChanged("ButtonDateIsChecked");
                    break;
                default:
                    break;
            }
        }
    }
}
