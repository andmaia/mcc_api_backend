using Application.services.Comission;
using Common.Responses.wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Comission.Query
{
    public class GetComissionByIdQuery : IRequest<IResponseWrapper>
    {
        public string Id { get; set; }
    }

    public class GetComissionByIdQueryHandler : IRequestHandler<GetComissionByIdQuery, IResponseWrapper>
    {
        private IComissionService _service;

        public GetComissionByIdQueryHandler(IComissionService service)
        {
            _service = service;
        }

        public async Task<IResponseWrapper> Handle(GetComissionByIdQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetComissionById(request.Id);
        }
    }
}
