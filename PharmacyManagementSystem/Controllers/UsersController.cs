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
        [HttpGet, Authorize(Roles = "SuperAdmin")]


        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            try
            {

                var user = await IUser.GetAllUsers();
                if (user == null)
                {
                    return NotFound();
                }

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/Users/GetAuthorizedUsers
        // Access : SuperAdmin
        [HttpGet("GetAuthorizedUsers"), Authorize(Roles = "SuperAdmin")]

        public async Task<ActionResult<IEnumerable<User>>> GetAuthorizedUsers()
        {
            try
            {

                var user = await IUser.GetAuthorizedUsers();
                if (user == null)
                {
                    return NotFound();
                }

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // GET: api/Users/GetUnAuthorizedUsers
        // Access : SuperAdmin
        [HttpGet("GetUnAuthorizedUsers"), Authorize(Roles = "SuperAdmin")]

        public async Task<ActionResult<IEnumerable<User>>> GetUnAuthorizedUsers()
        {
            try
            {
                var user = await IUser.GetUnAuthorizedUsers();
                if (user == null)
                {
                    return NotFound();
                }

                return user;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        // GET: api/Users/5
        // Access : SuperAdmin,Admin,Doctor
        [HttpGet("{id}"), Authorize(Roles = "SuperAdmin,Admin,Doctor")]
        public async Task<ActionResult<User>> GetUsers(int id)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        ////// put: api/users/5

        [HttpPut("{id}"), Authorize(Roles = "Admin,Doctor")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            try
            {

                bool check = await IUser.PutUser(id, user);
                if (check == false)
                {
                    return BadRequest();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // PUT: api/Users/5
        // Access : SuperAdmin
        [HttpPut("ChangeUserRole/{id}"), Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> ChangeUserRole(int id, ChangeRoleDto request)
        {
            try
            {

                bool check = await IUser.ChangeUserRole(id, request);
                if (check == false)
                {
                    return BadRequest();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // PUT: api/Users/5
        // Access : SuperAdmin
        [HttpGet("UnAuthoizeUser/{id}"), Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> UnAuthoizeUser(int id)
        {
            try
            {

                bool check = await IUser.UnAuthoizeUser(id);
                if (check == false)
                {
                    return BadRequest();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        //// DELETE: api/Users/5
        [HttpDelete("{id}"), Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {

                bool res = await IUser.DeleteUserById(id);
                if (res == false)
                {
                    return BadRequest();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
