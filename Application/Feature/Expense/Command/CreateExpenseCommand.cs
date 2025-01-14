using Application.services.Expense;
using Application.services.Order;
using Common.requests.Expense;
using Common.requests.Order;
using Common.Responses.wrappers;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Expense.Command
{
    public class CreateExpenseCommand : IRequest<IResponseWrapper>
    {
        public CreateExpenseRequest Request { get; set; }
    }

    public class CreateExpenseCommandHandler : IRequestHandler<CreateExpenseCommand, IResponseWrapper>
    {
        private readonly IExpenseService _service;
        private readonly IValidator<CreateExpenseRequest> _validator;

        public CreateExpenseCommandHandler(IExpenseService service, IValidator<CreateExpenseRequest> validator)
        {
            _service = service;
            _validator = validator;
        }

        public async Task<IResponseWrapper> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
        {

            var validationResult = await _validator.ValidateAsync(request.Request);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await ResponseWrapper<List<string>>.FailAsync(errorMessages);
            }

            return await _service.CreateExpense(request.Request);
        }
    }
}
