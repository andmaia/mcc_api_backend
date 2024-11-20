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

    public class GetAllEnableEmployeesFromCompanyCommand : IRequest<IResponseWrapper>
    {
        public string idCompany { get; set; }
    }

    public class GGetAllEnableEmployeesFromCompanyCommandHandler : IRequestHandler<GetAllEnableEmployeesFromCompanyCommand, IResponseWrapper>
    {
        private readonly IEmployeService _service;

        public GGetAllEnableEmployeesFromCompanyCommandHandler(IEmployeService service)
        {
            _service = service;
        }

        public async Task<IResponseWrapper> Handle(GetAllEnableEmployeesFromCompanyCommand request, CancellationToken cancellationToken)
        {
            return await _service.GetAllEnableEmployeesFromCompany(request.idCompany);
        }
    }
}
