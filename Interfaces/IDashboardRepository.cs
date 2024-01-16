using Beat2Book.Models;
using Beat2Book.Models;

namespace Beat2Book.Interfaces
{
    public interface IDashboardRepository
    { 
        Task<List<Event>> GetAllUserRaces(); 
        Task<List<Band>> GetAllUserClubs();
        Task<AppUser> GetUserById(string id); 

        Task<AppUser> GetByIdNoTracking(string id); 
        bool Update(AppUser user);
        bool Save(); 
    }
}

