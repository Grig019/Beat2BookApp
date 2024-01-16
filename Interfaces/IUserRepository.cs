using Beat2Book.Models;

namespace Beat2Book.Interfaces
{
    public interface IUserRepository
    { 
        Task<IEnumerable<AppUser>> GetAllUser(); 
        Task<AppUser> GetUserById(string id); 
        bool Add(AppUser user); 
        bool Update(AppUser user);  
        bool Delete(AppUser user);
        bool Save(); 
    }
}
