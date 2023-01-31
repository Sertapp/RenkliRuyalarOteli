using System.ComponentModel.DataAnnotations;

namespace RenkliRuyalarOteli.MvcUI.Models
{
    public class LoginVM
    {
        [Required(AllowEmptyStrings =false,ErrorMessage ="Email Alani Zorunludur")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Şifre  Zorunludur")]
        public string Password { get; set; }

        public bool RememberMe{ get; set; }
    }
}
