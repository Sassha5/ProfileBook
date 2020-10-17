using ProfileBook.Enums;
using ProfileBook.Models;
using ProfileBook.Services.Repository;
using ProfileBook.Services.Settings;
using System.Linq;
using System.Text.RegularExpressions;

namespace ProfileBook.Services.Registration
{
    class RegistrationService : IRegistrationService
    {
        private readonly IRepository<User> _repository;

        public RegistrationService(IRepository<User> repository)
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
            _repository.InsertItem(user);
        }

        public Status Validate(string login, string password, string confirmPassword)
        {
            if (TryFindUser(login)) return Status.LoginIsTaken;
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

        private bool TryFindUser(string login)
        {
            User user = _repository.GetItems().Where(x => x.Login == login).FirstOrDefault();
            return user != null;
        }
    }
}
