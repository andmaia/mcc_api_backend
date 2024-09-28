using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Authorization
{
    public static class AppRoleGroup
    {
        public const string AdminManagement = nameof(AdminManagement);
        public const string EmployeeAndCommandManagement = nameof(EmployeeAndCommandManagement);
        public const string FinancialManagement = nameof(FinancialManagement);
        public const string Reporting = nameof(Reporting);
    }
}
