﻿using HumanityService.DataContracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HumanityService.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task Login(string username, string password);
        Task Logout(string username, string accessTokenId, string refreshToken);
        Task RefreshAccessToken(string username, string oldAccessToken, string refreshToken);
    }
}
