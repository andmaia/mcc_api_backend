using Common.requests.company;
using Common.Responses.wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.services.Company
{
    public interface ICompanyService
    {
        Task<IResponseWrapper> GetCompanyById(string companyId);
        Task<IResponseWrapper> UpdateCompany(CompanyUpdate companyUpdate);
        Task<IResponseWrapper> CreateCompany(CompanyRequest companyRequest);
        Task<IResponseWrapper> GetCompanyByUserId(string id);
        Task<IResponseWrapper> GetCompanyByEmployeeUd(string id);


    }
}
