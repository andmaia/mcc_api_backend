using Common.requests.identity;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.Identity
{
    public class UpdateUserNameRequestValidator : AbstractValidator<UpdateUserNameRequest>
    {
        public UpdateUserNameRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.")
                .Length(36).WithMessage("Id must be 36 characters.");

            RuleFor(x => x.OldUserName)
                .NotEmpty().WithMessage("OldUserName is required.")
                .MaximumLength(12).WithMessage("OldUserName must be less than 12 characters.");

            RuleFor(x => x.NewUserName)
                .NotEmpty().WithMessage("NewUserName is required.")
                .MaximumLength(12).WithMessage("NewUserName must be less than 12 characters.");
        }
    }
}
