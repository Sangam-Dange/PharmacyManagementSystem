using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PharmacyManagementSystem.Data;
using PharmacyManagementSystem.Dtos.AuthDtos;
using PharmacyManagementSystem.Models;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PharmacyManagementSystem.Controllers
{
    public class Response
    {
        public string message { get; set; }
        public bool success { get; set; }
        public HttpStatusCode code { get; set; }
        public string token { get; set; }
        public User payload { get; set; }
        public Response() { }
    }

    public class Token
    {
        public string token { get; set; } = string.Empty;
    }


    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly PharmacyManagementSystemContext _context;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthController(PharmacyManagementSystemContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }




        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(RegisterUserDto request)
        {
            try
            {
                User checkEmailExist = await _context.User.Where(x => x.Email == request.Email).FirstOrDefaultAsync();

                if (checkEmailExist != null)
                {
                    //throw new Exception("Email already exist");
                    return StatusCode(409, value: "Email is already taken");
                }

                CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

                User user = new User();
                user.Name = request.Name;
                user.Email = request.Email;
                user.Contact = request.Contact;
                user.requestedFor = request.requestedFor;
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                if (_context.User == null)
                {
                    return Problem("Entity set 'PharmacyManagementSystemContext.User'  is null.");
                }

                _context.User.Add(user);
                await _context.SaveChangesAsync();

                return Ok(user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(LoginUserDto request)
        {
            try
            {

                User user = await _context.User.Where(w => w.Email == request.Email).FirstOrDefaultAsync();
                if (user == null)
                {
                    return BadRequest("User not found.");

                }

                if (!VerifyPassword(request.Password, user.PasswordHash, user.PasswordSalt))
                {
                    return BadRequest("Invalid Credentials.");

                }

                string token = CreateToken(user);

                return Ok(new Response { token = token, code = HttpStatusCode.OK, message = "User loged in successfully", success = true, payload = user });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private string CreateToken(User user)

        {

            string check = "";
            if (user.isSuperAdmin == true)
            {
                check = "SuperAdmin";
            }
            else if (user.isAdmin == true)
            {
                check = "Admin";
            }
            else if (user.isAdmin == false)
            {
                check = "Doctor";
            }
            List<Claim> claims = new List<Claim>
            {

                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Role,check),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
            };
            try
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:Key").Value));

                var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
                var token = new JwtSecurityToken(
                  claims: claims,
                  expires: DateTime.Now.AddDays(1),
                  signingCredentials: cred
                  );
                var jwt = new JwtSecurityTokenHandler().WriteToken(token);

                return jwt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        [HttpGet("getUserByToken")]

        public async Task<ActionResult<User>> GetUserByToken()
        {
            try
            {

                var userId = _httpContextAccessor?.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId == null)
                {
                    return BadRequest();
                }
                User user = await _context.User.Where(x => x.Id == int.Parse(userId)).FirstOrDefaultAsync();
                if (user == null)
                {
                    return NotFound();
                }

                return user;
            }
            catch (Exception ex)
            {
                //TODO: Logger.Error
                throw new Exception(ex.Message);
            }

        }


    }
}