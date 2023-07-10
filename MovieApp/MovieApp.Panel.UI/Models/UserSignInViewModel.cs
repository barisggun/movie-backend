using System.ComponentModel.DataAnnotations;

namespace MovieApp.Panel.UI.Models
{
    public class UserSignInViewModel
    {
        [Required(ErrorMessage ="Lütfen kullanıcı adını giriniz.")]
        public string username { get; set; }

        [Required(ErrorMessage = "Lütfen şifrenizi giriniz.")]
        public string password { get; set; }  
        public int ConfirmCode { get; set; }  
    }
}
