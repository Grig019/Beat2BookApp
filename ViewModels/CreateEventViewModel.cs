using Beat2Book.Data.Enum;
using Beat2Book.Models;

namespace Beat2Book.homeViewModels
{
    public class CreateEventViewModel
    { 
          
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Address Address { get; set; }
        public IFormFile Image { get; set; }

        public int ZipCode { get; set; }

        public DateTime EventDate { get; set; }

        public EventCategory EventCategory { get; set; } 

        public string AppUserId { get; set; }

        
    }
}

