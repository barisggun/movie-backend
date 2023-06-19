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
        [MaxLength(80, ErrorMessage = "Blog başlığı maksimum 60 karakter olabilir")]
        [MinLength(5, ErrorMessage = "Blog başlığı minimum 5 karakter olabilir")]
        public string BlogTitle { get; set; }

        [Required(ErrorMessage = "Blog metni alanı boş geçilemez.")]
        [DisplayName("Blog İçeriği")]
        [MaxLength(450, ErrorMessage = "Blog metni maksimum 60 karakter olabilir")]
        [MinLength(105, ErrorMessage = "Blog metni minimum 100 karakter olabilir")]
        public string BlogContent { get; set; }
        public string? BlogThumbnailImage { get; set; }


        [Required(ErrorMessage = "Blog fotoğraf alanı boş geçilemez.")]
        [DisplayName("Blog Fotoğrafı")]
        public string? BlogImage { get; set; }
        public DateTime BlogCreateDate { get; set; }
        public bool? BlogStatus { get; set; }
        public int AppUserId { get; set; }
        public AppUser? AppUsers { get; set; }
        public int MovieId { get; set; }
        public Movie? Movies { get; set; }
        public List<Comment>? Comments { get; set; }
    }
}
