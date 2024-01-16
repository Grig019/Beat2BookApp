using Beat2Book.Data.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Beat2Book.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        [ForeignKey("Address")]
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public EventCategory EventCategory { get; set; }

        [ForeignKey("AppUser")]
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

        public List<RelatedProjectPhoto> RelatedProjectPhotos { get; set; } = new List<RelatedProjectPhoto>();
    }

    public class RelatedProjectPhoto
    {
        [Key]
        public int Id { get; set; }
        public string ImageUrl { get; set; }

        [ForeignKey("Event")]
        public int EventId { get; set; }
        public Event Event { get; set; }
    }

    public class FileUpload
    {
        public IFormFile File { get; set; }
    }
}

