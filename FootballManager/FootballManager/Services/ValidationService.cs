using FootballManager.Contracts;
using System.ComponentModel.DataAnnotations;


namespace FootballManager.Services
{
    public class ValidationService : IValidationService
    {
		public (bool isValid, string error) ValidateModel(object model)
		{
			var context = new ValidationContext(model);
			var errorResult = new List<ValidationResult>();

			bool isValid = Validator.TryValidateObject(model, context, errorResult, true);

			if (isValid)
			{
				return (isValid, null);
			}

			string error = String.Join(", ", errorResult.Select(e => e.ErrorMessage).ToList());

			return (isValid, error);
		}
	}
}
