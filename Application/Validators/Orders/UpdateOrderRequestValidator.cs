using Common.requests.Order;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.Orders
{
    public class UpdateOrderRequestValidator : AbstractValidator<UpdateOrderRequest>
    {
        public UpdateOrderRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("O Id não pode ser vazio.")
                .When(x => !string.IsNullOrEmpty(x.Id));

            RuleFor(x => x.TotalValue)
                .GreaterThan(0).WithMessage("O valor total deve ser maior que zero.")
                .When(x => x.TotalValue.HasValue);

            RuleFor(x => x.CommissionPercentage)
                .InclusiveBetween(0, 1000).WithMessage("A porcentagem de comissão deve estar entre 0 e 1000.")
                .When(x => x.CommissionPercentage.HasValue);

            RuleFor(x => x.branchId)
                .Length(36).WithMessage("O branchId deve ter exatamente 36 caracteres.")
                .When(x => !string.IsNullOrEmpty(x.branchId));

            RuleFor(x => x.EmployeeId)
                .Length(36).WithMessage("O EmployeeId deve ter exatamente 36 caracteres.")
                .When(x => !string.IsNullOrEmpty(x.EmployeeId));
        }
    }

}
