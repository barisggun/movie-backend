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
    public class Movie : BaseEntity
    {
        [Required(ErrorMessage = "Film adı boş geçilemez.")]
        [DisplayName("Film Adı")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "max 60 min 3")]
        public string MovieTitle { get; set; }

        [Required(ErrorMessage = "Film açıklaması boş geçilemez.")]
        [StringLength(800, MinimumLength = 5, ErrorMessage = "max 60 min 3")]
        [DisplayName("Açıklama")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Çıkış tarihi boş geçilemez.")]
        [DisplayName("Çıkış Tarihi")]
        public DateTime ReleaseDate { get; set; }

        [DisplayName("Poster")]
        public string? Poster { get; set; }

 
        [DisplayName("Detay Posteri")]
        public string? DetailPoster { get; set; }

        //[DisplayName("Film Kategorisi")]
        //public int CategoryId { get; set; }
        //public Category Category { get; set; }

        [Required(ErrorMessage = "Yönetmen alanı boş geçilemez.")]
        [DisplayName("Yönetmenler")]
        public List<Director> Directors { get; set; }
        //public List<Actor> Actors { get; set; }
        public List<Actor> Actors { get; set; } = new List<Actor>();
        public List<Category> Categories { get; set; } = new List<Category>();

        public List<Blog> Blogs { get; set; } = new List<Blog>();
    }
}
