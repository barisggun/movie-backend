using MovieApp.EntityLayer.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.EntityLayer.Entities
{
    public class HomepageCover:BaseEntity
    {

        [Required(ErrorMessage = "Anasayfa header fotoğrafı alanı boş geçilemez.")]
        [DisplayName("Anasayfa header fotoğrafı")]
        public string ImageUrl { get; set; }


        [Required(ErrorMessage = "Anasayfa header yazısı alanı boş geçilemez.")]
        [DisplayName("Anasayfa header yazısı")]
        [StringLength(80, MinimumLength = 5, ErrorMessage = "max 60 min 3")]
        public string Title { get; set; }
    }
}
