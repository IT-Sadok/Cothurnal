using System.Security.Claims;

namespace DataAccounts
{
    public static class UserConstants
    {
        public const string Admin = "Admin";
        public const string User = "User";

        public static IReadOnlyList<string> GetRole()
        {
            var userRoles = typeof(UserConstants)
            .GetFields()
            .Select(field => field.GetValue(null)?.ToString())
            .ToList();

            return userRoles.AsReadOnly()!;
        }
    }
}
