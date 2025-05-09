﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public interface ITokenService
    {
        Task<string> GenerateAccessTokenAsync(int userId, string email, string role);
        Task<string> GenerateRefreshTokenAsync();
    }
}
