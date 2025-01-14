using AutoMapper;
using Common.requests.branch;
using Common.requests.company;
using Common.requests.Employee;
using Common.requests.identity;
using Common.requests.Order;
using Common.requests.Payment;
using Common.requests.PaymentForm;
using Common.Responses.branch;
using Common.Responses.Company;
using Common.Responses.Employee;
using Common.Responses.Expense;
using Common.Responses.identity;
using Common.Responses.order;
using Common.Responses.Paymenet;
using Common.Responses.PaymentForm;
using Domain.models;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<UserRegistrationRequest, ApplicationUser>();
            CreateMap<ApplicationUser, UserResponse>();
            CreateMap<UserPreRegistrationRequest, ApplicationUser>();
            CreateMap<CompanyRequest, Company>();
            CreateMap<Company, CompanyResponse>();
            CreateMap<CompanyUpdate, Company>();
            CreateMap<UpdateEmployee, Employee>();
            CreateMap<UpdateEmployeeToCompany, Employee>();
            CreateMap<EmployeeRegisterRequest, Employee>();
            CreateMap<FinishRegisterEmployee, Employee>();
            CreateMap<Employee, EmployeeResponse>();
            CreateMap<PaymentForm, PaymentFormResponse>();
            CreateMap<UpdatePaymentFormRequest, PaymentForm>();
            CreateMap<CreatePaymentFormRequest, PaymentForm>();
            CreateMap<Order, ResponseOrder>();
            CreateMap<CreateOrderRequest, Order>();
            CreateMap<UpdateOrderRequest, Order>();
            CreateMap<UpdateBranchRequest, Branch>();
            CreateMap<CreateBranchRequest, Branch>();
            CreateMap<Branch, ResponseBranch>();

            CreateMap<CreatePaymentRequest, Payment>();
            CreateMap<Payment, ResponsePayment>();
                CreateMap<Expense, ExpenseResponse>();










        }
    }
}
