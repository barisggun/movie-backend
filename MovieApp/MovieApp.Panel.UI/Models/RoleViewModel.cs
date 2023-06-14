using System.ComponentModel.DataAnnotations;

namespace MovieApp.Panel.UI.Models
{
    public class RoleViewModel
    {
        [Required(ErrorMessage ="Lütfen rol adı giriniz")]
        public string name { get; set; }
    }
}
