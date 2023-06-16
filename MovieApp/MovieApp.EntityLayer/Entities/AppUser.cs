using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.EntityLayer.Entities
{
    public class AppUser:IdentityUser<int>
    {

        [Required(ErrorMessage = "Ad ve soyad alanı boş geçilemez.")]
        [DisplayName("Ad ve Soyad")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "max 60 min 3")]
        public string NameSurname { get; set; } 
        public string? ImageUrl { get; set; }
        public string? About { get; set; }
        public List<Blog> Blogs { get; set; }

    }

}
