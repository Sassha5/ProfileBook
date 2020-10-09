using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.Services.Registration
{
    public interface IRegistrationService
    {
        void Register(string login, string password);
    }
}
