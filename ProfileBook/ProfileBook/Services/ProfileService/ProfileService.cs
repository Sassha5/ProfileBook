using ProfileBook.Models;
using ProfileBook.Services.Repository;
using SQLite;
using System.Collections.Generic;

namespace ProfileBook.Services.ProfileService
{
    class ProfileService : IProfileService
    {
        private SQLiteConnection database;

        public ProfileService(IRepository repository)
        {
            database = repository.database;
            database.CreateTable<Profile>();
        }

        public IEnumerable<Profile> GetProfiles()
        {
            return database.Table<Profile>().ToList();
        }

        public Profile GetProfile(int id)
        {
            return database.Get<Profile>(id);
        }

        public int DeleteProfile(int id)
        {
            return database.Delete<Profile>(id);
        }

        public int SaveProfile(Profile item)
        {
            if (item.Id != 0)
            {
                database.Update(item);
                return item.Id;
            }
            else
            {
                return database.Insert(item);
            }
        }
    }
}
