using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepoLayer;

namespace ServiceLayer
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository adminRepository;
        private readonly ITokenService tokenService;

        public AdminService(IAdminRepository adminRepository, ITokenService tokenService)
        {
            this.adminRepository = adminRepository;
            this.tokenService = tokenService;
        }

        public async Task RegisterAdminAsync(Admin admin)
        {
            await adminRepository.RegisterAdminAsync(admin);
        }

        public async Task<bool> ForgetPasswordAsync(string email)
        {
            var admin = await adminRepository.GetAdminByEmailAsync(email);
            return admin != null;
        }

        public async Task<bool> ResetPasswordAsync(string email,string newPassword)
        {
            var admin = await adminRepository.GetAdminByEmailAsync(email);
            if (admin != null)
            {
                await adminRepository.UpdateAdminPasswordAsync(email, newPassword);
                return true;
            }
            return false;
        }

        public async Task<TokenResponse?> LoginAsync(string email,string password)
        {
            var admin = await adminRepository.GetAdminByEmailAsync(email);
            if (admin == null || admin.Password != password)
            {
                return null;
            }

            string accessToken = await tokenService.GenerateAccessTokenAsync(admin.Email, admin.Role);
            string refreshToken = await tokenService.GenerateRefreshTokenAsync();

            return new TokenResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }
    }
}