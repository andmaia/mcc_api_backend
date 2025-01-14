using Application.services.Payment;
using AutoMapper;
using Common.requests.Payment;
using Common.Responses.order;
using Common.Responses.Paymenet;
using Common.Responses.wrappers;
using Domain.enums;
using Domain.models;
using Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.services.paymenet
{
    public class PaymentService : IPaymentService
    {

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PaymentService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IResponseWrapper> CreatePayment(CreatePaymentRequest request)
        {
            var paymentForm = await _context.PaymentForms
         .FirstOrDefaultAsync(pf => pf.Id == request.PaymentFormId && pf.IsActive == true);

            if (paymentForm == null)
            {
                return ResponseWrapper.Fail("Payment form not found");
            }

            var order = await _context.Orders
                .Include(o => o.Payments)
                .FirstOrDefaultAsync(o => o.Id == request.OrderId && o.IsActive == true);

            if (order == null)
            {
                return ResponseWrapper.Fail("Order not found");
            }

            if (order.PaymentOrderStatus == Domain.enums.StatusPayment.PAID)
            {
                return ResponseWrapper.Fail("Order is paid");
            }

            if (!VerifyIfValueIsValid(request.Value, order))
            {
                return ResponseWrapper.Fail("The payment value exceeds the available amount for this order.");
            }

            var discount = request.Value * (request.Tax / 100);

            var payment = new Payment
            {
                Id = Guid.NewGuid().ToString(),
                Value = request.Value,
                Tax = request.Tax,
                Discount = discount,
                Amount = request.Value - discount,
                IsActive = true,
                OrderId = request.OrderId,
                PaymentFormId = request.PaymentFormId,
                Order = order,
                PaymentForm = paymentForm,
                Url = request.Url,
                CreationDate = DateTime.Now,
                CompletionDate = DateTime.MinValue,
                PaymentDate = DateTime.MinValue,
                UpdatedDate = DateTime.MinValue,

            };

            if (order.Payments.Count == 0 && order.PaymentOrderStatus == StatusPayment.PENDING)
            {
                order.PaymentOrderStatus = Domain.enums.StatusPayment.UNPAID;
            }

            order.Payments.Add(payment);
            order.TotalDiscount += discount;

            _context.Orders.Update(order);
            await _context.Payments.AddAsync(payment);
            var result = await _context.SaveChangesAsync();
            var paymentResponse = _mapper.Map<ResponsePayment>(payment);
            return result > 0
                ? await ResponseWrapper<ResponsePayment>.SuccessAsync(paymentResponse)
                : await ResponseWrapper<ResponsePayment>.FailAsync("Failed to create payment.");

        }

        public async Task<IResponseWrapper> GetPaymentById(string id)
        {
            var payment = await _context.Payments.AsNoTracking()
        .FirstOrDefaultAsync(pf => pf.Id == id && pf.IsActive ==true);

            if (payment == null)
            {
                return ResponseWrapper.Fail("Payment  not found");
            }

            var paymentResponse = _mapper.Map<ResponsePayment>(payment);
            return await ResponseWrapper<ResponsePayment>.SuccessAsync(paymentResponse);

        }

        public async Task<IResponseWrapper> GetPaymentsByOrderId(string id)
        {
            var order = await _context.Orders
                .Include(o => o.Payments)
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.Id == id && o.IsActive == true);

            if (order == null)
            {
                return ResponseWrapper.Fail("Order does not exist.");
            }
            order.Payments = order.Payments.Where(p => p.IsActive == true).ToList();
            var paymentResponses = _mapper.Map<List<ResponsePayment>>(order.Payments);

            return await ResponseWrapper<List<ResponsePayment>>.SuccessAsync(paymentResponses);
        }

        public async Task<IResponseWrapper> RemovePayment(string id)
        {
            var payment = await _context.Payments
        .FirstOrDefaultAsync(pf => pf.Id == id && pf.IsActive ==true);
            if (payment == null)
            {
                return ResponseWrapper.Fail("Payment not found");
            }
            var order = await _context.Orders
               .FirstOrDefaultAsync(o => o.Id == payment.OrderId && o.IsActive == true);

            if (order == null)
            {
                return ResponseWrapper.Fail("Payment does'has a order");
            }

            if (order.PaymentOrderStatus == Domain.enums.StatusPayment.PAID)
            {
                return ResponseWrapper.Fail("Payment is from a order already paid");
            }
            if (order.ComissionId is not null)
            {
                return ResponseWrapper.Fail("Payment if from a order that already has a comission");
            }
            order.TotalDiscount -= payment.Discount;
            payment.IsActive = false;
            payment.CompletionDate = DateTime.Now;
            _context.Orders.Update(order);
            _context.Payments.Update(payment);
            var result = await _context.SaveChangesAsync();
            return result > 0
                ? await ResponseWrapper<ResponsePayment>.SuccessAsync()
                : await ResponseWrapper<ResponsePayment>.FailAsync("Failed to remove payment.");
        }

        public async Task<IResponseWrapper> UpdatePayment(UpdatePaymentRequest request)
        {
            var payment = await _context.Payments
       .FirstOrDefaultAsync(pf => pf.Id == request.Id && pf.IsActive == true);
            if (payment == null)
            {
                return ResponseWrapper.Fail("Payment not found");
            }

            var paymentForm = await _context.PaymentForms
         .FirstOrDefaultAsync(pf => pf.Id == request.PaymentFormId && pf.IsActive == true);

            if (paymentForm == null)
            {
                return ResponseWrapper.Fail("Payment form not found");
            }

            var order = await _context.Orders
                .Include(o => o.Payments)
                .FirstOrDefaultAsync(o => o.Id == request.OrderId && o.IsActive == true);

            if (order == null)
            {
                return ResponseWrapper.Fail("Order not found");
            }

            if (order.PaymentOrderStatus == Domain.enums.StatusPayment.PAID)
            {
                return ResponseWrapper.Fail("Order is paid");
            }
            if (order.ComissionId is not null)
            {
                return ResponseWrapper.Fail("Order already has a comission");
            }

            if (!VerifyIfValueIsValid(request.Value, order, payment.Id))
            {
                return ResponseWrapper.Fail("The payment value exceeds the available amount for this order.");
            }

            order.TotalDiscount -= payment.Discount;

            var discount = request.Value * (request.Tax / 100);
            payment.Tax = request.Tax;
            payment.Value = request.Value;
            payment.Discount = discount;
            payment.Amount = payment.Value - discount;
            payment.UpdatedDate = DateTime.Now;

            order.TotalDiscount += discount;

            _context.Orders.Update(order);
            _context.Payments.Update(payment);
            var result = await _context.SaveChangesAsync();
            var paymentResponse = _mapper.Map<ResponsePayment>(payment);
            return result > 0
                ? await ResponseWrapper<ResponsePayment>.SuccessAsync(paymentResponse)
                : await ResponseWrapper<ResponsePayment>.FailAsync("Failed to update payment.");

        }

        private bool VerifyIfValueIsValid(decimal value, Order order, string currentPaymentId=null)
        {
            var totalPayments = order.Payments
                .Where(p => p.Id != currentPaymentId) // Exclui o pagamento atual
                .Sum(p => p.Value);

            return value <= (order.TotalValue - totalPayments);
        }


    }
}
