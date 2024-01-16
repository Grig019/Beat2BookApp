using Beat2Book.Models;

namespace Beat2Book.Interfaces
{
    public interface IBandRepository
    {  

        Task<IEnumerable<Band>> GetAll(); 
        Task<Band> GetByIdAsync(int id);
        Task<Band> GetByIdAsyncNoTracking(int id);
        Task<IEnumerable<Band>> GetClubByCity(string city); 
        bool Add(Band club);
        bool Update(Band club); 

        bool Delete(Band club);

        bool Save(); 
    }
}
