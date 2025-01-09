using Common.requests.identity;
using Common.requests.Payment;
using Common.Responses.wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.services.Payment
{
    public interface IPaymentService
    {
        Task<IResponseWrapper> CreatePayment(CreatePaymentRequest request);
        Task<IResponseWrapper> UpdatePayment(UpdatePaymentRequest request);
        Task<IResponseWrapper> RemovePayment(string id);

        Task<IResponseWrapper> GetPaymentById(string id);
        Task<IResponseWrapper> GetPaymentsByOrderId(string id);



    }
}
