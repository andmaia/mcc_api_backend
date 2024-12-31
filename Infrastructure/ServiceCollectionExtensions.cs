using Application.services.Branch;
using Application.services.Company;
using Application.services.Employee;
using Application.services.identity;
using Application.services.Order;
using Application.services.PaymentForm;
using Infrastructure.Context;
using Infrastructure.services.branch;
using Infrastructure.services.company;
using Infrastructure.services.employee;
using Infrastructure.services.identity;
using Infrastructure.services.order;
using Infrastructure.services.paymentForm;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseMySql(configuration.GetConnectionString("DefaultConnection"),
                ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection")),
                mysqlOptions =>
                {
                    mysqlOptions.SchemaBehavior(MySqlSchemaBehavior.Ignore);
                })).AddTransient<SeederDbContext>();
            return services;
        }

        public static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            services
                .AddTransient<ITokenService, TokenService>()
                .AddTransient<IUserService, UserService>()
                .AddTransient<ICompanyService,CompanyService>()
                .AddTransient<IEmployeService,EmployeeService>()
                .AddTransient<IPaymentFormService,PaymentFormService>()
                .AddTransient<IOrderService, OrderService>()
                .AddTransient<IBranchService, BranchService>()

                .AddAutoMapper(assembly);

            return services;
        }



    }
}
