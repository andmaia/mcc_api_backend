using Application.services.identity;
using AutoMapper;
using Common.Authorization;
using Common.requests.identity;
using Common.Responses.identity;
using Common.Responses.wrappers;
using Infrastructure.Migrations;
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

  
        public async Task<IResponseWrapper> GetUserByIdAsync(string id)
        {
            try
            {
                var userInDb = await _userManager.FindByIdAsync(id);
                if (userInDb is not null)
                {
                    var mappedUser = _mapper.Map<UserResponse>(userInDb);
                    var roles = await _userManager.GetRolesAsync(userInDb);
                    mappedUser.Roles = roles.ToList(); 

                    return await ResponseWrapper<UserResponse>.SuccessAsync(mappedUser);
                }
                return await ResponseWrapper.FailAsync("User does not exist.");
            }
            catch (DbUpdateException ex)
            {
                return await ResponseWrapper.FailAsync(ex.Message);
            }
        }

        public async Task<IResponseWrapper> FinishRegisterUserAsync(UserRegistrationRequest request)
        {
            var userPreRegisted =await _userManager.FindByEmailAsync(request.Email);
            if (userPreRegisted is not null)
            {
             

                var userWithCellPhoneRegistered = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == request.PhoneNumber);
                if (userWithCellPhoneRegistered is not null)
                {
                    return await ResponseWrapper.FailAsync("Fail to register user. CellphoneNumber already exists");
                }
                userPreRegisted.PhoneNumber = request.PhoneNumber;
                userPreRegisted.PhoneNumberConfirmed = true;
                userPreRegisted.EmailConfirmed = true;
                userPreRegisted.IsActive = true;

                var password = new PasswordHasher<ApplicationUser>();
                userPreRegisted.PasswordHash = password.HashPassword(userPreRegisted, request.Password);

                var identityResult = await _userManager.UpdateAsync(userPreRegisted);
                if (identityResult.Succeeded)
                {
                    return await ResponseWrapper<string>.SuccessAsync("User registered successfully.");
                }

                return await ResponseWrapper.FailAsync("Fail to register user");
            }
            return await ResponseWrapper.FailAsync("Email doens't has user pre-registred");

        }

        public async Task<IResponseWrapper> PreRegisterUserAsync(UserPreRegistrationRequest request)
        {
            var userWithEmailRegistred = await _userManager.FindByEmailAsync(request.Email);
            if (userWithEmailRegistred is not null)
            {
                return await ResponseWrapper.FailAsync("Fail to pre-register user. Email already exists");
            }

            var userWithNameRegistred = await _userManager.FindByNameAsync(request.UserName);
            if (userWithNameRegistred is not null)
            {
                return await ResponseWrapper.FailAsync("Fail to register user. Username already exists");
            }

            var newUser = _mapper.Map<ApplicationUser>(request);
            newUser.IsActive = false;

            var identityResult = await _userManager.CreateAsync(newUser);
            if (identityResult.Succeeded)
            {
                var role = AppRole.DefaultRoles.FirstOrDefault(r => r == request.Role);
                if (role != null)
                {
                    await _userManager.AddToRoleAsync(newUser, role);
                    return await ResponseWrapper<string>.SuccessAsync("User pre-registered successfully.");
                }
                else
                {
                    return await ResponseWrapper.FailAsync("Fail to pre-register user.Role doesn't exists");
                }
            }
            return await ResponseWrapper.FailAsync("Fail to pre-register user");


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
                newUser.IsActive= true;
                newUser.EmailConfirmed = true;
                newUser.PhoneNumberConfirmed = true;

                var password = new PasswordHasher<ApplicationUser>();
                newUser.PasswordHash = password.HashPassword(newUser, request.Password);

                var identityResult = await _userManager.CreateAsync(newUser);
                if (identityResult.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newUser, AppRole.Admin);
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

        public async Task<IResponseWrapper> GetUserByEmailAsync(string email)
        {
            try
            {
                var userInDb = await _userManager.FindByEmailAsync(email);
                if (userInDb is not null)
                {
                    var mappedUser = _mapper.Map<UserResponse>(userInDb);
                    var roles = await _userManager.GetRolesAsync(userInDb);
                    mappedUser.Roles = roles.ToList();

                    return await ResponseWrapper<UserResponse>.SuccessAsync(mappedUser);
                }
                return await ResponseWrapper.FailAsync("User does not exist.");
            }
            catch (DbUpdateException ex)
            {
                return await ResponseWrapper.FailAsync(ex.Message);
            }
        }

    }
}
