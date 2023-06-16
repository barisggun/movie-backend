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
    public class Director : BaseEntity
    {
        [Required(ErrorMessage = "Yönetmen adı boş geçilemez.")]
        [DisplayName("Yönetmen Adı")]
        [MaxLength(50, ErrorMessage = "Yönetmen adı maksimum 60 karakter olabilir")]
        [MinLength(5, ErrorMessage = "Yönetmen adı minimum 5 karakter olabilir")]
        public string DirectorName { get; set; }

        public List<Movie>? Movies { get; set; }
    }
}
