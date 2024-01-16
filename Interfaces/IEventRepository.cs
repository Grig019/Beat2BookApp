using Beat2Book.Models;
using Beat2Book.Models;

namespace Beat2Book.Interfaces
{
    public interface IEventRepository
    {

        Task<IEnumerable<Event>> GetAll();
        Task<Event> GetByIdAsync(int id);
        Task<Event> GetByIdAsyncNoTracking(int id);
        Task<IEnumerable<Event>> GetAllRacesByCity(string city);
        bool Add(Event race);
        bool Update(Event race);

        bool Delete(Event race);

        bool Save();
    }
}
