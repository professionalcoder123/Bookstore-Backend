﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepoLayer;
using ServiceLayer;

namespace BookstoreBackend.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService adminService;
        private readonly IResponseHelper response;

        public AdminController(IAdminService adminService, IResponseHelper response)
        {
            this.adminService = adminService;
            this.response = response;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAdmin([FromBody] Admin admin)
        {
            if (!ModelState.IsValid)
            {
                var validationErrors = ModelState
                    .Where(m=>m.Value.Errors.Count>0)
                    .SelectMany(m => m.Value.Errors.Select(e=>new ValidationError
                    {
                        Field = m.Key,
                        Message = e.ErrorMessage
                    }))
                    .ToList();
                var validationErrorResponse = new ValidationErrorResponse
                {
                    Status = false,
                    Errors = validationErrors
                };
                return BadRequest(response.Failure("Validation failed!", validationErrorResponse.Errors));
            }
            var addedAdmin = await adminService.RegisterAdminAsync(admin);
            return Ok(response.Success("Admin registered successfully!", addedAdmin));
        }

        [HttpPost("forget-password")]
        public async Task<IActionResult> ForgetPassword([FromQuery] string email)
        {
            var exists = await adminService.ForgetPasswordAsync(email);
            if (!exists)
            {
                return NotFound(response.Failure<string>("Email not found!"));
            }
            return Ok(response.Success<string>("Password reset allowed!"));
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromQuery] string email,string newPassword)
        {
            var reset = await adminService.ResetPasswordAsync(email, newPassword);
            if (!reset)
            {
                return NotFound(response.Failure<string>("Email not found or cannot reset password!"));
            }
            return Ok(response.Success<string>("Password reset successful!"));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var tokens = await adminService.LoginAsync(request.Email, request.Password);
            if (tokens == null)
            {
                return BadRequest(APIResponse<string>.Fail("Invalid email or password!"));
            }
            return Ok(APIResponse<TokenResponse>.Success("Login successful!", tokens));
        }
    }
}