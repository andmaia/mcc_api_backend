using Common.requests.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Builders.orders
{
    public class CreateOrderRequestBuilder
    {
        private readonly CreateOrderRequest _orderRequest;

        public CreateOrderRequestBuilder()
        {
            _orderRequest = new CreateOrderRequest
            {
                CustomerName = "Default Customer",
                TotalValue = 1000m,
                CommissionPercentage = 10m,
                CompanyId = Guid.NewGuid().ToString(),
                branchId = Guid.NewGuid().ToString(),
                EmployeeId = Guid.NewGuid().ToString()
            };
        }

        public CreateOrderRequestBuilder WithCustomerName(string customerName)
        {
            _orderRequest.CustomerName = customerName;
            return this;
        }

        public CreateOrderRequestBuilder WithTotalValue(decimal totalValue)
        {
            _orderRequest.TotalValue = totalValue;
            return this;
        }

        public CreateOrderRequestBuilder WithCommissionPercentage(decimal commissionPercentage)
        {
            _orderRequest.CommissionPercentage = commissionPercentage;
            return this;
        }

        public CreateOrderRequestBuilder WithCompanyId(string companyId)
        {
            _orderRequest.CompanyId = companyId;
            return this;
        }

        public CreateOrderRequestBuilder WithFilial(string filial)
        {
            _orderRequest.branchId = filial;
            return this;
        }

        public CreateOrderRequestBuilder WithEmployeeId(string employeeId)
        {
            _orderRequest.EmployeeId = employeeId;
            return this;
        }

        public CreateOrderRequest Build()
        {
            return _orderRequest;
        }
    }
}
