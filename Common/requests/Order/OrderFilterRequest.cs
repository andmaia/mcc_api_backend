using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.requests.Order
{
    public class OrderSearchFilter
    {
        public string? Id { get; set; }
        public bool? IsActive { get; set; } = true;
        public bool? IsPaid { get; set; } = true;
        public bool? IsComissionPaid { get; set; } = true;
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }

    }

}
