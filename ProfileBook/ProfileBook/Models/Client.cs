using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.Models
{
    public class Client
    {
        public void Register(string login, string password)
        {
            App.Database.SaveItem(new User
            {
                Login = login,
                Password = password
            });
        }

        public bool Authorize(string login, string password)
        {
            var users = App.Database.GetItems();
            foreach (User user in users)
            {
                if (user.Login == login && user.Password == password)
                {
                    App.currentUser = user;
                    return true;
                }
            }
            return false;
        }
    }
}
