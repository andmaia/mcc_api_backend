using Common.requests.Employee;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.Employee
{
    public class EmployeeRegisterRequestValidator : AbstractValidator<EmployeeRegisterRequest>
    {


        public EmployeeRegisterRequestValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.")
                .Length(36).WithMessage("UserId must be exactly 36 characters.");

            RuleFor(x => x.CompanyId)
                .NotEmpty().WithMessage("CompanyId is required.")
                .Length(36).WithMessage("CompanyId must be exactly 36 characters.");


            RuleFor(x => x.Position)
                .NotEmpty().WithMessage("Position is required.")
                .Must(BeAValidPosition)
                .WithMessage("Position must be one of the following: Receptionist, Manager, ServiceProvider.");

            RuleFor(x => x.ComissionPercentage)
                .GreaterThanOrEqualTo(0).WithMessage("Commission percentage must be a positive number.")
                .LessThanOrEqualTo(100).WithMessage("Commission percentage cannot exceed 100.");
        }

        private bool BeAValidPosition(string position)
        {
            var validPositions = new[] { "Receptionist", "Manager", "ServiceProvider" };
            return validPositions.Contains(position);
        }
    }
}
