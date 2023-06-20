using MovieApp.EntityLayer.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.EntityLayer.Entities
{
    public class Rating: BaseEntity
    {
        [Range(1, 5)]
        public int Score { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public int AppUserId { get; set; }
        public AppUser? AppUsers { get; set; }


    }
}
