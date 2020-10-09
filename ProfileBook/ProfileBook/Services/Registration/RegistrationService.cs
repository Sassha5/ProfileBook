using ProfileBook.Models;
using ProfileBook.Services.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.Services.Registration
{
    class RegistrationService : IRegistrationService
    {
        private readonly IRepository _repository;

        public RegistrationService(IRepository repository)
        {
            _repository = repository;
        }
        public void Register(string login, string password)
        {
            User user = new User
            {
                Login = login,
                Password = password
            };
            App.currentUser = user;
            _repository.SaveUser(user);
        }
    }
}
