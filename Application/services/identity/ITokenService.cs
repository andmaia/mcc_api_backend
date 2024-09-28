﻿using Common.Responses.identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.services.identity
{
    public interface ITokenService
    {
        Task<ResponseWrapper<TokenResponse>> GetTokenAsync(TokenRequest tokenRequest);
        Task<ResponseWrapper<TokenResponse>> GetRefreshTokenAsync(RefreshTokenRequest refreshTok);


    }
}
