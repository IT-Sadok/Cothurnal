using AutoMapper;
using DataAccounts.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using System.Data;

namespace DataAccounts
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;

        public UserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task Register(User user, string role)
        {
            var result = await _userManager.CreateAsync(user);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, role);
            }
        }

        public async Task<User> GetByEmail(string email)
        {
            var user = await _userManager.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);

            if (user != null)
            {
                return user;
            }
            return null;
        }

        public async Task<bool> IsCorrectPassword(string email, string password)
        {
            var user = await _userManager.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);

            return password == user.PasswordHash;
        }
    }
}