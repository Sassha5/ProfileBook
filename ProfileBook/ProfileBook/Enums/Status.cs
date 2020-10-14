using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.Enums
{
    public enum Status{
        Success,
        LoginIsTooShort,
        LoginIsTooLong,
        LoginIsTaken,
        LoginStartsWithNumber,
        PasswordIsTooShort,
        PasswordIsTooLong,
        PasswordIsWeak,
        PasswordsAreNotEqual
    }
}
