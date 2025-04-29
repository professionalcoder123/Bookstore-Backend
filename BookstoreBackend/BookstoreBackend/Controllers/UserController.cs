using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepoLayer;
using ServiceLayer;

namespace BookstoreBackend.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IResponseHelper response;

        public UserController(IUserService userService, IResponseHelper response)
        {
            this.userService = userService;
            this.response = response;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(response.Failure<string>("Invalid model!"));
            }
            await userService.RegisterUserAsync(user);
            return Ok(response.Success<string>("User registered successfully!"));
        }

        [HttpPost("forget-password")]
        public async Task<IActionResult> ForgetPassword([FromQuery] string email)
        {
            var exists = await userService.ForgetPasswordAsync(email);
            if (!exists)
            {
                return NotFound(response.Failure<string>("Email not found!"));
            }
            return Ok(response.Success<string>("Password reset allowed!"));
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromQuery] string email,string newPassword)
        {
            var reset = await userService.ResetPasswordAsync(email, newPassword);
            if (!reset)
            {
                return NotFound(response.Failure<string>("Email not found or cannot reset password!"));
            }
            return Ok(response.Success<string>("Password reset successful!"));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var tokens = await userService.LoginAsync(request.Email, request.Password);
            if (tokens == null)
            {
                return Unauthorized(APIResponse<string>.Fail("Invalid email or password!"));
            }
            return Ok(APIResponse<TokenResponse>.Success("Login successful!", tokens));
        }
    }
}