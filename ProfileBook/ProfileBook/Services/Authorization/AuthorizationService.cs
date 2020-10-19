using ProfileBook.Models;
using ProfileBook.Services.Repository;
using ProfileBook.Services.Settings;
using System.Linq;

namespace ProfileBook.Services.Authorization
{
    class AuthorizationService : IAuthorizationService
    {
        private readonly IRepository<User> _repository;
        private readonly ISettingsManager _settingsManager;
        public AuthorizationService(IRepository<User> repository, ISettingsManager settingsManager)
        {
            _repository = repository;
            _settingsManager = settingsManager;
        }

        public bool Authorize(string login, string password)
        {
            var user = _repository.GetItems().FirstOrDefault(x => x.Login == login && x.Password == password);
            if (user != null)
            {
                _settingsManager.AuthorizedUserID = user.Id;
                return true;
            }
            return false;
        }
    }
}
