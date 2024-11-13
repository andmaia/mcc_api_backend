using Common.requests.company;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.Domain.Company
{
    public class CompanyRequestValidator : AbstractValidator<CompanyRequest>
    {
        public CompanyRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("The Name field is required.")
                .MaximumLength(50).WithMessage("The Name must not exceed 50 characters.");

            RuleFor(x => x.CNPJ)
                .NotEmpty().WithMessage("The CNPJ field is required.")
                .Matches(@"^\d{14}$|^\d{2}\.\d{3}\.\d{3}/\d{4}-\d{2}$")
                .Length(14).WithMessage("The CNPJ must be exactly 14 characters.");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("The UserId field is required.")
                .Length(36).WithMessage("The UserId must be exactly 36 characters.");
        }
    }
}
