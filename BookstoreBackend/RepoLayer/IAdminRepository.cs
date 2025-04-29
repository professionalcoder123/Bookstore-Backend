using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer
{
    public interface IAdminRepository
    {
        Task RegisterAdminAsync(Admin admin);

        Task<Admin> GetAdminByEmailAsync(string email);

        Task UpdateAdminPasswordAsync(string email, string newPassword);
    }
}