using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RepoLayer
{
    public class AdminRepository : IAdminRepository
    {
        private readonly UserDbContext context;

        public AdminRepository(UserDbContext context)
        {
            this.context = context;
        }

        public async Task<Admin> RegisterAdminAsync(Admin admin)
        {
            admin.Role = "ADMIN";
            var result = await context.Admins.AddAsync(admin);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Admin> GetAdminByEmailAsync(string email)
        {
            return await context.Admins.FirstOrDefaultAsync(a => a.Email == email);
        }

        public async Task UpdateAdminPasswordAsync(string email,string newPassword)
        {
            var admin = await context.Admins.FirstOrDefaultAsync(a => a.Email == email);
            if (admin != null)
            {
                admin.Password = newPassword;
                await context.SaveChangesAsync();
            }
        }
    }
}