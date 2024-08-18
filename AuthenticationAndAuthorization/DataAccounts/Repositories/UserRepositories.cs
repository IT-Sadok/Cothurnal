using DataAccounts.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using System.Data;

namespace DataAccounts
{
    public class UserRepositories : IUserRepositories
    {
        private readonly UserManager<UserEntity> _userManager;

        public UserRepositories(UserManager<UserEntity> userManager)
        {
            _userManager = userManager;
        }

        public async Task Register(User user, string role)
        {
            var userEntity = new UserEntity
            {
                Id = user.Id,
                UserName = user.Username,
                Email = user.Email,
            };

            var result = await _userManager.CreateAsync(userEntity, user.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(userEntity, role);
            }
        }

        public async Task<User> GetEmail(string email)
        {
            var user = await _userManager.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);

            if (user != null)
            {
                return new User(user.Id,user.UserName!,user.Email!,user.PasswordHash!);
            }
            throw new NullReferenceException();
        }

        public async Task<bool> IsCorrectPassword(string email,string password)
        {
            var user = await _userManager.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
            var result = await _userManager.CheckPasswordAsync(user!, password);

            return result;
        }
    }
} 
