using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public class PaginationValidator : AbstractValidator<int>
    {
        public PaginationValidator()
        {
            RuleFor(x => x)
                .InclusiveBetween(0, 10)
                .WithMessage("The value must be between 0 and 10.");
        }
    }
}
