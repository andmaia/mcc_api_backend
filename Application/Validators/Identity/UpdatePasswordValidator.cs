using Common.requests.identity;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.Identity
{
    public class UpdatePasswordRequestValidator : AbstractValidator<UpdatePasswordRequest>
    {
        public UpdatePasswordRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.")
                .Length(36).WithMessage("Id must be 36 characters.");

            RuleFor(x => x.OldPassword)
                .NotEmpty().WithMessage("Old password is required.")
                .MaximumLength(20).WithMessage("Old password must be less than 20 characters.")
                .MinimumLength(10).WithMessage("Old password must be at least 10 characters.");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("New password is required.")
                .MaximumLength(20).WithMessage("New password must be less than 20 characters.")
                .MinimumLength(10).WithMessage("New password must be at least 10 characters.")
                .Matches(@"[A-Z]").WithMessage("New password must contain at least one uppercase letter.")
                .Matches(@"[a-z]").WithMessage("New password must contain at least one lowercase letter.")
                .Matches(@"\d").WithMessage("New password must contain at least one number.")
                .NotEqual(x => x.OldPassword).WithMessage("New password cannot be the same as the old password.");

            RuleFor(x => x.ConfirmationPassword)
                .Equal(x => x.NewPassword).WithMessage("Confirmation password must match the new password.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");
        }
    }
}
