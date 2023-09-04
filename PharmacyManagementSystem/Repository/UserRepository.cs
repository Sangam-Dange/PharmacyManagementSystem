using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmacyManagementSystem.Controllers.Dtos.AdminDto;
using PharmacyManagementSystem.Data;
using PharmacyManagementSystem.Interface;
using PharmacyManagementSystem.Models;

namespace PharmacyManagementSystem.Repository
{
    public class UserRepository : IUser
    {
        private readonly PharmacyManagementSystemContext _context;


        public UserRepository(PharmacyManagementSystemContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {

            return await _context.User.ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<User>>> GetAuthorizedUsers()
        {

            return await _context.User.Where(x => x.isAdmin != null).ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<User>>> GetUnAuthorizedUsers()
        {

            return await _context.User.Where(x => x.isAdmin == null).ToListAsync();
        }

        public async Task<ActionResult<User>?> GetUserById(int id)
        {

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return null;
            }
            return user;
        }

        public async Task<bool> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return false;
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> ChangeUserRole(int id, ChangeRoleDto request)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return false;
            }

            user.isAdmin = request.isAdmin;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> UnAuthoizeUser(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return false;
            }

            user.isAdmin = null;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }

            return true;
        }
        public async Task<bool> DeleteUserById(int id)
        {
            if (_context.User == null)
            {
                return false;
            }
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return false;
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
