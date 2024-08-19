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
        private readonly IUserRepository _userRepositories;
        private readonly IJwtService _jwtService;
        
        public UserService(IUserRepository userRepositories, IJwtService jwtService)
        {
            _userRepositories = userRepositories;
            _jwtService = jwtService;
        }

        public async Task<string> LoginUserAsync(LoginUserRequest model)
        {
            var user = await _userRepositories.GetByEmail(model.Email);
            var result = await _userRepositories.IsCorrectPassword(model.Email,model.Password);
            if (result == false)
            {
                throw new ArgumentException();
            }
            return _jwtService.GenerateJwt(user.Id,user.UserName);
        }

        public async Task RegisterUserAsync(RegisterUserRequest request)
        {
            string role = "User";

            if (request.Email.Equals("admin@example.com", StringComparison.OrdinalIgnoreCase))
            {
                role = "Admin";
            }

            var user = new User()
            {
                Id = Guid.NewGuid(),
                UserName = request.Email,
                Email = request.Email,
                PasswordHash = request.Password
            };

            await _userRepositories.Register(user, role);
        }
    }
}
