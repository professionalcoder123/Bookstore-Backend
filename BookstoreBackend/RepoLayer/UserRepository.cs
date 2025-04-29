using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RepoLayer
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext context;

        public UserRepository(UserDbContext context)
        {
            this.context = context;
        }

        public async Task RegisterUserAsync(User user)
        {
            user.Role = "USER";
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task UpdateUserPasswordAsync(string email, string newPassword)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user != null)
            {
                user.Password = newPassword;
                await context.SaveChangesAsync();
            }
        }
    }
}