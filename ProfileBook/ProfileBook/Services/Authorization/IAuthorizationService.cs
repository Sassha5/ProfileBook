using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.Services.Authorization
{
    public interface IAuthorizationService
    {
        bool Authorize(string login, string password);
        bool CheckAuthorized();
    }
}
