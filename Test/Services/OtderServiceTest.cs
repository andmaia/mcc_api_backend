using System;
using System.Threading.Tasks;
using Application.services.identity;
using Application.services.Order;
using Common.requests.Order;
using Common.Responses.order;
using Domain.enums;
using Domain.models;
using FluentAssertions;
using global::Test.Builders.branchs;
using global::Test.Builders.company;
using global::Test.Builders.EmployeeTest;
using global::Test.Builders.orders;
using global::Test.Configuration;
using Infrastructure.services.order;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Moq;
using Xunit;
using Infrastructure;
using Test.Builders.payments;

namespace Test.Services
{
    [Collection("IntegrationTests")]
    public class OrderServiceTest
    {
        private readonly IntegrationFixture _fixture;
        private readonly IOrderService _orderServiceMock;
        private readonly IMapper _mapperMock;

        public OrderServiceTest(IntegrationFixture fixture)
        {
            var mappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfiles());
            });

            _mapperMock = new Mapper(mappingConfig);
            _fixture = fixture;
            _orderServiceMock = new OrderService(_fixture.DbContext, _mapperMock);
        }

        [Fact]
        public async Task Should_Add_And_Retrieve_Order_Directly_From_DbContext()
        {
            var dbContext = _fixture.DbContext;

            var newOrder = new Order
            {
                Id = Guid.NewGuid().ToString(),
                CustomerName = "Jane Doe",
                IsActive = true,
                CreationDate = DateTime.UtcNow,
                TotalValue = 1500m,
                CommissionPercentage = 15m,
                CompanyId = "Company456",
                branchId = "Branch789",
                EmployeeId = "Employee123",
                ComissionId = "Commission456"
            };

            await dbContext.Orders.AddAsync(newOrder);
            await dbContext.SaveChangesAsync();

            var retrievedOrder = await dbContext.Orders
                .FirstOrDefaultAsync(o => o.Id == newOrder.Id);

            Assert.NotNull(retrievedOrder);
            Assert.Equal(newOrder.CustomerName, retrievedOrder?.CustomerName);
            Assert.Equal(newOrder.TotalValue, retrievedOrder?.TotalValue);
            Assert.Equal(newOrder.CommissionPercentage, retrievedOrder?.CommissionPercentage);
            Assert.Equal(newOrder.CompanyId, retrievedOrder?.CompanyId);
        }

        [Fact]
        public async Task CREATE_ORDER_SHOULD_FAIL_IF_COMPANY_NOT_EXISTS()
        {
            var createOrderRequest = new CreateOrderRequestBuilder().Build();

            var result = await _orderServiceMock.CreateOrderAsync(createOrderRequest);

            result.IsSuccessful.Should().BeFalse();
            result.Messages.Should().Contain("Employee does not belong this company.");
        }

        [Fact]
        public async Task CREATE_ORDER_SHOULD_CREATE_USER_WITH_COMMISSION_AS_NOT_PAID_AND_PAYMENTSTATUS()
        {
            var company = new CompanyBuilder().Build();
            var employee = new EmployeeBuilder().WithCompanyId(company.Id).Build();
            var branch = new BranchBuilder().WithCompanyId(company.Id).Build();
            var createOrderRequest = new CreateOrderRequestBuilder()
                .WithCompanyId(company.Id)
                .WithEmployeeId(employee.Id)
                .Build();

            _fixture.DbContext.Companies.Add(company);
            _fixture.DbContext.Employees.Add(employee);
            _fixture.DbContext.Branchs.Add(branch);
            await _fixture.DbContext.SaveChangesAsync();

            var result = await _orderServiceMock.CreateOrderAsync(createOrderRequest);

            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public async Task Update_ORDER_SHOULD_FAIL_IF_branch_NOT_EXISTS()
        {
            var order = new OrderBuilder().Build();
            var employee = new EmployeeBuilder().Build();
            var branch = new BranchBuilder().Build();

            var updateRequest = new UpdateOrderRequest
            {
                Id = order.Id,
                branchId = branch.Id,
                CommissionPercentage = 100,
                TotalValue = 100,
                EmployeeId = employee.Id,
            };

            _fixture.DbContext.Orders.Add(order);
            _fixture.DbContext.Employees.Add(employee);
            await _fixture.DbContext.SaveChangesAsync();

            var result = await _orderServiceMock.UpdateOrderAsync(updateRequest);

            result.IsSuccessful.Should().BeFalse();
            result.Messages.Should().Contain("Branch does not exist.");
        }

        [Fact]
        public async Task Update_ORDER_SHOULD_FAIL_IF_Branch_NOT_EXISTS()
        {
            var order = new OrderBuilder().Build();
            var employee = new EmployeeBuilder().Build();

            var updateRequest = new UpdateOrderRequest
            {
                Id = order.Id,
                branchId = Guid.NewGuid().ToString(),
                CommissionPercentage = 100,
                TotalValue = 100,
                EmployeeId = employee.Id,
            };

            _fixture.DbContext.Orders.Add(order);
            _fixture.DbContext.Employees.Add(employee);
            await _fixture.DbContext.SaveChangesAsync();

            var result = await _orderServiceMock.UpdateOrderAsync(updateRequest);

            result.IsSuccessful.Should().BeFalse();
            result.Messages.Should().Contain("Branch does not exist.");
        }

        [Fact]
        public async Task Update_ORDER_SHOULD_FAIL_IF_Employee_NOT_EXISTS()
        {
            var order = new OrderBuilder().Build();
            var branch = new BranchBuilder().Build();

            var updateRequest = new UpdateOrderRequest
            {
                Id = order.Id,
                branchId = branch.Id,
                CommissionPercentage = 100,
                TotalValue = 100,
                EmployeeId = Guid.NewGuid().ToString(),
            };

            _fixture.DbContext.Branchs.Add(branch);
            _fixture.DbContext.Orders.Add(order);
            await _fixture.DbContext.SaveChangesAsync();

            var result = await _orderServiceMock.UpdateOrderAsync(updateRequest);

            result.IsSuccessful.Should().BeFalse();
            result.Messages.Should().Contain("Employee does not exist.");
        }

        [Fact]
        public async Task Update_ORDER_SHOULD_UPDATE_ONLY_TotalValue()
        {
            var order = new OrderBuilder().Build();
            _fixture.DbContext.Orders.Add(order);
            await _fixture.DbContext.SaveChangesAsync();

            var updateRequest = new UpdateOrderRequest
            {
                Id = order.Id,
                TotalValue = 500,
            };

            var result = await _orderServiceMock.UpdateOrderAsync(updateRequest);

            result.IsSuccessful.Should().BeTrue();
            var updatedOrder = _fixture.DbContext.Orders.First(o => o.Id == order.Id);
            updatedOrder.TotalValue.Should().Be(500);
            updatedOrder.CommissionPercentage.Should().Be(order.CommissionPercentage);
            updatedOrder.EmployeeId.Should().Be(order.EmployeeId);
        }

        [Fact]
        public async Task Update_ORDER_SHOULD_UPDATE_All_Provided_Fields()
        {
            var employee = new EmployeeBuilder().Build();
            var branch = new BranchBuilder().Build();
            var order = new OrderBuilder().Build();

            _fixture.DbContext.Employees.Add(employee);
            _fixture.DbContext.Branchs.Add(branch);
            _fixture.DbContext.Orders.Add(order);
            await _fixture.DbContext.SaveChangesAsync();

            var updateRequest = new UpdateOrderRequest
            {
                Id = order.Id,
                branchId = branch.Id,
                EmployeeId = employee.Id,
                CommissionPercentage = 200,
                TotalValue = 1000,
            };

            var result = await _orderServiceMock.UpdateOrderAsync(updateRequest);

            result.IsSuccessful.Should().BeTrue();
            var updatedOrder = _fixture.DbContext.Orders.First(o => o.Id == order.Id);
            updatedOrder.EmployeeId.Should().Be(employee.Id);
            updatedOrder.CommissionPercentage.Should().Be(200);
            updatedOrder.TotalValue.Should().Be(1000);
        }

        [Fact]
        public async Task Update_ORDER_SHOULD_FAIL_IF_Order_NOT_EXISTS()
        {
            var updateRequest = new UpdateOrderRequest
            {
                Id = Guid.NewGuid().ToString(),
                TotalValue = 500,
            };

            var result = await _orderServiceMock.UpdateOrderAsync(updateRequest);

            result.IsSuccessful.Should().BeFalse();
            result.Messages.Should().Contain("Order does not exist.");
        }

        [Fact]
        public async Task UPDATE_ORDER_SHOULD_PARTIALLY_UPDATE_FIELDS()
        {
            // Arrange
            var order = new OrderBuilder().Build();
            _fixture.DbContext.Orders.Add(order);
            await _fixture.DbContext.SaveChangesAsync();

            var updateRequest = new UpdateOrderRequest
            {
                Id = order.Id,
                TotalValue = 800
            };

            // Act
            var result = await _orderServiceMock.UpdateOrderAsync(updateRequest);

            // Assert
            result.IsSuccessful.Should().BeTrue();
            var updatedOrder = await _fixture.DbContext.Orders.FindAsync(order.Id);
            updatedOrder.Should().NotBeNull();
            updatedOrder.TotalValue.Should().Be(800);
            updatedOrder.CommissionPercentage.Should().Be(order.CommissionPercentage);
        }

        [Fact]
        public async Task DISABLE_ORDER_SHOULD_FAIL_IF_ORDER_DOES_NOT_EXIST()
        {
            // Arrange
            var nonExistentOrderId = Guid.NewGuid().ToString();

            // Act
            var result = await _orderServiceMock.DisableOrder(nonExistentOrderId);

            // Assert
            result.IsSuccessful.Should().BeFalse();
            result.Messages.Should().Contain("Order does not exist.");
        }

        [Fact]
        public async Task DISABLE_ORDER_SHOULD_FAIL_IF_COMMISSION_ASSOCIATED()
        {
            // Arrange
            var order = new OrderBuilder().WithComissionId(Guid.NewGuid().ToString()).Build();
            _fixture.DbContext.Orders.Add(order);
            await _fixture.DbContext.SaveChangesAsync();

            // Act
            var result = await _orderServiceMock.DisableOrder(order.Id);

            // Assert
            result.IsSuccessful.Should().BeFalse();
            result.Messages.Should().Contain("Order has associations with a commission and cannot be disabled.");
        }

        [Fact]
        public async Task DISABLE_ORDER_SHOULD_SET_ORDER_AND_PAYMENTS_AS_INACTIVE()
        {
            // Arrange
            var order = new OrderBuilder().Build();
            var payments = new[]
            {
                new PaymentBuilder().WithOrderId(order.Id).WithIsActive(true).Build(),
                new PaymentBuilder().WithOrderId(order.Id).WithIsActive(true).Build(),
                new PaymentBuilder().WithOrderId(order.Id).WithIsActive(true).Build()
            };

            _fixture.DbContext.Orders.Add(order);
            _fixture.DbContext.Payments.AddRange(payments);
            await _fixture.DbContext.SaveChangesAsync();

            // Act
            var result = await _orderServiceMock.DisableOrder(order.Id);

            // Assert
            result.IsSuccessful.Should().BeTrue();
            var updatedOrder = await _fixture.DbContext.Orders.FindAsync(order.Id);
            updatedOrder.Should().NotBeNull();
            updatedOrder.IsActive.Should().BeFalse();

            var updatedPayments = await _fixture.DbContext.Payments.Where(p => p.OrderId == order.Id).ToListAsync();
            updatedPayments.Should().OnlyContain(p => !p.IsActive);
        }

        [Fact]
        public async Task PAY_ORDER_SHOULD_FAIL_IF_AMOUNT_FROM_PAYMENTS_NOT_EQUAL_TOTAL_VALUE()
        {
            Decimal valueToTest = 600;
            // Arrange
            var order = new OrderBuilder().WithPaymentOrderStatus(StatusPayment.UNPAID).WithTotalValue(valueToTest).Build();
            var payments = new[]
            {
                new PaymentBuilder().WithOrderId(order.Id).WithIsActive(true).WithAmount(100).Build(),
                new PaymentBuilder().WithOrderId(order.Id).WithIsActive(true).WithAmount(100).Build(),
                new PaymentBuilder().WithOrderId(order.Id).WithIsActive(true).WithAmount(300).Build()
            };

            _fixture.DbContext.Orders.Add(order);
            _fixture.DbContext.Payments.AddRange(payments);
            await _fixture.DbContext.SaveChangesAsync();

            // Act
            var result = await _orderServiceMock.PayOrderAsync(order.Id);

            // Assert
            result.IsSuccessful.Should().BeFalse();
            result.Messages.Should().Contain("Total payments do not match the order's total value.");


        }

        [Fact]
        public async Task SearchOrdersByEmployee_Should_Filter_By_EmployeeId()
        {
            // Arrange
            var employee = new EmployeeBuilder().Build();
            var order1 = new OrderBuilder().WithEmployeeId(employee.Id).Build();
            var order2 = new OrderBuilder().WithEmployeeId(employee.Id).Build();
            var order3 = new OrderBuilder().Build();

            _fixture.DbContext.Employees.Add(employee);
            _fixture.DbContext.Orders.AddRange(order1, order2, order3);
            await _fixture.DbContext.SaveChangesAsync();

            var orderSearchFilter = new OrderSearchFilter
            {
                Id = employee.Id,
                BeginDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1),
                EndDate = DateTime.Now
            };

            // Act
            var result = await _orderServiceMock.SearchOrdersByEmployee(orderSearchFilter);

            // Assert
            result.ResponseData.Should().HaveCount(2);
            result.ResponseData.All(o => o.EmployeeId == employee.Id).Should().BeTrue();
        }

        [Fact]
        public async Task SearchOrdersByBranch_Should_Filter_By_BranchId()
        {
            // Arrange
            var branch = new BranchBuilder().Build();
            var order1 = new OrderBuilder().WithBranchId(branch.Id).Build();
            var order2 = new OrderBuilder().WithBranchId(branch.Id).Build();
            var order3 = new OrderBuilder().Build();

            _fixture.DbContext.Branchs.Add(branch);
            _fixture.DbContext.Orders.AddRange(order1, order2, order3);
            await _fixture.DbContext.SaveChangesAsync();

            var orderSearchFilter = new OrderSearchFilter
            {
                Id = branch.Id,
                BeginDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1),
                EndDate = DateTime.Now
            };

            // Act
            var result = await _orderServiceMock.SearchOrdersByBranch(orderSearchFilter);

            // Assert
            result.ResponseData.Should().HaveCount(2);
            result.ResponseData.All(o => o.branchId == branch.Id).Should().BeTrue();
        }

        [Fact]
        public async Task SearchOrdersByCompany_Should_Filter_By_CompanyId()
        {
            // Arrange
            var company = new CompanyBuilder().Build();
            var order1 = new OrderBuilder().WithCompanyId(company.Id).Build();
            var order2 = new OrderBuilder().WithCompanyId(company.Id).Build();
            var order3 = new OrderBuilder().Build();

            _fixture.DbContext.Companies.Add(company);
            _fixture.DbContext.Orders.AddRange(order1, order2, order3);
            await _fixture.DbContext.SaveChangesAsync();

            var orderSearchFilter = new OrderSearchFilter
            {
                Id = company.Id,
                BeginDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1),
                EndDate = DateTime.Now
            };

            // Act
            var result = await _orderServiceMock.SearchOrdersByCompany(orderSearchFilter);

            // Assert
            result.ResponseData.Should().HaveCount(2);
            result.ResponseData.All(o => o.CompanyId == company.Id).Should().BeTrue();
        }


    }

}

