using RepoLayer;

namespace ServiceLayer
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly ITokenService tokenService;

        public UserService(IUserRepository userRepository, ITokenService tokenService)
        {
            this.userRepository = userRepository;
            this.tokenService = tokenService;
        }

        public async Task RegisterUserAsync(User user)
        {
            await userRepository.RegisterUserAsync(user);
        }

        public async Task<bool> ForgetPasswordAsync(string email)
        {
            var user = await userRepository.GetUserByEmailAsync(email);
            return user != null;
        }

        public async Task<bool> ResetPasswordAsync(string email,string newPassword)
        {
            var user = await userRepository.GetUserByEmailAsync(email);
            if (user != null)
            {
                await userRepository.UpdateUserPasswordAsync(email, newPassword);
                return true;
            }
            return false;
        }

        public async Task<TokenResponse?> LoginAsync(string email,string password)
        {
            var user = await userRepository.GetUserByEmailAsync(email);
            if (user == null || user.Password != password)
            {
                return null;
            }

            string accessToken = await tokenService.GenerateAccessTokenAsync(user.Email, user.Role);
            string refreshToken = await tokenService.GenerateRefreshTokenAsync();

            return new TokenResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }
    }
}