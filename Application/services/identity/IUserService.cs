using Common.requests.identity;
using Common.Responses.identity;
using Common.Responses.wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.services.identity
{
    public interface IUserService
    {
        Task<IResponseWrapper> PreRegisterUserAsync(UserPreRegistrationRequest request);

        Task<IResponseWrapper> FinishRegisterUserAsync(UserRegistrationRequest request);
        Task<IResponseWrapper> UpdatePhoneNumber(UpdateCellPhoneNumberRequest request);


        Task<IResponseWrapper> RegisterUserAsync(UserRegistrationRequest request);
        Task<IResponseWrapper> GetUserByIdAsync(string id);

        Task<IResponseWrapper> GetUserByEmailAsync(string email);

        Task<IResponseWrapper> UpdateEmailUserAsync(UpdateEmailRequest request);
        Task<IResponseWrapper> UpdateUserNameAsync(UpdateUserNameRequest request);

    }
}
