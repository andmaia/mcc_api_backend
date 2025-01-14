using Application.Validators.Domain.Company;
using Application.Validators.Employee;
using Application.Validators.Expense;
using Application.Validators.Identity;
using Application.Validators.Orders;
using Application.Validators.Payment;
using Application.Validators.PaymentForm;
using Common.requests.company;
using Common.requests.Employee;
using Common.requests.Expense;
using Common.requests.identity;
using Common.requests.Order;
using Common.requests.Payment;
using Common.requests.PaymentForm;
using Common.Responses.identity;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application
{

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            return services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        }

        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            return services.AddTransient<IValidator<UserRegistrationRequest>, UserRegistrationRequestValidator>()
              .AddTransient<IValidator<UserPreRegistrationRequest>, PreUserRegistrationValidator>()
              .AddTransient<IValidator<UpdateEmailRequest>, UpdateEmailRequestValidator>()
              .AddTransient<IValidator<UpdateCellPhoneNumberRequest>, UpdateCellPhoneNumberRequestValidator>()
              .AddTransient<IValidator<UpdateUserNameRequest>, UpdateUserNameRequestValidator>()
              .AddTransient<IValidator<UpdatePasswordRequest>, UpdatePasswordRequestValidator>()
              .AddTransient<IValidator<CompanyRequest>, CompanyRequestValidator>()
              .AddTransient<IValidator<CompanyUpdate>, CompanyUpdateValidator>()
              .AddTransient<IValidator<UpdateRoleRequest>, UpdateRoleRequestValidator>()
              .AddTransient<IValidator<UpdateEmployee>, UpdateEmployeeValidator>()
              .AddTransient<IValidator<UpdateEmployeeToCompany>, UpdateEmployeeToCompanyValidator>()
              .AddTransient<IValidator<EmployeeRegisterRequest>, EmployeeRegisterRequestValidator>()
              .AddTransient<IValidator<FinishRegisterEmployee>, FinishRegisterEmployeeValidator>()
            .AddTransient<IValidator<CreatePaymentFormRequest>, CreatePaymentFormValidator>()
            .AddTransient<IValidator<UpdatePaymentFormRequest>, UpdatePaymentFormValidator>()
            .AddTransient<IValidator<OrderSearchFilter>, OrderSearchFilterValidator>()
                        .AddTransient<IValidator<UpdateOrderRequest>, UpdateOrderRequestValidator>()
                                    .AddTransient<IValidator<CreateOrderRequest>, CreateOrderRequestValidator>()
              .AddTransient<IValidator<CreatePaymentRequest>, CreatePaymentRequestValidator>()
                          .AddTransient<IValidator<UpdatePaymentRequest>, UpdatePaymentRequestValidator>()
            .AddTransient<IValidator<CreateExpenseRequest>,CreateExpenseRequestValidator>()
                        .AddTransient<IValidator<UpdateExpenseRequest>, UpdateExpenseRequestValidator>();




        }
    }

}
