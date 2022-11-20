using System.ComponentModel.DataAnnotations;

namespace NoteApp.Models
{
    public class UserCreateViewModel
    {
        [Required(ErrorMessage = "Lütfen adınızı giriniz")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Lütfen soyadınızı giriniz")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Lütfen kullanıcı adın giriniz")]
        public string Username
        {
            get; set;
        }
        [Required(ErrorMessage = "Lütfen mail adresini giriniz")]
        public string Mail { get; set; }

        [Required(ErrorMessage = "Lütfen şifre giriniz")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Lütfen şifreyi terkrar giriniz")]
        [Compare("Password", ErrorMessage = "Şifreler eşleşmiyor")]
        public string ConfrimPassword { get; set; }
        [Required(ErrorMessage = "Cinsiyet alanı zorunludur")]
        public string Gender { get; set; }
    }
}
