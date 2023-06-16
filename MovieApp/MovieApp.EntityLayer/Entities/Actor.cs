using MovieApp.EntityLayer.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.EntityLayer.Entities
{
    public class Actor : BaseEntity
    {
        [DisplayName("Oyuncu Adı")]
        [MaxLength(60, ErrorMessage = "Oyuncu adı maksimum 60 karakter olabilir")]
        [MinLength(5, ErrorMessage = "Oyuncu adı maksimum 60 karakter olabilir")]
        public string ActorName { get; set; }
        public List<Movie>? Movies { get; set; }
    }
}



