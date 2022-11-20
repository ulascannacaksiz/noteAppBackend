using System.ComponentModel.DataAnnotations;

namespace NoteApp.Models
{
    public class UserLoginViewModel
    {
        [Required(ErrorMessage = "Email Alanı zorunludur")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Şifre Alanı zorunludur")]
        public string Password { get; set; }
    }
}
