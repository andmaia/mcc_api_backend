using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.requests.Payment
{
    public class CreatePaymentRequest
    {
      
        public string PaymentFormId { get; set; }
        public string OrderId { get; set; }
        public decimal Tax { get; set; }
        public string Url { get; set; }
        public decimal Value { get; set; }
    }
}
