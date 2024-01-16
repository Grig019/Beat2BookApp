using Beat2Book.Data.Enum;
using Beat2Book.Models;

namespace Beat2Book.homeViewModels
{
    public class EditBandViewModel
    {
        public int Id { get; set; } 
        public string Title { get; set; }
        public string Description { get; set; } 
        public IFormFile Image { get; set; }  
        public string? URL { get; set; } 
        
        public int AddressId { get; set; }    

        public Address Address {  get; set; }    
        public BandCategory ClubCategory { get; set; }  
    }
}
