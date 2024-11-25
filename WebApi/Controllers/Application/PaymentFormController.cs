using Application.Feature.Application.Company.Commands;
using Application.Feature.Employee.Queries;
using Application.Feature.PaymentForm.Commands;
using Application.Feature.PaymentForm.Queries;
using Common.Authorization;
using Common.requests.company;
using Common.requests.PaymentForm;
using Microsoft.AspNetCore.Mvc;
using WebApi.Attributes;

namespace WebApi.Controllers.Application
{
    [Route("api/paymentform/")]
    public class PaymentFormController : MyBaseController<PaymentFormController>
    {
        [MustHavePermission(AppFeature.PaymentMethods, AppAction.Create)]
        [HttpPost("register-paymentform/")]
        public async Task<IActionResult> RegisterPaymentForm([FromBody] CreatePaymentFormRequest request)
        {
            var response = await MediatorSender.Send(new CreatePaymentFormCommand { CreatePaymentFormRequest = request });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [MustHavePermission(AppFeature.PaymentMethods, AppAction.Update)]
        [HttpPut("update-paymentform/")]
        public async Task<IActionResult> UpdatePaymentForm([FromBody] UpdatePaymentFormRequest request)
        {
            var response = await MediatorSender.Send(new UpdatePaymentFormCommand { UpdatePaymentFormRequest = request });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [MustHavePermission(AppFeature.PaymentMethods, AppAction.Read)]
        [HttpGet("company/{companyId}/enable")]
        public async Task<IActionResult> GetAllEnablePaymentFormById([FromRoute] string companyId)
        {
            var response = await MediatorSender.Send(new GetAllEnablePaymentFormsFromCompanyIdCommand { CompanyId = companyId });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [MustHavePermission(AppFeature.PaymentMethods, AppAction.Read)]
        [HttpGet("company/{companyId}/disable")]
        public async Task<IActionResult> GetAllDisablePaymentFormById([FromRoute] string companyId)
        {
            var response = await MediatorSender.Send(new GetAllDisablePaymentFormsFromCompanyIdCommand { CompanyId = companyId });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [MustHavePermission(AppFeature.PaymentMethods, AppAction.Read)]
        [HttpGet("company/{companyId}/all")]
        public async Task<IActionResult> GetAllDPaymentFormById([FromRoute] string companyId)
        {
            var response = await MediatorSender.Send(new GetAllPaymentFormsFromCompanyIdCommand { CompanyId = companyId });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [MustHavePermission(AppFeature.PaymentMethods, AppAction.Read)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentFormById([FromRoute] string id)
        {
            var response = await MediatorSender.Send(new GetAPaymentFormByIdCommand { Id = id });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [MustHavePermission(AppFeature.PaymentMethods, AppAction.Update)]
        [HttpPut("{id}/company/{companyId}/enable")]
        public async Task<IActionResult> EnablePaymentFormById([FromRoute] string id, string companyId)
        {
            var response = await MediatorSender.Send(new EnablePaymentFormCommand { Id = id, CompanyId = companyId });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [MustHavePermission(AppFeature.PaymentMethods, AppAction.Update)]
        [HttpPut("{id}/company/{companyId}/disable")]
        public async Task<IActionResult> DisablePaymentFormById([FromRoute] string id, string companyId)
        {
            var response = await MediatorSender.Send(new DisablePaymentFormCommand { Id = id, CompanyId = companyId });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
