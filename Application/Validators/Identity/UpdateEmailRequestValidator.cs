using Common.requests.identity;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.Identity
{
    public class UpdateEmailRequestValidator : AbstractValidator<UpdateEmailRequest>
    {
        public UpdateEmailRequestValidator()
        {
            RuleFor(request => request.Id)
                .NotEmpty().WithMessage("Id is required.")
                .Length(36).WithMessage("Id must be exactly 36 characters long.");

            RuleFor(request => request.OldEmail)
                .NotEmpty().WithMessage("Old email is required.")
                .EmailAddress().WithMessage("Old email must be a valid email address.");

            RuleFor(request => request.NewEmail)
                .NotEmpty().WithMessage("New email is required.")
                .EmailAddress().WithMessage("New email must be a valid email address.");

            RuleFor(x => x)
                .Must(x => x.OldEmail != x.NewEmail)
                .WithMessage("new email and old email must not be the same.");
        }
    }

}
