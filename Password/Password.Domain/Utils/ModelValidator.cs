using Password.Domain.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Password.Domain.Utils
{
    public static class ModelValidator
    {
        public static void Validate(User user)
        {
            var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(user, new ValidationContext(user), validationResults))
            {
                throw new ValidationException(validationResults.First().ErrorMessage);
            }
        }
    }
}
