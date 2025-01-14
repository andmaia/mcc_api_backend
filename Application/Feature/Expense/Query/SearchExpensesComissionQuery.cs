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
   
    public class SearchExpensesByComissionQuery : IRequest<IResponseWrapper>
    {
        public ExpenseFilterRequest request { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
    }

    public class SearchExpensesByComissionQueryHandler : IRequestHandler<SearchExpensesByComissionQuery, IResponseWrapper>
    {
        private readonly IExpenseService _service;

        public SearchExpensesByComissionQueryHandler(IExpenseService service)
        {
            _service = service;
        }

        public async Task<IResponseWrapper> Handle(SearchExpensesByComissionQuery request, CancellationToken cancellationToken)
        {
            return await _service.SearchOrdersByComission(request.request, request.Skip, request.Take);
        }
    }
}
