using ProfileBook.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.Services.Settings
{
    public interface ISettingsManager
    {
        int AuthorizedUserID { get; set; }
        int SortingType { get; set; }
    }
}
