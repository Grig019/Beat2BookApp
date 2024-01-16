using Microsoft.AspNetCore.Identity; 
using Beat2Book.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beat2Book.Models
{
    public class AppUser : IdentityUser 
    {
      
        public int? Pace {  get; set; } 
        public int? Mileage { get; set; }
       

        public string? ProfileImageUrl { get; set; }    
        public string? City {  get; set; }   
        public string? State { get; set; }
        [ForeignKey("Address")]
        public int? AddressId { get; set; }


        public Address? Address { get; set; }    

        public ICollection<Band> Clubs { get; set; } 
        public ICollection<Event> Races { get; set; } 
        
    }
}
