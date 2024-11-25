using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Responses.PaymentForm
{
    public class PaymentFormResponse
    {
        public string Id { get; set; }
        public decimal Tax { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public bool IsActive { get; set; }
        public string CompanyId { get; set; }
    }
}
