using Application.services.Comission;
using Common.Responses.wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Comission.Command
{
    public class DeleteComissionCommand:IRequest<IResponseWrapper>
    {
        public string Id { get; set; }
    }

    public class DeleteComissionCommandHandler : IRequestHandler<DeleteComissionCommand, IResponseWrapper>
    {

        private IComissionService _service;

        public DeleteComissionCommandHandler(IComissionService service)
        {
            _service = service;
        }

        public async Task<IResponseWrapper> Handle(DeleteComissionCommand request, CancellationToken cancellationToken)
        {
            return await _service.DeleteComission(request.Id); ;
        }
    }


}
