using Application.services.Employee;
using Common.Responses.wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Employee.Queries
{
    public class GetEmployeeByUserIdCommand : IRequest<IResponseWrapper>
    {
        public string UserId { get; set; }
        public string CompanyId { get; set; }
    }

    public class GetEmployeeByUserIdCommandHandler : IRequestHandler<GetEmployeeByUserIdCommand, IResponseWrapper>
    {
        private readonly IEmployeService _service;

        public GetEmployeeByUserIdCommandHandler(IEmployeService service)
        {
            _service = service;
        }

        public async Task<IResponseWrapper> Handle(GetEmployeeByUserIdCommand request, CancellationToken cancellationToken)
        {
            return await _service.GetEmployeeByUserId(request.UserId, request.CompanyId);
        }
    }
}
