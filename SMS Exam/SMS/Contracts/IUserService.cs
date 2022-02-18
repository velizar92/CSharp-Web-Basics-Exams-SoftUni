using SMS.Models;

namespace SMS.Contracts
{
    public interface IUserService
    {

        string GetUsername(string userId);

        (bool registered, string error) Register(RegisterViewModel model);

        string Login(LoginViewModel model);

    }
}
