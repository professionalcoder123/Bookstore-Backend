using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;

namespace BookstoreBackend
{
    public class CustomAuthorizationHandler : IAuthorizationMiddlewareResultHandler
    {
        private readonly AuthorizationMiddlewareResultHandler defaultHandler = new();

        public async Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
        {
            if (authorizeResult.Forbidden && context.User.Identity.IsAuthenticated)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                context.Response.ContentType = "application/json";

                var roleClaim = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
                var role = roleClaim?.Value;

                string message = role switch
                {
                    "ADMIN" => "Admins are not authorized to perform this operation.",
                    "USER" => "Users are not authorized to perform this operation.",
                    _ => "Users are not authorized to perform this operation."
                };

                var result = new
                {
                    status = false,
                    message,
                    data = (object)null
                };

                var json = JsonSerializer.Serialize(result);
                await context.Response.WriteAsync(json);
            }
            else
            {
                await defaultHandler.HandleAsync(next, context, policy, authorizeResult);
            }
        }
    }
}
