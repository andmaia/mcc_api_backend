using Application.services.Expense;
using Common.requests.Expense;
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
    

    public class DisableExpenseCommand : IRequest<IResponseWrapper>
    {
        public string id { get; set; }
    }

    public class DisableExpenseCommandHandler : IRequestHandler<DisableExpenseCommand, IResponseWrapper>
    {
        private readonly IExpenseService _service;

        public DisableExpenseCommandHandler(IExpenseService service)
        {
            _service = service;
        }

        public async Task<IResponseWrapper> Handle(DisableExpenseCommand request, CancellationToken cancellationToken)
        {
            return await _service.DisableExpense(request.id);

        }
    }
}
