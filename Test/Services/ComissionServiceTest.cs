using Application.services.Comission;
using Application.services.Order;
using AutoMapper;
using FluentAssertions;
using Infrastructure;
using Infrastructure.services.Comission;
using Infrastructure.services.order;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using Test.Builders.branchs;
using Test.Builders.Comission;
using Test.Builders.company;
using Test.Builders.EmployeeTest;
using Test.Builders.Expense;
using Test.Builders.orders;
using Test.Builders.paymentForm;
using Test.Builders.payments;
using Test.Configuration;

namespace Test.Services
{
    [Collection("IntegrationTests")]

    public class ComissionServiceTest
    {
        private readonly IntegrationFixture _fixture;
        private readonly IComissionService _comissionServiceMock;
        private readonly IMapper _mapperMock;

        public ComissionServiceTest(IntegrationFixture fixture)
        {
            var mappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfiles());
            });

            _mapperMock = new Mapper(mappingConfig);
            _fixture = fixture;
            _comissionServiceMock = new ComissionService(_fixture.DbContext, _mapperMock);
        }

        [Fact]
        public async Task CREATE_COMISSION_SHOULD_FAIL_IF_EMPLOYEE_NOT_EXISTS()
        {
            var createComissionRequest = new CreateComissionRequestBuilder().Build();

            var result = await _comissionServiceMock.CreateComissionAsync(createComissionRequest);

            result.IsSuccessful.Should().BeFalse();
            result.Messages.Should().Contain("Employee not found");
        }

        [Fact]
        public async Task CREATE_COMISSION_SHOULD_FAIL_IF_SOME_ORDER_NOT_EXISTS()
        {
            var companyMock = new CompanyBuilder().Build();
            var paymentFormMock = new PaymentFormBuilder().WithCompanyId(companyMock.Id).Build();
            var employeeMock = new EmployeeBuilder().WithComissionPercentage(50).WithCompanyId(companyMock.Id).Build();
            var orderMock1 = new OrderBuilder()
                .WithEmployeeId(employeeMock.Id)
                .WithPaymentOrderStatus(Domain.enums.StatusPayment.PAID)
                .WithCommissionPercentage(50)
                .WithTotalValue(100)
                .Build();
            var paymentMock1 = new PaymentBuilder().WithOrderId(orderMock1.Id)
                .WithPaymentFormId(paymentFormMock.Id).WithAmount(100).Build();
            var idOrderNotFound = Guid.NewGuid().ToString();
            var createComissionRequestMock = new CreateComissionRequestBuilder()
                .WithEmployeeId(employeeMock.Id)
                .WithApplyDiscount(false)
                .WithPercentage(50)
                .WithOrders(new List<string> { orderMock1.Id, idOrderNotFound })
            .Build();

            await _fixture.DbContext.Orders.AddAsync(orderMock1);
            await _fixture.DbContext.PaymentForms.AddAsync(paymentFormMock);
            await _fixture.DbContext.Employees.AddAsync(employeeMock);
            await _fixture.DbContext.Companies.AddAsync(companyMock);
            await _fixture.DbContext.Payments.AddAsync(paymentMock1);

            await _fixture.DbContext.SaveChangesAsync();


            var result = await _comissionServiceMock.CreateComissionAsync(createComissionRequestMock);

            result.IsSuccessful.Should().BeFalse();
            result.Messages.Should().Contain($"Invalid Orders: {idOrderNotFound}.");
        }

        [Fact]
        public async Task CREATE_COMISSION_SHOULD_FAIL_IF_SOME_Expense_NOT_EXISTS()
        {
            var companyMock = new CompanyBuilder().Build();
            var paymentFormMock = new PaymentFormBuilder().WithCompanyId(companyMock.Id).Build();
            var employeeMock = new EmployeeBuilder().WithComissionPercentage(50).WithCompanyId(companyMock.Id).Build();
            var orderMock1 = new OrderBuilder()
                .WithEmployeeId(employeeMock.Id)
                .WithPaymentOrderStatus(Domain.enums.StatusPayment.PAID)
                .WithCommissionPercentage(50)
                .WithTotalValue(100)
                .Build();
            var paymentMock1 = new PaymentBuilder().WithOrderId(orderMock1.Id)
                .WithPaymentFormId(paymentFormMock.Id).WithAmount(100).Build();
            var expenseMock1 = new ExpenseBuilder().WithEmployeeId(employeeMock.Id).WithCompanyId(companyMock.Id).WithStatusPayment(Domain.enums.StatusPayment.PAID).Build();
            var idExoenseNotFound = Guid.NewGuid().ToString();
            var createComissionRequestMock = new CreateComissionRequestBuilder()
                .WithEmployeeId(employeeMock.Id)
                .WithApplyDiscount(true)
                .WithPercentage(50)
                .WithOrders(new List<string> { orderMock1.Id })
                .WithExpenses(new List<string> { expenseMock1.Id, idExoenseNotFound })
            .Build();


            await _fixture.DbContext.Orders.AddAsync(orderMock1);
            await _fixture.DbContext.Expenses.AddAsync(expenseMock1);
            await _fixture.DbContext.PaymentForms.AddAsync(paymentFormMock);
            await _fixture.DbContext.Employees.AddAsync(employeeMock);
            await _fixture.DbContext.Companies.AddAsync(companyMock);
            await _fixture.DbContext.Payments.AddAsync(paymentMock1);

            await _fixture.DbContext.SaveChangesAsync();

            var result = await _comissionServiceMock.CreateComissionAsync(createComissionRequestMock);

            result.IsSuccessful.Should().BeFalse();
            result.Messages.Should().Contain($"Invalid Expenses: {idExoenseNotFound}.");
        }

        [Fact]
        public async Task CREATE_COMISSION_SHOULD_HAVE_ORDERS_AND_EXPENSES_WITH_COMISSIONID_IF_SUCESSUFUL()
        {
            var companyMock = new CompanyBuilder().Build();
            var paymentFormMock = new PaymentFormBuilder().WithCompanyId(companyMock.Id).Build();
            var employeeMock = new EmployeeBuilder().WithComissionPercentage(50).WithCompanyId(companyMock.Id).Build();
            var orderMock1 = new OrderBuilder()
                .WithEmployeeId(employeeMock.Id)
                .WithPaymentOrderStatus(Domain.enums.StatusPayment.PAID)
                .WithCommissionPercentage(50)
                .WithTotalValue(100)
                .Build();
            var paymentMock1 = new PaymentBuilder().WithOrderId(orderMock1.Id)
                .WithPaymentFormId(paymentFormMock.Id).WithAmount(100).Build();
            var expenseMock1 = new ExpenseBuilder().WithEmployeeId(employeeMock.Id).WithCompanyId(companyMock.Id).WithStatusPayment(Domain.enums.StatusPayment.PAID).Build();
            var idExoenseNotFound = Guid.NewGuid().ToString();
            var createComissionRequestMock = new CreateComissionRequestBuilder()
                .WithEmployeeId(employeeMock.Id)
                .WithApplyDiscount(true)
                .WithPercentage(50)
                .WithOrders(new List<string> { orderMock1.Id })
                .WithExpenses(new List<string> { expenseMock1.Id })
            .Build();


            await _fixture.DbContext.Orders.AddAsync(orderMock1);
            await _fixture.DbContext.Expenses.AddAsync(expenseMock1);
            await _fixture.DbContext.PaymentForms.AddAsync(paymentFormMock);
            await _fixture.DbContext.Employees.AddAsync(employeeMock);
            await _fixture.DbContext.Companies.AddAsync(companyMock);
            await _fixture.DbContext.Payments.AddAsync(paymentMock1);

            await _fixture.DbContext.SaveChangesAsync();

            var result = await _comissionServiceMock.CreateComissionAsync(createComissionRequestMock);

            result.IsSuccessful.Should().BeTrue();
            var orderToTest = await _fixture.DbContext.Orders.FirstOrDefaultAsync(o => o.Id == orderMock1.Id && o.ComissionId == result.ResponseData.Id);
            var expenseToTest = await _fixture.DbContext.Expenses.FirstOrDefaultAsync(o => o.Id == expenseMock1.Id && o.ComissionId == result.ResponseData.Id);

            orderToTest.Should().NotBeNull();
            expenseToTest.Should().NotBeNull();
        }

        [Fact]
        public async Task CREATE_COMISSION_SHOULD_CALCULATE_CORRECT_COMMISSION_AMOUNT()
        {
            // Arrange
            var companyMock = new CompanyBuilder().Build();
            var paymentFormMock = new PaymentFormBuilder().WithTax(5).WithCompanyId(companyMock.Id).Build();
            var employeeMock = new EmployeeBuilder().WithComissionPercentage(50).WithCompanyId(companyMock.Id).Build();
            var orderMock1 = new OrderBuilder()
                .WithEmployeeId(employeeMock.Id)
                .WithPaymentOrderStatus(Domain.enums.StatusPayment.PAID)
                .WithCommissionPercentage(50)
                .WithTotalValue(200)
                .WithTotalDiscount(10)
                .Build();
            var expenseMock1 = new ExpenseBuilder()
                .WithEmployeeId(employeeMock.Id)
                .WithCompanyId(companyMock.Id)
                .WithStatusPayment(Domain.enums.StatusPayment.PAID)
                .WithValue(50)
                .Build();
            var paymentMock1 = new PaymentBuilder().WithOrderId(orderMock1.Id)
                .WithPaymentFormId(paymentFormMock.Id).WithAmount(200).WithDiscount(10).Build();

            var createComissionRequestMock = new CreateComissionRequestBuilder()
                .WithEmployeeId(employeeMock.Id)
                .WithApplyDiscount(true)
                .WithPercentage(50)
                .WithOrders(new List<string> { orderMock1.Id })
                .WithExpenses(new List<string> { expenseMock1.Id })
                .Build();

            await _fixture.DbContext.Orders.AddAsync(orderMock1);
            await _fixture.DbContext.Expenses.AddAsync(expenseMock1);
            await _fixture.DbContext.PaymentForms.AddAsync(paymentFormMock);
            await _fixture.DbContext.Payments.AddAsync(paymentMock1);
            await _fixture.DbContext.Employees.AddAsync(employeeMock);
            await _fixture.DbContext.Companies.AddAsync(companyMock);

            await _fixture.DbContext.SaveChangesAsync();

            // Act
            var result = await _comissionServiceMock.CreateComissionAsync(createComissionRequestMock);

            // Assert
            result.IsSuccessful.Should().BeTrue();
            Decimal totalComission = ((orderMock1.TotalValue - orderMock1.TotalDiscount) * (orderMock1.CommissionPercentage / 100)) - expenseMock1.Value;
            result.ResponseData.TotalCommission.Should().Be(totalComission);
        }

        [Fact]
        public async Task CREATE_COMISSION_SHOULD_CALCULATE_CORRECT_COMMISSION_AMOUNT_WITHOUT_EXPENSE()
        {
            // Arrange
            var companyMock = new CompanyBuilder().Build();
            var paymentFormMock = new PaymentFormBuilder().WithTax(5).WithCompanyId(companyMock.Id).Build();
            var employeeMock = new EmployeeBuilder().WithComissionPercentage(50).WithCompanyId(companyMock.Id).Build();
            var orderMock1 = new OrderBuilder()
                .WithEmployeeId(employeeMock.Id)
                .WithPaymentOrderStatus(Domain.enums.StatusPayment.PAID)
                .WithCommissionPercentage(50)
                .WithTotalValue(200)
                .WithTotalDiscount(10)
                .Build();
           
            var paymentMock1 = new PaymentBuilder().WithOrderId(orderMock1.Id)
                .WithPaymentFormId(paymentFormMock.Id).WithAmount(200).WithDiscount(10).Build();

            var createComissionRequestMock = new CreateComissionRequestBuilder()
                .WithEmployeeId(employeeMock.Id)
                .WithApplyDiscount(true)
                .WithPercentage(50)
                .WithOrders(new List<string> { orderMock1.Id })
                .WithExpenses(new List<string> { })
                .Build();

            await _fixture.DbContext.Orders.AddAsync(orderMock1);
            await _fixture.DbContext.PaymentForms.AddAsync(paymentFormMock);
            await _fixture.DbContext.Payments.AddAsync(paymentMock1);
            await _fixture.DbContext.Employees.AddAsync(employeeMock);
            await _fixture.DbContext.Companies.AddAsync(companyMock);

            await _fixture.DbContext.SaveChangesAsync();

            // Act
            var result = await _comissionServiceMock.CreateComissionAsync(createComissionRequestMock);

            // Assert
            result.IsSuccessful.Should().BeTrue();
            Decimal totalComission = ((orderMock1.TotalValue - orderMock1.TotalDiscount) * (orderMock1.CommissionPercentage / 100));
            result.ResponseData.TotalCommission.Should().Be(totalComission);
        }

    }
}
