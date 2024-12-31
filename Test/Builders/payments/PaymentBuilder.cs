using Domain.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Builders.payments
{
    public class PaymentBuilder
    {
        private readonly Payment _payment;

        public PaymentBuilder()
        {
            _payment = new Payment
            {
                Id = Guid.NewGuid().ToString(),
                CreationDate = DateTime.Now,
                PaymentDate = DateTime.MinValue,
                UpdatedDate = DateTime.MinValue,
                CompletionDate = DateTime.MinValue,
                IsActive = false,
                PaymentFormId = Guid.NewGuid().ToString(),
                PaymentForm = null,
                Order = null,
                OrderId = Guid.NewGuid().ToString(),
                Tax = 0,
                Url = string.Empty,
                Discount = 0,
                Amount = 0
            };
        }

        public Payment Build() => _payment;

        public PaymentBuilder WithPaymentDate(DateTime? paymentDate)
        {
            _payment.PaymentDate = paymentDate;
            return this;
        }

        public PaymentBuilder WithUpdatedDate(DateTime? updatedDate)
        {
            _payment.UpdatedDate = updatedDate;
            return this;
        }

        public PaymentBuilder WithCompletionDate(DateTime? completionDate)
        {
            _payment.CompletionDate = completionDate;
            return this;
        }

        public PaymentBuilder WithIsActive(bool isActive)
        {
            _payment.IsActive = isActive;
            return this;
        }

        public PaymentBuilder WithPaymentFormId(string paymentFormId)
        {
            _payment.PaymentFormId = paymentFormId;
            return this;
        }

        public PaymentBuilder WithPaymentForm(PaymentForm paymentForm)
        {
            _payment.PaymentForm = paymentForm;
            return this;
        }

        public PaymentBuilder WithOrder(Order order)
        {
            _payment.Order = order;
            return this;
        }

        public PaymentBuilder WithOrderId(string orderId)
        {
            _payment.OrderId = orderId;
            return this;
        }

        public PaymentBuilder WithTax(decimal tax)
        {
            _payment.Tax = tax;
            return this;
        }

        public PaymentBuilder WithUrl(string url)
        {
            _payment.Url = url;
            return this;
        }

        public PaymentBuilder WithDiscount(decimal discount)
        {
            _payment.Discount = discount;
            return this;
        }

        public PaymentBuilder WithAmount(decimal amount)
        {
            _payment.Amount = amount;
            return this;
        }
    }
}
