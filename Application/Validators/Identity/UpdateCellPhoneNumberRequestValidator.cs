using Common.requests.identity;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.Identity
{
    public class UpdateCellPhoneNumberRequestValidator : AbstractValidator<UpdateCellPhoneNumberRequest>
    {
        public UpdateCellPhoneNumberRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.")
                .Length(36).WithMessage("Id must be exactly 36 characters.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Length(11).WithMessage("Phone number must be exactly 11 digits.")
                .Matches(@"^\d+$").WithMessage("Phone number must contain only digits.");

            RuleFor(x => x.OldPhoneNumber)
                .NotEmpty().WithMessage("Old phone number is required.")
                .Length(11).WithMessage("Old phone number must be exactly 11 digits.")
                .Matches(@"^\d+$").WithMessage("Old phone number must contain only digits.");

            RuleFor(x => x)
                .Must(x => x.PhoneNumber != x.OldPhoneNumber)
                .WithMessage("Phone number and old phone number must not be the same.");
        }
    }
}
