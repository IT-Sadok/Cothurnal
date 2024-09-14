using BusinessLogic.Model;
using BusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAndAuthorization.Endpoints
{
    public static class UserEndpoints
    {
        public static void MapUserEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost("sign-up", ([FromServices] IUserService userService, [FromBody] RegisterUserRequest usermodel)
                => userService.RegisterUserAsync(usermodel));

            endpoints.MapPost("sign-in", ([FromServices] IUserService userService, [FromBody] LoginUserRequest usermodel)
                => userService.LoginUserAsync(usermodel));
        }
    }
}
