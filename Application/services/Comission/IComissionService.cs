using Common.requests.Comission;
using Common.requests.Order;
using Common.Responses.Comission;
using Common.Responses.order;
using Common.Responses.wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.services.Comission
{
    public interface IComissionService
    {
        Task<IResponseWrapper<ResponseComissionWithAllDetails>> CreateComissionAsync(CreateComissionRequest request);
        Task<IResponseWrapper<ResponseComissionWithAllDetails>> GetComissionById(string id);
        Task<IResponseWrapper> DeleteComission(string id);
        Task<IResponseWrapper> PayComission(string id);
        Task<IResponseWrapper> UnpayComission(string id);

        Task<IResponseWrapper<IList<ResponseComissionWithAllDetails>>> GetComissionByEmployee(ComissionFilterRequest request,int take,int page);
        Task<IResponseWrapper<IList<ResponseComissionWithAllDetails>>> GetComissionByCompany(ComissionFilterRequest request ,int take ,int page);

        Task<IResponseWrapper<IList<ResponseComissionWithAllDetails>>> GetComissionByUser(ComissionFilterRequest request, int take, int page);

    }
}
