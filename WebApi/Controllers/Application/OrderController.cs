using Application.Feature.Order.command;
using Application.Feature.Order.queries;
using Application.Feature.PaymentForm.Commands;
using Application.Feature.PaymentForm.Queries;
using Common.Authorization;
using Common.requests.Order;
using Common.requests.PaymentForm;
using Microsoft.AspNetCore.Mvc;
using WebApi.Attributes;

namespace WebApi.Controllers.Application
{
    [Route("api/order/")]
    public class OrderController : MyBaseController<OrderController>
    {


        [MustHavePermission(AppFeature.Commands, AppAction.Create)]
        [HttpPost("create/")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            var response = await MediatorSender.Send(new CreateOrderAsyncCommand { CreateOrderAsyncRequest = request });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }


        [MustHavePermission(AppFeature.Commands, AppAction.Update)]
        [HttpPut("update/")]
        public async Task<IActionResult> Update([FromBody] UpdateOrderRequest request)
        {
            var response = await MediatorSender.Send(new UpdateOrderAsyncCommand { UpdateOrderRequest = request });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }


        [MustHavePermission(AppFeature.PaymentMethods, AppAction.Update)]
        [HttpPut("/{OrderId}/pay")]
        public async Task<IActionResult> PayOrder([FromRoute] string OrderId)
        {
            var response = await MediatorSender.Send(new PayOrderAsyncCommand { Id = OrderId });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

      


        [MustHavePermission(AppFeature.PaymentMethods, AppAction.Update)]
        [HttpPut("/{OrderId}/unpay")]
        public async Task<IActionResult> UnPayOrder([FromRoute] string OrderId)
        {
            var response = await MediatorSender.Send(new UnpayOrderAsyncCommand { Id = OrderId });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }


        [MustHavePermission(AppFeature.PaymentMethods, AppAction.Update)]
        [HttpPut("/{OrderId}/disable")]
        public async Task<IActionResult> DisableOrder([FromRoute] string OrderId)
        {
            var response = await MediatorSender.Send(new DisableOrderCommand { Id = OrderId });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }


        [MustHavePermission(AppFeature.PaymentMethods, AppAction.Update)]
        [HttpGet("/{OrderId}")]
        public async Task<IActionResult> GetOrderById([FromRoute] string OrderId)
        {
            var response = await MediatorSender.Send(new GetOrderByIdQuery { Id = OrderId });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [MustHavePermission(AppFeature.Statistics, AppAction.Read)]
        [HttpPost("Get-all/Companies")]
        public async Task<IActionResult> GetAllByCompany(OrderSearchFilter orderSearch)
        {
            var response = await MediatorSender.Send(new SearchOrdersByCompanyQuery { Filter = orderSearch });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }


        [MustHavePermission(AppFeature.Statistics, AppAction.Read)]
        [HttpPost("Get-all/Branch")]
        public async Task<IActionResult> GetAllByBranch(OrderSearchFilter orderSearch)
        {
            var response = await MediatorSender.Send(new SearchOrdersByBranchQuery { Filter = orderSearch });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }


        [MustHavePermission(AppFeature.Statistics, AppAction.Read)]
        [HttpPost("Get-all/Employee")]
        public async Task<IActionResult> GetAllByEmployee(OrderSearchFilter orderSearch)
        {
            var response = await MediatorSender.Send(new SearchOrdersByEmployeeQuery { Filter = orderSearch });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

    }
}
