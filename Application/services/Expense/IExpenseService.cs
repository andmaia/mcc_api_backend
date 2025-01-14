using Common.requests.Expense;
using Common.requests.Order;
using Common.Responses.Expense;
using Common.Responses.order;
using Common.Responses.wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.services.Expense
{
    public interface IExpenseService
    {
        Task<IResponseWrapper<ExpenseResponse>> CreateExpense(CreateExpenseRequest request);
        Task<IResponseWrapper<ExpenseResponse>> GetExpenseById(string id);
        Task<IResponseWrapper<ExpenseResponse>> UpdateExpese(UpdateExpenseRequest request);
        Task<IResponseWrapper<ExpenseResponse>> DisableExpense(string id);

        Task<IResponseWrapper<IList<ExpenseResponse>>> SearchOrdersByEmployee(ExpenseFilterRequest orderSearchFilter, int page = 1, int take = 10);
        Task<IResponseWrapper<IList<ExpenseResponse>>> SearchOrdersByCompany(ExpenseFilterRequest orderSearchFilter, int page = 1, int take = 10);
        Task<IResponseWrapper<IList<ExpenseResponse>>> SearchOrdersByComission(ExpenseFilterRequest orderSearchFilter, int page = 1, int take = 10);


    }
}
