using Common.requests.branch;
using Common.requests.company;
using Common.Responses.wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.services.Branch
{
    public interface IBranchService
    {
        Task<IResponseWrapper> GetBranchById(string companyId);
        Task<IResponseWrapper> UpdateBranch(UpdateBranchRequest request);
        Task<IResponseWrapper> DisableBranch(string id);
        Task<IResponseWrapper> EnableBranch(string id);


        Task<IResponseWrapper> CreateBranch(CreateBranchRequest request);
        Task<IResponseWrapper> GetBranchsByCompany(string id);
        Task<IResponseWrapper> GetOrdersByBranch(string id);
    }
}
