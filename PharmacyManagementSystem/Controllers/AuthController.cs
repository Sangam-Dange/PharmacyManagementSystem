using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PharmacyManagementSystem.Authentication;
using PharmacyManagementSystem.Data;
using PharmacyManagementSystem.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace PharmacyManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly PharmacyManagementSystemContext _context;
        private readonly IConfiguration _configuration;
        public AuthController(PharmacyManagementSystemContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }



        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(RegisterUserDto request)
        {

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

        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(LoginUserDto request)
        {
            User user = await _context.User.Where(w => w.Email == request.Email).FirstOrDefaultAsync();
            if (user == null)
            {
                return BadRequest("User not found.");

            }

            if (!VerifyPassword(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Invalid credentials.");

            }

            string token = CreateToken(user);

            return Ok(token);
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
                new Claim(ClaimTypes.Name,user.Name),
                new Claim(ClaimTypes.Role,check)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:Key").Value));
            Console.WriteLine(key);
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }


    }
}
