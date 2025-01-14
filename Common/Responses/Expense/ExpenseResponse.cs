using Domain.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Responses.Expense
{
    public class ExpenseResponse
    {
        public string Id { get; set; }
        public decimal Value { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public StatusPayment StatusPayment { get; set; }
        public bool IsActive { get; set; }
       
        public string EmployeeId { get; set; }
        
        public string ComissionId { get; set; }
        public string CompanyId { get; set; }
    }
}
