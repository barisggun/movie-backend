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
        public string MovieTitle { get; set; }
        [Required(ErrorMessage = "Film açıklaması boş geçilemez.")]
        [DisplayName("Açıklama")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Çıkış tarihi boş geçilemez.")]
        [DisplayName("Çıkış Tarihi")]
        public DateTime ReleaseDate { get; set; }
        [Required(ErrorMessage = "Film posteri boş geçilemez.")]
        [DisplayName("Poster")]
        public string Poster { get; set; }

        [Required(ErrorMessage = "Detaylı poster boş geçilemez.")]
        [DisplayName("Detay Posteri")]
        public string DetailPoster { get; set; }
        [Required(ErrorMessage = "Kategori boş geçilemez.")]
        [DisplayName("Film Kategorisi")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        [Required(ErrorMessage = "Yönetmen alanı boş geçilemez.")]
        [DisplayName("Yönetmenler")]
        public List<Director> Directors { get; set; }
        public List<Actor> Actors { get; set; }
    }
}
