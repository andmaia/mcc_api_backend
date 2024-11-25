using Common.requests.identity;
using Common.requests.PaymentForm;
using Common.Responses.wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.services.PaymentForm
{
    public interface IPaymentFormService
    {
        Task<IResponseWrapper> CreatePaymentForm(CreatePaymentFormRequest request);
        Task<IResponseWrapper> UpdatePaymentForm(UpdatePaymentFormRequest request);
        Task<IResponseWrapper> DisablePaymentForm(string id, string companyId);
        Task<IResponseWrapper> EnablePaymentForm(string id, string companyId);
        Task<IResponseWrapper> GetAllPaymentFormsFromCompanyId(string id);
        Task<IResponseWrapper> GetAllEnablePaymentFormsFromCompanyId(string id);
        Task<IResponseWrapper> GetAllDisablePaymentFormsFromCompanyId(string id);
        Task<IResponseWrapper> GetAPaymentFormById(string id);



    }
}
