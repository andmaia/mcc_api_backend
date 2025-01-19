using Application.services.Comission;
using Common.requests.Comission;
using Common.Responses.wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Comission.Query
{
    

    public class GetComissionByCompanyQuery : IRequest<IResponseWrapper>
    {
        public ComissionFilterRequest ComissionFilterRequest { get; set; }
        public int page { get; set; }
        public int take { get; set; }
    }
    public class GetComissionByCompanyQueryHandler : IRequestHandler<GetComissionByCompanyQuery, IResponseWrapper>
    {
        private IComissionService _service;

        public GetComissionByCompanyQueryHandler(IComissionService service)
        {
            _service = service;
        }

        public async Task<IResponseWrapper> Handle(GetComissionByCompanyQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetComissionByUser(request.ComissionFilterRequest, request.take, request.page);
        }
    }
}
