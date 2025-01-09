using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Responses.Paymenet
{
    public class ResponsePayment
    {
        public string Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public bool IsActive { get; set; }
        public string PaymentFormId { get; set; }
        public string OrderId { get; set; }
        public decimal Value { get; set; }
        public decimal Tax { get; set; }
        public string Url { get; set; }
        public decimal Discount { get; set; }
        public decimal Amount { get; set; }
    }
}
