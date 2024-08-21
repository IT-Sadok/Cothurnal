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
        private readonly IMapper _mapper;

        public UserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task Register(CreateUserDto user, string role)
        {
            var userEntity = _mapper.Map<User>(user);

            var result = await _userManager.CreateAsync(userEntity, user.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(userEntity, role);
            }
        }

        public async Task<User> GetByEmail(string email)
        {
            var user = await _userManager.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);

            if (user != null)
            {
                return new User()
                {
                    UserName = user.Email,
                    Email = user.Email,
                    PasswordHash = user.PasswordHash
                }; ;
            }
            return null;
        }

        public async Task<bool> IsCorrectPassword(string email, string password)
        {
            var user = await _userManager.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
            var result = await _userManager.CheckPasswordAsync(user!, password);

            return result;
        }
    }
}