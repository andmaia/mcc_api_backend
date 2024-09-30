using Common.requests.identity;
using Common.Responses.identity;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.Identity
{
    public class PreUserRegistrationValidator: AbstractValidator<UserPreRegistrationRequest>
    {
        public PreUserRegistrationValidator() 
        {
            RuleFor(x => x.Email)
              .NotEmpty().WithMessage("Email is required.")
              .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Role)
    .NotEmpty().WithMessage("Role is required.")
    .Must(role => role == "Admin" || role == "Basic" || role == "Manager")
    .WithMessage("Role must be either 'Admin', 'Basic', or 'Manager'.");
        }

    }
}
