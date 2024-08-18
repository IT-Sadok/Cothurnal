using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccounts.Repositories
{
    public interface IUserRepositories
    {
        Task Register(User user, string role);
        Task<User> GetEmail(string email);
        Task<bool> IsCorrectPassword(string email, string password);
    }
}
