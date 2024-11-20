using Common.requests.Employee;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.Employee
{
    public class UpdateEmployeeValidator : AbstractValidator<UpdateEmployee>
    {
        public UpdateEmployeeValidator()
        {
           
            RuleFor(x => x.Name)
                .MaximumLength(50).WithMessage("Name must not exceed 50 characters.")
                .When(x => !string.IsNullOrEmpty(x.Name)); 

           
            RuleFor(x => x.CPF)
                .Matches(@"^\d{11}$").WithMessage("CPF must be a valid number without dots or dashes.")
                .When(x => !string.IsNullOrEmpty(x.CPF)); 

          
            RuleFor(x => x.UrlPerfil)
                .NotNull().WithMessage("UrlPerfil must be a string.")
                .When(x => !string.IsNullOrEmpty(x.UrlPerfil));
        }
    }
}
