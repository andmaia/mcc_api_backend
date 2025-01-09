using Domain.enums;
using Domain.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Builders.branchs
{
    public class OrderBuilder
    {
        private readonly Order _order;

        public OrderBuilder()
        {
            _order = new Order
            {
                Id = Guid.NewGuid().ToString(),
                CustomerName = "Default Customer",
                IsActive = true,
                CreationDate = DateTime.Now,
                UpdatedDate = DateTime.MinValue,
                CompletionDate = DateTime.MinValue,
                PaymentOrderStatus = StatusPayment.PENDING,
                CommissionOrderStatus = StatusPayment.UNPAID,
                TotalValue = 100,
                CommissionPercentage = 10,
                TotalDiscount = 0,
                CompanyId = Guid.NewGuid().ToString(),
                branchId = Guid.NewGuid().ToString(),
                EmployeeId = Guid.NewGuid().ToString(),
                ComissionId = null,
                Payments = new List<Payment>()
            };
        }

        public OrderBuilder WithId(string id)
        {
            _order.Id = id;
            return this;
        }

        public OrderBuilder WithCustomerName(string customerName)
        {
            _order.CustomerName = customerName;
            return this;
        }

        public OrderBuilder WithIsActive(bool isActive)
        {
            _order.IsActive = isActive;
            return this;
        }

        public OrderBuilder WithCreationDate(DateTime creationDate)
        {
            _order.CreationDate = creationDate;
            return this;
        }

        public OrderBuilder WithUpdatedDate(DateTime? updatedDate)
        {
            _order.UpdatedDate = updatedDate;
            return this;
        }

        public OrderBuilder WithCompletionDate(DateTime? completionDate)
        {
            _order.CompletionDate = completionDate;
            return this;
        }

        public OrderBuilder WithPaymentOrderStatus(StatusPayment status)
        {
            _order.PaymentOrderStatus = status;
            return this;
        }

        public OrderBuilder WithCommissionOrderStatus(StatusPayment status)
        {
            _order.CommissionOrderStatus = status;
            return this;
        }

        public OrderBuilder WithTotalValue(decimal totalValue)
        {
            _order.TotalValue = totalValue;
            return this;
        }

        public OrderBuilder WithCommissionPercentage(decimal commissionPercentage)
        {
            _order.CommissionPercentage = commissionPercentage;
            return this;
        }

        public OrderBuilder WithTotalDiscount(decimal totalDiscount)
        {
            _order.TotalDiscount = totalDiscount;
            return this;
        }

        public OrderBuilder WithCompanyId(string companyId)
        {
            _order.CompanyId = companyId;
            return this;
        }

        public OrderBuilder WithBranchId(string branchId)
        {
            _order.branchId = branchId;
            return this;
        }

        public OrderBuilder WithEmployeeId(string employeeId)
        {
            _order.EmployeeId = employeeId;
            return this;
        }

        public OrderBuilder WithComissionId(string? comissionId)
        {
            _order.ComissionId = comissionId;
            return this;
        }

        

        public Order Build()
        {
            return _order;
        }
    }
}
