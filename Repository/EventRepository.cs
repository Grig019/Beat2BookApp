using Microsoft.EntityFrameworkCore;
using Beat2Book.Models;
using Beat2Book.Data;
using Beat2Book.Interfaces;
using Beat2Book.Models;

namespace Beat2Book.Repository
{
    public class EventRepository : IEventRepository
    {
        private readonly ApplicationDbContext _context;

        public EventRepository(ApplicationDbContext context)
        {
        _context = context;
        }

        public bool Add(Event race)
        {
            _context.Add(race);
            return Save();
        }

        public bool Delete(Event race)
        {
            _context.Remove(race);
            return Save();
        }

        public async Task<IEnumerable<Event>> GetAll()
        {
            return await _context.Races.ToListAsync();
        }

        public async Task<IEnumerable<Event>> GetAllRacesByCity(string city)
        {
            return await _context.Races.Where(c => c.Address.City.Contains(city)).ToListAsync();
        }

        public async Task<Event> GetByIdAsync(int id)
        {
            return await _context.Races.Include(i => i.Address).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Event> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Races.Include(i => i.Address).AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges(); 
            return saved > 0 ? true : false;    
        }

        public bool Update(Event race)
        {
            _context.Update(race);
            return Save();  
        }
    }
}
