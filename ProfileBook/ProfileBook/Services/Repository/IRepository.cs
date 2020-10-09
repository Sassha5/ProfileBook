using ProfileBook.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.Services.Repository
{
    public interface IRepository
    {
        SQLiteConnection database { get; set; }
        IEnumerable<User> GetUsers();
        User GetUser(int id);
        int DeleteUser(int id);
        int SaveUser(User item);
        bool FindUser(string login);
    }
}
