using Application.services.Expense;
using Common.Responses.wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Expense.Query
{
    
    public class GetExpenseByIdQuery : IRequest<IResponseWrapper>
    {
        public string id { get; set; }
    }

    public  class GetExpenseByIdQueryHandler : IRequestHandler<GetExpenseByIdQuery, IResponseWrapper>
    {
        private readonly IExpenseService _service;

        public GetExpenseByIdQueryHandler(IExpenseService service)
        {
            _service = service;
        }

        public async Task<IResponseWrapper> Handle(GetExpenseByIdQuery request, CancellationToken cancellationToken)
        {
            return await _service.DisableExpense(request.id);

        }
    }
}
