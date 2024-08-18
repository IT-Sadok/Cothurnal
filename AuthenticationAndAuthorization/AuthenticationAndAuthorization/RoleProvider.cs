using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace AuthenticationAndAuthorization
{
    public class RoleProvider
    {
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public RoleProvider(RoleManager<IdentityRole<Guid>> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task AddRoles()
        {
            string[] roles = { "User", "Admin" };

            foreach (var roleName in roles)
            {
                var roleExists = await _roleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                {
                    await _roleManager.CreateAsync(new IdentityRole<Guid> { Name = roleName });
                }
            }
        }
    }
}