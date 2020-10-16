using ProfileBook.Enums;
using ProfileBook.Models;
using ProfileBook.Services.Repository;
using ProfileBook.Services.Settings;
using System.Text.RegularExpressions;

namespace ProfileBook.Services.Registration
{
    class RegistrationService : IRegistrationService
    {
        private readonly IRepository _repository;
        private readonly ISettingsManager _settingsManager;

        public RegistrationService(IRepository repository, ISettingsManager settingsManager)
        {
            _repository = repository;
            _settingsManager = settingsManager;
        }
        public void Register(string login, string password)
        {
            User user = new User
            {
                Login = login,
                Password = password
            };
            _settingsManager.AuthorizedUserID = user.Id;
            _repository.SaveUser(user);
        }

        public Status Validate(string login, string password, string confirmPassword)
        {
            if (_repository.FindUser(login) == true) return Status.LoginIsTaken;
            if (login.Length > Constants.MaxLoginLength) return Status.LoginIsTooLong;
            if (login.Length < Constants.MinLoginLength) return Status.LoginIsTooShort;
            if (char.IsDigit(login[0])) return Status.LoginStartsWithNumber;
            if (password.Length > Constants.MaxPasswordLength) return Status.PasswordIsTooLong;
            if (password.Length < Constants.MinPasswordLength) return Status.PasswordIsTooShort;
            if (!RegexMatch(password)) return Status.PasswordIsWeak;
            if (!password.Equals(confirmPassword)) return Status.PasswordsAreNotEqual;

            return Status.Success;
        }

        private bool RegexMatch(string pass)
        {
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasLowerChar = new Regex(@"[a-z]+");

            return hasNumber.IsMatch(pass) && hasUpperChar.IsMatch(pass) && hasLowerChar.IsMatch(pass);
        }
    }
}
