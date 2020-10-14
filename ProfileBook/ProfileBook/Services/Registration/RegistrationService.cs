using ProfileBook.Enums;
using ProfileBook.Models;
using ProfileBook.Services.Repository;
using ProfileBook.Services.Settings;
using System;
using System.Collections.Generic;
using System.Text;
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
            if (login.Length > 16) return Status.LoginIsTooLong;
            if (login.Length < 4) return Status.LoginIsTooShort;
            if (char.IsDigit(login[0])) return Status.LoginStartsWithNumber;
            if (password.Length > 16) return Status.PasswordIsTooLong;
            if (password.Length < 8) return Status.PasswordIsTooShort;
            Regex regex = new Regex(@"(?=^.{8,16}$)(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\s).*$");
            //"(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$" another regex to try
            if (regex.IsMatch(password)) return Status.PasswordIsWeak; //not working right
            if (!password.Equals(confirmPassword)) return Status.PasswordsAreNotEqual;

            return Status.Success;
        }
    }
}
