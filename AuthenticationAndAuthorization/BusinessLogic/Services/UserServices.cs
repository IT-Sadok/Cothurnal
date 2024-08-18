using BusinessLogic;
using BusinessLogic.Model;
using DataAccounts;
using DataAccounts.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic
{
    public class UserService: IUserService
    {
        private readonly IUserRepositories _userRepositories;
        private readonly IJwtService _jwtService;
        
        public UserService(IUserRepositories userRepositories, IJwtService jwtService)
        {
            _userRepositories = userRepositories;
            _jwtService = jwtService;
        }

        public async Task<string> LoginUserAsync(LoginUserRequest model)
        {
            var user = await _userRepositories.GetEmail(model.Email);
            var result = await _userRepositories.IsCorrectPassword(model.Email,model.Password);
            if (result == false)
            {
                throw new ArgumentException();
            }
            return _jwtService.GenerateJwt(user.Id,user.Username);
        }

        public async Task RegisterUserAsync(RegisterUserRequest request)
        {
            string role = "User";

            if (request.Email.Equals("admin@example.com", StringComparison.OrdinalIgnoreCase))
            {
                role = "Admin";
            }

            var user = new User(Guid.NewGuid(), request.Username, request.Email, request.Password);

            await _userRepositories.Register(user, role);
        }
    }
}
