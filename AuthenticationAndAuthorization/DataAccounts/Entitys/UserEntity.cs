using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccounts
{
    public class UserEntity : IdentityUser<Guid>
    {
        public UserEntity() : base() { }
    }
}
