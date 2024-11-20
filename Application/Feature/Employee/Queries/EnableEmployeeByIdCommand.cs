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
    public class EnableEmployeeByIdCommand : IRequest<IResponseWrapper>
    {
        public string EmployeeId { get; set; }
        public string CompanyId { get; set; }
    }

    public class EnableEmployeeByIdCommandHandler : IRequestHandler<EnableEmployeeByIdCommand, IResponseWrapper>
    {
        private readonly IEmployeService _service;

        public EnableEmployeeByIdCommandHandler(IEmployeService service)
        {
            _service = service;
        }

        public async Task<IResponseWrapper> Handle(EnableEmployeeByIdCommand request, CancellationToken cancellationToken)
        {
            return await _service.EnableEmployeeById(request.EmployeeId, request.CompanyId);
        }
    }
}
