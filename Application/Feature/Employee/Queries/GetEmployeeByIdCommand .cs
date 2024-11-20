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
    public class GetEmployeeByIdCommand : IRequest<IResponseWrapper>
    {
        public string Id { get; set; }
        public string CompanyId { get; set; }
    }

    public class GetEmployeeByIdCommandHandler : IRequestHandler<GetEmployeeByIdCommand, IResponseWrapper>
    {
        private readonly IEmployeService _service;

        public GetEmployeeByIdCommandHandler(IEmployeService service)
        {
            _service = service;
        }

        public async Task<IResponseWrapper> Handle(GetEmployeeByIdCommand request, CancellationToken cancellationToken)
        {
            return await _service.GetEmployeeById(request.Id, request.CompanyId);
        }
    }
}
