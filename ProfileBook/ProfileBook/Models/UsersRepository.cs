using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.Models
{
    public class UsersRepository : IRepository<User>
    {
        SQLiteConnection database;
        public UsersRepository(string databasePath)
        {
            database = new SQLiteConnection(databasePath);
            database.CreateTable<User>();
        }
        public IEnumerable<User> GetItems()
        {
            return database.Table<User>().ToList();
        }
        public User GetItem(int id)
        {
            return database.Get<User>(id);
        }
        public int DeleteItem(int id)
        {
            return database.Delete<User>(id);
        }
        public int SaveItem(User item)
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

        public bool FindUser (string login)
        {
            var t = database.Table<User>().Where(x => x.Login == login).FirstOrDefault();
            if (t != null)
            {
                return true;
            }
            else
                return false;
        }
    }
}
