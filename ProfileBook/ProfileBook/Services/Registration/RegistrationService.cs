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
            Status status;
            if (TryFindUser(login)) status = Status.LoginIsTaken;
            if (login.Length > Constants.MaxLoginLength) status = Status.LoginIsTooLong;
            if (login.Length < Constants.MinLoginLength) status = Status.LoginIsTooShort;
            if (char.IsDigit(login[0])) status = Status.LoginStartsWithNumber;
            if (password.Length > Constants.MaxPasswordLength) status = Status.PasswordIsTooLong;
            if (password.Length < Constants.MinPasswordLength) status = Status.PasswordIsTooShort;
            if (!RegexMatch(password)) status = Status.PasswordIsWeak;
            if (!password.Equals(confirmPassword)) status = Status.PasswordsAreNotEqual;
            status = Status.Success;

            return status;
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
