using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepoLayer;

namespace ServiceLayer
{
    public interface IUserService
    {
        Task RegisterUserAsync(User user);

        Task<bool> ForgetPasswordAsync(string email);

        Task<bool> ResetPasswordAsync(string email, string newPassword);

        Task<TokenResponse?> LoginAsync(string email, string password);
    }
}
