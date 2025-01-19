using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.requests.Comission
{
    public class ComissionFilterRequest
    {
        public string? Id { get; set; }
        public bool? IsActive { get; set; } = true;
        public bool? IsPaid { get; set; } = true;
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? CreatedDate { get; set; }

        public ComissionFilterRequest()
        {
            var currentDate = DateTime.Now;

            BeginDate ??= new DateTime(currentDate.Year, currentDate.Month, 1, 0, 0, 0);
            CreatedDate ??= BeginDate;

            EndDate ??= new DateTime(currentDate.Year, currentDate.Month, DateTime.DaysInMonth(currentDate.Year, currentDate.Month), 23, 59, 59);
        }

    }
}
