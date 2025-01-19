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
    

    public class GetComissionByEmployeeQuery : IRequest<IResponseWrapper>
    {
        public ComissionFilterRequest ComissionFilterRequest { get; set; }
        public int page { get; set; }
        public int take { get; set; }
    }
    public class GetComissionByEmployeeQueryHandler : IRequestHandler<GetComissionByEmployeeQuery, IResponseWrapper>
    {
        private IComissionService _service;

        public GetComissionByEmployeeQueryHandler(IComissionService service)
        {
            _service = service;
        }

        public async Task<IResponseWrapper> Handle(GetComissionByEmployeeQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetComissionByUser(request.ComissionFilterRequest, request.take, request.page);
        }
    }
}
