using Common.requests.Comission;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.Comiission
{
    public class CreateComissionRequestValidator : AbstractValidator<CreateComissionRequest>
    {
        public CreateComissionRequestValidator()
        {
            RuleFor(x => x.Percentage)
                .InclusiveBetween(0, 100)
                .WithMessage("Percentage must be between 0 and 100.");

            RuleFor(x => x.CommissionStartDate)
                .LessThanOrEqualTo(x => x.CommissionEndDate)
                .WithMessage("Commission start date must be before or equal to the end date.");

            RuleFor(x => x.CommissionStartDate)
                .Must(BeAValidDate)
                .WithMessage("Start date must be a valid date.");

            RuleFor(x => x.CommissionEndDate)
                .Must(BeAValidDate)
                .WithMessage("End date must be a valid date.");

            RuleFor(x => x.EmployeeId)
                .Length(36)
                .WithMessage("EmployeeId must be 36 characters long.");

            RuleFor(x => x.Orders)
                .NotEmpty()
                .WithMessage("Orders list cannot be empty.")
                .ForEach(order => order
                    .NotEmpty()
                    .Length(36)
                    .WithMessage("Each Order ID must be 36 characters long.")
                );

            RuleFor(x => x.Expenses)
                .NotEmpty()
                .WithMessage("Expenses list cannot be empty.")
                .ForEach(expense => expense
                    .NotEmpty()
                    .Length(36)
                    .WithMessage("Each Expense ID must be 36 characters long.")
                );
        }

        private bool BeAValidDate(DateTime date)
        {
            return date != default(DateTime);
        }
    }
}
