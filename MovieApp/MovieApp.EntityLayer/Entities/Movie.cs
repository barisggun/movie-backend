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
        public DateTime ReleaseDate { get; set; }
        public string Poster { get; set; }
        public string DetailPoster { get; set; }
        public DateTime UploadDate { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int ActorId { get; set; }
        public Actor Actor { get; set; }
        public int DirectorId { get; set; }
        public Director Director { get; set; }
    }
}
