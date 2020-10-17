using ProfileBook.Models;
using System.Collections.Generic;

namespace ProfileBook.Services.ProfileService
{
    public interface IProfileService
    {
        IEnumerable<Profile> GetCurrentUserProfiles();
        IEnumerable<Profile> GetCurrentUserSortedProfiles();
        Profile GetProfile(int id);
        int DeleteProfile(int id);
        int SaveProfile(Profile item);
    }
}
