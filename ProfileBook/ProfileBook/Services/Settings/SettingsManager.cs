using Plugin.Settings.Abstractions;

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
    }
}
