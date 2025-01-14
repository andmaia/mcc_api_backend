using Common.requests.Expense;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.Expense
{
 
    public class CreateExpenseRequestValidator : AbstractValidator<CreateExpenseRequest>
    {
        public CreateExpenseRequestValidator()
        {
            RuleFor(x => x.Value)
                .GreaterThanOrEqualTo(0).WithMessage("Value must not be negative.");

            RuleFor(x => x.EmployeeId)
                .Length(36).WithMessage("EmployeeId must be exactly 36 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(256).WithMessage("Description must not exceed 256 characters.");

            RuleFor(x => x.Url)
                .Must(url => url == null || Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
                .WithMessage("Url must be a valid URL or null.");
        }
    }
}
