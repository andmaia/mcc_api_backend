using Common.Responses.Expense;
using Common.Responses.order;
using Domain.enums;
using Domain.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Responses.Comission
{
    public class ResponseComiision
    {
        public string Id { get; set; }
        public DateTime CommissionStartDate { get; set; }
        public DateTime CommissionEndDate { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime? FinishedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsActive { get; set; }
        public StatusPayment PaymentStatus { get; set; }
        public bool ApplyDiscount { get; set; }
        public decimal TotalValue { get; set; }
        public decimal TotalFees { get; set; }
        public decimal TotalDiscounts { get; set; }
        public decimal Percentage { get; set; }
        public decimal TotalCommission { get; set; }
        public string Url { get; set; }
        public string EmployeeId { get; set; }
        public string CompanyId { get; set; }

        public ICollection<ResponseOrder> Orders { get; set; }
        public ICollection<ExpenseResponse> Expenses { get; set; }
    }
}
