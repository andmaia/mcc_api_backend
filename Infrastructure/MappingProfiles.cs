using AutoMapper;
using Common.Responses.identity;
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
        }
    }
}
