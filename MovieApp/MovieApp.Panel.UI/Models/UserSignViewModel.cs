using System.ComponentModel.DataAnnotations;

namespace MovieApp.Panel.UI.Models
{
    public class UserSignViewModel
    {
        [Required(ErrorMessage ="Lütfen kullanıcı adını giriniz.")]
        public string username { get; set; }
        [Required(ErrorMessage = "Lütfen şifrenizi giriniz.")]
        public string password { get; set; }
    }
}
