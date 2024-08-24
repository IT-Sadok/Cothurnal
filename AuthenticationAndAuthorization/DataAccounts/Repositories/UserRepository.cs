using DataAccounts.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DataAccounts
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserRepository(UserManager<User> userManager,SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task Register(User user,string password, string role)
        {
            var result = await _userManager.CreateAsync(user,password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, role);
            }
        }

        public async Task<bool> SignIn(string email, string password, bool isPersistent, bool lockoutOnFailure)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure);
                return result.Succeeded;
            } 
            return false;
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
    }       
}