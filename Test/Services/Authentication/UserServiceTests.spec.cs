using Application.services.identity;
using AutoMapper;
using Common.requests.identity;
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
        private readonly IUserService _userService; 
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
            var userResponseMock = new ApplicationUserBuilder().WithEmail(email).Build();
            var userRegistrationMock = new UserRegistrationRequestBuilder().WithEmail(email).Build();

            _userManagerMock.Setup(um => um.FindByEmailAsync(userRegistrationMock.Email)).ReturnsAsync(userResponseMock);

            var result =await _userService.RegisterUserAsync(userRegistrationMock);

            result.IsSuccessful.Should().BeFalse();
            result.Messages.Should().Contain("Fail to register user. Email already exists");
        }

        [Fact]
        public async Task RegistrationUser_Should_returns_fail_If_username_already_exists()
        {
            string username = "userTest";
            var userResponseMock = new ApplicationUserBuilder().WithUserName(username).Build();
            var userRegistrationMock = new UserRegistrationRequestBuilder().WithUserName(username).Build();

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
            var expectedUser = new ApplicationUserBuilder().WithEmail(userRegistrationMock.Email)
                .WithPhoneNumber(userRegistrationMock.PhoneNumber).WithUserName(userRegistrationMock.UserName).Build();

            _mapperMock.Setup(m => m.Map<ApplicationUser>(It.IsAny<UserRegistrationRequest>())).Returns(expectedUser);

            var mockUsers = users.AsQueryable().BuildMock();
            var error = new DbUpdateException("Database error occurred");
            _userManagerMock.Setup(um => um.Users).Returns(mockUsers);

            _userManagerMock.Setup(um => um.CreateAsync(It.IsAny<ApplicationUser>()))
                .ThrowsAsync(error);

            var result = await _userService.RegisterUserAsync(userRegistrationMock);

            result.IsSuccessful.Should().BeFalse();
            result.Messages.Should().Contain(error.Message);
        }

        [Fact]
        public async Task PreRegisterUser_Should_Return_Fail_If_Email_Already_Exists()
        {
            string email = "test@test.com";
            var userMock = new ApplicationUserBuilder().WithEmail(email).Build();
            var preRegisterRequest = new UserPreRegistrationRequestBuilder().WithEmail(email).Build();

            _userManagerMock.Setup(um => um.FindByEmailAsync(email)).ReturnsAsync(userMock);

            var result = await _userService.PreRegisterUserAsync(preRegisterRequest);

            result.IsSuccessful.Should().BeFalse();
            result.Messages.Should().Contain("Fail to pre-register user. Email already exists");
        }

        [Fact]
        public async Task PreRegisterUser_Should_Return_Fail_If_Username_Already_Exists()
        {
            string userName = "testUser";
            var userMock = new ApplicationUserBuilder().WithUserName(userName).Build();
            var preRegisterRequest = new UserPreRegistrationRequestBuilder().WithUserName(userName).Build();

            _userManagerMock.Setup(um => um.FindByNameAsync(userName)).ReturnsAsync(userMock);

            var result = await _userService.PreRegisterUserAsync(preRegisterRequest);

            result.IsSuccessful.Should().BeFalse();
            result.Messages.Should().Contain("Fail to register user. Username already exists");
        }

        [Fact]
        public async Task PreRegisterUser_Should_Return_Success_When_User_Is_Registered_Successfully()
        {
            var identityResult = IdentityResult.Success;
            var preRegisterRequest = new UserPreRegistrationRequestBuilder().Build();
            var expecetedUser = new ApplicationUserBuilder().WithUserName(preRegisterRequest.UserName)
                .WithEmail(preRegisterRequest.Email).Build();

            _mapperMock.Setup(m => m.Map<ApplicationUser>(It.IsAny<UserPreRegistrationRequest>()))
                   .Returns(expecetedUser);

            _userManagerMock.Setup(um => um.FindByEmailAsync(preRegisterRequest.Email)).ReturnsAsync((ApplicationUser)null);
            _userManagerMock.Setup(um => um.FindByNameAsync(preRegisterRequest.UserName)).ReturnsAsync((ApplicationUser)null);
            _userManagerMock.Setup(um => um.CreateAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(identityResult);
            _userManagerMock.Setup(um => um.AddToRoleAsync(It.IsAny<ApplicationUser>(), preRegisterRequest.Role)).ReturnsAsync(identityResult);

            var result = await _userService.PreRegisterUserAsync(preRegisterRequest);

            result.IsSuccessful.Should().BeTrue();
            result.Messages.Should().Contain("User pre-registered successfully.");
        }

        [Fact]
        public async Task PreRegisterUser_Should_Return_Fail_If_Role_Not_Found()
        {

            var identityResult = new IdentityResult();
            var preRegisterRequest = new UserPreRegistrationRequestBuilder().Build();
            var expecetedUser = new ApplicationUserBuilder().WithUserName(preRegisterRequest.UserName)
                .WithEmail(preRegisterRequest.Email).Build();

            _mapperMock.Setup(m => m.Map<ApplicationUser>(It.IsAny<UserPreRegistrationRequest>()))
            .Returns(expecetedUser);


            _userManagerMock.Setup(um => um.FindByEmailAsync(preRegisterRequest.Email)).ReturnsAsync((ApplicationUser)null);
            _userManagerMock.Setup(um => um.FindByNameAsync(preRegisterRequest.UserName)).ReturnsAsync((ApplicationUser)null);
            _userManagerMock.Setup(um => um.CreateAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(identityResult);
            _userManagerMock.Setup(um => um.AddToRoleAsync(It.IsAny<ApplicationUser>(), preRegisterRequest.Role)).ReturnsAsync(identityResult);

            var result = await _userService.PreRegisterUserAsync(preRegisterRequest);

            result.IsSuccessful.Should().BeFalse();
            result.Messages.Should().Contain("Fail to pre-register user");
        }

        [Fact]
        public async Task FinishRegisterUser_Should_Return_Fail_If_Email_Not_PreRegistered()
        {
            var request = new UserRegistrationRequestBuilder().Build();

            _userManagerMock.Setup(um => um.FindByEmailAsync(request.Email)).ReturnsAsync((ApplicationUser)null);

            var result = await _userService.FinishRegisterUserAsync(request);

            result.IsSuccessful.Should().BeFalse();
            result.Messages.Should().Contain("Email doens't has user pre-registred");
        }



      
        [Fact]
        public async Task FinishRegisterUser_Should_Return_Fail_If_CellPhone_Already_Registered()
        {
            var request = new UserRegistrationRequestBuilder().Build();
            var userMock = new ApplicationUserBuilder().WithEmail(request.Email).WithPhoneNumber(request.PhoneNumber).WithPasswordHash(request.Password).Build();
           
            var users = new List<ApplicationUser>() {userMock };
            var mockUsers = users.AsQueryable().BuildMock();

            _userManagerMock.Setup(um => um.FindByEmailAsync(request.Email)).ReturnsAsync(userMock);
            _userManagerMock.Setup(um => um.Users).Returns(mockUsers);

            var result = await _userService.FinishRegisterUserAsync(request);

            result.IsSuccessful.Should().BeFalse();
            result.Messages.Should().Contain("Fail to register user. CellphoneNumber already exists");

        }

        [Fact]
        public async Task FinishRegisterUser_Should_Return_Success_When_User_Is_Updated_Successfully()
        {
            var request = new UserRegistrationRequestBuilder().Build();
            var userMock = new ApplicationUserBuilder().WithEmail(request.Email).WithPhoneNumber(request.PhoneNumber).WithPasswordHash(request.Password).Build();
            var identityResult = IdentityResult.Success;

            var users = new List<ApplicationUser>();
            var mockUsers = users.AsQueryable().BuildMock();

            _userManagerMock.Setup(um => um.FindByEmailAsync(request.Email)).ReturnsAsync(userMock);
            _userManagerMock.Setup(um => um.Users).Returns(mockUsers);
            _userManagerMock.Setup(um => um.UpdateAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(identityResult);
            var result = await _userService.FinishRegisterUserAsync(request);

            result.IsSuccessful.Should().BeTrue();
            result.Messages.Should().Contain("User registered successfully.");
        }


        [Fact]
        public async Task GetUserByEmail_Should_Return_Success_If_User_Exists()
        {
            string email = "test@test.com";
            var userMock = new ApplicationUser { Email = email };
            var userResponseMock = new UserResponse { Email = email };

            _userManagerMock.Setup(um => um.FindByEmailAsync(email)).ReturnsAsync(userMock);

            _mapperMock.Setup(m => m.Map<UserResponse>(It.IsAny<ApplicationUser>()))
                  .Returns(userResponseMock);

            var roles = new List<string> { "Admin" };
            _userManagerMock.Setup(um => um.GetRolesAsync(userMock)).ReturnsAsync(roles);

            var result = await _userService.GetUserByEmailAsync(email);

            result.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public async Task GetUserByEmail_Should_Return_Fail_If_User_Does_Not_Exist()
        {
            string email = "nonexistent@test.com";
            _userManagerMock.Setup(um => um.FindByEmailAsync(email)).ReturnsAsync((ApplicationUser)null);

            var result = await _userService.GetUserByEmailAsync(email);

            result.IsSuccessful.Should().BeFalse();
            result.Messages.Should().Contain("User does not exist.");
        }
        [Fact]
        public async Task UpdateEmailUserAsync_Should_Return_Fail_If_User_Does_Not_Exist()
        {
            var updateEmailRequest = new UpdateEmailRequestBuilder().Build();

            var users = new List<ApplicationUser>();
            var mockUsers = users.AsQueryable().BuildMock();
            _userManagerMock.Setup(um => um.Users).Returns(mockUsers);

            var result = await _userService.UpdateEmailUserAsync(updateEmailRequest);

            result.IsSuccessful.Should().BeFalse();
            result.Messages.Should().Contain("Fail to update user. User does'nt exists.");

        }

        [Fact]
        public async Task UpdateEmailUserAsync_Should_Return_Fail_If_oldEmail_Does_Not_Exist()
        {
            var userMock = new ApplicationUserBuilder().WithEmail("default2@gmail.com").Build();
            var updateEmailRequest = new UpdateEmailRequestBuilder().WithId(userMock.Id).WithOldEmail("default1@gmail.com").Build();
            var users = new List<ApplicationUser>() {userMock};
            var mockUsers = users.AsQueryable().BuildMock();

            _userManagerMock.Setup(um => um.Users).Returns(mockUsers);

            var result = await _userService.UpdateEmailUserAsync(updateEmailRequest);

            result.IsSuccessful.Should().BeFalse();
            result.Messages.Should().Contain("Fail to update user. oldEmail is not equal email user.");

        }

        [Fact]
        public async Task UpdateEmailUserAsync_Should_Return_Fail_If_newEmail_Exist()
        {
            var userMock = new ApplicationUserBuilder().WithEmail("default@gmail.com").Build();
            var userMockWithEmailRegistred = new ApplicationUserBuilder().WithEmail("defaultRegistred@gmail.com").Build();
            var updateEmailRequest = new UpdateEmailRequestBuilder().WithId(userMock.Id).WithOldEmail(userMock.Email).WithNewEmail(userMockWithEmailRegistred.Email).Build();
            var users = new List<ApplicationUser>() { userMock };
            var mockUsers = users.AsQueryable().BuildMock();

            _userManagerMock.Setup(um => um.Users).Returns(mockUsers);
            _userManagerMock.Setup(um => um.FindByEmailAsync(updateEmailRequest.NewEmail)).ReturnsAsync(userMockWithEmailRegistred);

            var result = await _userService.UpdateEmailUserAsync(updateEmailRequest);

            result.IsSuccessful.Should().BeFalse();
            result.Messages.Should().Contain("Fail to update user. NewEmail already has a user.");

        }

        [Fact]
        public async Task UpdateEmailUserAsync_Should_Return_Sucess_If_email_updated()
        {
            var identityResult = IdentityResult.Success;
            var userMock = new ApplicationUserBuilder().WithEmail("default@gmail.com").Build();
            var updateEmailRequest = new UpdateEmailRequestBuilder().WithId(userMock.Id).WithOldEmail(userMock.Email).Build();
            var users = new List<ApplicationUser>() {userMock };
            var mockUsers = users.AsQueryable().BuildMock();

            _userManagerMock.Setup(um => um.Users).Returns(mockUsers);
            _userManagerMock.Setup(um => um.FindByEmailAsync(updateEmailRequest.NewEmail)).ReturnsAsync((ApplicationUser)null);
            _userManagerMock.Setup(um => um.UpdateAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(identityResult);

            var result = await _userService.UpdateEmailUserAsync(updateEmailRequest);

            result.IsSuccessful.Should().BeTrue();

        }

        [Fact]
        public async Task UpdateEmailUserAsync_Should_Return_fail_If_email_not_updated()
        {
            var identityResult = new IdentityResult();
            var userMock = new ApplicationUserBuilder().WithEmail("default@gmail.com").Build();
            var updateEmailRequest = new UpdateEmailRequestBuilder().WithId(userMock.Id).WithOldEmail(userMock.Email).Build();
            var users = new List<ApplicationUser>() { userMock };
            var mockUsers = users.AsQueryable().BuildMock();

            _userManagerMock.Setup(um => um.Users).Returns(mockUsers);
            _userManagerMock.Setup(um => um.FindByEmailAsync(updateEmailRequest.NewEmail)).ReturnsAsync((ApplicationUser)null);
            _userManagerMock.Setup(um => um.UpdateAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(identityResult);

            var result = await _userService.UpdateEmailUserAsync(updateEmailRequest);

            result.IsSuccessful.Should().BeFalse();
            result.Messages.Should().Contain("Fail to update user.");


        }

        [Fact]
        public async Task UpdatePhoneNumbweUserAsync_Should_Return_Fail_If_User_Does_Not_Exist()
        {
            var updateCellPhoneNumberRequest = new UpdateCellPhoneNumberRequestBuilder().Build();

            var users = new List<ApplicationUser>();
            var mockUsers = users.AsQueryable().BuildMock();
            _userManagerMock.Setup(um => um.Users).Returns(mockUsers);

            var result = await _userService.UpdatePhoneNumber(updateCellPhoneNumberRequest);

            result.IsSuccessful.Should().BeFalse();
            result.Messages.Should().Contain("Fail to update user. User does'nt exists.");

        }


    }
}
