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
        [StringLength(50, MinimumLength = 5, ErrorMessage = "max 60 min 3")]
        public string DirectorName { get; set; }

        public List<Movie> Movies { get; set; }
    }
}
