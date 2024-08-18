

using BusinessLogic.Model;

namespace BusinessLogic
{
    public interface IUserService
    {
        Task RegisterUserAsync(RegisterUserRequest model);
        Task<string> LoginUserAsync(LoginUserRequest model);
    }
}
