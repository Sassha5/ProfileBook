using Plugin.Settings.Abstractions;
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
    }
}
