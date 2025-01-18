using Domain.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Responses.Expense
{
    public class ExpenseResponseToComission
    {
        public string Id { get; set; }
        public decimal Value { get; set; }
        public DateTime CreationDate { get; set; }
        public string Description { get; set; }
        public StatusPayment StatusPayment { get; set; }

    }
}
