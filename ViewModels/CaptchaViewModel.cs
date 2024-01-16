using System.ComponentModel.DataAnnotations;

namespace Beat2Book.ViewModels
{
    public class CaptchaViewModel
    {
        [Required]
        public string Captcha {  get; set; }    
    }
}
