using System.ComponentModel.DataAnnotations;

namespace MovieApp.Panel.UI.Models
{
    public class ForgotPasswordViewModel
    {
        [Display(Name = "E-posta")]
        [Required(ErrorMessage = "E-posta adresinizi giriniz.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string Email { get; set; }
    }
}
