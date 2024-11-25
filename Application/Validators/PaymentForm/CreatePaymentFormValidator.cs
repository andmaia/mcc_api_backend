using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.PaymentForm
{
    public class CreatePaymentFormValidator : AbstractValidator<Common.requests.PaymentForm.CreatePaymentFormRequest>
    {
        public CreatePaymentFormValidator()
        {
            RuleFor(x => x.Tax)
                .NotEmpty().WithMessage("Tax is required.")
                .InclusiveBetween(0, 100).WithMessage("Tax must be between 0 and 100.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(50).WithMessage("Name cannot exceed 50 characters.");

            RuleFor(x => x.CompanyId)
                .NotEmpty().WithMessage("CompanyId is required.")
                .Length(36).WithMessage("CompanyId must have exactly 36 characters.");
        }
    }
}
