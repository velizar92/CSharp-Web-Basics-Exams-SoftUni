using SharedTrip.Contracts;
using SharedTrip.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace SharedTrip.Services
{
    public class ValidationService : IValidationService
    {

        public (bool isValid, IEnumerable<ErrorViewModel> errors) ValidateModel(object model)
        {
            var context = new ValidationContext(model);
            var errorResult = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(model, context, errorResult, true);

            if (isValid)
            {
                return (isValid, null);
            }

            IEnumerable<ErrorViewModel> errors = errorResult
                                        .Select(e => new ErrorViewModel(e.ErrorMessage)).ToList();

            return (isValid, errors);
        }
    }
}
