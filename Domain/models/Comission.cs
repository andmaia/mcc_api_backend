using Domain.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.models
{
    public class Comission
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
        public Employee Employee { get; set; }
        public string CompanyId { get; set; }
        public Company Company { get; set; }

        public ICollection<Order> Orders { get; set; }
        public ICollection<Expense> Expenses { get; set; }
    }
}
