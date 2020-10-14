using Plugin.Settings.Abstractions;
using ProfileBook.Enums;
using System;
using System.Collections.Generic;
using System.Text;

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
            get => _appSettings.GetValueOrDefault(nameof(AuthorizedUserID), -1);
            set => _appSettings.AddOrUpdateValue(nameof(AuthorizedUserID), value);
        }
        public int SortingType 
        {
            get => _appSettings.GetValueOrDefault(nameof(SortingType), (int)Sorting.Date);
            set => _appSettings.AddOrUpdateValue(nameof(SortingType), value);
        }
    }
}
