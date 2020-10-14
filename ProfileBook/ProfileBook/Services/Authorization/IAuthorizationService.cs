namespace ProfileBook.Services.Authorization
{
    public interface IAuthorizationService
    {
        bool Authorize(string login, string password);
        bool CheckAuthorized();
    }
}
