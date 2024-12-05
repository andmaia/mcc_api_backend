using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Services
{
    using Common.requests.Order;
    using Domain.enums;
    using Domain.models;
    using global::Test.Configuration;
    using Infrastructure.services.order;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;
    using Xunit;

    namespace Test.Services
    {
        [Collection("IntegrationTests")]
        public class OrderServiceTest
        {
            private readonly IntegrationFixture _fixture;

            public OrderServiceTest(IntegrationFixture fixture)
            {
                _fixture = fixture;
            }

            [Fact]
            public async Task Should_Add_And_Retrieve_Order_Directly_From_DbContext()
            {
                // Arrange
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

                // Act
                await dbContext.Orders.AddAsync(newOrder); // Adiciona diretamente no banco
                await dbContext.SaveChangesAsync();        // Salva as mudanças no banco

                var retrievedOrder = await dbContext.Orders
                    .FirstOrDefaultAsync(o => o.Id == newOrder.Id); // Recupera diretamente do banco

                // Assert
                Assert.NotNull(retrievedOrder);
                Assert.Equal(newOrder.CustomerName, retrievedOrder?.CustomerName);
                Assert.Equal(newOrder.TotalValue, retrievedOrder?.TotalValue);
                Assert.Equal(newOrder.CommissionPercentage, retrievedOrder?.CommissionPercentage);
                Assert.Equal(newOrder.CompanyId, retrievedOrder?.CompanyId);
            }
        
        }
    }

}
