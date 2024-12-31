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
    public class SearchOrdersByCompanyQuery : IRequest<IResponseWrapper>
    {
        public string CompanyId { get; set; }
        public OrderSearchFilter Filter { get; set; }
    }

    public class SearchOrdersByCompanyQueryHandler : IRequestHandler<SearchOrdersByCompanyQuery, IResponseWrapper>
    {
        private readonly IOrderService _orderService;
        private readonly IValidator<OrderSearchFilter> _validator;

        public SearchOrdersByCompanyQueryHandler(IOrderService orderService, IValidator<OrderSearchFilter> validator)
        {
            _orderService = orderService;
            _validator = validator;
        }

        public async Task<IResponseWrapper> Handle(SearchOrdersByCompanyQuery request, CancellationToken cancellationToken)
        {
            // Valida o filtro de pesquisa
            var validationResult = await _validator.ValidateAsync(request.Filter);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return await ResponseWrapper<List<string>>.FailAsync(errorMessages);
            }

            // Chama o serviço para buscar as ordens com o filtro
            return await _orderService.SearchOrdersByCompany(request.Filter);
        }
    }

}
