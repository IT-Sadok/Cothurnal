using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccounts.Repositories
{
    public interface IUserRepository
    {
        Task Register(User user, string password, string role);
        Task<User> GetByEmail(string email);
        Task<bool> SignIn(string email, string password, bool isPersistent, bool lockoutOnFailure);
    }
}
