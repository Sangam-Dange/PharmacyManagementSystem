using PharmacyManagementSystem.Data;

namespace PharmacyManagementSystem.Repository
{
    public class AuthRepository
    {

        private readonly PharmacyManagementSystemContext _context;


        public AuthRepository(PharmacyManagementSystemContext context)
        {
            _context = context;
        }

        //public async Task<User> Register(RegisterUserDto request)
        //{

        //    if (_context.User == null)
        //    {
        //        throw new Exception("Entity set 'PharmacyManagementSystemContext.User'  is null.");
        //    }

        //    User checkEmailExist = await _context.User.Where(x => x.Email == request.Email).FirstOrDefaultAsync();

        //    if (checkEmailExist != null)
        //    {
        //        throw new Exception("Email already exist");

        //    }

        //    CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

        //    User user = new User();
        //    user.Name = request.Name;
        //    user.Email = request.Email;
        //    user.Contact = request.Contact;
        //    user.requestedFor = request.requestedFor;
        //    user.PasswordHash = passwordHash;
        //    user.PasswordSalt = passwordSalt;



        //    try
        //    {
        //        _context.User.Add(user);
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }



        //    return user;
        //}


        //public async Task<Response> Login(LoginUserDto request)
        //{
        //    if (_context.User == null)
        //    {
        //        throw new Exception("Entity set 'PharmacyManagementSystemContext.User'  is null.");
        //    }

        //    User user = await _context.User.Where(w => w.Email == request.Email).FirstOrDefaultAsync();
        //    if (user == null)
        //    {
        //        throw new Exception("User not found.");

        //    }

        //    if (!VerifyPassword(request.Password, user.PasswordHash, user.PasswordSalt))
        //    {
        //        throw new Exception("Invalid Credentials.");

        //    }

        //    string token = CreateToken(user);

        //    return new Response { token = token, code = HttpStatusCode.OK, message = "User loged in successfully", success = true, payload = user };
        //}

        //    private string CreateToken(User user)

        //    {
        //        string check = "";
        //        if (user.isSuperAdmin == true)
        //        {
        //            check = "SuperAdmin";
        //        }
        //        else if (user.isAdmin == true)
        //        {
        //            check = "Admin";
        //        }
        //        else if (user.isAdmin == false)
        //        {
        //            check = "Doctor";
        //        }
        //        List<Claim> claims = new List<Claim>
        //        {

        //            new Claim(ClaimTypes.Email,user.Email),
        //            new Claim(ClaimTypes.Role,check),
        //            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
        //        };

        //        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:Key").Value));

        //        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        //        var token = new JwtSecurityToken(
        //            claims: claims,
        //            expires: DateTime.Now.AddDays(1),
        //            signingCredentials: cred
        //            );
        //        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        //        return jwt;
        //    }

        //    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        //    {
        //        using (var hmac = new HMACSHA512())
        //        {
        //            passwordSalt = hmac.Key;
        //            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        //        }
        //    }

        //    private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        //    {
        //        using (var hmac = new HMACSHA512(passwordSalt))
        //        {
        //            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        //            return computedHash.SequenceEqual(passwordHash);
        //        }
        //    }

        //    [HttpGet("getUserByToken")]

        //    public async Task<User> GetUserByToken(string authHeader)
        //    {
        //        try
        //        {
        //            var handler = new JwtSecurityTokenHandler();

        //            authHeader = authHeader.Replace("Bearer ", "");
        //            var jsonToken = handler.ReadToken(authHeader);
        //            var tokenS = handler.ReadToken(authHeader) as JwtSecurityToken;
        //            var email = tokenS.Claims.Where(x => x.Type == ClaimTypes.Email).FirstOrDefault().Value;

        //            User user = await _context.User.Where(x => x.Email == email).FirstOrDefaultAsync();
        //            if (user == null)
        //            {
        //                throw new Exception("User not found");
        //            }

        //            return user;
        //        }
        //        catch (Exception ex)
        //        {
        //            //TODO: Logger.Error
        //            throw new Exception(ex.Message);
        //        }


    }
}
