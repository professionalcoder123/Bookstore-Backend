using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepoLayer;

namespace ServiceLayer
{
    public interface IAdminService
    {
        Task RegisterAdminAsync(Admin admin);

        Task<bool> ForgetPasswordAsync(string email);

        Task<bool> ResetPasswordAsync(string email, string newPassword);

        Task<TokenResponse?> LoginAsync(string email, string password);
    }
}