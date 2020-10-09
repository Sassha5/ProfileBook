using ProfileBook.Models;
using ProfileBook.Services.Repository;
using ProfileBook.Services.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProfileBook.Services.Authorization
{
    class AuthorizationService : IAuthorizationService
    {
        private readonly IRepository _repository;
        public AuthorizationService(IRepository repository)
        {
            _repository = repository;
        }

        public bool Authorize(string login, string password)
        {
            var user = _repository.GetUsers().FirstOrDefault(x => x.Login == login && x.Password == password);
            if (user != null)
            {
                App.currentUser = user;
                return true;
            }
            return false;
        }
    }
}
