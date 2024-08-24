using AutoMapper;
using BusinessLogic;
using BusinessLogic.Model;
using DataAccounts;
using DataAccounts.Repositories;
using System;

namespace BusinessLogic
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepositories;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;
        
        public UserService(IUserRepository userRepositories, IJwtService jwtService, IMapper mapper)
        {
            _userRepositories = userRepositories;
            _jwtService = jwtService;
            _mapper = mapper;
        }

        public async Task<string> LoginUserAsync(LoginUserRequest model)
        {
            var isPersistent = true;
            var lockoutOnFailure = false;

            var user = await _userRepositories.GetByEmail(model.Email);
            var result = await _userRepositories.SignIn(model.Email,model.Password, isPersistent, lockoutOnFailure);

            if (result == false)
            {
                throw new ArgumentException("Password is not correct!");
            }

            return _jwtService.GenerateJwt(user.Id, user.UserName);
        }

        public async Task RegisterUserAsync(RegisterUserRequest request)
        {
            string role = UserConstants.User;

            if (request.Email.Equals("admin@example.com", StringComparison.OrdinalIgnoreCase))
            {
                role = UserConstants.Admin;
            }

            var user = _mapper.Map<User>(request);

            await _userRepositories.Register(user,request.Password,role);
        }
    }
}
