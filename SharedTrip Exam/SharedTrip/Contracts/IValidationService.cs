using SharedTrip.Models;
using System.Collections.Generic;

namespace SharedTrip.Contracts
{
    public interface IValidationService
    {
        (bool isValid, IEnumerable<ErrorViewModel> errors) ValidateModel(object model);
    }
}
