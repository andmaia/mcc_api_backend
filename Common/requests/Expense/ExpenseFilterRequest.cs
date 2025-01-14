using Domain.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.requests.Expense
{
    public class ExpenseFilterRequest
    {
        public string? Id { get; set; }
        public bool? IsActive { get; set; } = true;
        public StatusPayment statusPayment { get; set; } = StatusPayment.PAID;
        public bool? IsComissionPaid { get; set; } = true;
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
