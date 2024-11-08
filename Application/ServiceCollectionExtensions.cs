﻿using Application.Validators.Identity;
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
           return services.AddTransient<IValidator<UserRegistrationRequest>, UserRegistrationRequestValidator>();

        }
    }

}
