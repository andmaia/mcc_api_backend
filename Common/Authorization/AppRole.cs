using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Authorization
{
    public static class AppRole
    {
        public const string Admin = nameof(Admin);
        public const string Basic = nameof(Basic);
        public const string Manager = nameof(Manager);
        public static IReadOnlyList<string> DefaultRoles { get; }
            = new ReadOnlyCollection<string>(new[]
            {
                Admin,
                Manager,
                Basic
            });
        public static bool IsDefault(string roleName)
            => DefaultRoles.Any(r => r == roleName);
    }

}
