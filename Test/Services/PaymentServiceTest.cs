using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Services
{
    using System;
    using System.Threading.Tasks;
    
    using FluentAssertions;
    using Moq;
    using Xunit;
    using Microsoft.EntityFrameworkCore;
    using AutoMapper;
    using Application.services.Payment;
    using Common.requests.Payment;
    using Domain.enums;
    using global::Test.Builders.branchs;
    using global::Test.Builders.payments;
    using global::Test.Configuration;
    using Infrastructure.services.paymenet;
    using global::Test.Builders.paymentForm;
    using Infrastructure;

    namespace Test.Services
    {
        [Collection("IntegrationTests")]
        public class PaymentServiceTest
        {
            private readonly IntegrationFixture _fixture;
            private readonly IPaymentService _paymentServiceMock;
            private readonly IMapper _mapperMock;

            public PaymentServiceTest(IntegrationFixture fixture)
            {
                var mappingConfig = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new MappingProfiles());
                });

                _mapperMock = new Mapper(mappingConfig);
                _fixture = fixture;
                _paymentServiceMock = new PaymentService(_fixture.DbContext, _mapperMock);
            }

            [Fact]
            public async Task UpdatePayment_Should_Fail_If_Payment_Not_Found()
            {
                var updatePaymentRequest = new UpdatePaymentRequest { Id = Guid.NewGuid().ToString() };

                var result = await _paymentServiceMock.UpdatePayment(updatePaymentRequest);

                result.IsSuccessful.Should().BeFalse();
                result.Messages.Should().Contain("Payment not found");
            }

            [Fact]
            public async Task UpdatePayment_Should_Fail_If_PaymentForm_Not_Found()
            {
                var payment = new PaymentBuilder().Build();
                _fixture.DbContext.Payments.Add(payment);
                await _fixture.DbContext.SaveChangesAsync();

                var updatePaymentRequest = new UpdatePaymentRequest
                {
                    Id = payment.Id,
                    PaymentFormId = Guid.NewGuid().ToString()
                };

                var result = await _paymentServiceMock.UpdatePayment(updatePaymentRequest);

                result.IsSuccessful.Should().BeFalse();
                result.Messages.Should().Contain("Payment form not found");
            }

            [Fact]
            public async Task UpdatePayment_Should_Fail_If_Order_Not_Found()
            {
                var payment = new PaymentBuilder().Build();
                var paymentForm = new PaymentFormBuilder().Build();
                _fixture.DbContext.Payments.Add(payment);
                _fixture.DbContext.PaymentForms.Add(paymentForm);
                await _fixture.DbContext.SaveChangesAsync();

                var updatePaymentRequest = new UpdatePaymentRequest
                {
                    Id = payment.Id,
                    PaymentFormId = paymentForm.Id,
                    OrderId = Guid.NewGuid().ToString()
                };

                var result = await _paymentServiceMock.UpdatePayment(updatePaymentRequest);

                result.IsSuccessful.Should().BeFalse();
                result.Messages.Should().Contain("Order not found");
            }

            [Fact]
            public async Task UpdatePayment_Should_Fail_If_Order_Paid_Or_Has_Commission()
            {
                var payment = new PaymentBuilder().Build();
                var paymentForm = new PaymentFormBuilder().Build();
                var order = new OrderBuilder().WithPaymentOrderStatus(StatusPayment.PAID).WithComissionId(Guid.NewGuid().ToString()).Build();
                _fixture.DbContext.Payments.Add(payment);
                _fixture.DbContext.PaymentForms.Add(paymentForm);
                _fixture.DbContext.Orders.Add(order);
                await _fixture.DbContext.SaveChangesAsync();

                var updatePaymentRequest = new UpdatePaymentRequest
                {
                    Id = payment.Id,
                    PaymentFormId = paymentForm.Id,
                    OrderId = order.Id
                };

                var result = await _paymentServiceMock.UpdatePayment(updatePaymentRequest);

                result.IsSuccessful.Should().BeFalse();
                result.Messages.Should().Contain("Order is paid");
            }

            [Fact]
            public async Task UpdatePayment_Should_Fail_If_Value_Exceeds_Available_Amount()
            {
                var payment = new PaymentBuilder().Build();
                var paymentForm = new PaymentFormBuilder().Build();
                var order = new OrderBuilder().WithTotalValue(500).Build();
                _fixture.DbContext.Payments.Add(payment);
                _fixture.DbContext.PaymentForms.Add(paymentForm);
                _fixture.DbContext.Orders.Add(order);
                await _fixture.DbContext.SaveChangesAsync();

                var updatePaymentRequest = new UpdatePaymentRequest
                {
                    Id = payment.Id,
                    PaymentFormId = paymentForm.Id,
                    OrderId = order.Id,
                    Value = 600
                };

                var result = await _paymentServiceMock.UpdatePayment(updatePaymentRequest);

                result.IsSuccessful.Should().BeFalse();
                result.Messages.Should().Contain("The payment value exceeds the available amount for this order.");
            }

            [Fact]
            public async Task UpdatePayment_Should_Update_Payment_And_Order()
            {
                var payment = new PaymentBuilder().Build();
                var paymentForm = new PaymentFormBuilder().Build();
                var order = new OrderBuilder().WithTotalValue(1000).Build();
                _fixture.DbContext.Payments.Add(payment);
                _fixture.DbContext.PaymentForms.Add(paymentForm);
                _fixture.DbContext.Orders.Add(order);
                await _fixture.DbContext.SaveChangesAsync();

                var updatePaymentRequest = new UpdatePaymentRequest
                {
                    Id = payment.Id,
                    PaymentFormId = paymentForm.Id,
                    OrderId = order.Id,
                    Value = 200,
                    Tax = 10
                };

                var result = await _paymentServiceMock.UpdatePayment(updatePaymentRequest);

                result.IsSuccessful.Should().BeTrue();
                var updatedPayment = await _fixture.DbContext.Payments.FirstOrDefaultAsync(p => p.Id == payment.Id);
                var updatedOrder = await _fixture.DbContext.Orders.FirstOrDefaultAsync(o => o.Id == order.Id);

                updatedPayment.Should().NotBeNull();
                updatedOrder.Should().NotBeNull();
                updatedPayment.Value.Should().Be(200);
                updatedPayment.Tax.Should().Be(10);
                updatedPayment.Discount.Should().Be(20); // 200 * (10 / 100)
                updatedOrder.TotalDiscount.Should().Be(20);
            }

            public async Task RemovePayment_Should_Fail_If_Payment_Not_Found()
            {
                var result = await _paymentServiceMock.RemovePayment(Guid.NewGuid().ToString());

                result.IsSuccessful.Should().BeFalse();
                result.Messages.Should().Contain("Payment not found");
            }

            [Fact]
            public async Task RemovePayment_Should_Fail_If_Order_Not_Found()
            {
                var payment = new PaymentBuilder().Build();
                _fixture.DbContext.Payments.Add(payment);
                await _fixture.DbContext.SaveChangesAsync();

                var result = await _paymentServiceMock.RemovePayment(payment.Id);

                result.IsSuccessful.Should().BeFalse();
                result.Messages.Should().Contain("Payment does'has a order");
            }

            [Fact]
            public async Task RemovePayment_Should_Fail_If_Order_Is_Paid()
            {
                var order = new OrderBuilder().WithPaymentOrderStatus(StatusPayment.PAID).Build();
                var payment = new PaymentBuilder().WithOrderId(order.Id).Build();

                _fixture.DbContext.Payments.Add(payment);
                _fixture.DbContext.Orders.Add(order);
                await _fixture.DbContext.SaveChangesAsync();

                var result = await _paymentServiceMock.RemovePayment(payment.Id);

                result.IsSuccessful.Should().BeFalse();
                result.Messages.Should().Contain("Payment is from a order already paid");
            }

            [Fact]
            public async Task RemovePayment_Should_Fail_If_Order_Has_Commission()
            {
                var order = new OrderBuilder().WithComissionId(Guid.NewGuid().ToString()).Build();
                var payment = new PaymentBuilder().WithOrderId(order.Id).Build();

                _fixture.DbContext.Payments.Add(payment);
                _fixture.DbContext.Orders.Add(order);
                await _fixture.DbContext.SaveChangesAsync();

                var result = await _paymentServiceMock.RemovePayment(payment.Id);

                result.IsSuccessful.Should().BeFalse();
                result.Messages.Should().Contain("Payment if from a order that already has a comission");
            }

            [Fact]
            public async Task RemovePayment_Should_Deactivate_Payment_And_Update_Order_Discount()
            {
                var payment = new PaymentBuilder().WithDiscount(50).Build();
                var order = new OrderBuilder().WithTotalDiscount(100).Build();
                order.Payments.Add(payment);
                _fixture.DbContext.Payments.Add(payment);
                _fixture.DbContext.Orders.Add(order);
                await _fixture.DbContext.SaveChangesAsync();

                var result = await _paymentServiceMock.RemovePayment(payment.Id);

                result.IsSuccessful.Should().BeTrue();
                var updatedPayment = await _fixture.DbContext.Payments.FirstOrDefaultAsync(p => p.Id == payment.Id);
                var updatedOrder = await _fixture.DbContext.Orders.FirstOrDefaultAsync(o => o.Id == order.Id);

                updatedPayment.Should().NotBeNull();
                updatedPayment.IsActive.Should().BeFalse();
                updatedPayment.CompletionDate.Should().NotBeNull();

                updatedOrder.Should().NotBeNull();
                updatedOrder.TotalDiscount.Should().Be(50);
            }

           
        }
    }

}
