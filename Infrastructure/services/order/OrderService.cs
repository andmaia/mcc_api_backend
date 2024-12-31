using Application.services.Order;
using AutoMapper;
using Common.requests.Order;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Infrastructure.services.order
{
    public class OrderService : IOrderService
    {

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public OrderService(ApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IResponseWrapper<ResponseOrder>> CreateOrderAsync(CreateOrderRequest request)
        {
            var isEmployeeValid = await _context.Employees
                       .AnyAsync(e => e.Id == request.EmployeeId && e.CompanyId == request.CompanyId);

            if (!isEmployeeValid)
            {
                return await ResponseWrapper<ResponseOrder>.FailAsync("Employee does not belong this company.");
            }

            var isBranchValid = await _context.Branchs.AnyAsync(e => e.Id == request.branchId && e.companyId == request.CompanyId);
            if (isBranchValid ==false)
            {
                return await ResponseWrapper<ResponseOrder>.FailAsync("Branch does not belong this company.");
            }
         
            Order order = new Order()
            {
                Id = Guid.NewGuid().ToString(),
                CompletionDate = DateTime.MinValue,
                UpdatedDate = DateTime.MinValue,
                EmployeeId = request.EmployeeId,
                branchId = request.branchId,
                CreationDate = DateTime.Now,
                CommissionOrderStatus = Domain.enums.StatusPayment.UNPAID,
                CustomerName = request.CustomerName,
                CommissionPercentage = request.CommissionPercentage,
                CompanyId = request.CompanyId,
                IsActive = true,
                PaymentOrderStatus = Domain.enums.StatusPayment.PENDING,
                TotalDiscount = 0,
                TotalValue = request.TotalValue
            };

            await _context.Orders.AddAsync(order);
            var result = await _context.SaveChangesAsync();
            var OrderResponse = _mapper.Map<ResponseOrder>(order);
            return result > 0
                ? await ResponseWrapper<ResponseOrder>.SuccessAsync(OrderResponse)
                : await ResponseWrapper<ResponseOrder>.FailAsync("Failed to create order.");
        }

       public async Task<IResponseWrapper> DisableOrder(string id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if (order == null)
            {
                return ResponseWrapper<ResponseOrder>.Fail("Order does not exist.");
            }

            if (!string.IsNullOrEmpty(order.ComissionId))
            {
                return ResponseWrapper<ResponseOrder>.Fail("Order has associations with a commission and cannot be disabled.");
            }

            var payments = await _context.Payments.Where(p => p.OrderId == id).ToListAsync();
            foreach (var payment in payments)
            {
                payment.IsActive = false;
            }

            order.CompletionDate = DateTime.MinValue;
            order.IsActive = false;

           var result = await _context.SaveChangesAsync();
            return result > 0
               ? await ResponseWrapper.SuccessAsync()
               : await ResponseWrapper.FailAsync("Failed to disable order.");
        }

        public async Task<IResponseWrapper<ResponseOrder>> GetOrderById(string id)
        {
            var o = await _context.Orders.Include(o => o.Company)
                .Include(o => o.Branch)
                .Include(o => o.Employee)
                .Include(o => o.Comission).AsNoTracking().FirstOrDefaultAsync(o => o.Id == id);
            if (o == null)
            {
                return ResponseWrapper<ResponseOrder>.Fail("Order does not exist.");
            }

            var response = new ResponseOrder
            {
                Id = o.Id,
                CustomerName = o.CustomerName,
                IsActive = o.IsActive,
                CreationDate = o.CreationDate,
                UpdatedDate = o.UpdatedDate,
                CompletionDate = o.CompletionDate,
                PaymentOrderStatus = o.PaymentOrderStatus.ToString(),
                CommissionOrderStatus = o.CommissionOrderStatus.ToString(),
                TotalValue = o.TotalValue,
                CommissionPercentage = o.CommissionPercentage,
                TotalDiscount = o.TotalDiscount,
                CompanyId = o.CompanyId,
                branchId = o.branchId,
                EmployeeId = o.EmployeeId,
                ComissionId = o.ComissionId
            };

            return new ResponseWrapper<ResponseOrder>
            {
                ResponseData = response
            };

        }

        public async Task<IResponseWrapper> PayOrderAsync(string id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if (order == null)
            {
                return ResponseWrapper.Fail("Order does not exist.");
            }

            if (!order.IsActive)
            {
                return ResponseWrapper.Fail("Order is not active.");
            }
            if (order.PaymentOrderStatus != StatusPayment.UNPAID && order.PaymentOrderStatus != StatusPayment.PENDING)
            {
                return ResponseWrapper.Fail("Order is not marked as unpaid.");
            }


            var payments = await _context.Payments.Where(p => p.OrderId == id).AsNoTracking().ToListAsync();
            var totalPayments = payments.Sum(p => p.Amount);

            if (totalPayments != order.TotalValue)
            {
                return ResponseWrapper.Fail("Total payments do not match the order's total value.");
            }

            order.CompletionDate = DateTime.UtcNow;
            order.PaymentOrderStatus = StatusPayment.PAID;

            var result = await _context.SaveChangesAsync();

            return result > 0
                ? await ResponseWrapper.SuccessAsync()
                : await ResponseWrapper.FailAsync("Failed to pay order.");
        }

        public async Task<IResponseWrapper<IList<ResponseOrder>>> SearchOrdersByBranch(OrderSearchFilter orderSearchFilter, int page = 1, int take = 10)
        {
            var beginDate = orderSearchFilter.BeginDate ?? new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var endDate = orderSearchFilter.EndDate ?? DateTime.Now;

            var query = _context.Orders
                .Include(o => o.Company)
                .Include(o => o.Branch)
                .Include(o => o.Employee)
                .Include(o => o.Comission).AsNoTracking()
                .Where(o =>
                    o.branchId == orderSearchFilter.Id &&
                    o.IsActive == orderSearchFilter.IsActive &&
                    o.CreationDate >= beginDate &&
                    o.CreationDate <= endDate)
                .OrderByDescending(o => o.CreationDate)
                .Skip((page - 1) * take)
                .Take(take)
                .Select(o => new ResponseOrder
                {
                    Id = o.Id,
                    CustomerName = o.CustomerName,
                    IsActive = o.IsActive,
                    CreationDate = o.CreationDate,
                    UpdatedDate = o.UpdatedDate,
                    CompletionDate = o.CompletionDate,
                    PaymentOrderStatus = o.PaymentOrderStatus.ToString(),
                    CommissionOrderStatus = o.CommissionOrderStatus.ToString(),
                    TotalValue = o.TotalValue,
                    CommissionPercentage = o.CommissionPercentage,
                    TotalDiscount = o.TotalDiscount,
                    CompanyId = o.CompanyId,
                    branchId = o.branchId,
                    EmployeeId = o.EmployeeId,
                    ComissionId = o.ComissionId
                });

            return new ResponseWrapper<IList<ResponseOrder>>
            {
                ResponseData = await query.ToListAsync(),
                IsSuccessful = true,
            };
        }
        public async Task<IResponseWrapper<IList<ResponseOrder>>> SearchOrdersByEmployee(OrderSearchFilter orderSearchFilter, int page = 1, int take = 10)
        {
            var beginDate = orderSearchFilter.BeginDate ?? new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var endDate = orderSearchFilter.EndDate ?? DateTime.Now;

            var query = _context.Orders
                .Include(o => o.Company)  // Inclui a entidade relacionada à empresa
                .Include(o => o.Branch)   // Inclui a entidade relacionada à filial
                .Include(o => o.Employee) // Inclui a entidade relacionada ao funcionário
                .Include(o => o.Comission).AsNoTracking() // Inclui a entidade relacionada à comissão
                .Where(o =>
                    o.EmployeeId == orderSearchFilter.Id &&
                    o.CreationDate >= beginDate &&
                    o.CreationDate <= endDate)
                .OrderByDescending(o => o.CreationDate)
                .Skip((page - 1) * take)
                .Take(take);

            var orders = await query
                .Select(o => new ResponseOrder
                {
                    Id = o.Id,
                    CustomerName = o.CustomerName,
                    IsActive = o.IsActive,
                    CreationDate = o.CreationDate,
                    UpdatedDate = o.UpdatedDate,
                    CompletionDate = o.CompletionDate,
                    PaymentOrderStatus = o.PaymentOrderStatus.ToString(),
                    CommissionOrderStatus = o.CommissionOrderStatus.ToString(),
                    TotalValue = o.TotalValue,
                    CommissionPercentage = o.CommissionPercentage,
                    TotalDiscount = o.TotalDiscount,
                    CompanyId = o.CompanyId,
                    branchId = o.branchId,
                    EmployeeId = o.EmployeeId,
                    ComissionId = o.ComissionId
                })
                .ToListAsync();

            return new ResponseWrapper<IList<ResponseOrder>>
            {
                ResponseData = orders,
                IsSuccessful = true,

            };
        }

        public async Task<IResponseWrapper<IList<ResponseOrder>>> SearchOrdersByCompany(OrderSearchFilter orderSearchFilter, int page = 1, int take = 10)
        {
            var beginDate = orderSearchFilter.BeginDate ?? new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var endDate = orderSearchFilter.EndDate ?? DateTime.Now;

            var query = _context.Orders
                .Include(o => o.Company)
                .Include(o => o.Branch)
                .Include(o => o.Employee)
                .Include(o => o.Comission).AsNoTracking()
                .Where(o =>
                    o.CompanyId == orderSearchFilter.Id &&
                    o.IsActive == orderSearchFilter.IsActive &&
                    o.CreationDate >= beginDate &&
                    o.CreationDate <= endDate)
                .OrderByDescending(o => o.CreationDate)
                .Skip((page - 1) * take)
                .Take(take)
                .Select(o => new ResponseOrder
                {
                    Id = o.Id,
                    CustomerName = o.CustomerName,
                    IsActive = o.IsActive,
                    CreationDate = o.CreationDate,
                    UpdatedDate = o.UpdatedDate,
                    CompletionDate = o.CompletionDate,
                    PaymentOrderStatus = o.PaymentOrderStatus.ToString(),
                    CommissionOrderStatus = o.CommissionOrderStatus.ToString(),
                    TotalValue = o.TotalValue,
                    CommissionPercentage = o.CommissionPercentage,
                    TotalDiscount = o.TotalDiscount,
                    CompanyId = o.CompanyId,
                    branchId = o.branchId,
                    EmployeeId = o.EmployeeId,
                    ComissionId = o.ComissionId
                });

            return new ResponseWrapper<IList<ResponseOrder>>
            {
                ResponseData = await query.ToListAsync(),
                IsSuccessful = true,

            };
        }

       

        public async Task<IResponseWrapper> UnpayOrderAsync(string id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if (order == null)
            {
                return ResponseWrapper.Fail("Order does not exist.");
            }

            if (!order.IsActive)
            {
                return ResponseWrapper.Fail("Order is not active.");
            }

            if (order.PaymentOrderStatus != StatusPayment.PAID)
            {
                return ResponseWrapper.Fail("Order is not marked as paid.");
            }


            order.CompletionDate = DateTime.MinValue;
            order.PaymentOrderStatus = StatusPayment.UNPAID;

            var result = await _context.SaveChangesAsync();

            return result > 0
                ? await ResponseWrapper.SuccessAsync()
                : await ResponseWrapper.FailAsync("Failed to pay order.");
        }

        public async Task<ResponseWrapper<ResponseOrder>> UpdateOrderAsync(UpdateOrderRequest request)
        {
            var order = await _context.Orders.FindAsync(request.Id);
            if (order == null)
            {
                return await ResponseWrapper<ResponseOrder>.FailAsync("Order does not exist.");
            }

            if (!string.IsNullOrEmpty(request.branchId))
            {
                var branch = await _context.Branchs.FindAsync(request.branchId);
                if (branch == null)
                {
                    return await ResponseWrapper<ResponseOrder>.FailAsync("Branch does not exist.");
                }
                order.branchId = branch.Id;
            }

            if (!string.IsNullOrEmpty(request.EmployeeId))
            {
                var employee = await _context.Employees.FindAsync(request.EmployeeId);
                if (employee == null)
                {
                    return await ResponseWrapper<ResponseOrder>.FailAsync("Employee does not exist.");
                }
                order.EmployeeId = employee.Id;
            }

            if (request.CommissionPercentage is not null)
            {
                order.CommissionPercentage = (decimal)request.CommissionPercentage;
            }

            if (request.TotalValue is not null)
            {
                order.TotalValue = (decimal)request.TotalValue;
            }
            order.UpdatedDate = DateTime.Now;

            var result = await _context.SaveChangesAsync();

            var orderResponse = _mapper.Map<ResponseOrder>(order);
            return result > 0
                ? await ResponseWrapper<ResponseOrder>.SuccessAsync(orderResponse)
                : await ResponseWrapper<ResponseOrder>.FailAsync("Failed to update order.");
        }

       


    }



}
