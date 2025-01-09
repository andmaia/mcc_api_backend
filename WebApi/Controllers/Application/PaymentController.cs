using Application.Feature.Order.command;
using Application.Feature.Order.queries;
using Application.Feature.Payment.Command;
using Application.Feature.Payment.Queries;
using Common.Authorization;
using Common.requests.Order;
using Common.requests.Payment;
using Microsoft.AspNetCore.Mvc;
using WebApi.Attributes;

namespace WebApi.Controllers.Application
{ 


    [Route("api/payment/")]
    public class PaymentController : MyBaseController<PaymentController>
    {

        [MustHavePermission(AppFeature.Commands, AppAction.Create)]
        [HttpPost("create/")]
        public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentRequest request)
        {
            var response = await MediatorSender.Send(new CreatePaymentCommand { CreatePaymentRequest = request });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [MustHavePermission(AppFeature.Commands, AppAction.Update)]
        [HttpPut("update/")]
        public async Task<IActionResult> UpdatePayment([FromBody] UpdatePaymentRequest request)
        {
            var response = await MediatorSender.Send(new UpdatePaymentCommand { updatePaymentRequest = request });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [MustHavePermission(AppFeature.Commands, AppAction.Delete)]
        [HttpDelete("disable/{Id}")]
        public async Task<IActionResult> UpdatePayment([FromRoute] string Id)
        {
            var response = await MediatorSender.Send(new RemovePaymentCommand { Id = Id });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }




        [MustHavePermission(AppFeature.Commands, AppAction.Read)]
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetPaymentById([FromRoute] string Id)
        {
            var response = await MediatorSender.Send(new GetPaymentByIdQuery { Id = Id });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }


        [MustHavePermission(AppFeature.Commands, AppAction.Read)]
        [HttpGet("{Id}/Order")]
        public async Task<IActionResult> GetPaymentByOderId([FromRoute] string Id)
        {
            var response = await MediatorSender.Send(new GetPaymentsByOrderQuery { Id = Id });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
