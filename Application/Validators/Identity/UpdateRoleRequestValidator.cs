using Common.requests.identity;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.Identity
{
    internal class UpdateRoleRequestValidator : AbstractValidator<UpdateRoleRequest>
    {
        public UpdateRoleRequestValidator()
        {
            RuleFor(x => x.IdUserToUpdate)
                .NotEmpty().WithMessage("IdUserToUpdate is required.")
                .Length(36).WithMessage("IdUserToUpdate must be 36 characters long.");

            RuleFor(x => x.RoleOld)
                .NotEmpty().WithMessage("RoleOld is required.")
                .Must(BeAValidRole).WithMessage("RoleOld must be one of the following: Basic, Admin, Manager.");

            RuleFor(x => x.RoleNew)
                .NotEmpty().WithMessage("RoleNew is required.")
                .Must(BeAValidRole).WithMessage("RoleNew must be one of the following: Basic, Admin, Manager.");
        }

        private bool BeAValidRole(string role)
        {
            var validRoles = new[] { "Basic", "Admin", "Manager" };
            return validRoles.Contains(role);
        }
    }
}
