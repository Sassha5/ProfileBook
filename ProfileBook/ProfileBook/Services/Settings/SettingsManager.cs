using Plugin.Settings.Abstractions;
using ProfileBook.Localization;
using Xamarin.Forms;

namespace ProfileBook.Services.Settings
{
    class SettingsManager : ISettingsManager
    {
        private readonly ISettings _appSettings;

        public SettingsManager(ISettings appSettings)
        {
            _appSettings = appSettings;
        }
        public int AuthorizedUserID
        {
            get => _appSettings.GetValueOrDefault(nameof(AuthorizedUserID), Constants.NoAuthorizedUser);
            set => _appSettings.AddOrUpdateValue(nameof(AuthorizedUserID), value);
        }
        public int SortingType
        {
            get => _appSettings.GetValueOrDefault(nameof(SortingType), Constants.DefaultSorting);
            set => _appSettings.AddOrUpdateValue(nameof(SortingType), value);
        }
        public int Theme
        {
            get => _appSettings.GetValueOrDefault(nameof(Theme), (int)OSAppTheme.Light);
            set
            {
                _appSettings.AddOrUpdateValue(nameof(Theme), value);
                Application.Current.UserAppTheme = (OSAppTheme)value;
            }
        }
        public string Language
        {
            get => _appSettings.GetValueOrDefault(nameof(Language), Constants.DefaultLanguage);
            set
            {
                _appSettings.AddOrUpdateValue(nameof(Language), value);
                MessagingCenter.Send<object, CultureChangedMessage>(this, string.Empty, new CultureChangedMessage(value));
            }
        }
    }
}
