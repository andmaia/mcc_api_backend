using Common.requests.Employee;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.Employee
{

    public class FinishRegisterEmployeeValidator : AbstractValidator<FinishRegisterEmployee>
    {
        public FinishRegisterEmployeeValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(50).WithMessage("Name must not exceed 50 characters.");

            RuleFor(x => x.CPF)
                .NotEmpty().WithMessage("CPF is required.")
                .Matches(@"^\d{11}$").WithMessage("CPF must be a valid number without dots or dashes.");

        }
    }
}
