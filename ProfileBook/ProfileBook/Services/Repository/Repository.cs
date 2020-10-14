using ProfileBook.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;

namespace ProfileBook.Services.Repository
{
    public class Repository : IRepository
    {
        public SQLiteConnection database { get; set; }

        public Repository()
        {
            database = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Constants.DATABASE_NAME));
            database.CreateTable<User>();
        }

        public bool FindUser(string login)
        {
            var t = database.Table<User>().Where(x => x.Login == login).FirstOrDefault();
            if (t != null)
            {
                return true;
            }
            else
                return false;
        }

        public IEnumerable<User> GetUsers()
        {
            return database.Table<User>().ToList();
        }

        public User GetUser(int id)
        {
            return database.Get<User>(id);
        }

        public int DeleteUser(int id)
        {
            return database.Delete<User>(id);
        }

        public int SaveUser(User item)
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
