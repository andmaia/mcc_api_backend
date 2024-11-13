using AutoMapper;
using Common.requests.company;
using Common.requests.identity;
using Common.Responses.Company;
using Common.Responses.identity;
using Domain.models;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles() 
        {
            CreateMap<UserRegistrationRequest, ApplicationUser>();
            CreateMap<ApplicationUser,UserResponse>();
            CreateMap<UserPreRegistrationRequest, ApplicationUser>();
            CreateMap<CompanyRequest, Company>();
            CreateMap<Company, CompanyResponse>();
            CreateMap<CompanyUpdate, Company>();
        }
    }
}
