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
    public class Category : BaseEntity
    {
        [Required(ErrorMessage = "Kategori adı boş geçilemez.")]
        [DisplayName("Kategori Adı")]
        [MaxLength(30, ErrorMessage = "Kategori adı maksimum 60 karakter olabilir")]
        [MinLength(3, ErrorMessage = "Kategori adı minimum 3 karakter olabilir")]
        public string CategoryName { get; set; }
        public List<Movie>? Movies { get; set; }
    }
}
