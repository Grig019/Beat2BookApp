using Microsoft.EntityFrameworkCore;
using Beat2Book.Models;
using Beat2Book.Data;
using Beat2Book.Interfaces;

namespace Beat2Book.Repository
{
    public class BandRepository : IBandRepository
    {
        private readonly ApplicationDbContext _context;

        public BandRepository(ApplicationDbContext context) 
        {
            _context = context;
        }
        public bool Add(Band club)
        {
            _context.Add(club); 
            return Save(); 
        }

        public bool Delete(Band club)
        {
            _context.Remove(club);
            return Save(); 
        }

        public async Task<IEnumerable<Band>> GetAll()
        {
           return await _context.Clubs.ToListAsync(); 
        }

        public async Task<Band> GetByIdAsync(int id)
        {
            return await _context.Clubs.Include(i => i.Address).FirstOrDefaultAsync(i => i.Id == id); 

        }

        public async Task<Band> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Clubs.Include(i => i.Address).AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);

        }

            public async Task<IEnumerable<Band>> GetClubByCity(string city)
        {
            return await _context.Clubs.Where(c => c.Address.City.Contains(city)).ToListAsync(); 
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false; 
        }

        public bool Update(Band club)
        {
            _context.Update(club);
             return Save();
        }
    }
}
