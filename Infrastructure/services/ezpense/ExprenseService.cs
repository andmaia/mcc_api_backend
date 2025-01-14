using Application.services.Expense;
using AutoMapper;
using Common.requests.Expense;
using Common.requests.Order;
using Common.Responses.Expense;
using Common.Responses.order;
using Common.Responses.wrappers;
using Domain.enums;
using Domain.models;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.services.ezpense
{
    public class ExprenseService:IExpenseService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ExprenseService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IResponseWrapper<ExpenseResponse>> CreateExpense(CreateExpenseRequest request)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.Id == request.EmployeeId);

            if (employee is null)
            {
                return await ResponseWrapper<ExpenseResponse>.FailAsync("Employee not found");
            };

            var expense = new Expense
            {
                Id = Guid.NewGuid().ToString(),
                EmployeeId = employee.Id,
                Description = request.Description,
                IsActive = true,
                Url = request.Url,
                StatusPayment = StatusPayment.UNPAID,
                Value = request.Value,
                CompanyId = employee.CompanyId,
                CreationDate = DateTime.Now,
                UpdatedDate = DateTime.MinValue,
                CompletionDate = DateTime.MinValue
            };

            await _context.Expenses.AddAsync(expense);
            var result = await _context.SaveChangesAsync();
            var expenseResponse = _mapper.Map<ExpenseResponse>(expense);
            return result > 0
                ? await ResponseWrapper<ExpenseResponse>.SuccessAsync(expenseResponse)
                : await ResponseWrapper<ExpenseResponse>.FailAsync("Failed to create expense.");

        }

        public async Task<IResponseWrapper<ExpenseResponse>> DisableExpense(string id)
        {
            var expense = _context.Expenses.FirstOrDefault(x => x.Id == id);
            if (expense == null)
            {
                return await ResponseWrapper<ExpenseResponse>.FailAsync("Expense not found");
            }
            if (expense.ComissionId is not null)
            {
                return await ResponseWrapper<ExpenseResponse>.FailAsync("Expense already has a comission");

            }
            if (expense.StatusPayment == StatusPayment.PAID)
            {
                return await ResponseWrapper<ExpenseResponse>.FailAsync("Expense is paid");
            }

            expense.CompletionDate = DateTime.Now;
            expense.IsActive = false;

            _context.Expenses.Update(expense);
            var result = await _context.SaveChangesAsync();
            return result > 0
                ? await ResponseWrapper<ExpenseResponse>.SuccessAsync()
                : await ResponseWrapper<ExpenseResponse>.FailAsync("Failed to disable expense.");

        }

        public async Task<IResponseWrapper<ExpenseResponse>> GetExpenseById(string id)
        {
            var expense = await _context.Expenses
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (expense == null)
            {
                return await ResponseWrapper<ExpenseResponse>.FailAsync("Expense not found");
            }

            var expenseResponse = _mapper.Map<ExpenseResponse>(expense);
            return await ResponseWrapper<ExpenseResponse>.SuccessAsync(expenseResponse);
        }


        public async Task<IResponseWrapper<IList<ExpenseResponse>>> SearchOrdersByComission(ExpenseFilterRequest orderSearchFilter, int page = 1, int take = 10)
        {
            var beginDate = orderSearchFilter.BeginDate ?? new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var endDate = orderSearchFilter.EndDate ?? DateTime.Now;

   
            var query = _context.Expenses
                .AsNoTracking()
                .Where(o =>
                    o.ComissionId == orderSearchFilter.Id &&
                    o.StatusPayment == orderSearchFilter.statusPayment &&
                    o.IsActive == orderSearchFilter.IsActive &&
                    o.CreationDate >= beginDate &&
                    o.CreationDate <= endDate)
                .OrderByDescending(o => o.CreationDate)
                .Skip((page - 1) * take)
                .Take(take)
                .Select(o => new ExpenseResponse
                {
                    Id = o.Id,
                    IsActive = o.IsActive,
                    CreationDate = o.CreationDate,
                    UpdatedDate = o.UpdatedDate,
                    CompletionDate = o.CompletionDate,
                     StatusPayment = o.StatusPayment,
                    EmployeeId = o.EmployeeId,
                    ComissionId = o.ComissionId,
                    Url = o.Url,
                    CompanyId = o.CompanyId,
                    Description = o.Description,
                    Value = o.Value,
                });

            return new ResponseWrapper<IList<ExpenseResponse>>
            {
                ResponseData = await query.ToListAsync(),
                IsSuccessful = true,
            };
        }

        public async Task<IResponseWrapper<IList<ExpenseResponse>>> SearchOrdersByCompany(ExpenseFilterRequest orderSearchFilter, int page = 1, int take = 10)
        {
            var beginDate = orderSearchFilter.BeginDate ?? new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var endDate = orderSearchFilter.EndDate ?? DateTime.Now;


            var query = _context.Expenses
                .AsNoTracking()
                .Where(o =>
                    o.CompanyId == orderSearchFilter.Id &&
                    o.StatusPayment == orderSearchFilter.statusPayment &&
                    o.IsActive == orderSearchFilter.IsActive &&
                    o.CreationDate >= beginDate &&
                    o.CreationDate <= endDate)
                .OrderByDescending(o => o.CreationDate)
                .Skip((page - 1) * take)
                .Take(take)
                .Select(o => new ExpenseResponse
                {
                    Id = o.Id,
                    IsActive = o.IsActive,
                    CreationDate = o.CreationDate,
                    UpdatedDate = o.UpdatedDate,
                    CompletionDate = o.CompletionDate,
                    StatusPayment = o.StatusPayment,
                    EmployeeId = o.EmployeeId,
                    ComissionId = o.ComissionId,
                    Url = o.Url,
                    CompanyId = o.CompanyId,
                    Description = o.Description,
                    Value = o.Value,
                });

            return new ResponseWrapper<IList<ExpenseResponse>>
            {
                ResponseData = await query.ToListAsync(),
                IsSuccessful = true,
            };
        }

        public async Task<IResponseWrapper<IList<ExpenseResponse>>> SearchOrdersByEmployee(ExpenseFilterRequest orderSearchFilter, int page = 1, int take = 10)
        {
            var beginDate = orderSearchFilter.BeginDate ?? new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var endDate = orderSearchFilter.EndDate ?? DateTime.Now;


            var query = _context.Expenses
                .AsNoTracking()
                .Where(o =>
                    o.EmployeeId == orderSearchFilter.Id &&
                    o.StatusPayment == orderSearchFilter.statusPayment &&
                    o.IsActive == orderSearchFilter.IsActive &&
                    o.CreationDate >= beginDate &&
                    o.CreationDate <= endDate)
                .OrderByDescending(o => o.CreationDate)
                .Skip((page - 1) * take)
                .Take(take)
                .Select(o => new ExpenseResponse
                {
                    Id = o.Id,
                    IsActive = o.IsActive,
                    CreationDate = o.CreationDate,
                    UpdatedDate = o.UpdatedDate,
                    CompletionDate = o.CompletionDate,
                    StatusPayment = o.StatusPayment,
                    EmployeeId = o.EmployeeId,
                    ComissionId = o.ComissionId,
                    Url = o.Url,
                    CompanyId = o.CompanyId,
                    Description = o.Description,
                    Value = o.Value,
                });

            return new ResponseWrapper<IList<ExpenseResponse>>
            {
                ResponseData = await query.ToListAsync(),
                IsSuccessful = true,
            };
        }

        public async Task<IResponseWrapper<ExpenseResponse>> UpdateExpese(UpdateExpenseRequest request)
        {
            var expense = _context.Expenses.FirstOrDefault(x => x.Id == request.Id);
            if (expense == null)
            {
                return await ResponseWrapper<ExpenseResponse>.FailAsync("Expense not found");
            }

            var employee = _context.Employees.FirstOrDefault(e => e.Id == request.EmployeeId);

            if (employee is null)
            {
                return await ResponseWrapper<ExpenseResponse>.FailAsync("Employee not found");
            };

            expense.Description = request.Description;
            expense.Value = request.Value;
            expense.EmployeeId = employee.Id;
            expense.CompanyId = employee.CompanyId;
            expense.UpdatedDate = DateTime.Now;

             _context.Expenses.Update(expense);
            var result = await _context.SaveChangesAsync();
            var expenseResponse = _mapper.Map<ExpenseResponse>(expense);
            return result > 0
                ? await ResponseWrapper<ExpenseResponse>.SuccessAsync(expenseResponse)
                : await ResponseWrapper<ExpenseResponse>.FailAsync("Failed to update expense.");
        }
    }
}
