using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataAccounts
{
    public class User
    {
        public User(Guid id,string userName,string email, string passwordHash)
        {
            Id = id;
            Username = userName;
            Email = email;
            Password = passwordHash;
        } 
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
