using PharmacyManagementSystem.Controllers;
using PharmacyManagementSystem.Dtos.AuthDtos;
using PharmacyManagementSystem.Models;

namespace PharmacyManagementSystem.Interface
{
    public interface IAuth
    {
        Task<User> Register(RegisterUserDto request);
        Task<Response> Login(LoginUserDto request);
        Task<User> GetUserByToken(string authHeader);

    }
}
