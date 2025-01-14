using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.requests.Expense
{
    public class UpdateExpenseRequest
    {
        public string Id { get; set; }
        public decimal Value { get; set; }

        public string Description { get; set; }
        public string Url { get; set; }

        public string EmployeeId { get; set; }

    }
}
