using Common.Responses.PaymentForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Responses.Paymenet
{
    public class ResponsePaymentToComission
    {
      
            public string Id { get; set; }
            public DateTime CreationDate { get; set; }
            public DateTime? PaymentDate { get; set; }
            public string PaymentFormName { get; set; }
            public decimal Value { get; set; }
            public decimal Tax { get; set; }
            public decimal Discount { get; set; }
            public decimal Amount { get; set; }
        
    }
}
