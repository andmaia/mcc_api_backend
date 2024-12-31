using Common.requests.Order;
using FluentValidation;
using System;
using System.Globalization;

namespace Application.Validators.Orders
{
    public class OrderSearchFilterValidator : AbstractValidator<OrderSearchFilter>
    {
        public OrderSearchFilterValidator()
        {
            RuleFor(x => x.Id)
                .Length(36).WithMessage("O Id deve ter exatamente 36 caracteres.")
                .When(x => !string.IsNullOrEmpty(x.Id));

            RuleFor(x => x.IsActive)
                .NotNull().WithMessage("IsActive não pode ser nulo.");

            RuleFor(x => x.IsPaid)
                .NotNull().WithMessage("IsPaid não pode ser nulo.");

            RuleFor(x => x.IsComissionPaid)
                .NotNull().WithMessage("IsComissionPaid não pode ser nulo.");

            RuleFor(x => x.BeginDate)
                .Must(BeValidDateFormat).WithMessage("A data de início não está no formato dd/MM/yyyy.")
                .When(x => x.BeginDate.HasValue);

            RuleFor(x => x.EndDate)
                .Must(BeValidDateFormat).WithMessage("A data de término não está no formato dd/MM/yyyy.")
                .When(x => x.EndDate.HasValue);

            RuleFor(x => new { x.BeginDate, x.EndDate })
                .Must(x => !x.BeginDate.HasValue || !x.EndDate.HasValue || x.BeginDate <= x.EndDate)
                .WithMessage("A data de início não pode ser maior que a data de término.");
        }

        // Alterando o método para usar o tipo correto de parâmetro e retorno
        private bool BeValidDateFormat(DateTime? date)
        {
            if (!date.HasValue)
                return true; // Se a data for nula, a validação é passada

            return DateTime.TryParseExact(date.Value.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
        }
    }
}
