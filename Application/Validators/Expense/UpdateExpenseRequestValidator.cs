using Common.requests.Expense;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.Expense
{
    public class UpdateExpenseRequestValidator: AbstractValidator<UpdateExpenseRequest>
    {
        public UpdateExpenseRequestValidator()
        {
            // Validação para o Id (assumindo que é um GUID com 36 caracteres)
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.")
                .Matches(@"^[A-Fa-f0-9\-]{36}$").WithMessage("Id must be a valid GUID with 36 characters.");

            RuleFor(x => x.EmployeeId)
                .NotEmpty().WithMessage("EmployeeId is required.")
                .Matches(@"^[A-Fa-f0-9\-]{36}$").WithMessage("EmployeeId must be a valid GUID with 36 characters.");

            // Validação para o Value (deve ser um número decimal)
            RuleFor(x => x.Value)
                .GreaterThan(0).WithMessage("Value must be greater than 0.");
            // Validação para o Url (máximo de 256 caracteres)
            RuleFor(x => x.Url)
                .MaximumLength(256).WithMessage("Url must not exceed 256 characters.");

            // Validação para a Description (máximo de 500 caracteres)
            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");
        }
    }
   

}
