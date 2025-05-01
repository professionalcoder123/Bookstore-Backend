using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer
{
    public interface IUserRepository
    {
        Task<User> RegisterUserAsync(User user);

        Task<User> GetUserByEmailAsync(string email);

        Task UpdateUserPasswordAsync(string email, string newPassword);
    }
}
