using Common.Responses.identity;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.Identity
{
    public class UserRegistrationRequestValidator : AbstractValidator<UserRegistrationRequest>
    {
        public UserRegistrationRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Username is required.")
                .MaximumLength(20).WithMessage("Username must be less than 20 characters.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
                .MaximumLength(20).WithMessage("Password must be no more than 20 characters long.")
                .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches(@"[0-9]").WithMessage("Password must contain at least one number.");

            RuleFor(x => x.ComfirmPassword)
                .Equal(x => x.Password).WithMessage("Passwords must match.");
           RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\d+$").WithMessage("Phone number must contain only numeric characters.")
                 .Matches(@"^\d{11}$").WithMessage("Phone number must contain exactly 11 digits.");
        }
    }
}
