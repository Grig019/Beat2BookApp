using Beat2Book.Interfaces;
using Microsoft.EntityFrameworkCore;
using Beat2Book.Data;
using Beat2Book.Interfaces;
using Beat2Book.Models;

namespace Beat2Book.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context; 
        public UserRepository(ApplicationDbContext context )
        {
            _context = context; 
        }
        public bool Add(AppUser user)
        {
            throw new NotImplementedException();
        }

        public bool Delete(AppUser user)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AppUser>> GetAllUser()
        {
            return await _context.Users.ToListAsync(); 
        }

        public async Task<AppUser> GetUserById(string id)
        {
            return await _context.Users.FindAsync(id);
        }

        public bool Save()
        {
           var saved = _context.SaveChanges();  
            return saved > 0 ? true : false;    
        }

        public bool Update(AppUser user)
        {
            _context.Update(user);
            return Save(); 
        }
    }
}
