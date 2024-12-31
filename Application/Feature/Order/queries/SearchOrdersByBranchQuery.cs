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
    public class SearchOrdersByBranchQuery : IRequest<IResponseWrapper>
    {
        public string BranchId { get; set; }
        public OrderSearchFilter Filter { get; set; }
    }

    public class SearchOrdersByBranchQueryHandler : IRequestHandler<SearchOrdersByBranchQuery, IResponseWrapper>
    {
        private readonly IOrderService _orderService;
        private readonly IValidator<OrderSearchFilter> _validator;

        public SearchOrdersByBranchQueryHandler(IOrderService orderService, IValidator<OrderSearchFilter> validator)
        {
            _orderService = orderService;
            _validator = validator;
        }

        public async Task<IResponseWrapper> Handle(SearchOrdersByBranchQuery request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.Filter);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await ResponseWrapper<List<string>>.FailAsync(errorMessages);
            }

            return await _orderService.SearchOrdersByBranch(request.Filter);
        }
    }

}
