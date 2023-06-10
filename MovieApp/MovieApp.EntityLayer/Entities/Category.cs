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
        public string CategoryName { get; set; }
        public List<Movie> Movies { get; set; }
    }
}
