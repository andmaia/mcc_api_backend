using Common.requests.Comission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Builders.Comission
{
    public class CreateComissionRequestBuilder
    {
        private CreateComissionRequest _request;

        public CreateComissionRequestBuilder()
        {
            _request = new CreateComissionRequest
            {
                CommissionStartDate = DateTime.Now.AddDays(-1),
                CommissionEndDate = DateTime.Now.AddDays(1),
                ApplyDiscount = false,
                Percentage = 10.0m,
                Url = "",
                EmployeeId = Guid.NewGuid().ToString(),
                Orders = new List<string>(),
                Expenses = new List<string>()
            };
        }

        public CreateComissionRequestBuilder WithCommissionStartDate(DateTime startDate)
        {
            _request.CommissionStartDate = startDate;
            return this;
        }

        public CreateComissionRequestBuilder WithCommissionEndDate(DateTime endDate)
        {
            _request.CommissionEndDate = endDate;
            return this;
        }

        public CreateComissionRequestBuilder WithApplyDiscount(bool applyDiscount)
        {
            _request.ApplyDiscount = applyDiscount;
            return this;
        }

        public CreateComissionRequestBuilder WithPercentage(decimal percentage)
        {
            _request.Percentage = percentage;
            return this;
        }

        public CreateComissionRequestBuilder WithUrl(string url)
        {
            _request.Url = url;
            return this;
        }

        public CreateComissionRequestBuilder WithEmployeeId(string employeeId)
        {
            _request.EmployeeId = employeeId;
            return this;
        }

        public CreateComissionRequestBuilder WithOrders(ICollection<string> orders)
        {
            _request.Orders = orders;
            return this;
        }

        public CreateComissionRequestBuilder WithExpenses(ICollection<string> expenses)
        {
            _request.Expenses = expenses;
            return this;
        }

        public CreateComissionRequest Build()
        {
            return _request;
        }
    }
}
