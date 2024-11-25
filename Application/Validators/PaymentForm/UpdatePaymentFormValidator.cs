using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.PaymentForm
{
    public class UpdatePaymentFormValidator : AbstractValidator<Common.requests.PaymentForm.UpdatePaymentFormRequest>
    {
        public UpdatePaymentFormValidator()
        {
            RuleFor(x => x.Tax)
                .InclusiveBetween(0, 100).When(x => x.Tax != default)
                .WithMessage("Tax must be between 0 and 100.");

            RuleFor(x => x.Name)
                .MaximumLength(50).When(x => !string.IsNullOrEmpty(x.Name))
                .WithMessage("Name cannot exceed 50 characters.");

            RuleFor(x => x.CompanyId)
                .Length(36).When(x => !string.IsNullOrEmpty(x.CompanyId))
                .WithMessage("CompanyId must have exactly 36 characters.");

            RuleFor(x => x.PaymentFormId)
                .Length(36).When(x => !string.IsNullOrEmpty(x.PaymentFormId))
                .WithMessage("PaymentFormId must have exactly 36 characters.");
        }
    }
}
