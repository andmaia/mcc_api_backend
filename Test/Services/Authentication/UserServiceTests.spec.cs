using Application.services.identity;
using AutoMapper;
using Common.Responses.identity;
using FluentAssertions;
using Infrastructure.Models;
using Infrastructure.services.identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MockQueryable;
using Moq;

using Test.Builders;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Test.Services.Authentication
{
    public class UserServiceTests
    {
        private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
        private readonly IUserService _sut;
        private readonly UserService _userService; // Classe a ser testada
        private readonly Mock<IMapper> _mapperMock;
        public UserServiceTests()
        {
            var store = new Mock<IUserStore<ApplicationUser>>();
            _userManagerMock = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);
            _mapperMock = new Mock<IMapper>();
            _userService = new UserService(_userManagerMock.Object,_mapperMock.Object);
        }

        [Fact]
        public async Task RegistrationUser_Should_returns_fail_If_email_already_exists()
        {
            string email = "email@gmail.com";
            var userResponseMock = new ApplicationUser
            {
                Email =email
            };
            var userRegistrationMock = new UserRegistrationRequestBuilder()
                .WithEmail(email).Build();

            _userManagerMock.Setup(um => um.FindByEmailAsync(userRegistrationMock.Email))
                .ReturnsAsync(userResponseMock);

            var result =await _userService.RegisterUserAsync(userRegistrationMock);

            result.IsSuccessful.Should().BeFalse();
            result.Messages.Should().Contain("Fail to register user. Email already exists");
        }

        [Fact]
        public async Task RegistrationUser_Should_returns_fail_If_username_already_exists()
        {
            string username = "userTest";
            var userResponseMock = new ApplicationUser
            {
                UserName = username
            };
            var userRegistrationMock = new UserRegistrationRequestBuilder()
                .WithUserName(username).Build();

            _userManagerMock.Setup(um => um.FindByNameAsync(userRegistrationMock.UserName))
                .ReturnsAsync(userResponseMock);

            var result = await _userService.RegisterUserAsync(userRegistrationMock);

            result.IsSuccessful.Should().BeFalse();
            result.Messages.Should().Contain("Fail to register user. Username already exists");
        }

        [Fact]
        public async Task RegistrationUser_Should_Return_Fail_When_DatabaseErrorOccurs()
        {
            var userRegistrationMock = new UserRegistrationRequestBuilder().Build();

            var users = new List<ApplicationUser>();
            var expectedUser = new ApplicationUser
            {
                Email = userRegistrationMock.Email,
                UserName = userRegistrationMock.UserName,
                PhoneNumber = userRegistrationMock.PhoneNumber
            };

            _mapperMock.Setup(m => m.Map<ApplicationUser>(It.IsAny<UserRegistrationRequest>()))
                        .Returns(expectedUser);

            var mockUsers = users.AsQueryable().BuildMock();
            var error = new DbUpdateException("Database error occurred");
            _userManagerMock.Setup(um => um.Users).Returns(mockUsers);

            _userManagerMock.Setup(um => um.CreateAsync(It.IsAny<ApplicationUser>()))
                .ThrowsAsync(error);

            var result = await _userService.RegisterUserAsync(userRegistrationMock);

            result.IsSuccessful.Should().BeFalse();
            result.Messages.Should().Contain(error.Message);
        }
     
    }
}
