using Microsoft.AspNetCore.Mvc;
using PharmacyManagementSystem.Controllers.Dtos.AdminDto;
using PharmacyManagementSystem.Models;

namespace PharmacyManagementSystem.Interface
{
    public interface IUser
    {
        Task<ActionResult<IEnumerable<User>>> GetAllUsers();
        Task<ActionResult<IEnumerable<User>>> GetAuthorizedUsers();
        Task<ActionResult<IEnumerable<User>>> GetUnAuthorizedUsers();
        Task<ActionResult<User>> GetUserById(int id);
        Task<bool> PutUser(int id, User user);
        Task<bool> ChangeUserRole(int id, ChangeRoleDto request);
        Task<bool> UnAuthoizeUser(int id);
        Task<bool> DeleteUserById(int id);

    }
}
