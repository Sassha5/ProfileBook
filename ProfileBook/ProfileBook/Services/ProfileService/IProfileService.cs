using ProfileBook.Models;
using ProfileBook.Services.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.Services.ProfileService
{
    public interface IProfileService
    {
        IEnumerable<Profile> GetProfiles();

        Profile GetProfile(int id);

        int DeleteProfile(int id);

        int SaveProfile(Profile item);
    }
}
