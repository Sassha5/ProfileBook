using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.Models
{
    public class ProfilesRepository
    {
        SQLiteConnection database;
        public ProfilesRepository(string databasePath)
        {
            database = new SQLiteConnection(databasePath);
            database.CreateTable<Profile>();
        }
        public IEnumerable<Profile> GetItems()
        {
            return database.Table<Profile>().ToList();
        }
        public Profile GetItem(int id)
        {
            return database.Get<Profile>(id);
        }
        public int DeleteItem(int id)
        {
            return database.Delete<Profile>(id);
        }
        public int SaveItem(Profile item)
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
