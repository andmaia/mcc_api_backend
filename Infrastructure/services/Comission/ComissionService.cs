using Application.services.Comission;
using AutoMapper;
using Common.requests.Comission;
using Common.requests.Order;
using Common.Responses.Comission;
using Common.Responses.Company;
using Common.Responses.Expense;
using Common.Responses.order;
using Common.Responses.wrappers;
using Domain.enums;
using Domain.models;
using Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.services.Comission
{
    public class ComissionService : IComissionService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ComissionService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<IResponseWrapper<ResponseComissionWithAllDetails>> CreateComissionAsync(CreateComissionRequest request)
        {
            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.Id == request.EmployeeId);

            if (employee == null)
            {
                return await ResponseWrapper<ResponseComissionWithAllDetails>.FailAsync("Employee not found");
            }

            var orders = await _context.Orders.Include(o=>o.Payments).ThenInclude(p=>p.PaymentForm)
                .Where(o => request.Orders.Contains(o.Id) && 
                            o.EmployeeId == request.EmployeeId &&
                            o.PaymentOrderStatus == StatusPayment.PAID &&
                            o.CreationDate >= request.CommissionStartDate &&
                            o.CreationDate <= request.CommissionEndDate)
                .ToListAsync();

            var invalidOrderIds = request.Orders.Except(orders.Select(o => o.Id)).ToList();
            if (invalidOrderIds.Any())
            {
                return await ResponseWrapper<ResponseComissionWithAllDetails>.FailAsync($"Invalid Orders: {string.Join(", ", invalidOrderIds)}.");
            }


            decimal totalExpenses = 0;

            var expenses = new List<Expense>();
            if (request.ApplyDiscount)
            {
                expenses = await _context.Expenses
                   .Where(e => request.Expenses.Contains(e.Id) &&
                               e.EmployeeId == request.EmployeeId &&
                               e.StatusPayment == StatusPayment.UNPAID &&
                               e.CreationDate >= request.CommissionStartDate &&
                               e.CreationDate <= request.CommissionEndDate)
                   .ToListAsync();

                var invalidExpenseIds = request.Expenses.Except(expenses.Select(e => e.Id)).ToList();
                if (invalidExpenseIds.Any())
                {
                    return await ResponseWrapper<ResponseComissionWithAllDetails>.FailAsync($"Invalid Expenses: {string.Join(", ", invalidExpenseIds)}.");
                }

                totalExpenses = expenses.Sum(e => e.Value);
            }


            var totalValue = orders.Sum(o => o.TotalValue);
            var totalDiscount = orders.Sum(o => o.TotalDiscount);
            var netValue = totalValue - totalDiscount;
            var commissionAmount = ((netValue * request.Percentage) / 100) - totalExpenses;


            var commission = new Domain.models.Comission
            {
                Id = Guid.NewGuid().ToString(),
                CommissionStartDate = request.CommissionStartDate,
                CommissionEndDate = request.CommissionEndDate,
                EmployeeId = request.EmployeeId,
                CompanyId = employee.CompanyId,
                TotalValue = totalValue,
                TotalFees = totalDiscount,
                TotalDiscounts = totalExpenses,
                TotalCommission = commissionAmount,
                CreationDate = DateTime.Now,
                UpdatedDate = DateTime.MinValue,
                PaymentDate = DateTime.Now,
                IsActive = true,
                ApplyDiscount = request.ApplyDiscount,
                PaymentStatus = StatusPayment.PAID,
                Url = request.Url,
                Percentage = request.Percentage,
                Expenses = expenses,
                Orders = orders,

            };

            orders.ForEach(o =>
            {
                o.ComissionId = commission.Id;
                o.CommissionOrderStatus = StatusPayment.PAID;
                o.Comission = commission;


            });

            expenses.ForEach(e =>
            {
                e.ComissionId = commission.Id;
                e.StatusPayment = StatusPayment.PAID;
                e.Comission = commission;
            });


            await _context.Comissions.AddAsync(commission);
            _context.Orders.UpdateRange(orders);
            _context.Expenses.UpdateRange(expenses);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                var response = _mapper.Map<ResponseComissionWithAllDetails>(commission);
                return await ResponseWrapper<ResponseComissionWithAllDetails>.SuccessAsync(response);

            }
            return await ResponseWrapper<ResponseComissionWithAllDetails>.FailAsync("Failed to create commission.");
        }

        public async Task<IResponseWrapper> DeleteComission(string id)
        {

            var commission = await _context.Comissions
       .FirstOrDefaultAsync(c => c.Id == id);

            if (commission == null)
            {
                return await ResponseWrapper<ResponseComissionWithAllDetails>.FailAsync("Comission not found");
            }
            if (commission.PaymentStatus == StatusPayment.PAID)
            {
                return await ResponseWrapper<ResponseComissionWithAllDetails>.FailAsync("Comission is paid. Fail to delete comission");
            }


            var orders = await _context.Orders.Where(c =>c.ComissionId == id).ToListAsync();
            if (orders.Any())
            {
                orders.ForEach(o =>
                {
                    o.ComissionId = null;
                    o.CommissionOrderStatus = StatusPayment.UNPAID;
                    o.UpdatedDate = DateTime.Now;
                    o.Comission = null;


                });
            }

            var expenses = await _context.Expenses
                  .Where(e => e.ComissionId == id)
                  .ToListAsync();

            if (expenses.Any())
            {
                expenses.ForEach(o =>
                {
                    o.StatusPayment = StatusPayment.UNPAID;
                    o.ComissionId = null;
                    o.Comission = null;
                    o.UpdatedDate = DateTime.Now;

                });
            }


            commission.IsActive = false;
            commission.UpdatedDate = DateTime.Now;
            commission.FinishedDate = DateTime.Now;

             _context.Comissions.Update(commission);
            _context.Orders.UpdateRange(orders);
            _context.Expenses.UpdateRange(expenses);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return await ResponseWrapper.SuccessAsync();

            }
            return await ResponseWrapper.FailAsync("Failed to delete commission.");


        }

        public async Task<IResponseWrapper<IList<ResponseComissionWithAllDetails>>> GetComissionByCompany(ComissionFilterRequest request, int take, int page)
        {
            try
            {
                StatusPayment statusPayment = request.IsPaid == true ? StatusPayment.PAID : StatusPayment.UNPAID;

                var query = _context.Comissions.AsNoTracking()
                    .Include(c => c.Orders)
                        .ThenInclude(o => o.Payments)
                    .Include(c => c.Expenses)
                    .Where(o =>
                        o.CompanyId == request.Id &&
                        o.CommissionStartDate >= request.BeginDate &&
                        o.CommissionEndDate <= request.EndDate &&
                        o.CreationDate <= request.CreatedDate &&
                        o.PaymentStatus == statusPayment &&
                        o.IsActive == request.IsActive)
                    .OrderByDescending(o => o.CreationDate);

                var pagedQuery = await query.Skip((page - 1) * take)
                                            .Take(take)
                                            .ToListAsync();

                if (!pagedQuery.Any())
                {
                    return await ResponseWrapper<IList<ResponseComissionWithAllDetails>>.FailAsync("There's no comissions");

                }

                var orders = pagedQuery.Select(o => _mapper.Map<ResponseComissionWithAllDetails>(o)).ToList();
                return await ResponseWrapper<IList<ResponseComissionWithAllDetails>>.SuccessAsync (orders);
            }

            catch (Exception ex)
            {
                 return await ResponseWrapper< IList<ResponseComissionWithAllDetails>>.FailAsync("An error occurred while retrieving commissions. Please try again later.");

            }
        }

        public async Task<IResponseWrapper<IList<ResponseComissionWithAllDetails>>> GetComissionByEmployee(ComissionFilterRequest request, int take, int page)
        {
            try
            {
                StatusPayment statusPayment = request.IsPaid == true ? StatusPayment.PAID : StatusPayment.UNPAID;

                var query = _context.Comissions.AsNoTracking()
                    .Include(c => c.Orders)
                        .ThenInclude(o => o.Payments)
                    .Include(c => c.Expenses)
                    .Where(o =>
                        o.EmployeeId == request.Id &&
                        o.CommissionStartDate >= request.BeginDate &&
                        o.CommissionEndDate <= request.EndDate &&
                        o.CreationDate <= request.CreatedDate &&
                        o.PaymentStatus == statusPayment &&
                        o.IsActive == request.IsActive)
                    .OrderByDescending(o => o.CreationDate);

                var pagedQuery = await query.Skip((page - 1) * take)
                                            .Take(take)
                                            .ToListAsync();

                if (!pagedQuery.Any())
                {
                    return await ResponseWrapper<IList<ResponseComissionWithAllDetails>>.FailAsync("There's no comissions");

                }

                var orders = pagedQuery.Select(o => _mapper.Map<ResponseComissionWithAllDetails>(o)).ToList();
                return await ResponseWrapper<IList<ResponseComissionWithAllDetails>>.SuccessAsync(orders);
            }

            catch (Exception ex)
            {
                return await ResponseWrapper<IList<ResponseComissionWithAllDetails>>.FailAsync("An error occurred while retrieving commissions. Please try again later.");

            }
        }

        public async Task<IResponseWrapper<ResponseComissionWithAllDetails>> GetComissionById(string id)
        {

            var commission = await _context.Comissions.AsNoTracking()
       .Include(c => c.Orders)
               .ThenInclude(o => o.Payments)
       .Include(c => c.Expenses)
       .Include(c => c.Employee)
       .FirstOrDefaultAsync(c => c.Id == id);


            if (commission == null)
            {
                return await ResponseWrapper<ResponseComissionWithAllDetails>.FailAsync("Comission not found");
            }

            var response = _mapper.Map<ResponseComissionWithAllDetails>(commission);
            return await ResponseWrapper<ResponseComissionWithAllDetails>.SuccessAsync(response);

        }

        public async Task<IResponseWrapper<IList<ResponseComissionWithAllDetails>>> GetComissionByUser(ComissionFilterRequest request, int take, int page)
        {
            try
            {
                StatusPayment statusPayment = request.IsPaid == true ? StatusPayment.PAID : StatusPayment.UNPAID;

                var query = _context.Comissions.AsNoTracking()
                    .Include(c => c.Orders)
                        .ThenInclude(o => o.Payments)
                          .Include(c => c.Employee)
                    .Include(c => c.Expenses)
                    .Where(o =>
                        o.Employee.UserId == request.Id &&
                        o.CommissionStartDate >= request.BeginDate &&
                        o.CommissionEndDate <= request.EndDate &&
                        o.CreationDate <= request.CreatedDate &&
                        o.PaymentStatus == statusPayment &&
                        o.IsActive == request.IsActive)
                    .OrderByDescending(o => o.CreationDate);

                var pagedQuery = await query.Skip((page - 1) * take)
                                            .Take(take)
                                            .ToListAsync();

                if (!pagedQuery.Any())
                {
                    return await ResponseWrapper<IList<ResponseComissionWithAllDetails>>.FailAsync("There's no comissions");

                }

                var orders = pagedQuery.Select(o => _mapper.Map<ResponseComissionWithAllDetails>(o)).ToList();
                return await ResponseWrapper<IList<ResponseComissionWithAllDetails>>.SuccessAsync(orders);
            }

            catch (Exception ex)
            {
                return await ResponseWrapper<IList<ResponseComissionWithAllDetails>>.FailAsync("An error occurred while retrieving commissions. Please try again later.");

            }

        }

        public async Task<IResponseWrapper> PayComission(string id)
        {
            var commission = await _context.Comissions
    .FirstOrDefaultAsync(c => c.Id == id);

            if (commission == null)
            {
                return await ResponseWrapper<ResponseComissionWithAllDetails>.FailAsync("Comission not found");
            }
            if (commission.PaymentStatus == StatusPayment.PAID)
            {
                return await ResponseWrapper<ResponseComissionWithAllDetails>.FailAsync("Comission already is paid.");
            }

            commission.PaymentStatus = StatusPayment.PAID;
            commission.UpdatedDate = DateTime.Now;
            commission.PaymentDate = DateTime.Now;
            _context.Comissions.Update(commission);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return await ResponseWrapper.SuccessAsync();

            }
            return await ResponseWrapper.FailAsync("Failed to pay commission.");
        }

        public async Task<IResponseWrapper> UnpayComission(string id)
        {
            var commission = await _context.Comissions
    .FirstOrDefaultAsync(c => c.Id == id);

            if (commission == null)
            {
                return await ResponseWrapper<ResponseComissionWithAllDetails>.FailAsync("Comission not found");
            }
            if (commission.PaymentStatus == StatusPayment.UNPAID)
            {
                return await ResponseWrapper<ResponseComissionWithAllDetails>.FailAsync("Comission already is u´npay.");
            }

            commission.PaymentStatus = StatusPayment.UNPAID;
            commission.UpdatedDate = DateTime.Now;
            commission.PaymentDate = DateTime.MinValue;
            _context.Comissions.Update(commission);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return await ResponseWrapper.SuccessAsync();

            }
            return await ResponseWrapper.FailAsync("Failed to unpay commission.");
        }
    }
}
