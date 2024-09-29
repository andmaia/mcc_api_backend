using Application.services.identity;
using AutoMapper;
using Common.Authorization;
using Common.Responses.identity;
using Common.Responses.wrappers;
using Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.services.identity
{
    public class UserService : IUserService
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<IResponseWrapper> RegisterUserAsync(UserRegistrationRequest request)
        {
            try
            {
                var userWithEmailRegistred = await _userManager.FindByEmailAsync(request.Email);
                if (userWithEmailRegistred is not null)
                {
                    return await ResponseWrapper.FailAsync("Fail to register user. Email already exists");
                }

                var userWithNameRegistred = await _userManager.FindByNameAsync(request.UserName);
                if (userWithNameRegistred is not null)
                {
                    return await ResponseWrapper.FailAsync("Fail to register user. Username already exists");
                }

                var userWithCellPhoneRegistered = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == request.PhoneNumber);
                if (userWithCellPhoneRegistered is not null)
                {
                    return await ResponseWrapper.FailAsync("Fail to register user. CellphoneNumber already exists");
                }

                var newUser = _mapper.Map<ApplicationUser>(request);
                var password = new PasswordHasher<ApplicationUser>();
                newUser.PasswordHash = password.HashPassword(newUser, request.Password);

                var identityResult = await _userManager.CreateAsync(newUser);
                if (identityResult.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newUser, AppRole.Basic);
                    return await ResponseWrapper<string>.SuccessAsync("User registered successfully.");
                }

                return await ResponseWrapper.FailAsync("Fail to register user");
            }
            catch (DbUpdateException ex)
            {
                return await ResponseWrapper.FailAsync(ex.Message) ;
            }
            catch (Exception ex)
            {
                return await ResponseWrapper.FailAsync(ex.Message);
            }
        }

    }
}
