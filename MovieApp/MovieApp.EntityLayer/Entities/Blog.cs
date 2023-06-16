using MovieApp.EntityLayer.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MovieApp.EntityLayer.Entities
{
    public class Blog:BaseEntity
    {
        [Required(ErrorMessage = "Blog Başlığı alanı boş geçilemez.")]
        [DisplayName("Blog Başlığı")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "max 60 min 3")]
        public string BlogTitle { get; set; }

        [Required(ErrorMessage = "Blog içeriği alanı boş geçilemez.")]
        [DisplayName("Blog İçeriği")]
        [StringLength(450, MinimumLength = 100, ErrorMessage = "max 60 min 3")]
        public string BlogContent { get; set; }
        public string? BlogThumbnailImage { get; set; }


        [Required(ErrorMessage = "Blog fotoğraf alanı boş geçilemez.")]
        [DisplayName("Blog Fotoğrafı")]
        public string? BlogImage { get; set; }
        public DateTime BlogCreateDate { get; set; }
        public bool? BlogStatus { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUsers { get; set; }
        public int MovieId { get; set; }
        public Movie Movies { get; set; }
        public List<Comment>? Comments { get; set; }
    }
}
