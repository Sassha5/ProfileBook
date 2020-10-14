using ProfileBook.Enums;

namespace ProfileBook.Services.Registration
{
    public interface IRegistrationService
    {
        void Register(string login, string password);
        Status Validate(string login, string password, string confirmPassword);
    }
}
