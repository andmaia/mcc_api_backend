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
   
    public class SearchExpensesByCompanyQuery : IRequest<IResponseWrapper>
    {
        public ExpenseFilterRequest request { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
    }

    public class SearchExpensesByCompanyQueryHandler : IRequestHandler<SearchExpensesByCompanyQuery, IResponseWrapper>
    {
        private readonly IExpenseService _service;

        public SearchExpensesByCompanyQueryHandler(IExpenseService service)
        {
            _service = service;
        }

        public async Task<IResponseWrapper> Handle(SearchExpensesByCompanyQuery request, CancellationToken cancellationToken)
        {
            return await _service.SearchOrdersByCompany(request.request, request.Skip, request.Take);
        }
    }
}
