using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagementSystem.Controllers.Dtos.AdminDto;
using PharmacyManagementSystem.Interface;
using PharmacyManagementSystem.Models;

namespace PharmacyManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUser IUser;

        public UsersController(IUser IUser)
        {
            this.IUser = IUser;
        }



        // GET: api/Users
        // Access : SuperAdmin
        [HttpGet]


        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var user = await IUser.GetAllUsers();
            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // GET: api/Users/GetAuthorizedUsers
        // Access : SuperAdmin
        [HttpGet("GetAuthorizedUsers"), Authorize(Roles = "SuperAdmin")]

        public async Task<ActionResult<IEnumerable<User>>> GetAuthorizedUsers()
        {
            var user = await IUser.GetAuthorizedUsers();
            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // GET: api/Users/GetUnAuthorizedUsers
        // Access : SuperAdmin
        [HttpGet("GetUnAuthorizedUsers"), Authorize(Roles = "SuperAdmin")]

        public async Task<ActionResult<IEnumerable<User>>> GetUnAuthorizedUsers()
        {
            var user = await IUser.GetUnAuthorizedUsers();
            if (user == null)
            {
                return NotFound();
            }

            return user;
        }


        // GET: api/Users/5
        // Access : SuperAdmin,Admin,Doctor
        [HttpGet("{id}"), Authorize(Roles = "SuperAdmin,Admin,Doctor")]
        public async Task<ActionResult<User>> GetUsers(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var user = await IUser.GetUserById(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        ////// put: api/users/5

        [HttpPut("{id}"), Authorize(Roles = "Admin,Doctor")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            bool check = await IUser.PutUser(id, user);
            if (check == false)
            {
                return BadRequest();
            }

            return Ok();
        }

        // PUT: api/Users/5
        // Access : SuperAdmin
        [HttpPut("ChangeUserRole/{id}"), Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> ChangeUserRole(int id, ChangeRoleDto request)
        {
            bool check = await IUser.ChangeUserRole(id, request);
            if (check == false)
            {
                return BadRequest();
            }

            return Ok();
        }




        //// DELETE: api/Users/5
        [HttpDelete("{id}"), Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            bool res = await IUser.DeleteUserById(id);
            if (res == false)
            {
                return BadRequest();
            }
            return Ok();
        }

    }
}
