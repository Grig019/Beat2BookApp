using Beat2Book.Models;

namespace Beat2Book.HomeViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Band> Clubs { get; set; }    
        public string City { get; set; }    
        public string State { get; set; }   

    }
}
