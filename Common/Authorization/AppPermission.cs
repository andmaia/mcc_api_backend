using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Authorization
{


    public record AppPermission(string Feature, string Action, string Group, string Description, string[] Roles)
    {
        public string Name => NameFor(Feature, Action);

        public static string NameFor(string feature, string action)
        {
            return $"Permissions.{feature}.{action}";
        }
    }

    public class AppPermissions
    {
        private static readonly AppPermission[] _all = new AppPermission[]
        {
            new(AppFeature.Users, AppAction.Create, AppRoleGroup.AdminManagement, "Create Users", new[] { AppRole.Admin }),
            new(AppFeature.Users, AppAction.Update, AppRoleGroup.AdminManagement, "Update Users", new[] { AppRole.Admin }),
            new(AppFeature.Users, AppAction.Read, AppRoleGroup.AdminManagement, "Read Users", new[] { AppRole.Admin, AppRole.Manager }),
            new(AppFeature.Users, AppAction.Delete, AppRoleGroup.AdminManagement, "Delete Users", new[] { AppRole.Admin }),
            new(AppFeature.Employees, AppAction.Create, AppRoleGroup.EmployeeAndCommandManagement, "Create Employees", new[] { AppRole.Admin }),
            new(AppFeature.Employees, AppAction.Update, AppRoleGroup.EmployeeAndCommandManagement, "Update Employees", new[] { AppRole.Admin }),
            new(AppFeature.Employees, AppAction.Read, AppRoleGroup.EmployeeAndCommandManagement, "Read Employees", new[] { AppRole.Admin, AppRole.Manager }),
            new(AppFeature.Employees, AppAction.Delete, AppRoleGroup.EmployeeAndCommandManagement, "Delete Employees", new[] { AppRole.Admin }),
            new(AppFeature.Commands, AppAction.Create, AppRoleGroup.EmployeeAndCommandManagement, "Create Commands", new[] { AppRole.Admin }),
            new(AppFeature.Commands, AppAction.Update, AppRoleGroup.EmployeeAndCommandManagement, "Update Commands", new[] { AppRole.Admin }),
            new(AppFeature.Commands, AppAction.Read, AppRoleGroup.EmployeeAndCommandManagement, "Read Commands", new[] { AppRole.Admin, AppRole.Manager, AppRole.Basic }),
            new(AppFeature.Commands, AppAction.Delete, AppRoleGroup.EmployeeAndCommandManagement, "Delete Commands", new[] { AppRole.Admin }),
            new(AppFeature.PaymentMethods, AppAction.Create, AppRoleGroup.FinancialManagement, "Create Payment Methods", new[] { AppRole.Admin }),
            new(AppFeature.PaymentMethods, AppAction.Update, AppRoleGroup.FinancialManagement, "Update Payment Methods", new[] { AppRole.Admin }),
            new(AppFeature.PaymentMethods, AppAction.Read, AppRoleGroup.FinancialManagement, "Read Payment Methods", new[] { AppRole.Admin, AppRole.Manager, AppRole.Basic }),
            new(AppFeature.PaymentMethods, AppAction.Delete, AppRoleGroup.FinancialManagement, "Delete Payment Methods", new[] { AppRole.Admin }),

            new(AppFeature.Commissions, AppAction.Read, AppRoleGroup.FinancialManagement, "Read Commissions", new[] { AppRole.Admin, AppRole.Manager, AppRole.Basic }),
            new(AppFeature.Commissions, AppAction.Update, AppRoleGroup.FinancialManagement, "Update Commissions", new[] { AppRole.Admin }),

            new(AppFeature.Statistics, AppAction.Read, AppRoleGroup.Reporting, "Read Statistics", new[] { AppRole.Admin, AppRole.Manager, AppRole.Basic })
        };

        public static IReadOnlyList<AppPermission> AdminPermissions { get; } =
            new ReadOnlyCollection<AppPermission>(_all.Where(p => p.Roles.Contains(AppRole.Admin)).ToArray());

        public static IReadOnlyList<AppPermission> ManagerPermissions { get; } =
            new ReadOnlyCollection<AppPermission>(_all.Where(p => p.Roles.Contains(AppRole.Manager)).ToArray());

        public static IReadOnlyList<AppPermission> BasicPermissions { get; } =
            new ReadOnlyCollection<AppPermission>(_all.Where(p => p.Roles.Contains(AppRole.Basic)).ToArray());

        public static IReadOnlyList<AppPermission> AllPermissions { get; } =
            new ReadOnlyCollection<AppPermission>(_all);
    }
}
