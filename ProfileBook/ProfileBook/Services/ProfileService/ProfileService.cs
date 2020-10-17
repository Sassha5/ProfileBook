using ProfileBook.Enums;
using ProfileBook.Models;
using ProfileBook.Services.Repository;
using ProfileBook.Services.Settings;
using System.Collections.Generic;
using System.Linq;

namespace ProfileBook.Services.ProfileService
{
    class ProfileService : IProfileService
    {
        private readonly IRepository<Profile> _repository;
        private readonly ISettingsManager _settingsManager;

        public ProfileService(IRepository<Profile> repository, ISettingsManager settingsManager)
        {
            _repository = repository;
            _settingsManager = settingsManager;
        }

        public IEnumerable<Profile> GetCurrentUserProfiles()
        {
            return _repository.GetItems().Where(x => x.UserId == _settingsManager.AuthorizedUserID).ToList();
        }

        public IEnumerable<Profile> GetCurrentUserSortedProfiles()
        {
            IEnumerable<Profile> profiles = GetCurrentUserProfiles();
            switch (_settingsManager.SortingType)
            {
                case (int)Sorting.Date:
                    profiles = profiles.OrderBy(x => x.Date).ToList();
                    break;
                case (int)Sorting.Name:
                    profiles = profiles.OrderBy(x => x.Name).ToList();
                    break;
                case (int)Sorting.Nickname:
                    profiles = profiles.OrderBy(x => x.Nickname).ToList();
                    break;
                default:
                    break;
            }
            return profiles;
        }

        public Profile GetProfile(int id)
        {
            return _repository.GetItem(id);
        }

        public int DeleteProfile(int id)
        {
            return _repository.DeleteItem(id);
        }

        public int SaveProfile(Profile item)
        {
            if (item.Id != 0)
            {
                _repository.UpdateItem(item);
                return item.Id;
            }
            else
            {
                return _repository.InsertItem(item);
            }
        }
    }
}
