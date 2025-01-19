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
    public class GetComissionByUserQuery : IRequest<IResponseWrapper>
    {
        public ComissionFilterRequest ComissionFilterRequest { get; set; }
        public int page { get; set; }
        public int take { get; set; }
    }
    public class GetComissionByUserQueryHandler : IRequestHandler<GetComissionByUserQuery, IResponseWrapper>
    {
        private IComissionService _service;

        public GetComissionByUserQueryHandler(IComissionService service)
        {
            _service = service;
        }

        public async Task<IResponseWrapper> Handle(GetComissionByUserQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetComissionByUser(request.ComissionFilterRequest, request.take, request.page) ;
        }
    }

}
