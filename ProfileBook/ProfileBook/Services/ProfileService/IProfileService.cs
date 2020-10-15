using ProfileBook.Models;
using System.Collections.Generic;

namespace ProfileBook.Services.ProfileService
{
    public interface IProfileService
    {
        IEnumerable<Profile> GetProfiles();

        IEnumerable<Profile> GetUserProfiles(int userId);

        Profile GetProfile(int id);

        int DeleteProfile(int id);

        int SaveProfile(Profile item);
    }
}
