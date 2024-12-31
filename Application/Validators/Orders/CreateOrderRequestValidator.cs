using Common.requests.Order;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.Orders
{
    public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
    {
        public CreateOrderRequestValidator()
        {
            RuleFor(x => x.CustomerName)
                .NotEmpty().WithMessage("O nome do cliente não pode ser vazio.");

            RuleFor(x => x.TotalValue)
                .GreaterThan(0).WithMessage("O valor total deve ser maior que zero.");

            RuleFor(x => x.CommissionPercentage)
                .InclusiveBetween(0, 1000).WithMessage("A porcentagem de comissão deve estar entre 0 e 1000.");

            RuleFor(x => x.CompanyId)
                .Length(36).WithMessage("O CompanyId deve ter exatamente 36 caracteres.");

            RuleFor(x => x.branchId)
                .Length(36).WithMessage("O branchId deve ter exatamente 36 caracteres.");

            RuleFor(x => x.EmployeeId)
                .Length(36).WithMessage("O EmployeeId deve ter exatamente 36 caracteres.");
        }
    }
}
