using Common.requests.Payment;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.Payment
{
    public class CreatePaymentRequestValidator : AbstractValidator<CreatePaymentRequest>
    {
        public CreatePaymentRequestValidator()
        {
            RuleFor(x => x.PaymentFormId)
                .NotEmpty().WithMessage("PaymentFormId não pode ser vazio.")
                .Length(36).WithMessage("PaymentFormId deve ter exatamente 36 caracteres.");

            RuleFor(x => x.OrderId)
                .NotEmpty().WithMessage("OrderId não pode ser vazio.")
                .Length(36).WithMessage("OrderId deve ter exatamente 36 caracteres.");

            RuleFor(x => x.Tax)
                .GreaterThanOrEqualTo(0).WithMessage("Tax não pode ser menor que zero.");

            RuleFor(x => x.Value)
                .GreaterThanOrEqualTo(0).WithMessage("Value não pode ser menor que zero.");

            RuleFor(x => x.Url)
                .Cascade(CascadeMode.Stop)
                .Must(url => string.IsNullOrEmpty(url) || Uri.IsWellFormedUriString(url, UriKind.Absolute))
                .WithMessage("URL deve ser uma URL válida ou nula.");
        }
    }
}
