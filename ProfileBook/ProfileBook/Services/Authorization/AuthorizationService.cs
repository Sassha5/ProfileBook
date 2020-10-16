using ProfileBook.Services.Repository;
using ProfileBook.Services.Settings;
using System.Linq;

namespace ProfileBook.Services.Authorization
{
    class AuthorizationService : IAuthorizationService
    {
        private readonly IRepository _repository;
        private readonly ISettingsManager _settingsManager;
        public AuthorizationService(IRepository repository, ISettingsManager settingsManager)
        {
            _repository = repository;
            _settingsManager = settingsManager;
        }

        public bool Authorize(string login, string password)
        {
            var user = _repository.GetUsers().FirstOrDefault(x => x.Login == login && x.Password == password);
            if (user != null)
            {
                _settingsManager.AuthorizedUserID = user.Id;
                return true;
            }
            return false;
        }

        public bool CheckAuthorized()
        {
            return _settingsManager.AuthorizedUserID != Constants.NoAuthorizedUser;
        }
    }
}
