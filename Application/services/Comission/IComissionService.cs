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
    }
}
