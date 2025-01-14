using Application.services.Expense;
using Common.requests.Expense;
using Common.Responses.wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Expense.Query
{
   
    public class SearchExpensesByEmployeeQuery : IRequest<IResponseWrapper>
    {
        public ExpenseFilterRequest request { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
    }

    public class SearchExpensesByEmployeeQueryHandler : IRequestHandler<SearchExpensesByEmployeeQuery, IResponseWrapper>
    {
        private readonly IExpenseService _service;

        public SearchExpensesByEmployeeQueryHandler(IExpenseService service)
        {
            _service = service;
        }

        public async Task<IResponseWrapper> Handle(SearchExpensesByEmployeeQuery request, CancellationToken cancellationToken)
        {
            return await _service.SearchOrdersByEmployee(request.request, request.Skip, request.Take);
        }
    }
}
