using SharedTrip.Models;
using System.Collections.Generic;

namespace SharedTrip.Contracts
{
    public interface IUserService
    {
        (bool registered, IEnumerable<ErrorViewModel> errors) Register(RegisterViewModel model);
        string Login(LoginViewModel model);
    }
}
