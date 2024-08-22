using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccounts
{
    public class User : IdentityUser<Guid>
    {
    }
}
