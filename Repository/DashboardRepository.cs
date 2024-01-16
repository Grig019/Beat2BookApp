using Microsoft.EntityFrameworkCore;
using Beat2Book.Models;
using Beat2Book.Data;
using Beat2Book.Interfaces;
using Beat2Book.Models;

namespace Beat2Book.Repository
{
    public class DashboardRepository : IDashboardRepository
    { 
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor; 
        public DashboardRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor; 
        }
        public async Task<List<Band>> GetAllUserClubs()
        {
            var curUser = _httpContextAccessor.HttpContext?.User;    
            var userClubs = _context.Clubs.Where(r => r.AppUser.Id == curUser.GetUserId());
            return userClubs.ToList(); 
        }

        public async Task<List<Event>> GetAllUserRaces()
        {
            var curUser = _httpContextAccessor.HttpContext?.User;
            var userRaces = _context.Races.Where(r => r.AppUser.Id == curUser.GetUserId());
            return userRaces.ToList();
        } 

        public async Task<AppUser> GetUserById(string id)
        {
            return await _context.Users.FindAsync(id); 
        } 

        public async Task<AppUser> GetByIdNoTracking(string id)
        {
            return await _context.Users.Where(u => u.Id == id).AsNoTracking().FirstOrDefaultAsync();
        } 

        public bool Update(AppUser user)
        {
            _context.Users.Update(user);
            return Save(); 
        } 

        public bool Save()
        { 
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;   
            
        }
    }
}
