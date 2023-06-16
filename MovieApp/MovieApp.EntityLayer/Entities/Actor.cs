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
       public string ActorName { get; set; }
        public List<Movie> Movies { get; set; }
    }
}



