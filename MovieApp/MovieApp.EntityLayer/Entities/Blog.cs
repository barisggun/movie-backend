using MovieApp.EntityLayer.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MovieApp.EntityLayer.Entities
{
    public class Blog:BaseEntity
    {
        public string? BlogTitle { get; set; }
        public string? BlogContent { get; set; }
        public string? BlogThumbnailImage { get; set; }
        public string? BlogImage { get; set; }
        public DateTime BlogCreateDate { get; set; }
        public bool? BlogStatus { get; set; }

        public int AppUserId { get; set; }
        public AppUser AppUsers { get; set; }

        public int MovieId { get; set; }
        public Movie Movies { get; set; }

        public List<Comment>? Comments { get; set; }
    }
}
