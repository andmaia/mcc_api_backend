using Application.services.Order;
using Common.requests.Order;
using Common.Responses.wrappers;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Order.queries
{
    public class SearchOrdersByEmployeeQuery : IRequest<IResponseWrapper>
    {
        public string EmployeeId { get; set; }
        public OrderSearchFilter Filter { get; set; }
    }

    public class SearchOrdersByEmployeeQueryHandler : IRequestHandler<SearchOrdersByEmployeeQuery, IResponseWrapper>
    {
        private readonly IOrderService _orderService;
        private readonly IValidator<OrderSearchFilter> _validator;

        public SearchOrdersByEmployeeQueryHandler(IOrderService orderService, IValidator<OrderSearchFilter> validator)
        {
            _orderService = orderService;
            _validator = validator;
        }

        public async Task<IResponseWrapper> Handle(SearchOrdersByEmployeeQuery request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.Filter);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await ResponseWrapper<List<string>>.FailAsync(errorMessages);
            }
            return await _orderService.SearchOrdersByEmployee(request.Filter);
        }
    }
}
