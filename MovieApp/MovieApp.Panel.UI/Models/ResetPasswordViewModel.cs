using System.ComponentModel.DataAnnotations;

namespace MovieApp.Panel.UI.Models
{
    public class ResetPasswordViewModel
    {
        [Display(Name = "E-posta")]
        [Required(ErrorMessage = "E-posta adresinizi giriniz.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string Email { get; set; }

        [Display(Name = "Yeni Şifre")]
        [Required(ErrorMessage = "Yeni şifrenizi giriniz.")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Display(Name = "Yeni Şifre Onayı")]
        [Required(ErrorMessage = "Yeni şifrenizi onaylayınız.")]
        [Compare("NewPassword", ErrorMessage = "Şifreler uyuşmuyor.")]
        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; }

        public string ResetCode { get; set; }
    }
}
