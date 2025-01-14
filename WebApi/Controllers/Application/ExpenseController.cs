using Application.Feature.Expense.Command;
using Application.Feature.Expense.Query;
using Application.Feature.Order.command;
using Application.Feature.Order.queries;
using Common.Authorization;
using Common.requests.Expense;
using Common.requests.Order;
using Microsoft.AspNetCore.Mvc;
using WebApi.Attributes;
using WebApi.Permissions;

namespace WebApi.Controllers.Application
{

    [Route("api/expense/")]
    public class ExpenseController : MyBaseController<ExpenseController>
    {
        [MustHavePermission(AppFeature.Commands, AppAction.Create)]
        [HttpPost("create/")]
        public async Task<IActionResult> CreateExpense([FromBody] CreateExpenseRequest request)
        {
            var response = await MediatorSender.Send(new CreateExpenseCommand { Request = request });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [MustHavePermission(AppFeature.Commands, AppAction.Update)]
        [HttpPost("update/")]
        public async Task<IActionResult> UpdateExpense([FromBody] UpdateExpenseRequest request)
        {
            var response = await MediatorSender.Send(new UpdateExpeseCommand { Request = request });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [MustHavePermission(AppFeature.Commands, AppAction.Delete)]
        [HttpPost("disable/{Id}")]
        public async Task<IActionResult> DisableExpense([FromRoute] string Id)
        {
            var response = await MediatorSender.Send(new DisableExpenseCommand { id = Id });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [MustHavePermission(AppFeature.Commands, AppAction.Read)]
        [HttpPost("get/{Take}/{Skip}/employee")]
        public async Task<IActionResult> GetExpenseByEmployeeId([FromBody] ExpenseFilterRequest request, int Take, int Skip)
        {
            if (!ValideAtributes.ValidatePagination(Take, Skip, out string errorMessage))
            {
                return BadRequest(new { message = errorMessage });
            }
            var response = await MediatorSender.Send(new SearchExpensesByEmployeeQuery { request = request, Take = Take, Skip = Skip });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        [MustHavePermission(AppFeature.Commands, AppAction.Read)]
        [HttpPost("get/{Take}/{Skip}/company")]
        public async Task<IActionResult> GetExpenseByCompanyId([FromBody] ExpenseFilterRequest request, int Take, int Skip)
        {
            if (!ValideAtributes.ValidatePagination(Take, Skip, out string errorMessage))
            {
                return BadRequest(new { message = errorMessage });
            }
            var response = await MediatorSender.Send(new SearchExpensesByCompanyQuery { request = request, Take = Take, Skip = Skip });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
        [MustHavePermission(AppFeature.Commands, AppAction.Read)]
        [HttpPost("get/{Take}/{Skip}/comission")]
        public async Task<IActionResult> GetExpenseByComissionId([FromBody] ExpenseFilterRequest request, int Take, int Skip)
        {
            if (!ValideAtributes.ValidatePagination(Take, Skip, out string errorMessage))
            {
                return BadRequest(new { message = errorMessage });
            }

            var response = await MediatorSender.Send(new SearchExpensesByComissionQuery { request = request, Take = Take, Skip = Skip });
            if (response.IsSuccessful)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
