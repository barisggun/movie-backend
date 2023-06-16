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
        [StringLength(30, MinimumLength = 3, ErrorMessage = "max 60 min 3")]
        public string CategoryName { get; set; }
        public List<Movie> Movies { get; set; }
    }
}
