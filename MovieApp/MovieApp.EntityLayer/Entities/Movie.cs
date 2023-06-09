using MovieApp.EntityLayer.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.EntityLayer.Entities
{
    public class Movie : BaseEntity
    {
        public string MovieTitle { get; set; }
        public string Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Poster { get; set; }
        public string DetailPoster { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<Director> Directors { get; set; }
        public List<Actor> Actors { get; set; }
    }
}
